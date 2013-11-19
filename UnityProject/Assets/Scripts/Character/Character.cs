using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Character : MonoBehaviour
{
    public Ability[] MyAbilities = new Ability[] { new Ability(LilithAbilities.Attack), new Ability(LilithAbilities.Victory) };
    public LilithStatList Stats = new LilithStatList();
    public Queue<IntendedAction> AbilityQue = new Queue<IntendedAction>();
    private IntendedAction _currentAction;
    private bool _isExecutingAction;
    private AbilityRadial _radial;
    private Texture Arch;

    public bool IsAlive
    {
        get
        {
            return Stats.GetHealth().CurrentValue > 0;
        }
    }



    void Start()
    {
        _radial = this.gameObject.EnsureComponent<AbilityRadial>();

        _radial.Abilities = MyAbilities;

        Arch = Resources.Load("Images/CrescentArch") as Texture;

        Stats.Add(LilithStats.Health, new Stat<LilithStats>(1000));
        Stats.Add(LilithStats.MoveSpeed, new Stat<LilithStats>(3.3f));
        Stats.Add(LilithStats.Strength, new Stat<LilithStats>(50));

        Stats.GetHealth().Changed += HealthChanged;

        this.gameObject.GetComponentInChildren<HealthArch>().Stat = Stats.GetHealth();
    }

    void HealthChanged(Stat<LilithStats> healthStat, StatChangedArgs changedArgs)
    {
        // TODO: Floating Combat Text
        // TODO: DIE

        if (!IsAlive)
        {
            this.gameObject.animation.CrossFade("Death");
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    void Update()
    {
        if (!IsAlive)
            return;

        if (AbilityQue.Count > 0 && _currentAction == null)
        {
            _currentAction = AbilityQue.Dequeue();
            _currentAction.Ability.UseAbility(this.gameObject, _currentAction.DestinationGameObject);
            _currentAction.Ability.AbilityCompleted += (ability) =>
            {
                _currentAction = null;
            };
            _radial.Close();
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