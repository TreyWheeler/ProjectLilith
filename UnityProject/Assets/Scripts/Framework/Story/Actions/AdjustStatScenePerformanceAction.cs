﻿using UnityEngine;
using System.Collections;

public class AdjustStatScenePerformanceAction : ScenePerformanceActionBase
{
    public Stat<LilithStats> Stat;
    public float Adjustment;
    public float Seconds;

    private float adjustmentApplied = 0f;

    protected override void TellStory()
    {
        if (Seconds == 0)
        {
            Stat.CurrentValue += Adjustment;
            RaiseComplete();
        }
        else
        {
            float adjustmentStep = Adjustment / Seconds * Time.deltaTime;
            Stat.CurrentValue += adjustmentStep;
            adjustmentApplied += adjustmentStep;

            if (Adjustment < 0)
            {
                if (adjustmentApplied <= Adjustment)
                    RaiseComplete();
            }
            else
            {
                if (adjustmentApplied >= Adjustment)
                    RaiseComplete();
            }
            
        }
    }
}
