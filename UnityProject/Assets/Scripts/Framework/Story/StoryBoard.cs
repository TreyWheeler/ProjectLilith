using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryBoard
{
    public delegate void StoryBoardCompletedHandler(StoryBoard board);

    private Queue<StoryPart> _que = new Queue<StoryPart>();

    public event StoryBoardCompletedHandler Completed;

    bool wasCompleted = false;

    public void Update()
    {
        if(wasCompleted)
            return;

        if (_que.Count == 0)
        {
            if (Completed != null)
                Completed(this);

            wasCompleted = true;

            return;
        }

        _que.Peek().Update();

    }

    public void Que(StoryPart part)
    {
        _que.Enqueue(part);
        part.LinkStoryBoard(this);
    }


    public void PartCompleted()
    {
        _que.Dequeue();
    }
}
