using UnityEngine;
using System.Collections;

public class RunAnimationScenePerformanceAction : ScenePerformanceActionBase
{
    public GameObject Actor;
    public string Animation;

    protected override void TellStory()
    {
        Actor.animation[Animation].wrapMode = WrapMode.Loop;
        Actor.animation.CrossFade(Animation);

        RaiseComplete();
    }

}


public class RunAnimationOnceScenePerformanceAction : ScenePerformanceActionBase
{
    public GameObject Actor;
    public string Animation;

    bool started = false;

    protected override void TellStory()
    {
        // Note: Do we capture active animation, if its a looper, play before raise complete?
        //Actor.animation.PlayQueued(, QueueMode.PlayNow)
        if (!Actor.animation.IsPlaying(Animation))
        {
            if (started)
            {
                RaiseComplete();
                return;
            }

            Actor.animation.Play(Animation);
            started = true;
        }
    }
}