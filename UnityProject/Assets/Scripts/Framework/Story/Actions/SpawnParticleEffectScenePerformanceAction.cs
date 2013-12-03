using UnityEngine;
using System.Collections;

public class SpawnParticleEffectScenePerformanceAction : ScenePerformanceActionBase
{
    public GameObject Actor;
    public GameObject Target;
    public float Duration;


    GameObject particleObj;

    public override void Start()
    {
        base.Start();

        particleObj = new GameObject();
        particleObj.transform.parent = Actor.transform;
        particleObj.transform.localPosition = Vector3.zero;
        particleObj.transform.localRotation = Quaternion.identity;
        var particles = particleObj.AddComponent<ParticleSystem>();
    
        particles.loop = true;
        particles.enableEmission = true;
        particles.emissionRate = 1000f;
        particles.startColor = Color.white;
        particles.startLifetime = .6241f;
        particles.startSize = 1f;
        particles.startSpeed = 20f;
        particles.Play();

        TimedTaskManager.Instance.Add(Duration * 1000, () =>
        {
            Finish();
        });
    }

    public override void Update()
    {
        particleObj.LookAt(Target);
    }

    public override void Finish()
    {
        if (Finished)
            return;

        var system = particleObj.GetComponent<ParticleSystem>();
        system.Stop();

        GameObject.Destroy(particleObj, system.startLifetime);

        base.Finish();
    }
}
