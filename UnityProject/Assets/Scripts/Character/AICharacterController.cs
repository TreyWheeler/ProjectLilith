using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IoC;

public class AICharacterController : MonoBehaviour
{
    Character character;

    BattleScene scene;

    float randomEnergyValue;

    // Use this for initialization
    void Start()
    {
        scene = GameObject.Find("GameController").GetComponent<BattleScene>();
        character = this.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!character.IsAlive)
            return;

        if (randomEnergyValue == 0)
        {
            randomEnergyValue = Random.Range(character.Stats[LilithStats.Energy].MinValue, character.Stats[LilithStats.Energy].MaxValue);
        }

        if (character.Stats[LilithStats.Energy].CurrentValue >= randomEnergyValue)
        {
            UseRandomAbility();
        }
    }

    public void UseRandomAbility()
    {
        int abilityIndex = Mathf.RoundToInt(Random.Range(-0.49f, character.MyAbilities.Length - 1 + 0.49f));
        var ability = character.MyAbilities[abilityIndex];

        if (ability.cost <= character.Stats[LilithStats.Energy].CurrentValue)
        {
            List<Character> targets;
            if (ability.IsFriendly)
                targets = scene.Enemies;
            else
                targets = scene.Allies;

            int targetIndex = Mathf.RoundToInt(Random.Range(-0.49f, targets.Count - 1 + 0.49f));
            var target = targets[targetIndex];
            if (target.IsAlive)
                character.UseAbility(ability, target);
        }

        randomEnergyValue = 0;
    }
}
