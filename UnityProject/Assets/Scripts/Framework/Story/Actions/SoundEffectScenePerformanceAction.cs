using UnityEngine;
using System.Collections;

public class SoundEffectScenePerformanceAction : ScenePerformanceActionBase
{
    public GameObject AudioSourceActor;
    public AudioClip AudioFile;

    protected override void TellStory()
    {
        AudioSourceActor.audio.PlayOneShot(AudioFile);

        RaiseComplete();
    }
}
