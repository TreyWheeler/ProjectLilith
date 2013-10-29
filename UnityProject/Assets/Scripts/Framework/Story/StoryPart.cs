using UnityEngine;
using System.Collections;

public abstract class StoryPart
{
    private StoryBoard _board;

    internal void LinkStoryBoard(StoryBoard board)
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
