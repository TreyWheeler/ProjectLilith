using UnityEngine;
using System.Collections;

public class WaitScenePerformanceAction : ScenePerformanceActionBase
{
    public float Seconds;

    public override void Start()
    {
        TimedTaskManager.Instance.Add(Seconds * 1000, () =>
        {
            Finish();
        });
    }
}
