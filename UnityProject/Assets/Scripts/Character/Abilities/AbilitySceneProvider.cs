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
                    drawSwordAnim.RunOnce = "True";
                    drawSwordAnim.Actor = "{Caster}";
                    drawSwordAnim.Animation = "DrawBlade";
                    attackScene.Add(drawSwordAnim);                    

                    RunAnimationSceneAction anim = new RunAnimationSceneAction();
                    anim.Actor = "{Caster}";
                    anim.Animation = "Run";
                    attackScene.Add(anim);

                    MoveToEntitySceneAction movePart = new MoveToEntitySceneAction();
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
                case LilithAbilities.TurtleBoom:

                    SceneActionList blizzard = new SceneActionList();

                    var castingAnim = new RunAnimationSceneAction();
                    castingAnim.Actor = "{Caster}";
                    castingAnim.Animation = "Gentleman";
                    blizzard.Add(castingAnim);
                    
                    var emitter = new SpawnParticleEffectSceneAction();
                    emitter.Actor = "{Caster}";
                    emitter.Target = "{Target}";
                    blizzard.Add(emitter);
                    
                    var blizzardSound = new PlaySoundSceneAction();
                    blizzardSound.Sound = "Sounds/wind01 l";
                    blizzardSound.Actor = "{Caster}";
                    blizzard.Add(blizzardSound);

                    blizzardSound = new PlaySoundSceneAction();
                    blizzardSound.Sound = "Sounds/wind02 l";
                    blizzardSound.Actor = "{Caster}";
                    blizzard.Add(blizzardSound);

                    blizzardSound = new PlaySoundSceneAction();
                    blizzardSound.Sound = "Sounds/wind03 l";
                    blizzardSound.Actor = "{Caster}";
                    blizzard.Add(blizzardSound);

                    dmg = new AdjustStatSceneAction();
                    dmg.Adjustment = "{Caster}<Character>.Stats[LilithStats.Strength].CurrentValue * -10";
                    dmg.StatToAdjust = "{Target}<Character>.Stats[LilithStats.Health]";
                    dmg.OverSeconds = "6.241";
                    blizzard.Add(dmg);                    
                    
                    idleAnim = new RunAnimationSceneAction();
                    idleAnim.Actor = "{Caster}";
                    idleAnim.Animation = "Idle";
                    blizzard.Add(idleAnim);
                    

                    loadedScenes.Add(ability, blizzard);

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
    Attack, TurtleBoom
}