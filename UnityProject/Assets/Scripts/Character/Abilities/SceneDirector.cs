using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Linq;
using NCalc;
using System.Collections.Generic;

public static class SceneDirector
{
    // Single Responsibility: Turns a Script into a Performance

    public static ScenePerformance CreatePerformaneScript(SceneActionList scene, ISceneTranslator sceneTranslator)
    {
        ScenePerformance performance = new ScenePerformance();

        foreach (var action in scene)
        {
            if (action is RunAnimationSceneAction)
            {
                var anim = (RunAnimationSceneAction)action;

                bool runOnce = ReadExpression<bool>(anim.RunOnce, sceneTranslator);

                if (runOnce)
                {
                    RunAnimationOnceScenePerformanceAction perfAction = new RunAnimationOnceScenePerformanceAction();
                    perfAction.Actor = ReadExpression<GameObject>(anim.Actor, sceneTranslator);
                    perfAction.Animation = ReadExpression<string>(anim.Animation, sceneTranslator);
                    performance.Que(perfAction);
                }
                else
                {
                    RunAnimationScenePerformanceAction perfAction = new RunAnimationScenePerformanceAction();
                    perfAction.Actor = ReadExpression<GameObject>(anim.Actor, sceneTranslator);
                    perfAction.Animation = ReadExpression<string>(anim.Animation, sceneTranslator);
                    performance.Que(perfAction);
                }
            }
            else if (action is PlaySoundSceneAction)
            {
                var sound = (PlaySoundSceneAction)action;
                SoundEffectScenePerformanceAction soundPerf = new SoundEffectScenePerformanceAction();
                soundPerf.AudioFile = ReadExpression<AudioClip>(sound.Sound, sceneTranslator);
                soundPerf.AudioSourceActor = ReadExpression<GameObject>(sound.Actor, sceneTranslator);
                performance.Que(soundPerf);
            }
            else if (action is MoveSceneAction)
            {
                if (action is MoveToLocationSceneAction)
                {
                    var anim = (MoveToLocationSceneAction)action;
                    MoveToLocationScenePerformanceAction perfAction = new MoveToLocationScenePerformanceAction();
                    perfAction.WhoToMove = ReadExpression<GameObject>(anim.Actor, sceneTranslator);
                    perfAction.Location = ReadExpression<Vector3>(anim.Location, sceneTranslator);
                    perfAction.Speed = ReadExpression<float>(anim.Speed, sceneTranslator);
                    performance.Que(perfAction);
                }
                else if (action is MoveToEntitySceneAction)
                {
                    var anim = (MoveToEntitySceneAction)action;
                    MoveToGameObjectScenePerformanceAction perfAction = new MoveToGameObjectScenePerformanceAction();
                    perfAction.WhoToMove = ReadExpression<GameObject>(anim.Actor, sceneTranslator);
                    perfAction.Target = ReadExpression<GameObject>(anim.Entity, sceneTranslator);
                    perfAction.Speed = ReadExpression<float>(anim.Speed, sceneTranslator);
                    perfAction.HowClose = ReadExpression<float>(anim.HowClose, sceneTranslator);
                    performance.Que(perfAction);

                }
                else if (action is MoveInRangeOfEntitySceneAction)
                {
                    var anim = (MoveInRangeOfEntitySceneAction)action;
                    MoveInRangeOfGameObjectScenePerformanceAction perfAction = new MoveInRangeOfGameObjectScenePerformanceAction();
                    perfAction.WhoToMove = ReadExpression<GameObject>(anim.Actor, sceneTranslator);
                    perfAction.Target = ReadExpression<GameObject>(anim.Entity, sceneTranslator);
                    perfAction.Speed = ReadExpression<float>(anim.Speed, sceneTranslator);
                    perfAction.MinRange = ReadExpression<float>(anim.MinDistance, sceneTranslator);
                    perfAction.MaxRange = ReadExpression<float>(anim.MaxDistance, sceneTranslator);
                    performance.Que(perfAction);

                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (action is AdjustStatSceneAction)
            {
                var dmg = (AdjustStatSceneAction)action;
                AdjustStatScenePerformanceAction dmgPerformance = new AdjustStatScenePerformanceAction();
                dmgPerformance.Stat = ReadExpression<Stat<LilithStats>>(dmg.StatToAdjust, sceneTranslator);
                dmgPerformance.Adjustment = ReadExpression<float>(dmg.Adjustment, sceneTranslator);
                performance.Que(dmgPerformance);
            }
        }

        return performance;
    }

    private static T ReadExpression<T>(string expression, ISceneTranslator sceneTranslator)
    {
        // 5                                    Value
        // {Caster}                               Actor
        // Caster[Character].MoveSpeed          Actor[Component].Property

        Type typeOfT = typeof(T);
        if (expression.Contains('{'))
        {// Is an expression

            if (expression.EndsWith("}"))
            {
                string memberExpression = GetPropertyExpression(expression, 0);

                return GetActor(memberExpression, sceneTranslator).JustCastItDammit<T>();
            }

            if (typeOfT == typeof(System.Single))
                expression = EvaluatePrimitive(expression, sceneTranslator);
            else
                return (T)EvaluateObject(expression, sceneTranslator);
        }


        if (typeOfT == typeof(float))
        {
            return float.Parse(expression).JustCastItDammit<T>();
        }
        else if (typeOfT == typeof(string))
        {
            return expression.JustCastItDammit<T>();
        }
        else if (typeOfT == typeof(bool))
        {
            return bool.Parse(expression).JustCastItDammit<T>();
        }
        else if (typeOfT == typeof(Vector3))
        {
            float[] coordinates = expression.Replace(" ", "").Split(',').Select(el => float.Parse(el)).ToArray();
            return new Vector3(coordinates[0], coordinates[1], coordinates[2]).JustCastItDammit<T>();
        }
        else if (typeOfT == typeof(AudioClip))
        {
            return Resources.Load(expression).JustCastItDammit<T>();
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    private static string EvaluatePrimitive(string expression, ISceneTranslator sceneTranslator)
    {
        // 60 + ({Caster}<Character>.Stats[(23 + 3)].CurrentValue * 5)

        expression = EvaluateExpressionsParens(expression, sceneTranslator);

        // Translate Member Values
        for (int i = 0; i < expression.Length; i++)
        {
            var character = expression[i];
            if (character == '{')
            {
                string memberExpression = GetPropertyExpression(expression, i);
                object value = GetExpressionValue(expression, sceneTranslator, memberExpression);
                expression = expression.Replace(memberExpression, value.ToString());
            }
        }

        // Evaluate Expression
        return new Expression(expression).Evaluate().ToString();
    }

    private static string EvaluateExpressionsParens(string expression, ISceneTranslator sceneTranslator)
    {
        // Evaluate Parens First
        for (int i = 0; i < expression.Length; i++)
        {
            var character = expression[i];

            if (character == '(')
            {
                string parenExpression = GetParenBody(expression, i);
                string parenValue = EvaluatePrimitive(parenExpression, sceneTranslator);
                expression = expression.Replace(parenExpression, parenValue);
            }
        }
        return expression;
    }

    private static object GetExpressionValue(string expression, ISceneTranslator sceneTranslator, string memberExpression)
    {
        GameObject gameObject = GetActor(memberExpression, sceneTranslator);

        object componentObj = GetComponent(memberExpression, gameObject);

        int beginOfPropertyChain = memberExpression.IndexOf('>') + 2;

        string propertyChainExpression = memberExpression.Substring(beginOfPropertyChain);

        return GetValueFromPropertyChain(propertyChainExpression, componentObj);
    }

    private static object EvaluateObject(string expression, ISceneTranslator sceneTranslator)
    {
        // {Caster}<Character>.Stats[(23 + 3)]

        expression = EvaluateExpressionsParens(expression, sceneTranslator);

        // Translate Member Values
        for (int i = 0; i < expression.Length; i++)
        {
            var character = expression[i];
            if (character == '{')
            {
                string memberExpression = GetPropertyExpression(expression, i);
                return GetExpressionValue(expression, sceneTranslator, memberExpression);
            }
        }
        return null;
    }

    private static object GetValueFromPropertyChain(string expression, object parentObject)
    {
        expression = ReplaceEnumsWithInts(expression);

        var properties = expression.Split('.');

        object value = parentObject;

        for (int i = 0; i < properties.Length; i++)
        {
            value = GetValueFor(properties[i], value);
        }

        return value;
    }

    private static string ReplaceEnumsWithInts(string expression)
    {
        for (int i = 0; i < expression.Length; i++)
        {
            var character = expression[i];
            if (character == '[')
            {
                int beginIndexExpression = i;
                int endIndexExpression = expression.Substring(beginIndexExpression).IndexOf(']') + beginIndexExpression;

                string indexExpression = expression.Substring(beginIndexExpression + 1, endIndexExpression - beginIndexExpression - 1);

                if (indexExpression.Contains('.'))
                {
                    var enumparts = indexExpression.Split('.');
                    string enumType = enumparts[0];
                    string enumValue = enumparts[1];

                    Type enumTypeAsType = Assembly.GetExecutingAssembly().GetType(enumType);

                    int enumIndex = (int)Enum.Parse(enumTypeAsType, enumValue, true);

                    expression = expression.Replace(indexExpression, enumIndex.ToString());
                }
            }
        }

        return expression;
    }

    private static object GetValueFor(string propertyExpression, object obj)
    {
        int memberCloseIndex = findEndOfMember(propertyExpression, 0);

        string member = propertyExpression.Substring(0, memberCloseIndex + 1);

        Type type = obj.GetType();
        FieldInfo field = type.GetField(member);

        object value;
        if (field == null)
        {
            value = type.GetProperty(member).GetValue(obj, null);
        }
        else
        {//Is it a property?
            value = field.GetValue(obj);
        }

        if (propertyExpression.Length > memberCloseIndex + 1 && propertyExpression[memberCloseIndex + 1] == '[')
        {// We are dealing with an array
            int arrayBeginIndex = memberCloseIndex + 1;
            int arrayEndIndex = propertyExpression.IndexOf(']');

            int indexToUseForValue = int.Parse(propertyExpression.Substring(arrayBeginIndex + 1, arrayEndIndex - arrayBeginIndex - 1));

            return value.GetType().GetProperty("Item").GetValue(value, new object[] { indexToUseForValue });
        }
        else
        {
            return value;
        }
    }

    private static GameObject GetActor(string expression, ISceneTranslator sceneTranslator)
    {
        int actorOpenIndex = expression.IndexOf('{');
        int actorCloseIndex = expression.IndexOf('}');

        string actor = expression.Substring(actorOpenIndex + 1, actorCloseIndex - actorOpenIndex - 1);

        return sceneTranslator.GetActor(actor);
    }

    private static Component GetComponent(string expression, GameObject actorGO)
    {
        int componentOpenIndex = expression.IndexOf('<');
        int componentCloseIndex = expression.IndexOf('>');

        string component = expression.Substring(componentOpenIndex + 1, componentCloseIndex - componentOpenIndex - 1);

        return actorGO.GetComponent(component);
    }

    private static int findEndOfMember(string expression, int indexToStartAt)
    {
        // Space doesn't count because we trimmed all spaces out already

        for (int i = indexToStartAt; i < expression.Length; i++)
        {
            if (expression[i] == '[')
            {
                return i - 1;
            }
        }

        return expression.Length - 1;
    }

    private static string GetPropertyExpression(string expression, int indexToStartAt)
    {
        int indexToEndAt = 0;

        for (int i = indexToStartAt; i < expression.Length; i++)
        {
            if (expression[i] == ' ')
            {
                indexToEndAt = i - 1;
                break;
            }

            indexToEndAt = i;
        }

        return expression.Substring(indexToStartAt, indexToEndAt - indexToStartAt + 1);
    }

    private static string GetParenBody(string expression, int openParenIndex)
    {
        int parenCount = 1;
        for (int i = openParenIndex + 1; i < expression.Length; i++)
        {
            var charecter = expression[i];
            if (charecter == '(')
            {
                parenCount++;
            }
            else if (charecter == ')')
            {
                parenCount--;

                if (parenCount == 0)
                {
                    return expression.Substring(openParenIndex + 1, i - 1 - openParenIndex);
                }
            }
        }

        throw new IndexOutOfRangeException("Paren Mismatch");
    }

    private static string TranslatePropertyExpression(string propertyExpression)
    {
        //{Caster}<Character>.Stats[LilithStats.Intelligence]
        return null;
    }
}

public static class AnnoyingExtension
{
    public static T JustCastItDammit<T>(this object obj)
    {
        return (T)obj;
    }
}

public interface ISceneTranslator
{
    GameObject GetActor(string actor);
}
