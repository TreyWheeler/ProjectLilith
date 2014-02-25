using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using IoC;

public class Character : MonoBehaviour
{
    public Ability[] MyAbilities;// = new Ability[] { new Ability(LilithAbilities.Attack), new Ability(LilithAbilities.Blizzard), new Ability(LilithAbilities.Fireball), new Ability(LilithAbilities.Heal) };
    public LilithStatList Stats = new LilithStatList();
    public Queue<IntendedAction> AbilityQue = new Queue<IntendedAction>();
    private IntendedAction _currentAction;
    private bool _isExecutingAction;
    private AbilityRadial _radial;
    public Material Team2Mat;
    public CombatClass Class;
    public bool Team2;
    public float TimeSinceLastHit;
    public string TextureName;
    [Inject]
    public CharacterTracker Characters { get; set; }

    private List<Buff> _buffs = new List<Buff>();

    public bool IsAlive
    {
        get
        {
            return Stats.GetHealth().CurrentValue > 0;
        }
    }

    public IEnumerable<Character> MyTeam
    {
        get
        {
            return Characters.GetTeamCharactersFor(this);
        }
    }

    public IEnumerable<Character> Enemies
    {
        get
        {
            return Characters.GetEnemyCharactersFor(this);
        }
    }

    void Awake()
    {
        switch (Class)
        {
            case CombatClass.Wizard:
                Stats.Add(LilithStats.Health, new Stat<LilithStats>(1000));
                Stats.Add(LilithStats.Intelligence, new Stat<LilithStats>(75));
                Stats.Add(LilithStats.Strength, new Stat<LilithStats>(12));
                Stats.Add(LilithStats.MoveSpeed, new Stat<LilithStats>(3.3f));
                MyAbilities = new Ability[] { new Ability(LilithAbilities.Blizzard), new Ability(LilithAbilities.Fireball) };
                if (Team2)
                    TextureName = "enemy_mage";
                else
                    TextureName = "mage";
                break;
            case CombatClass.Melee:
                Stats.Add(LilithStats.Health, new Stat<LilithStats>(1000));
                Stats.Add(LilithStats.Intelligence, new Stat<LilithStats>(14));
                Stats.Add(LilithStats.Strength, new Stat<LilithStats>(0, 9001, 50));
                Stats.Add(LilithStats.MoveSpeed, new Stat<LilithStats>(3.3f));
                MyAbilities = new Ability[] { new Ability(LilithAbilities.Attack) };
                this.gameObject.animation.CrossFade("DrawBlade");
                var state = this.gameObject.animation.PlayQueued("Attack_standy", QueueMode.CompleteOthers);
                state.wrapMode = WrapMode.Loop;
                if (Team2)
                    TextureName = "enemy_warrior";
                else
                    TextureName = "warrior";
                break;
            case CombatClass.Support:
                Stats.Add(LilithStats.Health, new Stat<LilithStats>(1000));
                Stats.Add(LilithStats.Intelligence, new Stat<LilithStats>(100));
                Stats.Add(LilithStats.Strength, new Stat<LilithStats>(6));
                Stats.Add(LilithStats.MoveSpeed, new Stat<LilithStats>(3.3f));
                MyAbilities = new Ability[] { new Ability(LilithAbilities.Heal), new Ability(LilithAbilities.ChannelEmpower), new Ability(LilithAbilities.HealGroup) };
                if (Team2)
                    TextureName = "enemy_support";
                else
                    TextureName = "support";
                break;
            default:
                throw new NotImplementedException();
        }

        Stats.GetHealth().Changed += HealthChanged;
    }
    void Start()
    {
        this.Inject();
        this.Characters.AllCharacters.Add(this);//Well I am a character arent I?
        var vc = GameObject.Find("GameController").GetComponent<BattleScene>();

        if (Team2)
        {
            vc.Enemies.Add(this);
        }

        _radial = this.gameObject.EnsureComponent<AbilityRadial>();
        _radial.Abilities = MyAbilities;

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

    public void ApplyBuff(ScenePerformance performance)
    {
        Buff buff = new Buff(performance);
        _buffs.Add(buff);
        buff.Start();
    }

    void HealthChanged(Stat<LilithStats> healthStat, StatChangedArgs changedArgs)
    {
        if (IsAlive)
        {// Taking Damage
            if (changedArgs.Difference < 0 && TimeSinceLastHit > 2f)
            {
                var clip = Resources.Load("sounds/moomph04") as AudioClip;
                audio.PlayOneShot(clip);
            }
        }
        else
        {// Dead
            while (_buffs.Count > 0)
            {
                _buffs[0].Finish();
                _buffs.RemoveAt(0);
            }

            this.gameObject.animation.CrossFade("Death");
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            var clip = Resources.Load("Sounds/m die 03") as AudioClip;
            audio.PlayOneShot(clip);

            if (_currentAction != null)
            {
                _currentAction.Ability.Interupt();
            }
        }

        TimeSinceLastHit = 0f;
    }

    void Update()
    {
        TimeSinceLastHit += Time.deltaTime;

        if (!IsAlive)
            return;

        if (AbilityQue.Count > 0 && _currentAction == null)
        {
            _currentAction = AbilityQue.Dequeue();

            Character target = _currentAction.DestinationGameObject.GetComponent<Character>();
            if (!target.IsAlive)
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

        for (int i = 0; i < _buffs.Count; i++)
        {
            var buff = _buffs[i];

            // Before allows evaluate of finished buffs, that were finished from external influences (dispel)
            if (buff.Finished)
            {
                _buffs.Remove(buff);
                i--;
                continue;
            }

            buff.Update();
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