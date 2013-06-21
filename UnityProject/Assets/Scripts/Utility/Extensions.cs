using UnityEngine;
using System.Collections;

public static class Extensions {

    
    public static float Length(this Vector2 vector)
    {
        return Vector2.Distance(Vector2.zero, vector);   
    }
}
