using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TimedStatusEffect<StatEnum> : StatusEffect<StatEnum>
{
    public int Duration;

    public TimedStatusEffect(int duration)
    {
        Duration = duration;
    }

    public override void Apply(TimedTaskManager Tasks, StatList<StatEnum> Stats)
    {
        Tasks.Add(Duration, RaiseExpired);

        base.Apply(Tasks, Stats);
    }
}

public class ConditionalStatusEffect<StatEnum> : StatusEffect<StatEnum>
{
    public Func<bool> Condition;

    public ConditionalStatusEffect(Func<bool> condition)
    {
        Condition = condition;
    }

    public override void Apply(TimedTaskManager Tasks, StatList<StatEnum> Stats)
    {
        Tasks.Conditionals.Add(new Conditional() { Condition = Condition, Result = RaiseExpired });

        base.Apply(Tasks, Stats);
    }
}

public class StatFlag<StatEnum>
{
    public StatFlagType Type;
    public StatEnum Target;
}

public abstract class StatusEffect<StatEnum>
{
    public List<StatModifier<StatEnum>> Modifiers = new List<StatModifier<StatEnum>>();
    public List<StatFlag<StatEnum>> Flags = new List<StatFlag<StatEnum>>();
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

    public virtual void Apply(TimedTaskManager Tasks, StatList<StatEnum> Stats)
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

    public virtual void Remove(StatList<StatEnum> Stats)
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
