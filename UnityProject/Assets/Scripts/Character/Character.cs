using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Character : MonoBehaviour, IHaveAbilities
{

    public Abilities[] MyAbilities;
    public StatList Stats = new StatList();
    float _characterMoveSpeed = 3.3f;
    float _lerpTime;
    bool _moving = false;
    Action _storedAbility;
    Vector3 _positionBeforeMove;

    public bool Moving
    {
        get
        {
            return _moving;
        }
        private set
        {
            _moving = value;
            if (!_moving)
                _positionBeforeMove = transform.position;
        }
    }

    // Use this for initialization
    void Start()
    {
        Stats.Add(StatType.Health, new Stat(1000));
        _positionBeforeMove = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
            if (!animation.IsPlaying("DrawBlade"))
            {
                _lerpTime += Time.deltaTime / _durationOfReturning;
                animation.CrossFade("Run");
                gameObject.transform.position = Vector3.Lerp(_positionBeforeMove, endingPosition, _lerpTime);

                if (_lerpTime > 1f)
                {

                    Debug.Log("moving was set to false");
                    Moving = false;

                    _storedAbility();
                    enemyGameObject.animation.CrossFade("Attack02");
                    animation.CrossFade("Idle");
                    //abilityGameObject.animation["Gentleman"].wrapMode = WrapMode.Loop;
                    //abilityGameObject.animation.PlayQueued("Gentleman");
                }
            }
        }
    }
     
    float _durationOfReturning;
    GameObject enemyGameObject;
    Vector3 endingPosition;
    public void UseAbility(AbilitySphere ability, GameObject enemy)
    {
        if (!Moving)
        {
            _lerpTime = 0;
            _storedAbility = ability.Run;
            enemyGameObject = enemy;

            animation.CrossFade("DrawBlade");
            Vector3 enemyPosition = enemy.transform.position;
            Vector3 direction = Vector3.Normalize(enemyPosition - _positionBeforeMove);
            endingPosition = enemyPosition - (direction * ability.Range);
            var distance = Vector3.Distance(_positionBeforeMove, endingPosition);
            _durationOfReturning = distance / _characterMoveSpeed;
            Moving = true;
            gameObject.transform.LookAt(enemy.transform);
        }
    }

    public void RunAnimation()
    {
        gameObject.animation.CrossFade("Walk");
    }
    public void MoveTo(Vector3 endLocation)
    {
        this.transform.position = endLocation;
    }
    public void MoveTo(Character characterLocation)
    {
        MoveTo(characterLocation.transform.position);
    }
    public void UseAbility()
    {
        gameObject.animation.CrossFade("Walk");
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 70, 40), "Idle"))
        {
            RunAnimation();
        }
    }


}

public enum Abilities
{
    heal,
    bahamut,
    scratchTrey
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