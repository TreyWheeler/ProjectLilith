using UnityEngine;
using System;
using IoC;

public class UnityContext:MonoBehaviour
{
    public static readonly string ADDED = "OnAdded";  
}

public class UnityContext<T>: UnityContext where T:IContextRoot, new()
{    
    
    private T _applicationRoot;

    protected IContainer container { get { return _applicationRoot.container; } }
    
    protected virtual void Awake()
    {     
        _applicationRoot = new T();
    }
 
    //
    // Defining OnEnable as fix for UnityEngine execution order bug
    //
    void OnEnable()
    {    
    }
 
    void OnAdded(MonoBehaviour component)
    {            
        _applicationRoot.container.Inject(component);
    }
 
}