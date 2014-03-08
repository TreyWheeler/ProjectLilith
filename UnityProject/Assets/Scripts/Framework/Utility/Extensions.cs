using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static T EnsureComponent<T>(this MonoBehaviour monoBehavior) where T : Component
    {
        return monoBehavior.gameObject.EnsureComponent<T>();
    }
    public static T EnsureComponent<T>(this GameObject gameObj) where T : Component
    {
        T component = gameObj.GetComponent<T>();

        if (component == null)
            component = gameObj.AddComponent<T>();

        return component;
    }

    public static float Length(this Vector2 vector)
    {
        return Vector2.Distance(Vector2.zero, vector);
    }

    public static void AddOnClick(this GameObject gameObject, Action onClick)
    {
        ClickTracker clickTracker = Helper.EnsureGameObject("ClickTracker").EnsureComponent<ClickTracker>();

        clickTracker.AddOnClickHandler(gameObject, onClick);
    }

    public static void AddOnOutsideClick(this GameObject gameObject, Action onClick)
    {
        ClickTracker clickTracker = Helper.EnsureGameObject("ClickTracker").EnsureComponent<ClickTracker>();

        clickTracker.AddOnOutsideClickHandler(gameObject, onClick);
    }


    public static bool MouseOnMe(this GameObject go)
    {
        return Helper.GetGameOjectMouseIsOver() == go;
    }

    public static IEnumerable<GameObject> GetChildren(this GameObject gameObject)
    {
        foreach (Transform transform in gameObject.transform)
        {
            yield return transform.gameObject;
        }
    }

    public static T GetFirstComponentThatImplements<T>(this GameObject gameObject) where T : class
    {
        Component[] components = gameObject.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            T implementation = components[i] as T;
            if (implementation != null)
                return implementation;
        }
        return null;
    }

    public static void LookAt(this GameObject actor, GameObject target)
    {
        actor.transform.LookAt(target.transform);
    }

    public static void CrossFadeVolume(this MonoBehaviour monobehavior, AudioSource fadeOutAudio, AudioSource fadeInAudio, float duration)
    {
        monobehavior.StartCoroutine(FadeVolume(fadeOutAudio, 1.6f, 1, 0));
        monobehavior.StartCoroutine(FadeVolume(fadeInAudio, 1.6f, 0, 1));
    }

    public static void FadeVolume(this MonoBehaviour monobehavior, AudioSource audio, float duration, float initialVolume = 0f, float targetVolume = 1f)
    {
        monobehavior.StartCoroutine(FadeVolume(audio, duration, initialVolume, targetVolume));
    }

    private static IEnumerator FadeVolume(AudioSource audio, float duration, float initialVolume = 0f, float targetVolume = 1f)
    {
        audio.volume = initialVolume;
        if (!audio.isPlaying)
            audio.Play();
        float volumeDifference = targetVolume - initialVolume;
        float timeElapsed = 0;
        yield return null;
        float progressPercent = 0;
        while (progressPercent < 1)
        {
            timeElapsed += Time.deltaTime;
            progressPercent = Mathf.Clamp01(timeElapsed / duration);
            audio.volume = initialVolume + progressPercent * volumeDifference;
            yield return null;// Wait one frame
        }
    }
}