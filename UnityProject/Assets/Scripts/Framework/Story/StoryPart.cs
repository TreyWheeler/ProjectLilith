using UnityEngine;
using System.Collections;

public abstract class ScenePerformanceAction
{
    private ScenePerformance _board;

    internal void Link(ScenePerformance board)
    {
        _board = board;
    }

    public void Update()
    {
        TellStory();
    }

    protected abstract void TellStory();

    public void RaiseComplete()
    {
        _board.PartCompleted();
    }
}
