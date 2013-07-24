using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class Ability
{    
    private string _displayName;
    public string DisplayName
    {
        get { return _displayName; }
        set { _displayName = value; }
    }
    
    
    
    
    
    public void Do(CombatEntity actor, CombatEntity target)
    {
        target.Stats[StatType.Health].CurrentValue -= 10;
        
        
        new LocalDatabaseVersionManager().EnsureUpToDate();
    }
}

