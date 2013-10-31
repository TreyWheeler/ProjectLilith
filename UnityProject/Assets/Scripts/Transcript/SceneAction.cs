using UnityEngine;
using System.Collections;

public class SceneAction
{
    public string Actor; // {TargetedPlayer}, {Actor} -- Reserved words to be resolved by the Narrator contextually
}


public class RunAnimationSceneAction : SceneAction
{
    public string RunOnce = "False";
    public string Animation;
  
}

public class MoveSceneAction : SceneAction
{

    public string Speed = "1";
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


public class MoveInRangeOfEntitySceneAction : MoveSceneAction
{
    public string Entity;
    public string MinDistance = "1";
    public string MaxDistance = "2";
}