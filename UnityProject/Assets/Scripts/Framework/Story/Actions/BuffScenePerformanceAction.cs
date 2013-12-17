using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BuffScenePerformanceAction : ScenePerformanceActionBase
{
    public ScenePerformance Performance;
    public Character Target; 

    public override void Start()
    {
        base.Start();

        Target.ApplyBuff(Performance);

        Finish();
    }
}