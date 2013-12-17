using UnityEngine;
using System.Collections;

public class ModifyStatScenePerformanceAction : ScenePerformanceActionBase
{
    public Stat<LilithStats> Stat;
    public float Adjustment;
    public float Duration;
    public bool AppliesToMax;
    public bool IsMultiplicative;

    private StatModifier<LilithStats> _statModifier;

    public override void Start()
    {
        _statModifier = new StatModifier<LilithStats>();
        _statModifier.Target = AppliesToMax ? StatModifierTarget.Max : StatModifierTarget.Current;
        _statModifier.Value = Adjustment;
        _statModifier.Type = IsMultiplicative ? StatModifierType.Multiplicative : StatModifierType.Additive;
        Stat.AddModifier(_statModifier);
        TimedTaskManager.Instance.Add(Duration * 1000, () => 
        {
            Finish();
        });
        base.Start();
    }

    public override void Finish()
    {
        if (Finished)
            return;

        Stat.RemoveModifier(_statModifier);
        base.Finish();
    }


}
