using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class AbilitySceneProvider
{
    private static Dictionary<LilithAbilities, List<SceneActionBase>> loadedScenes = new Dictionary<LilithAbilities, List<SceneActionBase>>();
    private const string CASTER = "{Caster}";
    private const string TARGET = "{Target}";
    public static List<SceneActionBase> GetBy(LilithAbilities ability)
    {
        if (!loadedScenes.ContainsKey(ability))
        {
            switch (ability)
            {
                case LilithAbilities.Attack:
                    CreateSwingSword();
                    break;
                case LilithAbilities.Blizzard:
                    CreateBlizzard();
                    break;
                case LilithAbilities.Fireball:
                    CreateFireball();                    
                    break;
                case LilithAbilities.Heal:
                    CreateHeal();
                    break;
                case LilithAbilities.ChannelEmpower:
                    ModifyStrengthUp();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        return loadedScenes[ability];
    }
    public static void CreateSwingSword()
    {
        var attackScene = new List<SceneActionBase>();

        var swordHitSound = new PlaySoundSceneAction();
        swordHitSound.Sound = "Sounds/unsheth1";
        swordHitSound.Actor = CASTER;
        attackScene.Add(swordHitSound);

        var drawSwordAnim = new RunAnimationSceneAction();
        drawSwordAnim.BlocksStory = true;
        drawSwordAnim.RunOnce = "True";
        drawSwordAnim.Actor = CASTER;
        drawSwordAnim.Animation = "DrawBlade";
        attackScene.Add(drawSwordAnim);

        var anim = new RunAnimationSceneAction();
        anim.Actor = CASTER;
        anim.Animation = "Run";
        attackScene.Add(anim);

        var movePart = new MoveToEntitySceneAction();
        movePart.BlocksStory = true;
        movePart.Actor = CASTER;
        movePart.Speed = "{Caster}<Character>.Stats[LilithStats.MoveSpeed].CurrentValue";
        movePart.HowClose = "1";
        movePart.Entity = TARGET;
        attackScene.Add(movePart);

        swordHitSound = new PlaySoundSceneAction();
        swordHitSound.Sound = "Sounds/hvyswrd4";
        swordHitSound.Actor = CASTER;
        attackScene.Add(swordHitSound);

        var dmg = new AdjustStatSceneAction();
        dmg.Adjustment = "{Caster}<Character>.Stats[LilithStats.Strength].CurrentValue * -10";
        dmg.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
        attackScene.Add(dmg);

        var attackAnim = new RunAnimationSceneAction();
        attackAnim.BlocksStory = true;
        attackAnim.RunOnce = "True";
        attackAnim.Actor = CASTER;
        attackAnim.Animation = "Attack";
        attackScene.Add(attackAnim);

        var idleAnim = new RunAnimationSceneAction();
        idleAnim.Actor = CASTER;
        idleAnim.Animation = "Attack_standy";
        attackScene.Add(idleAnim);

        loadedScenes.Add(LilithAbilities.Attack, attackScene);
    }
    public static void CreateBlizzard()
    {
        List<SceneActionBase> blizzard = new List<SceneActionBase>();

        var castingAnim = new RunAnimationSceneAction();
        castingAnim.Actor = CASTER;
        castingAnim.Animation = "Gentleman";
        blizzard.Add(castingAnim);

        var emitter = new SpawnParticleEffectSceneAction();
        emitter.Actor = CASTER;
        emitter.Target = TARGET;
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
        blizzardSound.Actor = CASTER;
        blizzard.Add(blizzardSound);

        blizzardSound = new PlaySoundSceneAction();
        blizzardSound.Sound = "Sounds/wind03 l";
        blizzardSound.Actor = CASTER;
        blizzard.Add(blizzardSound);

        var dmg = new AdjustStatSceneAction();
        dmg.BlocksStory = true;
        dmg.Adjustment = "({Caster}<Character>.Stats[LilithStats.Intelligence].CurrentValue - ({Target}<Character>.Stats[LilithStats.Intelligence].CurrentValue / 2)) * -10";
        dmg.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
        dmg.OverSeconds = "2.8";
        blizzard.Add(dmg);

        var idleAnim = new RunAnimationSceneAction();
        idleAnim.Actor = CASTER;
        idleAnim.Animation = "Idle";
        blizzard.Add(idleAnim);

        loadedScenes.Add(LilithAbilities.Blizzard, blizzard);
    }
    private static void CreateFireball()
    {
        var fireball = new List<SceneActionBase>();

        var cast = new RunAnimationSceneAction();
        cast.RunOnce = "True";
        cast.Actor = CASTER;
        cast.Animation = "Attack02";
        fireball.Add(cast);

        var sound = new PlaySoundSceneAction();
        sound.Sound = "Sounds/fyrbal02";
        sound.Actor = CASTER;
        fireball.Add(sound);

        var emitter = new SpawnParticleEffectSceneAction();
        emitter.BlocksStory = true;
        emitter.Actor = CASTER;
        emitter.Duration = "1.2";
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

        var idleAnim = new RunAnimationSceneAction();
        idleAnim.Actor = CASTER;
        idleAnim.Animation = "Idle";
        fireball.Add(idleAnim);

        sound = new PlaySoundSceneAction();
        sound.Sound = "Sounds/fyrbal01";
        sound.Actor = CASTER;
        fireball.Add(sound);

        BuffSceneAction buff = new BuffSceneAction();
        buff.Actor = CASTER;
        buff.TargetCharacter = "{Target}<Character>";
        fireball.Add(buff);

        emitter = new SpawnParticleEffectSceneAction();
        emitter.Name = "Fireball";
        emitter.Actor = CASTER;
        emitter.Target = TARGET;
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
        buff.Actions.Add(emitter);

        var movePart = new MoveToEntitySceneAction();
        movePart.BlocksStory = true;
        movePart.Actor = "{#Fireball}.Emitter";
        movePart.Speed = "10";
        movePart.HowClose = "0";
        movePart.Entity = TARGET;
        buff.Actions.Add(movePart);

        sound = new PlaySoundSceneAction();
        sound.Sound = "Sounds/exp01";
        sound.Actor = TARGET;
        buff.Actions.Add(sound);

        var dmg = new AdjustStatSceneAction();
        dmg.Adjustment = "{Caster}<Character>.Stats[LilithStats.Intelligence].CurrentValue * -5";
        dmg.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
        buff.Actions.Add(dmg);

        dmg = new AdjustStatSceneAction();
        dmg.BlocksStory = true;
        dmg.Adjustment = "{Caster}<Character>.Stats[LilithStats.Intelligence].CurrentValue * -5";
        dmg.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
        dmg.OverSeconds = "1";
        buff.Actions.Add(dmg);

        FinishPartSceneAction finisher = new FinishPartSceneAction();
        finisher.PartToFinish = "{#Fireball}";
        finisher.DelayBeforeFinish = "1";
        buff.Actions.Add(finisher);

        var wait = new WaitSceneAction();
        wait.Seconds = ".5";
        fireball.Add(wait);

        loadedScenes.Add(LilithAbilities.Fireball, fireball);
    }
    private static void CreateHeal()
    {
        var heal = new List<SceneActionBase>();

        var cast = new RunAnimationSceneAction();
        cast.RunOnce = "True";
        cast.Actor = CASTER;
        cast.Animation = "Attack02";
        heal.Add(cast);

        var sound = new PlaySoundSceneAction();
        sound.Sound = "Sounds/birdseye";
        sound.Actor = CASTER;
        heal.Add(sound);

        var emitter = new SpawnParticleEffectSceneAction();
        emitter.BlocksStory = true;
        emitter.Actor = CASTER;
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

        var idleAnim = new RunAnimationSceneAction();
        idleAnim.Actor = CASTER;
        idleAnim.Animation = "Idle";
        heal.Add(idleAnim);

        sound = new PlaySoundSceneAction();
        sound.Sound = "Sounds/c light";
        sound.Actor = CASTER;
        heal.Add(sound);

        emitter = new SpawnParticleEffectSceneAction();
        emitter.Name = "Heal";
        emitter.Actor = CASTER;
        emitter.Target = TARGET;
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

        var movePart = new MoveToEntitySceneAction();
        movePart.BlocksStory = true;
        movePart.Actor = "{#Heal}.Emitter";
        movePart.Speed = "15";
        movePart.HowClose = "0";
        movePart.Entity = TARGET;
        heal.Add(movePart);

        sound = new PlaySoundSceneAction();
        sound.Sound = "Sounds/heal";
        sound.Actor = TARGET;
        heal.Add(sound);

        AdjustStatSceneAction health = new AdjustStatSceneAction();
        health.Adjustment = "{Caster}<Character>.Stats[LilithStats.Intelligence].CurrentValue * 10";
        health.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
        heal.Add(health);

        emitter = new SpawnParticleEffectSceneAction();
        emitter.BlocksStory = true;
        emitter.Actor = TARGET;
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

        var finisher = new FinishPartSceneAction();
        finisher.PartToFinish = "{#Heal}";
        finisher.DelayBeforeFinish = "1";
        heal.Add(finisher);

        loadedScenes.Add(LilithAbilities.Heal, heal);
    }
    public static void ModifyStrengthUp()
    {
        List<SceneActionBase> modifyStrength = new List<SceneActionBase>();

        var cast = new RunAnimationSceneAction();
        cast.RunOnce = "True";
        cast.Actor = CASTER;
        cast.Animation = "Buff";
        modifyStrength.Add(cast);

        var idleAnim = new RunAnimationSceneAction();
        idleAnim.Actor = CASTER;
        idleAnim.Animation = "Idle";
        modifyStrength.Add(idleAnim); 

        var sound = new PlaySoundSceneAction();
        sound.Sound = "Sounds/birdseye";
        sound.Actor = CASTER;
        modifyStrength.Add(sound);

        var emitter = new SpawnParticleEffectSceneAction();
        emitter.BlocksStory = true;
        emitter.Actor = CASTER;
        emitter.Duration = "1";
        emitter.ParticlesPerSecond = "100";
        emitter.LocalPosition = new Vector3(0, 1, 0);
        emitter.Color1 = new Color(.5f, .0f, .35f, .2f);
        emitter.Color2 = new Color(.5f, .2f, .35f, .5f);
        emitter.Color3 = new Color(.7f, .32f, 0f, 1f);
        emitter.Color4 = new Color(1f, 0f, .7f, .6f);
        emitter.Color5 = new Color(.4f, 0f, .5f, .2f);
        emitter.ParticleLifeTime = "1";
        emitter.ParticleSize = ".5";
        emitter.ParticleSpeed = "0";
        emitter.RandomVelocity = new Vector3(3, 3, 3);
        modifyStrength.Add(emitter);

        emitter = new SpawnParticleEffectSceneAction();
        emitter.Actor = TARGET;
        emitter.Duration = "1000";
        emitter.ParticlesPerSecond = "800";
        emitter.LocalPosition = new Vector3(0, 1, 0);
        emitter.Color1 = new Color(.5f, .4f, .35f, .2f);
        emitter.Color2 = new Color(.2f, .2f, .21f, .5f);
        emitter.Color3 = new Color(.7f, .32f, 0f, 1f);
        emitter.Color4 = new Color(.7f, 0f, .9f, .6f);
        emitter.Color5 = new Color(.4f, 0f, .5f, .2f);
        emitter.ParticleLifeTime = "1";
        emitter.ParticleSize = ".2";
        emitter.ParticleSpeed = ".5";
        emitter.RandomVelocity = new Vector3(2, 7, 0);
        modifyStrength.Add(emitter);

        var modify = new ModifyStatSceneAction();
        modify.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Strength]";
        modify.Adjustment = "50";
        modify.Duration = "100";
        modifyStrength.Add(modify);

        loadedScenes.Add(LilithAbilities.ChannelEmpower, modifyStrength);
    }
}

public enum LilithAbilities
{
    Attack = 1, 
    Blizzard = 2, 
    Fireball = 3, 
    Heal = 4, 
    ChannelEmpower = 5,
}