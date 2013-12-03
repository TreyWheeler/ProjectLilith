using UnityEngine;
using System.Collections;

public class SoundEffectScenePerformanceAction : ScenePerformanceActionBase
{
    public GameObject AudioSourceActor;
    public AudioClip AudioFile;

    GameObject audioSource;

    public override void Start()
    {
        base.Start();

        audioSource = new GameObject();
        audioSource.AddComponent<AudioSource>();
        audioSource.transform.parent = AudioSourceActor.transform;
        audioSource.transform.localPosition = Vector3.zero;

        audioSource.audio.clip = AudioFile;
        audioSource.audio.Play();

        TimedTaskManager.Instance.Add(AudioFile.length * 1100, () =>
        {
            Finish();
        });
    }


    public override void Finish()
    {
        if (Finished)
            return;

        if (audioSource.audio.isPlaying)
        {
            FloatTween volumeFade = new FloatTween();
            volumeFade.From = 1.0f;
            volumeFade.To = 0f;
            volumeFade.Duration = 750f;
            volumeFade.CurrentValueChanged += (args) =>
            {
                audioSource.audio.volume = args.NewValue;
                if (args.NewValue == 0f)
                    GameObject.Destroy(audioSource);
            };

            TimedTaskManager.Instance.Add(volumeFade);
        }
        else
        {
            GameObject.Destroy(audioSource);
        }

        base.Finish();
    }

}
