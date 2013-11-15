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

                    PlaySoundSceneAction swordHitSound = new PlaySoundSceneAction();
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
                    idleAnim.Animation = "Idle";
                    attackScene.Add(idleAnim);

                    loadedScenes.Add(ability, attackScene);

                    break;
                case LilithAbilities.Victory:

                    attackScene = new SceneActionList();

                    drawSwordAnim = new RunAnimationSceneAction();
                    drawSwordAnim.RunOnce = "true";
                    drawSwordAnim.Actor = "{Caster}";
                    drawSwordAnim.Animation = "Gentleman";
                    attackScene.Add(drawSwordAnim);

                    idleAnim = new RunAnimationSceneAction();
                    idleAnim.Actor = "{Caster}";
                    idleAnim.Animation = "Idle";
                    attackScene.Add(idleAnim);


                    loadedScenes.Add(ability, attackScene);

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
    Attack, Victory
}