using UnityEngine;
using System.Collections;
using System;

public class MoveToLocationScenePerformanceAction : ScenePerformanceActionBase
{

    public GameObject WhoToMove;
    public Vector3 Location;
    public float Speed;

    protected override void TellStory()
    {
        Vector3 ourPosition = WhoToMove.transform.position;
        Vector3 direction = Vector3.Normalize(Location - ourPosition);
        if (direction == Vector3.zero)
            RaiseComplete();

        WhoToMove.transform.LookAt(Location);
        var potentialLocation = WhoToMove.transform.position + direction * Speed * Time.deltaTime;

        var distanceToLocation = Vector3.Distance(ourPosition, Location);
        if (distanceToLocation == 0)
            RaiseComplete();
     
        else if (distanceToLocation < Vector3.Distance(ourPosition, potentialLocation))
        { // If distance to destination is shorter than a step
            WhoToMove.transform.position = Location;

            RaiseComplete();
        }
        else
        {
            WhoToMove.transform.position = potentialLocation;
        }
    }
}


public class MoveToGameObjectScenePerformanceAction : MoveToLocationScenePerformanceAction
{
    public float HowClose;
    public GameObject Target;

    protected override void TellStory()
    {
        Vector3 targetPosition = Target.transform.position;
        Vector3 ourPosition = WhoToMove.transform.position;
        Vector3 direction = Vector3.Normalize(targetPosition - ourPosition);

        Location = targetPosition - (direction * HowClose);

        base.TellStory();
    }

    public override void RaiseComplete()
    {
        WhoToMove.transform.LookAt(Target.transform.position);
        base.RaiseComplete();
    }
}


public class MoveInRangeOfGameObjectScenePerformanceAction : MoveToGameObjectScenePerformanceAction
{
    public float MinRange;
    public float MaxRange;

    protected override void TellStory()
    {
        Vector3 targetPosition = Target.transform.position;
        Vector3 ourPosition = WhoToMove.transform.position;
        Vector3 direction = Vector3.Normalize(targetPosition - ourPosition);

        Vector3 closestValidPosition = targetPosition - (direction * MinRange);
        var distanceToClosestValidPosition = Vector3.Distance(ourPosition, closestValidPosition);
        Vector3 furthestValidPosition = targetPosition - (direction * MaxRange);
        var distanceToFurthestValidPosition = Vector3.Distance(ourPosition, furthestValidPosition);

        if (Mathf.Approximately(distanceToFurthestValidPosition + distanceToClosestValidPosition, MaxRange - MinRange))
        {
            RaiseComplete();
            return;
        }

        HowClose = distanceToClosestValidPosition < distanceToFurthestValidPosition ? MinRange : MaxRange;

        base.TellStory();
    }
}

