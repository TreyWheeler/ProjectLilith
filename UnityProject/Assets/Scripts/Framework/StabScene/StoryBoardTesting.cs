using UnityEngine;
using System.Collections;

public class StoryBoardTesting : MonoBehaviour
{
    public GameObject Target;

    StoryBoard board = new StoryBoard();

    // Use this for initialization
    void Start()
    {
        RunAnimationOnceStoryPart drawSwordAnim = new RunAnimationOnceStoryPart();
        drawSwordAnim.Actor = gameObject;
        drawSwordAnim.Animation = "DrawBlade";
        board.Que(drawSwordAnim);

        RunAnimationStoryPart anim = new RunAnimationStoryPart();
        anim.Actor = gameObject;
        anim.Animation = "Run";
        board.Que(anim);
        
        MoveToGameObjectStoryPart movePart = new MoveToGameObjectStoryPart();
        movePart.Target = Target;
        movePart.Speed = 3f;
        movePart.HowClose = 1f;
        movePart.WhoToMove = gameObject;
        board.Que(movePart);

        RunAnimationOnceStoryPart attackAnim = new RunAnimationOnceStoryPart();
        attackAnim.Actor = gameObject;
        attackAnim.Animation = "Attack";
        board.Que(attackAnim);

        RunAnimationStoryPart idleAnim = new RunAnimationStoryPart();
        idleAnim.Actor = gameObject;
        idleAnim.Animation = "Idle";
        board.Que(idleAnim);

    }

    // Update is called once per frame
    void Update()
    {
        board.Update();
    }
}
