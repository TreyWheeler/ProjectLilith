using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Character : MonoBehaviour
{
    public Ability[] MyAbilities = new Ability[] { };
    public LilithStatList Stats = new LilithStatList();
    public float MoveSpeed = 3.3f;
    public Queue<IntendedAction> AbilityQue = new Queue<IntendedAction>();
    private IntendedAction _currentAction;
    private bool _isExecutingAction;

    void Start()
    {
        AbilityRadial radial = this.gameObject.EnsureComponent<AbilityRadial>();

        radial.Abilities = MyAbilities;

        Stats.Add(LilithStats.Health, new Stat<LilithStats>(1000));
    }

    void Update()
    {
        if (AbilityQue.Count > 0 && _currentAction == null)
        {
            _currentAction = AbilityQue.Dequeue();
            _currentAction.Ability.UseAbility(this.gameObject, _currentAction.DestinationGameObject);
            _currentAction.Ability.AbilityCompleted += (ability) =>
            {
                _currentAction = null;
            };
        }
        if (_currentAction != null)
        {
            _currentAction.Ability.Update();
        }
    }
}


public enum TargetType
{
    None,
    Self,
    FriendlySingle,
    FriendlyAll,
    EnemySingle,
    EnemyAll,
    All
}