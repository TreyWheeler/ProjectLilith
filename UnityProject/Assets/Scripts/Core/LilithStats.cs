using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LilithStats
{
    Health = 1,
    MoveSpeed = 2,
    Strength = 3,
    Intelligence = 4,
    Energy = 5,
    EnergyPerSecond = 6
}

public static class LilithStatExtensions
{
    public static Stat<LilithStats> GetHealth(this IDictionary<LilithStats, Stat<LilithStats>> stats)
    {
        return stats[LilithStats.Health];
    }

    public static Stat<LilithStats> GetHealth(this Character character)
    {
        return character.Stats[LilithStats.Health];
    }

    public static Stat<LilithStats> GetEnergy(this IDictionary<LilithStats, Stat<LilithStats>> stats)
    {
        return stats[LilithStats.Energy];
    }

    public static Stat<LilithStats> GetEnergy(this Character character)
    {
        return character.Stats[LilithStats.Energy];
    }
}

public class LilithStatList : StatList<LilithStats> { }