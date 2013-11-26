using UnityEngine;
using System.Collections;

public class SpawnParticleEffectScenePerformanceAction : ScenePerformanceActionBase
{
    public GameObject Actor;
    public GameObject Target;

    protected override void TellStory()
    {
        var particles = Actor.AddComponent<ParticleSystem>();
        Actor.LookAt(Target);
        //particles.constantForce.torque += new Vector3(25f, 0f, 0f);
        //particles.gravityModifier = 50f;
        //particles.
        particles.loop = false;
        particles.enableEmission = true;
        particles.emissionRate = 1000f;
        particles.startColor = Color.white;
        particles.startLifetime = .6241f;
        particles.startSize = 1f;
        particles.startSpeed = 20f;
        particles.Play();

        TimedTaskManager.Instance.Add(6241, () => 
        { 
            Component.DestroyImmediate(particles);
        });
        //particles.
        //particles.particleEmitter.angularVelocity = 90;

        RaiseComplete();
    }
}
