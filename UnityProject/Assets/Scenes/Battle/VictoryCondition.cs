using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VictoryCondition : MonoBehaviour
{
    public List<Character> Allies = new List<Character>();
    public List<Character> Enemies = new List<Character>();

    bool AnAllyIsAlive = true;
    bool AnEnemyIsAlive = true;

    bool triggered = false;

    void Start()
    {
        gameObject.EnsureComponent<AudioSource>();
    }

    public bool Won
    {
        get
        {
            return AnAllyIsAlive && !AnEnemyIsAlive;
        }
    }


    public bool Lost
    {
        get
        {
            return !AnAllyIsAlive && AnEnemyIsAlive;
        }
    }

    public bool Tied
    {
        get
        {
            return !AnAllyIsAlive && !AnEnemyIsAlive;
        }
    }
    void Update()
    {
        if (Allies.Count > 0 && Enemies.Count > 0)
        {
            AnAllyIsAlive = false;
            AnEnemyIsAlive = false;

            foreach (var ally in Allies)
            {
                AnAllyIsAlive = AnAllyIsAlive || ally.IsAlive;
            }

            foreach (var enemy in Enemies)
            {
                AnEnemyIsAlive = AnEnemyIsAlive || enemy.IsAlive;
            }
        }

        if (Won)
        {
            if (!triggered)
            {
                var clip = Resources.Load("sounds/ff7") as AudioClip;
                audio.PlayOneShot(clip);
                triggered = true;
            }
        }
        else if (Lost)
        {
            if (!triggered)
            {
                var clip = Resources.Load("sounds/gameover") as AudioClip;
                audio.PlayOneShot(clip);
                triggered = true;
            }
        }
        else if(Tied)
        {
            if (!triggered)
            {
                var clip = Resources.Load("sounds/TetrisRemix") as AudioClip;
                audio.PlayOneShot(clip);
                triggered = true;
            }
        }
    }

    void OnGUI()
    {
        if (Won)
        {
            GUI.Label(new Rect(5, 5, 200, 200), "Win!");
        }
        else if (Lost)
        {
            GUI.Label(new Rect(5, 5, 200, 200), "Lose!");
        }
        else if (Tied)
        {
            GUI.Label(new Rect(5, 5, 200, 200), "Tie!");
        }
    }


}
