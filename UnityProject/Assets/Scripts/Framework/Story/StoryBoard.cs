using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScenePerformance
{
    public delegate void StoryBoardCompletedHandler(ScenePerformance board);

    private Queue<ScenePerformanceAction> _que = new Queue<ScenePerformanceAction>();

    public event StoryBoardCompletedHandler Completed;

    bool wasCompleted = false;

    public void Perform()
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

    public void Que(ScenePerformanceAction part)
    {
        _que.Enqueue(part);
        part.Link(this);
    }


    public void PartCompleted()
    {
        _que.Dequeue();
        Perform();// This prevents single frame story parts
    }
}
