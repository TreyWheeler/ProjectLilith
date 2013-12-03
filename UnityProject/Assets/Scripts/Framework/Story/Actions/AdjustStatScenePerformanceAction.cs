using UnityEngine;
using System.Collections;

public class AdjustStatScenePerformanceAction : ScenePerformanceActionBase
{
    public Stat<LilithStats> Stat;
    public float Adjustment;
    public float Seconds;

    private float adjustmentApplied = 0f;

    public override void Update()
    {
        if (Seconds == 0)
        {
            Stat.CurrentValue += Adjustment;
            Finish();
        }
        else
        {
            float adjustmentStep = Adjustment / Seconds * Time.deltaTime;
            Stat.CurrentValue += adjustmentStep;
            adjustmentApplied += adjustmentStep;

            if (Adjustment < 0)
            {
                if (adjustmentApplied <= Adjustment)
                    Finish();
            }
            else
            {
                if (adjustmentApplied >= Adjustment)
                    Finish();
            }
            
        }
    }
}
