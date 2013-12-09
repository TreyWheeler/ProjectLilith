using UnityEngine;
using System.Collections;

public class FinishPartScenePerformanceAction : ScenePerformanceActionBase
{
    public ScenePerformanceActionBase PartToFinish;
    public float DelayBeforeFinish;

    public override void Start()
    {
        if (DelayBeforeFinish > 0)
            TimedTaskManager.Instance.Add(DelayBeforeFinish, () => { PartToFinish.Finish(); });
        else
            PartToFinish.Finish();

        Finish();
    }
}
