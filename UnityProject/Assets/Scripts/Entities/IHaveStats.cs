using System;
using System.Collections.Generic;
using System.Text;

public interface IHaveStats
{
    Dictionary<StatType, Stat> Stats
    {
        get;
    }
}

public static class IHaveStatsExtensions
{
    public static float Get(this IHaveStats statsHolder, StatType stat)
    {
        return statsHolder.Stats[stat].CurrentValue;   
    }
}