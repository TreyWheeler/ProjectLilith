﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LilithStats
{
    Health = 1,
    MoveSpeed = 2,
    Strength = 3,
    Intelligence = 4
}

public static class LilithStatExtensions
{
    public static Stat<LilithStats> GetHealth(this IDictionary<LilithStats, Stat<LilithStats>> stats)
    {
        return stats[LilithStats.Health];
    }
}

public class LilithStatList : StatList<LilithStats> { }