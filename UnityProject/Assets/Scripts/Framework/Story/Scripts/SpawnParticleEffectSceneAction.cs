using UnityEngine;
using System.Collections;

public class SpawnParticleEffectSceneAction : SceneActionBase
{
    public string Target;
    public string Duration;
    public string ParticlesPerSecond;
    public Color Color1 = Color.white;
    public Color Color2 = Color.white;
    public Color Color3 = Color.white;
    public Color Color4 = Color.white;
    public Color Color5 = Color.white;
    public string ParticleLifeTime;
    public string ParticleSize;
    public string ParticleSpeed;
    public Vector3 RandomVelocity;
    public Vector3 LocalPosition;
}
