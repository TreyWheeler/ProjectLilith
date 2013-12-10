using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class AbilitySceneProvider
{
    private static Dictionary<LilithAbilities, SceneActionList> loadedScenes = new Dictionary<LilithAbilities, SceneActionList>();

    public static SceneActionList GetBy(LilithAbilities ability)
    {
        if (!loadedScenes.ContainsKey(ability))
        {

            switch (ability)
            {
                case LilithAbilities.Attack:

                    SceneActionList attackScene = new SceneActionList();

                    PlaySoundSceneAction swordHitSound = new PlaySoundSceneAction();
                    swordHitSound.Sound = "Sounds/unsheth1";
                    swordHitSound.Actor = "{Caster}";
                    attackScene.Add(swordHitSound);

                    RunAnimationSceneAction drawSwordAnim = new RunAnimationSceneAction();
                    drawSwordAnim.BlocksStory = true;
                    drawSwordAnim.RunOnce = "True";
                    drawSwordAnim.Actor = "{Caster}";
                    drawSwordAnim.Animation = "DrawBlade";
                    attackScene.Add(drawSwordAnim);

                    RunAnimationSceneAction anim = new RunAnimationSceneAction();
                    anim.Actor = "{Caster}";
                    anim.Animation = "Run";
                    attackScene.Add(anim);

                    MoveToEntitySceneAction movePart = new MoveToEntitySceneAction();
                    movePart.BlocksStory = true;
                    movePart.Actor = "{Caster}";
                    movePart.Speed = "{Caster}<Character>.Stats[LilithStats.MoveSpeed].CurrentValue";
                    movePart.HowClose = "1";
                    movePart.Entity = "{Target}";
                    attackScene.Add(movePart);

                    swordHitSound = new PlaySoundSceneAction();
                    swordHitSound.Sound = "Sounds/hvyswrd4";
                    swordHitSound.Actor = "{Caster}";
                    attackScene.Add(swordHitSound);

                    AdjustStatSceneAction dmg = new AdjustStatSceneAction();
                    dmg.Adjustment = "{Caster}<Character>.Stats[LilithStats.Strength].CurrentValue * -10";
                    dmg.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
                    attackScene.Add(dmg);

                    RunAnimationSceneAction attackAnim = new RunAnimationSceneAction();
                    attackAnim.BlocksStory = true;
                    attackAnim.RunOnce = "True";
                    attackAnim.Actor = "{Caster}";
                    attackAnim.Animation = "Attack";
                    attackScene.Add(attackAnim);

                    RunAnimationSceneAction idleAnim = new RunAnimationSceneAction();
                    idleAnim.Actor = "{Caster}";
                    idleAnim.Animation = "Attack_standy";
                    attackScene.Add(idleAnim);

                    loadedScenes.Add(ability, attackScene);

                    break;
                case LilithAbilities.Blizzard:

                    SceneActionList blizzard = new SceneActionList();

                    var castingAnim = new RunAnimationSceneAction();
                    castingAnim.Actor = "{Caster}";
                    castingAnim.Animation = "Gentleman";
                    blizzard.Add(castingAnim);

                    var emitter = new SpawnParticleEffectSceneAction();
                    emitter.Actor = "{Caster}";
                    emitter.Target = "{Target}";
                    emitter.Duration = "3";
                    emitter.ParticlesPerSecond = "3500";
                    emitter.LocalPosition = Vector3.zero;
                    emitter.Color1 = Color.white;
                    emitter.Color2 = Color.white;
                    emitter.Color3 = Color.white;
                    emitter.Color4 = Color.white;
                    emitter.Color5 = Color.white;
                    emitter.ParticleLifeTime = ".5";
                    emitter.ParticleSize = ".4";
                    emitter.ParticleSpeed = "20";
                    emitter.RandomVelocity = new Vector3(10, 10, 10);
                    blizzard.Add(emitter);

                    var blizzardSound = new PlaySoundSceneAction();
                    blizzardSound.Sound = "Sounds/wind01 l";
                    blizzardSound.Actor = "{Caster}";
                    blizzard.Add(blizzardSound);

                    blizzardSound = new PlaySoundSceneAction();
                    blizzardSound.Sound = "Sounds/wind03 l";
                    blizzardSound.Actor = "{Caster}";
                    blizzard.Add(blizzardSound);

                    dmg = new AdjustStatSceneAction();
                    dmg.BlocksStory = true;
                    dmg.Adjustment = "({Caster}<Character>.Stats[LilithStats.Intelligence].CurrentValue - ({Target}<Character>.Stats[LilithStats.Intelligence].CurrentValue / 2)) * -10";
                    dmg.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
                    dmg.OverSeconds = "2.8";
                    blizzard.Add(dmg);

                    idleAnim = new RunAnimationSceneAction();
                    idleAnim.Actor = "{Caster}";
                    idleAnim.Animation = "Idle";
                    blizzard.Add(idleAnim);


                    loadedScenes.Add(ability, blizzard);

                    break;

                case LilithAbilities.Fireball:
                    SceneActionList fireball = new SceneActionList();

                    RunAnimationSceneAction cast = new RunAnimationSceneAction();
                    cast.RunOnce = "True";
                    cast.Actor = "{Caster}";
                    cast.Animation = "Attack02";
                    fireball.Add(cast);

                    var sound = new PlaySoundSceneAction();
                    sound.Sound = "Sounds/fyrbal02";
                    sound.Actor = "{Caster}";
                    fireball.Add(sound);

                    emitter = new SpawnParticleEffectSceneAction();
                    emitter.BlocksStory = true;
                    emitter.Actor = "{Caster}";
                    emitter.Duration = "1";
                    emitter.ParticlesPerSecond = "60";
                    emitter.LocalPosition = new Vector3(0, 1, 0);
                    emitter.Color1 = new Color(.5f, 0f, 1f, .2f);
                    emitter.Color2 = new Color(0, .5f, 1f, .5f);
                    emitter.Color3 = new Color(1f, .65f, 0f, 1f);
                    emitter.Color4 = new Color(1f, .35f, 0f, .6f);
                    emitter.Color5 = new Color(1f, 0f, 0f, .2f);
                    emitter.ParticleLifeTime = "1";
                    emitter.ParticleSize = ".5";
                    emitter.ParticleSpeed = "0";
                    emitter.RandomVelocity = new Vector3(5, 0, 5);
                    fireball.Add(emitter);

                    idleAnim = new RunAnimationSceneAction();
                    idleAnim.Actor = "{Caster}";
                    idleAnim.Animation = "Idle";
                    fireball.Add(idleAnim);

                    sound = new PlaySoundSceneAction();
                    sound.Sound = "Sounds/fyrbal01";
                    sound.Actor = "{Caster}";
                    fireball.Add(sound);

                    emitter = new SpawnParticleEffectSceneAction();
                    emitter.Name = "Fireball";
                    emitter.Actor = "{Caster}";
                    emitter.Target = "{Target}";
                    emitter.Duration = "100";
                    emitter.ParticlesPerSecond = "6000";
                    emitter.LocalPosition = new Vector3(0, 1, 0);
                    emitter.Color1 = new Color(.5f, .4f, .7f, .2f);
                    emitter.Color2 = new Color(.8f, .5f, .3f, .5f);
                    emitter.Color3 = new Color(1f, .65f, 0f, 1f);
                    emitter.Color4 = new Color(1f, .35f, 0f, .6f);
                    emitter.Color5 = new Color(1f, 0f, 0f, .2f);
                    emitter.RandomVelocity = new Vector3(3, 3, 3);
                    emitter.ParticleLifeTime = ".65";
                    emitter.ParticleSize = ".5";
                    emitter.ParticleSpeed = "0";
                    fireball.Add(emitter);

                    movePart = new MoveToEntitySceneAction();
                    movePart.BlocksStory = true;
                    movePart.Actor = "{#Fireball}.Emitter";
                    movePart.Speed = "10";
                    movePart.HowClose = "0";
                    movePart.Entity = "{Target}";
                    fireball.Add(movePart);

                    sound = new PlaySoundSceneAction();
                    sound.Sound = "Sounds/exp01";
                    sound.Actor = "{Target}";
                    fireball.Add(sound);

                    dmg = new AdjustStatSceneAction();
                    dmg.Adjustment = "{Caster}<Character>.Stats[LilithStats.Intelligence].CurrentValue * -5";
                    dmg.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
                    fireball.Add(dmg);

                    dmg = new AdjustStatSceneAction();
                    dmg.BlocksStory = true;
                    dmg.Adjustment = "{Caster}<Character>.Stats[LilithStats.Intelligence].CurrentValue * -5";
                    dmg.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
                    dmg.OverSeconds = "1";
                    fireball.Add(dmg);

                    FinishPartSceneAction finisher = new FinishPartSceneAction();
                    finisher.PartToFinish = "{#Fireball}";
                    finisher.DelayBeforeFinish = "1";
                    fireball.Add(finisher);

                    loadedScenes.Add(ability, fireball);
                    break;

                case LilithAbilities.Heal:
                     SceneActionList heal = new SceneActionList();

                    cast = new RunAnimationSceneAction();
                    cast.RunOnce = "True";
                    cast.Actor = "{Caster}";
                    cast.Animation = "Attack02";
                    heal.Add(cast);

                    sound = new PlaySoundSceneAction();
                    sound.Sound = "Sounds/birdseye";
                    sound.Actor = "{Caster}";
                    heal.Add(sound);

                    emitter = new SpawnParticleEffectSceneAction();
                    emitter.BlocksStory = true;
                    emitter.Actor = "{Caster}";
                    emitter.Duration = ".5";
                    emitter.ParticlesPerSecond = "100";
                    emitter.LocalPosition = new Vector3(0, 1, 0);
                    emitter.Color1 = new Color(1f, .0f, .35f, .2f);
                    emitter.Color2 = new Color(1f, 0f, .35f, .5f);
                    emitter.Color3 = new Color(1f, 0f, 0f, 1f);
                    emitter.Color4 = new Color(1f, 0f, .9f, .6f);
                    emitter.Color5 = new Color(1f, 0f, .5f, .2f);
                    emitter.ParticleLifeTime = "1";
                    emitter.ParticleSize = ".5";
                    emitter.ParticleSpeed = "0";
                    emitter.RandomVelocity = new Vector3(3, 0, 3);
                    heal.Add(emitter);

                    idleAnim = new RunAnimationSceneAction();
                    idleAnim.Actor = "{Caster}";
                    idleAnim.Animation = "Idle";
                    heal.Add(idleAnim);
                    
                    sound = new PlaySoundSceneAction();
                    sound.Sound = "Sounds/c light";
                    sound.Actor = "{Caster}";
                    heal.Add(sound);

                    emitter = new SpawnParticleEffectSceneAction();
                    emitter.Name = "Heal";
                    emitter.Actor = "{Caster}";
                    emitter.Target = "{Target}";
                    emitter.Duration = "100";
                    emitter.ParticlesPerSecond = "6000";
                    emitter.LocalPosition = new Vector3(0, 1, 0);
                    emitter.Color1 = new Color(1f, 0f, .05f, .2f);
                    emitter.Color2 = new Color(.5f, .2f, .2f, .5f);
                    emitter.Color3 = new Color(.5f, 0, .01f, 1f);
                    emitter.Color4 = new Color(.5f, .18f, .18f, .6f);
                    emitter.Color5 = new Color(0f, 0f, 0f, .2f);
                    emitter.RandomVelocity = new Vector3(1, 1, 1);
                    emitter.ParticleLifeTime = ".65";
                    emitter.ParticleSize = ".1";
                    emitter.ParticleSpeed = "0";
                    heal.Add(emitter);

                    movePart = new MoveToEntitySceneAction();
                    movePart.BlocksStory = true;
                    movePart.Actor = "{#Heal}.Emitter";
                    movePart.Speed = "15";
                    movePart.HowClose = "0";
                    movePart.Entity = "{Target}";
                    heal.Add(movePart);

                    sound = new PlaySoundSceneAction();
                    sound.Sound = "Sounds/heal";
                    sound.Actor = "{Target}";
                    heal.Add(sound);

                    AdjustStatSceneAction health = new AdjustStatSceneAction();
                    health.Adjustment = "{Caster}<Character>.Stats[LilithStats.Intelligence].CurrentValue * 10";
                    health.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
                    heal.Add(health);
                    
                    emitter = new SpawnParticleEffectSceneAction();
                    emitter.BlocksStory = true;
                    emitter.Actor = "{Target}";
                    emitter.Duration = "1";
                    emitter.ParticlesPerSecond = "250";
                    emitter.LocalPosition = new Vector3(0, 1, 0);
                    emitter.Color1 = new Color(1f, 0f, .05f, .2f);
                    emitter.Color2 = new Color(.5f, .2f, .2f, .5f);
                    emitter.Color3 = new Color(.5f, 0, .01f, .5f);
                    emitter.Color4 = new Color(.5f, .18f, .18f, .6f);
                    emitter.Color5 = new Color(0f, 0f, 0f, .2f);
                    emitter.RandomVelocity = new Vector3(3, 10, 1);
                    emitter.ParticleLifeTime = ".65";
                    emitter.ParticleSize = "1";
                    emitter.ParticleSpeed = ".2";
                    heal.Add(emitter);

                    finisher = new FinishPartSceneAction();
                    finisher.PartToFinish = "{#Heal}";
                    finisher.DelayBeforeFinish = "1";
                    heal.Add(finisher);

                    loadedScenes.Add(ability, heal);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        return loadedScenes[ability];

    }

}

public enum LilithAbilities
{
    Attack, Blizzard, Fireball, Heal
}