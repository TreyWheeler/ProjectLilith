using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public enum StatType
{
    Health,
    Psyche, 
    Endurance,        
    Lives,
    HealthPerSecond,
    PsychePerSecond,
    EndurancePerSecond,
    MovementSpeed,
    Visibility,
    ColorRed,
    ColorGreen,
    ColorBlue
}

public class StatChangedArgs : EventArgs
{
    public float Before;
    public float Now;
    public float Difference;

}

public enum StatModifierType
{
    Multiplicative,
    Additive
}

public enum StatModifierTarget
{
    Max,
    Current
}

public class StatModifier
{
    public StatModifierType Type = StatModifierType.Additive;
    public StatModifierTarget Target = StatModifierTarget.Current;
    public StatType TargetStat;
    public float Value;
}

public enum StatFlagType
{
    CannotDecrease
}

public class Stat
{
    public event Action<Stat> MinimumReached;
    public event Action<Stat, StatChangedArgs> Changed;

    private List<StatModifier> Modifiers = new List<StatModifier>();
    public List<StatFlagType> Flags = new List<StatFlagType>();

    public void AddModifier(StatModifier mod)
    {
        var oldValue = CurrentValue;
        Modifiers.Add(mod);
        if(oldValue != CurrentValue && Changed != null)
            Changed(this, new StatChangedArgs() { Before = oldValue, Now = CurrentValue, Difference = CurrentValue - oldValue });
                
    }

    public void RemoveModifier(StatModifier mod)
    {
        var oldValue = CurrentValue;
        Modifiers.Remove(mod);
        if(oldValue != CurrentValue && Changed != null)
            Changed(this, new StatChangedArgs() { Before = oldValue, Now = CurrentValue, Difference = CurrentValue - oldValue });
    }

    public void OnMinimumReached()
    {
        if(MinimumReached != null)
        {
            MinimumReached(this);
        }
    }

    private float _MaxValue = 1f;

    public float MaxValue
    {
        get
        {
            float ModValue = _MaxValue;

            if(Modifiers.Count > 0)
            {
                foreach(var mod in Modifiers.Where(m => m.Target == StatModifierTarget.Max && m.Type == StatModifierType.Additive))
                {
                    ModValue += mod.Value;
                }

                foreach(var mod in Modifiers.Where(m => m.Target == StatModifierTarget.Max && m.Type == StatModifierType.Multiplicative))
                {
                    ModValue *= mod.Value;
                }
            }

            return ModValue;
        }

        set
        {
            _MaxValue = value;
        }
    }

    private float _MinValue = 0f;

    public float MinValue
    {
        get
        {
            return _MinValue;
        }

        set
        {
            _MinValue = value;
        }
    }

    private float _CurrentValue;

    public float CurrentValue
    {
        get
        {
            float ModValue = _CurrentValue;

            if(Modifiers.Count > 0)
            {
                foreach(var mod in Modifiers.Where(m => m.Target == StatModifierTarget.Current && m.Type == StatModifierType.Additive))
                {
                    ModValue += mod.Value;
                }

                foreach(var mod in Modifiers.Where(m => m.Target == StatModifierTarget.Current && m.Type == StatModifierType.Multiplicative))
                {
                    ModValue *= mod.Value;
                }
            }

            return Mathf.Clamp(ModValue, MinValue, MaxValue);
        }

        set
        {
            if(value != _CurrentValue)
            {
                if(value < _CurrentValue && Flags.Contains(StatFlagType.CannotDecrease))
                {
                    return;
                }

                float valueBefore = _CurrentValue;

                if(value >= this.MaxValue)
                {
                    _CurrentValue = this.MaxValue;
                }
                else if(value <= MinValue)
                {
                    _CurrentValue = this.MinValue;
                    OnMinimumReached();
                }
                else
                {
                    _CurrentValue = value;
                }

                if(valueBefore != _CurrentValue && Changed != null)
                    Changed(this, new StatChangedArgs() { Before = valueBefore, Now = _CurrentValue, Difference = _CurrentValue - valueBefore });
            }
        }
    }

    public float CurrentRatio
    {
        get
        {
            return this.CurrentValue / this.MaxValue;
        }
    }

    public float CurrentPercent
    {
        get
        {
            return this.CurrentRatio * 100;
        }
    }

    public Stat(float MinValue, float MaxValue, float CurrentValue)
    {
        this.MinValue = MinValue;
        this.MaxValue = MaxValue;
        this.CurrentValue = CurrentValue;
    }

    public Stat(float MinValue, float MaxValue) : this( MinValue, MaxValue, MaxValue)
    {
    }

    public Stat(float MaxValue)
        : this(0, MaxValue, MaxValue)
    {
    }
}

public static class StatExtensions
{
    public static Stat GetHealth(this IDictionary<StatType, Stat> stats)
    {
        return stats[StatType.Health];
    }
}