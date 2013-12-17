using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Buff
{
    ScenePerformance Performance;

    public Buff(ScenePerformance performance) { Performance = performance; }

    public bool Finished
    {
        get
        {
            return Performance.IsComplete;
        }
    }

    public void Start() { }

    public void Update() { Performance.Perform(); }
    
    public void Finish()
    {
        Performance.Interupt();
    }
}