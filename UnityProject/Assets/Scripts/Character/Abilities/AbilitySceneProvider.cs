using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class AbilitySceneProvider
{
    private static Dictionary<LilithAbilities, SceneScript> loadedScenes = new Dictionary<LilithAbilities, SceneScript>();

    public static SceneScript GetBy(LilithAbilities ability)
    {
        if (!loadedScenes.ContainsKey(ability))
        {

            switch (ability)
            {
                case LilithAbilities.Attack:

                    SceneScript attackScene = new SceneScript();
                    
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

                    attackScene = new SceneScript();

                    drawSwordAnim = new RunAnimationSceneAction();
                    drawSwordAnim.Actor = "{Caster}";
                    drawSwordAnim.Animation = "Gentleman";
                    attackScene.Add(drawSwordAnim);
                    
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