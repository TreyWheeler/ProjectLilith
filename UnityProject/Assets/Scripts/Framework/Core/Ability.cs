using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Linq;

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
        Player player = new Player();
        player.Name = "Roy";
        player.Gold = 255;
        player.ID = 0;
        SQLPlayerRepository test = new SQLPlayerRepository();
        test.Insert(player);
        player = test.GetAll().FirstOrDefault();
        
        Application.LoadLevel("Scene2");
    }
}

