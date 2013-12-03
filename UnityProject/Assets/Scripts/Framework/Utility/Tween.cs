using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Goal Syntax:
//---------------
//Vector2Tween MissileTween = new Vector2Tween();
//MissileTween.From = new Vector2(400, 10);
//MissileTween.To = new Vector2(100, 5);
//MissileTween.Duration = 1000;
//MissileTween.CurrentValueChanged += (Vector2Tween sender, Vector2 currentValue) =>
//{
//    Missile.Location = currentValue;
//};
//Tasks.Add(MissileTween);

public delegate void ValueChangedEventHandler<T> (ValueChangedArgs<T> e);

public class ValueChangedArgs<T>
{
    public T OldValue;
    public T NewValue;
    public T Difference;
}

public class Vector2Tween : Tween<Vector2>
{
    protected override Vector2 GetPortionOfDifference(float step)
    {
        return Difference * step;
    }

    protected override Vector2 Add(Vector2 value1, Vector2 value2)
    {
        return value1 + value2;
    }

    protected override Vector2 Subtract(Vector2 value1, Vector2 value2)
    {
        return value1 - value2;
    }

    protected override bool IsPassedTarget(Vector2 step)
    {
        Vector2 DistanceToEnd = Subtract(To, CurrentValue);

        return Lowest(step, DistanceToEnd).Equals(DistanceToEnd);
    }

    private Vector2 Lowest(Vector2 value1, Vector2 value2)
    {// pretty sure this is wrong. May need to take into acount TO
        if(value1.Length() < value2.Length())
        {
            return value1;
        }
        else
        {
            return value2;
        }
    }
    
    public override bool IsEqual (Vector2 item1, Vector2 item2)
    {
        return item1.Equals(item2);
    }
}

public class IntTween : Tween<int>
{
    float CumulativePortion;

    protected override int Add(int value1, int value2)
    {
        return value1 + value2;
    }

    protected override int Subtract(int value1, int value2)
    {
        return value1 - value2;
    }

    protected override int GetPortionOfDifference(float step)
    {
        CumulativePortion += (float)Difference * step;
        int ReturnValue = (int)CumulativePortion;

        if(CumulativePortion >= 1 || CumulativePortion <= -1)
            CumulativePortion = 0;

        return ReturnValue;
    }

    protected override bool IsPassedTarget(int step)
    {
        if(Difference > 0)
        {
            return CurrentValue + step > To;
        }
        else
        {
            return CurrentValue + step < To;
        }
    }
}

public class FloatTween : Tween<float>
{
    protected override float Add(float value1, float value2)
    {
        return value1 + value2;
    }

    protected override float Subtract(float value1, float value2)
    {
        return value1 - value2;
    }

    protected override float GetPortionOfDifference(float step)
    {
        return Difference * step;
    }

    protected override bool IsPassedTarget(float step)
    {
        if(Difference > 0)
        {
            return CurrentValue + step > To;
        }
        else
        {
            return CurrentValue + step < To;
        }
    }
}

public abstract class Tween<T> : ITimedTask
{
    public event Action Completed;
    public event ValueChangedEventHandler<T> CurrentValueChanged;

    private float _duration;

    public float Duration
    {
        get
        {
            return _duration;
        }
        set
        {
            if(value < 0)
                throw new IndexOutOfRangeException("Duration cannot be less than 0");

            _duration = value;
        }
    }

    public T To;
    public T From;
    private T _difference;
 
    public virtual bool IsEqual(T item1, T item2)
    {
        if(item1 is IEquatable<T>)
        {
            ((IEquatable<T>)item1).Equals((IEquatable<T>)item2);   
        }
        
        return item1.Equals(item2);   
    }
    
    public T Difference
    {
        get
        {
            if(_difference.Equals(default(T)))
            {
                _difference = Subtract(To, From);
            }

            return _difference;
        }
    }

    private T _currentValue;

    public T CurrentValue
    {
        get
        {
            if(_currentValue.Equals(default(T)))
                _currentValue = From;

            return _currentValue;
        }

        protected set
        {
            if(CurrentValueChanged != null)
            {
                ValueChangedArgs<T> Args = new ValueChangedArgs<T>();

                Args.OldValue = _currentValue;
                Args.NewValue = value;
                Args.Difference = Subtract(_currentValue, value);

                _currentValue = value;

                CurrentValueChanged(Args);
            }
            else
            {
                _currentValue = value;
            }

            if(_currentValue.Equals(To) && Completed != null)
            {
                Completed();
            }
        }
    }

    protected abstract T Add(T value1, T value2);

    protected abstract T Subtract(T value1, T value2);

    protected abstract T GetPortionOfDifference(float step);

    protected abstract bool IsPassedTarget(T step);

    protected T GetStep()
    {
        return GetPortionOfDifference((float)Helper.DeltaTimeInMilliseconds / Duration);
    }

    public void Update()
    {
        T step = GetStep();

        if(IsPassedTarget(step))
        {
            CurrentValue = To;
        }
        else
        {
            CurrentValue = Add(CurrentValue, step);
        }
    }
}
