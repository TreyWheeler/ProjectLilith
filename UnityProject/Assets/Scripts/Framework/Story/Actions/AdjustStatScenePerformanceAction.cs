using UnityEngine;
using System.Collections;

public class AdjustStatScenePerformanceAction : ScenePerformanceActionBase
{
    public Stat<LilithStats> Stat;
    public float Adjustment;

    protected override void TellStory()
    {
        Stat.CurrentValue += Adjustment;
        Debug.Log(Stat);
        RaiseComplete();
    }
}
