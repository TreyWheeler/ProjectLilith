using UnityEngine;
using System.Collections;
using System;

public static class Helper
{

    public static int DeltaTimeInMilliseconds { get { return Convert.ToInt32(Time.deltaTime * 1000); } }
}
