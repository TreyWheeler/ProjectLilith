using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static T EnsureComponent<T>(this GameObject gameObj) where T : Component
    {
        T component = gameObj.GetComponent<T>();

        if (component == null)
            component = gameObj.AddComponent<T>();

        return component;
    }

    public static float Length(this Vector2 vector)
    {
        return Vector2.Distance(Vector2.zero, vector);
    }

    public static void AddOnClick(this GameObject gameObject, Action onClick)
    {
        ClickTracker clickTracker = Helper.EnsureGameObject("ClickTracker").EnsureComponent<ClickTracker>();

        clickTracker.AddOnClickHandler(gameObject, onClick);
    }

    public static void AddOnOutsideClick(this GameObject gameObject, Action onClick)
    {
        ClickTracker clickTracker = Helper.EnsureGameObject("ClickTracker").EnsureComponent<ClickTracker>();

        clickTracker.AddOnOutsideClickHandler(gameObject, onClick);
    }


    public static bool MouseOnMe(this GameObject go)
    {
        return Helper.GetGameOjectMouseIsOver() == go;
    }

    public static IEnumerable<GameObject> GetChildren(this GameObject gameObject)
    {
        foreach (Transform transform in gameObject.transform)
        {
            yield return transform.gameObject;
        }
    }

    public static T GetFirstComponentThatImplements<T>(this GameObject gameObject) where T : class
    {
        Component[] components = gameObject.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            T implementation = components[i] as T;
            if (implementation != null)
                return implementation;        
        }
        return null;
    }

    public static void LookAt(this GameObject actor, GameObject target)
    {
        actor.transform.LookAt(target.transform);
    }
}