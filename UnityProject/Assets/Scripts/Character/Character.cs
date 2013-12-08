using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Character : MonoBehaviour
{
    public Ability[] MyAbilities = new Ability[] { new Ability(LilithAbilities.Attack), new Ability(LilithAbilities.Blizzard), new Ability(LilithAbilities.Fireball) };
    public LilithStatList Stats = new LilithStatList();
    public Queue<IntendedAction> AbilityQue = new Queue<IntendedAction>();
    private IntendedAction _currentAction;
    private bool _isExecutingAction;
    private AbilityRadial _radial;
    private Texture Arch;
    public Material Team2Mat;
    public CombatClass Class;
    public bool Team2;
    public float timeSinceLastHit;

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


        if (Class == CombatClass.Melee)
        {
            this.gameObject.animation.CrossFade("DrawBlade");
            var state = this.gameObject.animation.PlayQueued("Attack_standy", QueueMode.CompleteOthers);
            state.wrapMode = WrapMode.Loop;
        }

        foreach (var childElement in this.gameObject.GetChildren())
        {
            if (Team2)
            {
                if (childElement.name == "uper_cloth" || childElement.name == "body_band")
                {
                    var render = childElement.GetComponent<SkinnedMeshRenderer>();
                    render.material = Team2Mat;
                    continue;
                }
            }

            if (childElement.name == "firehead" && Class != CombatClass.Wizard)
            {
                Destroy(childElement);
            }
            else if (childElement.name == "headusOBJexport009" && Class != CombatClass.Melee)
            {
                Destroy(childElement);
            }
        }

        this.gameObject.GetComponentInChildren<HealthArch>().Stat = Stats.GetHealth();
    }

    void HealthChanged(Stat<LilithStats> healthStat, StatChangedArgs changedArgs)
    {
        if (IsAlive)
        {// Taking Damage
            if (timeSinceLastHit > 2f)
            {
                var clip = Resources.Load("sounds/moomph04") as AudioClip;
                audio.PlayOneShot(clip);
            }
        }
        else
        {// Dead
            this.gameObject.animation.CrossFade("Death");
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            var clip = Resources.Load("Sounds/m die 03") as AudioClip;
            audio.PlayOneShot(clip);

            if(_currentAction != null)
            {
                _currentAction.Ability.Interupt();
            }
        }

        timeSinceLastHit = 0f;
    }

    void Update()
    {
        timeSinceLastHit += Time.deltaTime;

        if (!IsAlive)
            return;

        if (AbilityQue.Count > 0 && _currentAction == null)
        {            
            _currentAction = AbilityQue.Dequeue();

            Character target = _currentAction.DestinationGameObject.GetComponent<Character>();
            if(!target.IsAlive)
            {
                _currentAction = null;
                return;
            }

            _currentAction.Ability.AbilityCompleted += (ability) =>
            {
                _currentAction = null;
            };
            _currentAction.Ability.UseAbility(this.gameObject, _currentAction.DestinationGameObject);
        }
        if (_currentAction != null)
        {
            _currentAction.Ability.Update();
        }
    }



    internal void QueueAbility(IntendedAction intendedAction)
    {
        this.AbilityQue.Enqueue(intendedAction);
        _radial.Close();
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

public enum CombatClass
{
    Wizard, Melee, Support
}