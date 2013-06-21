using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatEntity : IHaveStats
{
    public string Name;
    public Ability[] abilities;
    private Dictionary<StatType, Stat> _stats = new Dictionary<StatType, Stat>();
    private List<StatusEffect> StatusEffects = new List<StatusEffect>();
    
    public Dictionary<StatType, Stat> Stats
    {
        get
        {
            return _stats;
        }
    }
    
    public CombatEntity()
    {
        _stats.Add(StatType.Health, new Stat(0, 200, 700));
        _stats[StatType.Health].MinimumReached += (tasty) => 
        {
            Name = "Dead";
        };
    }
    
}
