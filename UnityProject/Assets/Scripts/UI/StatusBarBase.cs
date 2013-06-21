using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class StatusBarBase : MonoBehaviour
{
    private StatType _StatType;

    public StatType StatType
    {
        get
        {
            return _StatType;
        }

        set
        {
            _StatType = value;
        }
    }

    private IHaveStats _TrackedObject;

    public IHaveStats TrackedObject
    {
        get
        {
            return _TrackedObject;
        }

        set
        {
            _TrackedObject = value;
        }
    }
}
