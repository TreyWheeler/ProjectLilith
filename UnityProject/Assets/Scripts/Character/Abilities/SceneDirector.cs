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

    public static ScenePerformance CreatePerformaneScript(List<SceneActionBase> scene, ISceneTranslator sceneTranslator)
    {
        ScenePerformance performance = new ScenePerformance();

        foreach (var scriptAction in scene)
        {
            ScenePerformanceActionBase action;

            if (scriptAction is RunAnimationSceneAction)
            {
                var actionScript = (RunAnimationSceneAction)scriptAction;

                bool runOnce = ReadExpression<bool>(actionScript.RunOnce, sceneTranslator, performance);

                if (runOnce)
                {
                    RunAnimationOnceScenePerformanceAction perfAction = new RunAnimationOnceScenePerformanceAction();
                    perfAction.BlocksStory = actionScript.BlocksStory;
                    perfAction.Actor = ReadExpression<GameObject>(actionScript.Actor, sceneTranslator, performance);
                    perfAction.Animation = ReadExpression<string>(actionScript.Animation, sceneTranslator, performance);
                    action = perfAction;
                }
                else
                {
                    RunAnimationScenePerformanceAction perfAction = new RunAnimationScenePerformanceAction();
                    perfAction.BlocksStory = actionScript.BlocksStory;
                    perfAction.Actor = ReadExpression<GameObject>(actionScript.Actor, sceneTranslator, performance);
                    perfAction.Animation = ReadExpression<string>(actionScript.Animation, sceneTranslator, performance);
                    action = perfAction;
                }
            }
            else if (scriptAction is PlaySoundSceneAction)
            {
                var actionScript = (PlaySoundSceneAction)scriptAction;
                SoundEffectScenePerformanceAction perfAction = new SoundEffectScenePerformanceAction();
                perfAction.BlocksStory = actionScript.BlocksStory;
                perfAction.AudioFile = ReadExpression<AudioClip>(actionScript.Sound, sceneTranslator, performance);
                perfAction.AudioSourceActor = ReadExpression<GameObject>(actionScript.Actor, sceneTranslator, performance);
                action = perfAction;
            }
            else if (scriptAction is MoveSceneAction)
            {
                if (scriptAction is MoveToLocationSceneAction)
                {
                    var actionScript = (MoveToLocationSceneAction)scriptAction;
                    MoveToLocationScenePerformanceAction perfAction = new MoveToLocationScenePerformanceAction();
                    perfAction.BlocksStory = actionScript.BlocksStory;
                    perfAction.WhoToMove = ReadExpression<GameObject>(actionScript.Actor, sceneTranslator, performance);
                    perfAction.Location = ReadExpression<Vector3>(actionScript.Location, sceneTranslator, performance);
                    perfAction.Speed = ReadExpression<float>(actionScript.Speed, sceneTranslator, performance);
                    action = perfAction;
                }
                else if (scriptAction is MoveToEntitySceneAction)
                {
                    var actionScript = (MoveToEntitySceneAction)scriptAction;
                    MoveToGameObjectScenePerformanceAction perfAction = new MoveToGameObjectScenePerformanceAction();
                    perfAction.BlocksStory = actionScript.BlocksStory;
                    perfAction.WhoToMove = ReadExpression<GameObject>(actionScript.Actor, sceneTranslator, performance);
                    perfAction.Target = ReadExpression<GameObject>(actionScript.Entity, sceneTranslator, performance);
                    perfAction.Speed = ReadExpression<float>(actionScript.Speed, sceneTranslator, performance);
                    perfAction.HowClose = ReadExpression<float>(actionScript.HowClose, sceneTranslator, performance);
                    action = perfAction;

                }
                else if (scriptAction is MoveInRangeOfEntitySceneAction)
                {
                    var actionScript = (MoveInRangeOfEntitySceneAction)scriptAction;
                    MoveInRangeOfGameObjectScenePerformanceAction perfAction = new MoveInRangeOfGameObjectScenePerformanceAction();
                    perfAction.BlocksStory = actionScript.BlocksStory;
                    perfAction.WhoToMove = ReadExpression<GameObject>(actionScript.Actor, sceneTranslator, performance);
                    perfAction.Target = ReadExpression<GameObject>(actionScript.Entity, sceneTranslator, performance);
                    perfAction.Speed = ReadExpression<float>(actionScript.Speed, sceneTranslator, performance);
                    perfAction.MinRange = ReadExpression<float>(actionScript.MinDistance, sceneTranslator, performance);
                    perfAction.MaxRange = ReadExpression<float>(actionScript.MaxDistance, sceneTranslator, performance);
                    action = perfAction;

                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (scriptAction is AdjustStatSceneAction)
            {
                var actionScript = (AdjustStatSceneAction)scriptAction;
                AdjustStatScenePerformanceAction perfAction = new AdjustStatScenePerformanceAction();
                perfAction.BlocksStory = actionScript.BlocksStory;
                perfAction.Stat = ReadExpression<Stat<LilithStats>>(actionScript.StatToAdjust, sceneTranslator, performance);
                perfAction.Adjustment = ReadExpression<float>(actionScript.Adjustment, sceneTranslator, performance);
                perfAction.Seconds = ReadExpression<float>(actionScript.OverSeconds, sceneTranslator, performance);
                action = perfAction;
            }
            else if (scriptAction is SpawnParticleEffectSceneAction)
            {
                var actionScript = (SpawnParticleEffectSceneAction)scriptAction;
                SpawnParticleEffectScenePerformanceAction perfAction = new SpawnParticleEffectScenePerformanceAction();
                perfAction.BlocksStory = actionScript.BlocksStory;
                perfAction.Duration = ReadExpression<float>(actionScript.Duration, sceneTranslator, performance);
                perfAction.Actor = ReadExpression<GameObject>(actionScript.Actor, sceneTranslator, performance);
                perfAction.Target = ReadExpression<GameObject>(actionScript.Target, sceneTranslator, performance);
                perfAction.ParticlesPerSecond = ReadExpression<float>(actionScript.ParticlesPerSecond, sceneTranslator, performance);
                perfAction.Color1 = actionScript.Color1;
                perfAction.Color2 = actionScript.Color2;
                perfAction.Color3 = actionScript.Color3;
                perfAction.Color4 = actionScript.Color4;
                perfAction.Color5 = actionScript.Color5;
                perfAction.LocalPosition = actionScript.LocalPosition;
                perfAction.RandomVelocity = actionScript.RandomVelocity;
                perfAction.ParticleLifeTime = ReadExpression<float>(actionScript.ParticleLifeTime, sceneTranslator, performance);
                perfAction.ParticleSize = ReadExpression<float>(actionScript.ParticleSize, sceneTranslator, performance);
                perfAction.ParticleSpeed = ReadExpression<float>(actionScript.ParticleSpeed, sceneTranslator, performance);
                action = perfAction;
            }
            else if (scriptAction is WaitSceneAction)
            {
                var actionScript = (WaitSceneAction)scriptAction;
                WaitScenePerformanceAction perfAction = new WaitScenePerformanceAction();
                perfAction.BlocksStory = actionScript.BlocksStory;
                perfAction.Seconds = ReadExpression<float>(actionScript.Seconds, sceneTranslator, performance);
                action = perfAction;
            }
            else if (scriptAction is FinishPartSceneAction)
            {
                var actionScript = (FinishPartSceneAction)scriptAction;
                var finishAction = new FinishPartScenePerformanceAction();
                finishAction.DelayBeforeFinish = ReadExpression<float>(actionScript.DelayBeforeFinish, sceneTranslator, performance);
                finishAction.PartToFinish = ReadExpression<ScenePerformanceActionBase>(actionScript.PartToFinish, sceneTranslator, performance);
                action = finishAction;
            }
            else if (scriptAction is ModifyStatSceneAction)
            {
                var actionScript = (ModifyStatSceneAction)scriptAction;
                var modifyAction = new ModifyStatScenePerformanceAction();
                modifyAction.BlocksStory = actionScript.BlocksStory;
                modifyAction.Stat = ReadExpression<Stat<LilithStats>>(actionScript.StatToAdjust, sceneTranslator, performance);
                modifyAction.Adjustment = ReadExpression<float>(actionScript.Adjustment, sceneTranslator, performance);
                modifyAction.Duration = ReadExpression<float>(actionScript.Duration, sceneTranslator, performance);
                modifyAction.AppliesToMax = actionScript.AppliesToMax;
                modifyAction.IsMultiplicative = actionScript.IsMultiplicative;
                action = modifyAction;
            }
            else if (scriptAction is BuffSceneAction)
            {
                var actionScript = (BuffSceneAction)scriptAction;
                var buffAction = new BuffScenePerformanceAction();
                buffAction.BlocksStory = actionScript.BlocksStory;
                buffAction.Performance = CreatePerformaneScript(actionScript.Actions, sceneTranslator);
                buffAction.Target = ReadExpression<Character>(actionScript.TargetCharacter, sceneTranslator, performance);
                action = buffAction;
            }
            else
            {
                throw new NotImplementedException();
            }

            action.Name = scriptAction.Name;

            performance.Que(action);
        }

        return performance;
    }

    private static T ReadExpression<T>(string expression, ISceneTranslator sceneTranslator, ScenePerformance performance)
    {
        // 5                                    
        // {Caster}                               
        // {Caster}<Character>.MoveSpeed          

        if (expression == null)
            return default(T);

        Type typeOfT = typeof(T);

        if (typeOfT.IsSubclassOf(typeof(Component)))
        {
            GameObject gameObject = GetActor(expression, sceneTranslator);
            return GetComponent(expression, gameObject).JustCastItDammit<T>();
        }

        // Contains Selector
        if (expression.Contains('{'))
        {// Is an expression

            //if (expression.EndsWith("}"))
            //{
            //    string memberExpression = GetPropertyExpression(expression, 0);

            //    return GetActor(memberExpression, sceneTranslator).JustCastItDammit<T>();
            //}

            if (typeOfT == typeof(System.Single)) // Single = Boxed Primitive
                expression = EvaluatePrimitive(expression, sceneTranslator, performance);
            else
                return (T)EvaluateObject(expression, sceneTranslator, performance);
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

    private static string EvaluatePrimitive(string expression, ISceneTranslator sceneTranslator, ScenePerformance performance)
    {
        // 60 + ({Caster}<Character>.Stats[(23 + 3)].CurrentValue * 5)

        expression = EvaluateExpressionsParens(expression, sceneTranslator, performance);

        // Translate Member Values
        for (int i = 0; i < expression.Length; i++)
        {
            var character = expression[i];
            if (character == '{')
            {
                string memberExpression = GetPropertyExpression(expression, i);
                object value = GetExpressionValue(sceneTranslator, memberExpression, performance);
                expression = expression.Replace(memberExpression, value.ToString());
            }
        }

        // Evaluate Expression
        return new Expression(expression).Evaluate().ToString();
    }

    private static string EvaluateExpressionsParens(string expression, ISceneTranslator sceneTranslator, ScenePerformance performance)
    {
        // Evaluate Parens First
        for (int i = 0; i < expression.Length; i++)
        {
            var character = expression[i];

            if (character == '(')
            {
                string parenExpression = GetParenBody(expression, i);
                string parenValue = EvaluatePrimitive(parenExpression, sceneTranslator, performance);
                expression = expression.Replace(parenExpression, parenValue);
            }
        }
        return expression;
    }

    private static object GetExpressionValue(ISceneTranslator sceneTranslator, string expression, ScenePerformance performance)
    {
        // TODO: We should probably change the expression to be
        // {Actor<Component>} instead of
        // {Actor}<Component> so that we can have the selector return everything prior to property chain

        // TODO: We should probably change the component resolution to be
        // {Actor}.GetComponent(Component)
        // {Actor<Component>}
        // So that we have method support and move more towards our syntax looking like C# and thus 2nd nature

        int expressionStart = 2;
        int expressionEnd = expression.IndexOf('}') - 1;

        if (expression[1] == '#')
        {// Get by ID


            // {#Trey}                                 |   {#Trey}.Property
            // 0123456                                 |   0123456789012345

            // name = "{#Trey}".Substring(2, 5-2+1);   |   name = "{#Trey}.Property".Substring(2, 5-2+1);
            // name = "{#Trey}".Substring(2, 4);       |   name = "{#Trey}.Property".Substring(2, 4);
            // name = "Trey";                          |   name = "Trey";
            String name = expression.Substring(expressionStart, expressionEnd - expressionStart + 1);

            object obj = performance.GetBy(name);

            // "{#Trey}".Length                        |   "{#Trey}.Property".Length
            // 7 > 5 + 2                               |   16 > 5 + 2
            // 7 > 7                                   |   16 > 7
            if (expression.Length > expressionEnd + 2)
            {// There is still more to parse
                int indexOfPropertyChainStart = expressionEnd + 3;

                String propertyChainExpression = expression.Substring(indexOfPropertyChainStart);

                return GetValueFromPropertyChain(propertyChainExpression, obj);
            }
            else
            {
                return obj;
            }
        }
        else
        {
            GameObject gameObject = GetActor(expression, sceneTranslator);

            if (expression.Length > expressionEnd + 2)
            {
                object componentObj = GetComponent(expression, gameObject);

                int beginOfPropertyChain = expression.IndexOf('>') + 2;

                string propertyChainExpression = expression.Substring(beginOfPropertyChain);

                return GetValueFromPropertyChain(propertyChainExpression, componentObj);
            }
            else
            {
                return gameObject;
            }
        }
    }

    private static object EvaluateObject(string expression, ISceneTranslator sceneTranslator, ScenePerformance performance)
    {
        // {Caster}<Character>.Stats[(23 + 3)]

        expression = EvaluateExpressionsParens(expression, sceneTranslator, performance);

        // Translate Member Values
        for (int i = 0; i < expression.Length; i++)
        {
            var character = expression[i];
            if (character == '{')
            {
                string memberExpression = GetPropertyExpression(expression, i);
                return GetExpressionValue(sceneTranslator, memberExpression, performance);
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
