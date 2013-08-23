using UnityEngine;
using System;

public class SceneParameters : MonoBehaviour
{
    public object Parameters { get; set; }
        
    public SceneParameters(object parameters)
    {
        Parameters = parameters;                        
    }
        
    protected void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);   
    }
}