using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Character : MonoBehaviour, IHaveAbilities
{

    public Abilities[] MyAbilities;
    public StatList Stats = new StatList();
    float _characterMoveSpeed = 3.3f;
    private Queue<IntendedAction> _abilityQue = new Queue<IntendedAction>();
    public Queue<IntendedAction> AbilityQue
    {
        get
        {
            return _abilityQue;
        }
    }
    private IntendedAction _currentAction;
    private bool _isExecutingAction;
    // Vector3 _positionBeforeMove;
    // float _lerpTime;
    //bool _moving = false;
    //Action _storedAbility;
    //public bool Moving
    //{
    //    get
    //    {
    //        return _moving;
    //    }
    //    private set
    //    {
    //        _moving = value;
    //        if (!_moving)
    //            _positionBeforeMove = transform.position;
    //    }
    //}

    // Use this for initialization
    void Start()
    {
        Stats.Add(StatType.Health, new Stat(1000));
        //_positionBeforeMove = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (AbilityQue.Count > 0 && _currentAction == null)
        {
            _currentAction = AbilityQue.Dequeue();
        }
        if (_currentAction != null)
        {
            RunCurrentAction();
        }

    }

    private void RunCurrentAction()
    {
        GameObject targetGO = _currentAction.DestinationGameObject;
        Ability ability = _currentAction.Ability;
        Vector3 targetPosition = targetGO.transform.position;
        Vector3 ourPosition = transform.position;
        Vector3 direction = Vector3.Normalize(targetPosition - ourPosition);
        Vector3 closestValidPosition = targetPosition - (direction * ability.minDistance);
        var distanceToClosestValidPosition = Vector3.Distance(ourPosition, closestValidPosition);
        Vector3 furthestValidPosition = targetPosition - (direction * ability.maxDistance);
        var distanceToFurthestValidPosition = Vector3.Distance(ourPosition, furthestValidPosition);
        if (Mathf.Approximately(distanceToFurthestValidPosition + distanceToClosestValidPosition, ability.BetweenDistance))
        {
            UseAbility(ability);
        }
        else
        {
            Vector3 positionToMoveTo = distanceToClosestValidPosition < distanceToFurthestValidPosition ? closestValidPosition : furthestValidPosition;
            animation.CrossFade("Run");
            transform.LookAt(targetPosition);
            var potentialLocation = transform.position + direction * _characterMoveSpeed * Time.deltaTime;
            if (Vector3.Distance(ourPosition, positionToMoveTo) < Vector3.Distance(ourPosition, potentialLocation))
                transform.position = positionToMoveTo;
            else
                transform.position = potentialLocation;
        }

    }

    private void UseAbility(Ability ability)
    {
        if (!_isExecutingAction)
        {
            _isExecutingAction = true;
            animation.CrossFade(ability.animationName);
        }
        if (!animation.IsPlaying(ability.animationName))
        {
            _currentAction = null;
            _isExecutingAction = false;
        }
    }

    private void MoveToTargetLocation(Vector3 positionToMoveTo)
    {

    }

    //if (Moving)
    //{
    //    if (!animation.IsPlaying("DrawBlade"))
    //    {
    //        _lerpTime += Time.deltaTime / _durationOfReturning;
    //        animation.CrossFade("Run");
    //        gameObject.transform.position = Vector3.Lerp(_positionBeforeMove, endingPosition, _lerpTime);

    //        if (_lerpTime > 1f)
    //        {

    //            Debug.Log("moving was set to false");
    //            Moving = false;

    //            _storedAbility();
    //            enemyGameObject.animation.CrossFade("Attack02");
    //            animation.CrossFade("Idle");
    //            //abilityGameObject.animation["Gentleman"].wrapMode = WrapMode.Loop;
    //            //abilityGameObject.animation.PlayQueued("Gentleman");
    //        }
    //    }
    //}
    //float _durationOfReturning;
    //GameObject enemyGameObject;
    //Vector3 endingPosition;
    //public void UseAbility(AbilitySphere ability, GameObject enemy)
    //{
    //    if (!Moving)
    //    {
    //        _lerpTime = 0;
    //        _storedAbility = ability.Run;
    //        enemyGameObject = enemy;

    //        animation.CrossFade("DrawBlade");
    //        Vector3 enemyPosition = enemy.transform.position;
    //        
    //        _durationOfReturning = distance / _characterMoveSpeed;
    //        Moving = true;
    //        gameObject.transform.LookAt(enemy.transform);
    //    }
    //}
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