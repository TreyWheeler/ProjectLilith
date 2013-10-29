using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Linq;

public class Ability
{
    public string DisplayName;
    public float minDistance = 1;
    public float maxDistance = 1;
    public string animationName = "Attack";
   
    public float BetweenDistance
    {
        get
        {
            return maxDistance - minDistance;
        }
    }
}

public enum Abilities
{
    heal,
    bahamut,
    scratchTrey
}
