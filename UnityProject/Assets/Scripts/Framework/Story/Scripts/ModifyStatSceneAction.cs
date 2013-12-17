using UnityEngine;
using System.Collections;

public class ModifyStatSceneAction : SceneActionBase
{
    public string StatToAdjust;
    public string Adjustment;
    public string Duration;
    public bool AppliesToMax = false;
    public bool IsMultiplicative = false;
}
