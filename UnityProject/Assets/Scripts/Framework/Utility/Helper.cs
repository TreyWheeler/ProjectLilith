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
        if(paramObj == null)
            return null;
        
        SceneParameters component = paramObj.GetComponent<SceneParameters>();
        
        if(component == null)
            return null;
        
        GameObject.Destroy(paramObj);
        
        return component.Parameters as T;
    }
    
    public static void RemoveComponent<T>(this GameObject obj)where T : Component
    {
        T component = obj.GetComponent<T>();
        if(component != null)
            Component.Destroy(component);
    }
}

