using UnityEngine;
using System.Collections;

public class WaitScenePerformanceAction : ScenePerformanceActionBase
{
    public float Seconds;
    private bool Created = false;

    protected override void TellStory()
    {
        if (Created)
            return;

        TimedTaskManager.Instance.Add(Seconds * 1000, () =>
        {
            RaiseComplete();
        });
        Created = true;
    }
}
