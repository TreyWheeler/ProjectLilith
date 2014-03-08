using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IoC;

public class BattleScene : MonoBehaviour
{
    public List<Character> Allies = new List<Character>();
    public List<Character> Enemies = new List<Character>();
    public VictoryCondition victoryCondition;
    private bool _isMatchOver;
    public bool IsMatchOver
    {
        get
        {
            return _isMatchOver;
        }
    }
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    void Update()
    {
        if (victoryCondition != null && victoryCondition.IsMatchOver)
            _isMatchOver = true;
    }
}
