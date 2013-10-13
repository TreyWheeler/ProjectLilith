using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MasterAbilityList
{
    public static List<Ability> AbilityList = new List<Ability>();
	static MasterAbilityList()
    {
        AbilityList.Add(new Ability(){ DisplayName = "Slash1"});
        AbilityList.Add(new Ability(){ DisplayName = "Slash2"});
        AbilityList.Add(new Ability(){ DisplayName = "Slash3"});
        AbilityList.Add(new Ability(){ DisplayName = "Slash4"});
        AbilityList.Add(new Ability(){ DisplayName = "Slash5"});
    }
}
