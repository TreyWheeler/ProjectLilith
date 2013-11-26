using UnityEngine;
using System.Collections;
using System;

public static class Helper
{

    public static int DeltaTimeInMilliseconds { get { return Convert.ToInt32(Time.deltaTime * 1000); } }

    public static Rect GetBoxCenteredOnPoint(float x, float y, float height, float width)
    {
        return new Rect(x - width / 2f, y - height / 2f, width, height);
    }

    public static void LoadScene<T>(string sceneName, T parameters) where T : class
    {
        SceneParameters sceneParameters = Helper.EnsureGameObject("SceneParameters").EnsureComponent<SceneParameters>();

        sceneParameters.Parameters = parameters;

        Application.LoadLevel(sceneName);
    }

    public static GameObject EnsureGameObject(string name)
    {
        GameObject paramObj = GameObject.Find(name);
        if (paramObj == null)
            paramObj = new GameObject(name);

        return paramObj;
    }

    public static T ReadSceneParameters<T>() where T : class
    {
        GameObject paramObj = GameObject.Find("SceneParameters");
        if (paramObj == null)
            return null;

        SceneParameters component = paramObj.GetComponent<SceneParameters>();

        if (component == null)
            return null;

        GameObject.Destroy(paramObj);

        return component.Parameters as T;
    }

    public static void RemoveComponent<T>(this GameObject obj) where T : Component
    {
        T component = obj.GetComponent<T>();
        if (component != null)
            Component.Destroy(component);
    }

    public static float GetAngleInDegreesFrom(GameObject go, GameObject target)
    {
        return Vector3.Angle(go.transform.position - target.transform.position, go.transform.forward);
    }

    public static GameObject GetGameOjectMouseIsOver()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            return hit.transform.gameObject;

        return null;
    }

    public static T GetFirstInstanceOfComponentMouseIsOver<T>(Predicate<T> conditionForsuccess = null) where T : Component
    {
        GameObject objectMouseIsOver = GetGameOjectMouseIsOver();
        if (objectMouseIsOver != null)
            return LoopOverGameObjectsBehindMouse<T>(GetGameOjectMouseIsOver(), conditionForsuccess);
        return null;
    }

    private static T LoopOverGameObjectsBehindMouse<T>(GameObject go, Predicate<T> conditionForsuccess) where T : Component
    {
        T possibleResult = go.GetComponent<T>();
        
        if (possibleResult != null)
        {
            if(conditionForsuccess == null || conditionForsuccess(possibleResult))
                return possibleResult;
        }
        LayerMask currentLayer = go.layer;
        go.layer = LayerMask.NameToLayer("Ignore Raycast");
        GameObject nextgo = Helper.GetGameOjectMouseIsOver();
        T result = null;
        if (nextgo != null)
        {
            result = LoopOverGameObjectsBehindMouse<T>(nextgo, conditionForsuccess);
        }
        go.layer = currentLayer;
        return result;
    }
}

