using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatEntity
{
    public string Name;
    public Ability[] abilities;
    
    Dictionary<StatType, Stat> Stats = new Dictionary<StatType, Stat>();
}
