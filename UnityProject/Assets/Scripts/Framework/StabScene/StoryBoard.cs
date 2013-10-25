using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryBoard 
{ 
    private Queue<StoryPart> _que = new Queue<StoryPart>();

    public void Update()
    {
        if (_que.Count == 0)
            return;
        
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
