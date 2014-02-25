using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class Ability : IEnergy
{
    public string TextureName;
    public string DisplayName;
    public float minDistance = 1;
    public float maxDistance = 1;
    public bool IsFriendly;
    public int cost = 3;

    public delegate void AbilityCompletedHandler(Ability ability);

    public event AbilityCompletedHandler AbilityCompleted;

    public ScenePerformance performance;

    private List<SceneActionBase> _abilitySceneScript;

    public Ability(LilithAbilities id)
    {
        _abilitySceneScript = AbilitySceneProvider.GetBy(id);
        TextureName = Enum.GetName(typeof(LilithAbilities), id);
        switch (id)
        {
            case LilithAbilities.Attack:
                this.cost = 2;
                break;
            case LilithAbilities.Blizzard:
                this.cost = 1;
                break;
            case LilithAbilities.Fireball:
                this.cost = 4;
                break;
            case LilithAbilities.Heal:
                this.cost = 3;
                IsFriendly = true;
                break;
            case LilithAbilities.ChannelEmpower:
                this.cost = 3;
                IsFriendly = true;
                break;
            case LilithAbilities.HealGroup:
                this.cost = 5;
                IsFriendly = true;
                break;
            default:
                break;
        }
    }

    public float BetweenDistance
    {
        get
        {
            return maxDistance - minDistance;
        }
    }

    public void UseAbility(GameObject actor, GameObject target)
    {
        performance = SceneDirector.CreatePerformaneScript(_abilitySceneScript, new AbilitySceenTranslator(actor, target));

        performance.Completed += (storyboard) =>
        {
            if (AbilityCompleted != null)
                AbilityCompleted(this);

            performance = null;
        };

        performance.Perform();
    }

    public void Interupt()
    {
        performance.Interupt();
    }

    internal void Update()
    {
        if (performance != null)
            performance.Perform();
    }

    public class AbilitySceenTranslator : ISceneTranslator
    {
        GameObject _actor;
        GameObject _target;

        public AbilitySceenTranslator(GameObject actor, GameObject target)
        {
            _actor = actor;
            _target = target;
        }

        public GameObject GetActor(string actor)
        {
            switch (actor.ToLower())
            {
                case "caster":
                    return _actor;
                case "target":
                    return _target;
                default:
                    throw new NotSupportedException("Unknown Actor: " + actor);
            }
        }
    }

    public int GetCurrentEnergy()
    {
        return this.cost;
    }

    public int GetMaxEnergy()
    {
        return this.cost;
    }
}