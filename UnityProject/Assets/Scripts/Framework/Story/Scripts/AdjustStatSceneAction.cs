using UnityEngine;
using System.Collections;


public class AdjustStatSceneAction : SceneActionBase
{
    public string StatToAdjust;
    public string Adjustment;
    public string OverSeconds = "0";
}

public class AdjustStatForManySceneAction : SceneActionBase
{
    public string TeamToAdjust;
    public LilithStats StatToAdjust;
    public string Adjustment;
    public string OverSeconds = "0";
    
}
