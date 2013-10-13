using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatEntity : IHaveStats
{
    public string Name;
    public StatList _stats = new StatList();
    public Ability[] abilities;    
    //private List<StatusEffect> StatusEffects = new List<StatusEffect>();
    
    public StatList Stats
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
    
    
    public void Save()
    {
//        string namePrefix = Name + "-";
//        for (int i = 0; i < Ability.length; i++) {
//            Ability currentAbility = Ability[i].Save();
//            string abilityPrefix = Name + "-"+ currentAbility.DisplayName + "-";
//            PlayerPrefs.SetString(abilityPrefix + currentAbility
//        
    }

}
