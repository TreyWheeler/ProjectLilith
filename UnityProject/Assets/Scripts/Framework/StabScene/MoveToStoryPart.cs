using UnityEngine;
using System.Collections;
using System;

public class MoveToLocationStoryPart : StoryPart
{

    public GameObject WhoToMove;
    public Vector3 Location;
    public float Speed;

    protected override void TellStory()
    {
        Vector3 ourPosition = WhoToMove.transform.position;
        Vector3 direction = Vector3.Normalize(Location - ourPosition);

        WhoToMove.transform.LookAt(Location);
        var potentialLocation = WhoToMove.transform.position + direction * Speed * Time.deltaTime;

        // If distance to destination is shorter than a step
        if (Vector3.Distance(ourPosition, Location) < Vector3.Distance(ourPosition, potentialLocation))
        {
            WhoToMove.transform.position = Location;

            RaiseComplete();
        }
        else
        {
            WhoToMove.transform.position = potentialLocation;
        }
    }
}


public class MoveToGameObjectStoryPart : MoveToLocationStoryPart
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
}


public class MoveInRangeOfGameObjectStoryPart : MoveToGameObjectStoryPart
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

