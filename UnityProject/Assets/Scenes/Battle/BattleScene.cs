using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleScene : MonoBehaviour
{

    public List<Character> Allies = new List<Character>();
    public List<Character> Enemies = new List<Character>();
    public PositionButtons _positionButtons;

    // Use this for initialization
    void Start()
    {
        if (_positionButtons != null)
        {
            List<ButtonDetails> details = new List<ButtonDetails>();
            foreach (var ally in Allies)
            {
                ButtonDetails newDetail = new ButtonDetails();
                newDetail.textureName = ally.textureName;
                details.Add(newDetail);
            }
            _positionButtons.Add(details);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
