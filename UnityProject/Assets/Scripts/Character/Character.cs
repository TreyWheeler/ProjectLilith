using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Character : MonoBehaviour
{
    public Ability[] MyAbilities;
    public LilithStatList Stats = new LilithStatList();
    public float MoveSpeed = 3.3f;
    public Queue<IntendedAction> AbilityQue = new Queue<IntendedAction>();
    private IntendedAction _currentAction;
    private bool _isExecutingAction;
    
    // Use this for initialization
    void Start()
    {
        Stats.Add(LilithStats.Health, new Stat<LilithStats>(1000));
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
            var potentialLocation = transform.position + direction * MoveSpeed * Time.deltaTime;
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