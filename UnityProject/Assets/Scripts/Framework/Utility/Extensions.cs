using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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
}
