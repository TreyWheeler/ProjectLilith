using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Conditional
{
    public Func<bool> Condition;
    public Action Result;
    public bool RemoveOnComplete = true;
}

public class TimedTaskManager : MonoBehaviour
{
    public static TimedTaskManager Instance
    {
        get
        {
            return Helper.EnsureGameObject("TimeManager").EnsureComponent<TimedTaskManager>();
        }
    }
    private List<ITimedTask> Tasks = new List<ITimedTask>();
    private List<Conditional> _conditionals;

    public List<Conditional> Conditionals
    {
        get
        {
            if(_conditionals == null)
            {
                _conditionals = new List<Conditional>();
            }

            return _conditionals;
        }
    }

    public void Update()
    {
        var tasks = Tasks.ToList();
        for(int index = 0; index < tasks.Count; index++)
        {
            ITimedTask TaskToUpdate = tasks[index];

            Action Completed = () =>
            {
                index--;
                tasks.Remove(TaskToUpdate);
            };

            TaskToUpdate.Completed += Completed;
            TaskToUpdate.Update();
            TaskToUpdate.Completed -= Completed;
        }

        if(_conditionals != null)
        {
            for(int i = 0; i < _conditionals.Count; i++)
            {
                if(_conditionals[i].Condition())
                {
                    _conditionals[i].Result();

                    if(_conditionals[i].RemoveOnComplete)
                    {
                        _conditionals.Remove(_conditionals[i]);
                        i--;
                    }
                }
            }
        }
    }

    public void Add(double Milliseconds, Action Function)
    {
        this.Add(new TimedEvent(Milliseconds, Function));
    }

    public void Add(double Milliseconds, int Iterations, Action Function)
    {
        this.Add(new IterativeTimedEvent(Milliseconds, Iterations, Function));
    }

    public void Add(ITimedTask TimedTask)
    {
        TimedTask.Completed += () =>
        {
            Tasks.Remove(TimedTask);
        };

        Tasks.Add(TimedTask);
    }
}

public interface ITimedTask
{
    event Action Completed;

    void Update();
}

public class TimedEvent : ITimedTask
{
    public event Action Completed;

    private double TimeBeforeEvent;
    private Action Event;
    private double ElapsedTime;

    public TimedEvent(double timeBeforeEvent, Action ev)
    {
        TimeBeforeEvent = timeBeforeEvent;
        Event = ev;
    }

    public void Update()
    {
        ElapsedTime += Helper.DeltaTimeInMilliseconds;

        if(ElapsedTime >= TimeBeforeEvent)
        {
            Event();
            Completed();
        }
    }
}

public class IterativeTimedEvent : ITimedTask
{
    public event Action Completed;

    private double TimeBeforeEvent;
    private Action Event;
    private double ElapsedTime;
    private int IterationsRemaining = 1;

    public IterativeTimedEvent(double timeBeforeEvent, int iterations, Action ev)
    {
        TimeBeforeEvent = timeBeforeEvent;
        Event = ev;
        IterationsRemaining = iterations;
    }

    public void Update()
    {
        ElapsedTime += Helper.DeltaTimeInMilliseconds;

        if(ElapsedTime >= TimeBeforeEvent)
        {
            Event();
            ElapsedTime = 0;
            IterationsRemaining--;

            if(IterationsRemaining == 0)
            {
                Completed();
            }
        }
    }
}
