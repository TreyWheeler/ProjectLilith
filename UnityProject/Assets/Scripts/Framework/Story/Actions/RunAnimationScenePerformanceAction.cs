using UnityEngine;
using System.Collections;

public class RunAnimationScenePerformanceAction : ScenePerformanceActionBase
{
    public GameObject Actor;
    public string Animation;

    public override void Update()
    {
        Actor.animation[Animation].wrapMode = WrapMode.Loop;
        Actor.animation.CrossFade(Animation);

        Finish();
    }

}


public class RunAnimationOnceScenePerformanceAction : ScenePerformanceActionBase
{
    public GameObject Actor;
    public string Animation;

    public override void Start()
    {
        base.Start();

        Actor.animation.Play(Animation);
    }

    public override void Update()
    {
        if (!Actor.animation.IsPlaying(Animation))
        {
            Finish();
        }
    }
}