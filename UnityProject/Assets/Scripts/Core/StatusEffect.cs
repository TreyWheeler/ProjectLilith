using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TimedStatusEffect : StatusEffect
{
    public int Duration;

    public TimedStatusEffect(int duration)
    {
        Duration = duration;
    }

    public override void Apply(TimedTaskManager Tasks, Dictionary<StatType, Stat> Stats)
    {
        Tasks.Add(Duration, RaiseExpired);

        base.Apply(Tasks, Stats);
    }
}

public class ConditionalStatusEffect : StatusEffect
{
    public Func<bool> Condition;

    public ConditionalStatusEffect(Func<bool> condition)
    {
        Condition = condition;
    }

    public override void Apply(TimedTaskManager Tasks, Dictionary<StatType, Stat> Stats)
    {
        Tasks.Conditionals.Add(new Conditional() { Condition = Condition, Result = RaiseExpired });

        base.Apply(Tasks, Stats);
    }
}

public class StatFlag
{
    public StatFlagType Type;
    public StatType Target;
}

public abstract class StatusEffect
{
    public List<StatModifier> Modifiers = new List<StatModifier>();
    public List<StatFlag> Flags = new List<StatFlag>();
    public Action Expired;
    public Action Applied;
    public Action Removed;

    protected void RaiseExpired()
    {
        if(Expired != null)
        {
            Expired();
        }
    }

    public virtual void Apply(TimedTaskManager Tasks, Dictionary<StatType, Stat> Stats)
    {
        foreach(var mod in Modifiers)
        {
            Stats[mod.TargetStat].AddModifier(mod);
        }

        foreach(var flag in Flags)
        {
            Stats[flag.Target].Flags.Add(flag.Type);
        }

        if(Applied != null)
            Applied();

    }

    public virtual void Remove(Dictionary<StatType, Stat> Stats)
    {
        foreach(var mod in Modifiers)
        {
            Stats[mod.TargetStat].RemoveModifier(mod);
        }

        foreach(var flag in Flags)
        {
            Stats[flag.Target].Flags.Remove(flag.Type);
        }

        if(Applied != null)
            Removed();
    }
}
