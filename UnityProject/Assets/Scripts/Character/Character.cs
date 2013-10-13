using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

    public Abilities[] MyAbilities;
    public StatList Stats = new StatList();

    // Use this for initialization
    void Start()
    {
        Stats.Add(StatType.Health, new Stat(1000));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RunAnimation()
    {
        gameObject.animation.CrossFade("Walk");
    }
    public void MoveTo(Vector3 endLocation)
    {
        this.transform.position = endLocation;
    }
    public void MoveTo(Character characterLocation)
    {
        MoveTo(characterLocation.transform.position);
    }
    public void UseAbility()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 70, 40), "Idle"))
        {
            RunAnimation();
        }
    }
}

public enum Abilities
{
    heal,
    bahamut,
    scratchTrey
}

public enum TargetType
{
    None,
    Self,
    FriendlySingle,
    FriendlyAll,
    EnemySingle,
    EnemyAll,
    All
}