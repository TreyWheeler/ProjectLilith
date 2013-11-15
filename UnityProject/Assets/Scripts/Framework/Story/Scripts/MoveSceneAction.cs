using UnityEngine;
using System.Collections;

public class MoveSceneAction : SceneActionBase
{

    public string Speed = "1";
}

public class MoveInRangeOfEntitySceneAction : MoveSceneAction
{
    public string Entity;
    public string MinDistance = "1";
    public string MaxDistance = "2";
}

public class MoveToEntitySceneAction : MoveSceneAction
{
    public string Entity;
    public string HowClose = "1";
}


public class MoveToLocationSceneAction : MoveSceneAction
{
    public string Location;
}

