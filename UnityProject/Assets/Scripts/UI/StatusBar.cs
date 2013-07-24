using UnityEngine;
using System.Collections;

public class StatusBar : StatusBarBase
{
    public Rect Dimensions;
    public static readonly int Width = 10;
    Texture2D texture = new Texture2D(1, 1);
    
    void Start()
    {
        texture.SetPixel(0, 0, Color.red);
        texture.Apply();
        
    }
 
    void OnGUI()
    {
        if(TrackedObject == null)
            return;
        
        GUI.skin.label.normal.background = texture;        
        float height = Dimensions.height * TrackedObject.Stats[StatType].CurrentRatio;
        
        if(height <= 0)
            return;
        
        GUI.Label(new Rect(Dimensions.x, Dimensions.y + (Dimensions.height - height), Dimensions.width, height), GUIContent.none);
    } 
    
}
