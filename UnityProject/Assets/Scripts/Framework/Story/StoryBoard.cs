using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScenePerformance
{
    public delegate void StoryBoardCompletedHandler(ScenePerformance board);

    private List<ScenePerformanceActionBase> _que = new List<ScenePerformanceActionBase>();

    public event StoryBoardCompletedHandler Completed;

    bool wasCompleted = false;

    public ScenePerformanceActionBase GetBy(string name)
    {
        foreach (var item in _que)
        {
            if (item.Name == name)
                return item;
        }

        return null;
    }

    public void Perform()
    {
        if (wasCompleted)
            return;

        if (IsComplete)
        {
            RaiseCompleted();

            return;
        }

        for (int i = 0; i < _que.Count; i++)
        {
            var item = _que[i];

            if (!item.Started)
                item.Start();

            if (item.Finished)
                continue;
            else
                item.Update();

            if (item.BlocksStory)
                break;
        }
    }

    private void RaiseCompleted()
    {
        if (Completed != null)
            Completed(this);

        wasCompleted = true;
    }

    public bool IsComplete
    {
        get
        {
            for (int i = 0; i < _que.Count; i++)
            {
                var item = _que[i];

                if (item.Started && !item.BlocksStory)
                    continue;

                if (!item.Finished)
                    return false;
            }

            return true;
        }
    }

    public void Interupt()
    {
        if (wasCompleted)
            return;

        for (int i = 0; i < _que.Count; i++)
        {
            var item = _que[i];

            if (item.Started && !item.Finished)
            {
                item.Finish();
            }
        }

        RaiseCompleted();
    }

    public void Que(ScenePerformanceActionBase part)
    {
        _que.Add(part);
    }
}
