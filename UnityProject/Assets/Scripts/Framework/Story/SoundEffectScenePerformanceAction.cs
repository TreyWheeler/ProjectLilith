using UnityEngine;
using System.Collections;

public class SoundEffectScenePerformanceAction : ScenePerformanceAction
{
    public GameObject AudioSourceActor;
    public AudioClip AudioFile;

    protected override void TellStory()
    {
        AudioSourceActor.audio.PlayOneShot(AudioFile);

        RaiseComplete();
    }
}
