using UnityEngine;
using System.Collections;

public abstract class ScenePerformanceActionBase
{
    // Completed
    // Attatched w/No Update
    // Needs Update

    public bool BlocksStory;

    public bool Started;
    public bool Finished;

    public virtual void Start() { Started = true; }

    public virtual void Update() {}

    public virtual void Finish()
    {
        Finished = true;
    }
}
