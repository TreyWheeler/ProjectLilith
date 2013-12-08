using UnityEngine;
using System.Collections;

public class SpawnParticleEffectScenePerformanceAction : ScenePerformanceActionBase
{
    public GameObject Actor;
    public GameObject Target;
    public float Duration;
    public float ParticlesPerSecond;
    public Color Color1 = Color.white;
    public Color Color2 = Color.white;
    public Color Color3 = Color.white;
    public Color Color4 = Color.white;
    public Color Color5 = Color.white;
    public float ParticleLifeTime;
    public float ParticleSize;
    public float ParticleSpeed;
    public Vector3 RandomVelocity;

    GameObject particleObj;

    public override void Start()
    {
        base.Start();

        particleObj = new GameObject();
        particleObj.name = Name != null ? Name : "Script Generated Particle Emitter";
        particleObj.transform.parent = Actor.transform;
        particleObj.transform.localPosition = Vector3.zero;
        particleObj.transform.localRotation = Quaternion.identity;

        var emitter = (ParticleEmitter)particleObj.AddComponent("EllipsoidParticleEmitter");
        emitter.minSize = ParticleSize;
        emitter.maxSize = ParticleSize;
        emitter.minEnergy = ParticleLifeTime;
        emitter.maxEnergy = ParticleLifeTime;
        emitter.minEmission = ParticlesPerSecond;
        emitter.maxEmission = ParticlesPerSecond;
        emitter.localVelocity = new Vector3(0, 0, ParticleSpeed);
        emitter.rndVelocity = RandomVelocity;

        var renderer = particleObj.AddComponent<ParticleRenderer>();
        renderer.castShadows = false;
        renderer.material = Resources.Load("Materials/Custom-Particle") as Material;
        renderer.material.mainTexture = Resources.Load("Textures/particle") as Texture2D;
        renderer.material.shader = Shader.Find("Particles/Alpha Blended Premultiply");

        var animator = particleObj.AddComponent<ParticleAnimator>();
        animator.doesAnimateColor = true;
        animator.colorAnimation = new Color[] { Color1, Color2, Color3, Color4, Color5 };

        TimedTaskManager.Instance.Add(Duration * 1000, () =>
        {
            Finish();
        });
    }

    public override T GetPart<T>(string partName)
    {
        switch(partName)
        {
            case "Emitter":
                return particleObj as T;
        }

        return base.GetPart<T>(partName);
    }

    public override void Update()
    {
        if (Target != null)
            particleObj.LookAt(Target);
    }

    public override void Finish()
    {
        if (Finished)
            return;

        var system = particleObj.GetComponent<ParticleEmitter>();
        system.emit = false;

        GameObject.Destroy(particleObj, ParticleLifeTime);

        base.Finish();
    }
}
