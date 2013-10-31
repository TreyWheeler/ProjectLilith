using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Linq;

public static class SceneDirector
{
    // Single Responsibility: Turns a Script into a Performance

    public static ScenePerformance CreatePerformaneScript(SceneScript scene, ISceneTranslator sceneTranslator)
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
        }

        return performance;
    }

    private static T ReadExpression<T>(string expression, ISceneTranslator sceneTranslator)
    {
        // 5                                    Value
        // Caster                               Actor
        // Caster[Character].MoveSpeed          Actor[Component].Property

        if (expression.StartsWith("{"))
        {// expression
            expression = expression.Substring(1, expression.Length - 2);

            int indexOfComponent = expression.IndexOf("[");

            if (indexOfComponent != -1)
            {// Has Component
                int indexofEndOfComponent = expression.IndexOf("]");

                string actor = expression.Substring(0, indexOfComponent);
                string component = expression.Substring(indexOfComponent + 1, indexofEndOfComponent - indexOfComponent - 1);
                string member = expression.Substring(indexofEndOfComponent + 2);

                GameObject actorGO = sceneTranslator.GetActor(actor);
                object componentObj = actorGO.GetComponent(component);

                Type type = componentObj.GetType();
                FieldInfo field = type.GetField(member);
                if (field == null)
                {
               
                    return (T)type.GetProperty(member).GetValue(componentObj, null);
                }
                else
                {//Is it a property?
                    return (T)field.GetValue(componentObj);
                }
            }

            // Assumed actor since no component
            return sceneTranslator.GetActor(expression).JustCastItDammit<T>();
        }
        else
        {// value
            Type typeOfT = typeof(T);

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
            else
            {
                throw new NotSupportedException();
            }
        }
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
