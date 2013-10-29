using System;
using System.Collections.Generic;
using System.Text;

public interface IHaveStats<StatEnum>
{
    StatList<StatEnum> Stats
    {
        get;
    }
}

public static class IHaveStatsExtensions
{
    public static float Get<StatEnum>(this IHaveStats<StatEnum> statsHolder, StatEnum stat)
    {
        return statsHolder.Stats[stat].CurrentValue;   
    }
}