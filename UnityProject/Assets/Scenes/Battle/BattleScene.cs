using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IoC;

public class BattleScene : MonoBehaviour
{
    public List<Character> Allies = new List<Character>();
    public List<Character> Enemies = new List<Character>();
    public VictoryCondition victoryCondition;
    public bool IsMatchOver
    {
        get
        {
            return victoryCondition.IsMatchOver;
        }
    }
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    void Update()
    {
        if (IsMatchOver)
        {
            foreach (Character character in Allies)
            {
                character.Stats[LilithStats.EnergyPerSecond].CurrentValue = 0;
            }
        }
    }
}
