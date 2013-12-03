using UnityEngine;
using System.Collections;
using System;

public class MoveToLocationScenePerformanceAction : ScenePerformanceActionBase
{

    public GameObject WhoToMove;
    public Vector3 Location;
    public float Speed;

    public override void Update()
    {
        Vector3 ourPosition = WhoToMove.transform.position;
        Vector3 direction = Vector3.Normalize(Location - ourPosition);
        if (direction == Vector3.zero)
            Finish();

        WhoToMove.transform.LookAt(Location);
        var potentialLocation = WhoToMove.transform.position + direction * Speed * Time.deltaTime;

        var distanceToLocation = Vector3.Distance(ourPosition, Location);
        if (distanceToLocation == 0)
            Finish();
     
        else if (distanceToLocation < Vector3.Distance(ourPosition, potentialLocation))
        { // If distance to destination is shorter than a step
            WhoToMove.transform.position = Location;

            Finish();
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

    public override void Update()
    {
        Vector3 targetPosition = Target.transform.position;
        Vector3 ourPosition = WhoToMove.transform.position;
        Vector3 direction = Vector3.Normalize(targetPosition - ourPosition);

        Location = targetPosition - (direction * HowClose);

        base.Update();
    }

    public override void Finish()
    {
        WhoToMove.transform.LookAt(Target.transform.position);
        base.Finish();
    }
}


public class MoveInRangeOfGameObjectScenePerformanceAction : MoveToGameObjectScenePerformanceAction
{
    public float MinRange;
    public float MaxRange;

    public override void Update()
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
            Finish();
            return;
        }

        HowClose = distanceToClosestValidPosition < distanceToFurthestValidPosition ? MinRange : MaxRange;

        base.Update();
    }
}

