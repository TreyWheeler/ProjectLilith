using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Linq;

public class Ability
{
    public string DisplayName;
    public float minDistance = 1;
    public float maxDistance = 1;

    public delegate void AbilityCompletedHandler(Ability ability);

    public event AbilityCompletedHandler AbilityCompleted;

    public StoryBoard board;

    public float BetweenDistance
    {
        get
        {
            return maxDistance - minDistance;
        }
    }

    public void UseAbility(GameObject actor, GameObject target)
    {
        board = new StoryBoard();

        RunAnimationOnceStoryPart drawSwordAnim = new RunAnimationOnceStoryPart();
        drawSwordAnim.Actor = actor;
        drawSwordAnim.Animation = "DrawBlade";
        board.Que(drawSwordAnim);

        RunAnimationStoryPart anim = new RunAnimationStoryPart();
        anim.Actor = actor;
        anim.Animation = "Run";
        board.Que(anim);

        MoveToGameObjectStoryPart movePart = new MoveToGameObjectStoryPart();
        movePart.Target = target;
        movePart.Speed = target.GetComponent<Character>().MoveSpeed;
        movePart.HowClose = 1f;
        movePart.WhoToMove = actor;
        board.Que(movePart);

        RunAnimationOnceStoryPart attackAnim = new RunAnimationOnceStoryPart();
        attackAnim.Actor = actor;
        attackAnim.Animation = "Attack";
        board.Que(attackAnim);

        RunAnimationStoryPart idleAnim = new RunAnimationStoryPart();
        idleAnim.Actor = actor;
        idleAnim.Animation = "Idle";
        board.Que(idleAnim);

        board.Completed += (storyboard) =>
        {
            if (AbilityCompleted != null)
                AbilityCompleted(this);

            board = null;
        };

        board.Update();
    }

    internal void Update()
    {
        if (board != null)
            board.Update();
    }
}