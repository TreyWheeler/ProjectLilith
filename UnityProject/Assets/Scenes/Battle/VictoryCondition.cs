using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VictoryCondition : MonoBehaviour
{
    bool AnAllyIsAlive = true;
    bool AnEnemyIsAlive = true;
    bool triggered = false;

    BattleScene currentScene;

    Texture2D winTexture;
    Texture2D loseTexture;
    Texture2D tieTexture;

    private AudioSource matchEndAudioSource;
    public AudioClip winSong;
    public AudioClip loseSong;
    public AudioClip tieSong;

    private AudioSource matchInProgressAudioSource;
    public AudioClip battleSong;

    void Start()
    {
        gameObject.EnsureComponent<AudioSource>();
        currentScene = gameObject.EnsureComponent<BattleScene>();

        winTexture = Resources.Load("Textures/Victory") as Texture2D;
        loseTexture = Resources.Load("Textures/Loss") as Texture2D;
        tieTexture = Resources.Load("Textures/Tie") as Texture2D;

        matchEndAudioSource = this.gameObject.AddComponent<AudioSource>();
        matchInProgressAudioSource = this.gameObject.AddComponent<AudioSource>();

        matchInProgressAudioSource.clip = battleSong;        

        this.FadeVolume(matchInProgressAudioSource, 3.6f);
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
        if (currentScene.Allies.Count > 0 && currentScene.Enemies.Count > 0)
        {
            AnAllyIsAlive = false;
            AnEnemyIsAlive = false;

            foreach (var ally in currentScene.Allies)
            {
                AnAllyIsAlive = AnAllyIsAlive || ally.IsAlive;
            }

            foreach (var enemy in currentScene.Enemies)
            {
                AnEnemyIsAlive = AnEnemyIsAlive || enemy.IsAlive;
            }
        }

        if (!triggered)
        {
            if (Won)
            {
                matchEndAudioSource.clip = winSong;
                this.CrossFadeVolume(matchInProgressAudioSource, matchEndAudioSource, 1.6f);
                triggered = true;
            }
            else if (Lost)
            {
                matchEndAudioSource.clip = loseSong;
                this.CrossFadeVolume(matchInProgressAudioSource, matchEndAudioSource, 1.6f);
                triggered = true;

            }
            else if (Tied)
            {
                matchEndAudioSource.clip = tieSong;
                this.CrossFadeVolume(matchInProgressAudioSource, matchEndAudioSource, 1.6f);
                triggered = true;

            }
        }
    }

    void OnGUI()
    {
        if (Won || Lost || Tied)
        {
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, Screen.height / 4), "Rematch"))
            {
                Application.LoadLevel(0);
            }
        }

        if (Won)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - winTexture.width / 2, Screen.height / 2 - winTexture.height, winTexture.width, winTexture.height), winTexture);
        }
        if (Lost)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - loseTexture.width / 2, Screen.height / 2 - loseTexture.height, loseTexture.width, loseTexture.height), loseTexture);
        }
        if (Tied)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - tieTexture.width / 2, Screen.height / 2 - tieTexture.height, tieTexture.width, tieTexture.height), tieTexture);
        }


    }





}
