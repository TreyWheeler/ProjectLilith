using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IoC;

public class BattleScene : MonoBehaviour
{
    public List<Character> Allies = new List<Character>();
    public List<Character> Enemies = new List<Character>();

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }
}
