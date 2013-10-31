using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Linq;

public class Ability
{
    LilithAbilities ID = LilithAbilities.Attack;

    public string DisplayName;
    public float minDistance = 1;
    public float maxDistance = 1;

    public delegate void AbilityCompletedHandler(Ability ability);

    public event AbilityCompletedHandler AbilityCompleted;

    public ScenePerformance performance;

    private SceneScript _abilitySceneScript;

    public Ability(LilithAbilities id)
    {
        _abilitySceneScript = AbilitySceneProvider.GetBy(id);
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
            switch(actor.ToLower())
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
}