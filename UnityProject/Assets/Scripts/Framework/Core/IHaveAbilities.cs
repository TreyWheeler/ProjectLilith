
using UnityEngine;
using System.Collections.Generic;
public interface IHaveAbilities
{
    //void UseAbility(AbilitySphere ability, GameObject enemy);

    Queue<IntendedAction> AbilityQue { get; }
}
