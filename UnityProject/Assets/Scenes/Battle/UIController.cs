﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class UIController : MonoBehaviour
{
    public BattleScene BattleScene;
    public CharacterIconsWheel PlayerIconsWheel;
    public AbilityIconsWheel AbilityIconsWheel;
    public CharacterIconsWheel TargetIconWheel;
    public UITexture PopupBlocker;
    public TweenAlpha FadeIn;

    private Ability selectedAbility;
    private Character selectedPlayerCharacter;

    void Awake()
    {
        FadeIn.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {
        PlayerIconsWheel.ShowEnergy = true;
        PlayerIconsWheel.ClearAndAdd(BattleScene.Allies.OrderBy(c => c.name));
        PlayerIconsWheel.OnCharacterSelection += PlayerIconsWheel_OnCharacterSelection;
        AbilityIconsWheel.OnAbilitySelection += AbilityIconsWheel_OnAbilitySelection;
        TargetIconWheel.OnCharacterSelection += TargetIconsWheel_OnCharacterSelection;
        AbilityIconsWheel.gameObject.SetActive(false);
        TargetIconWheel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (selectedPlayerCharacter != null && !selectedPlayerCharacter.IsAlive)
        {
            AbilityIconsWheel.gameObject.SetActive(false);
            TargetIconWheel.gameObject.SetActive(false);
            selectedPlayerCharacter = null;
            selectedAbility = null;
        }
        if (BattleScene.IsMatchOver)
        {
            PopupBlocker.alpha = .5f;
            PopupBlocker.gameObject.SetActive(true);            
        }
    }


    void PlayerIconsWheel_OnCharacterSelection(Character character)
    {
        selectedPlayerCharacter = character;
        AbilityIconsWheel.NoteSelected(character);
        AbilityIconsWheel.ClearAndAdd(character.MyAbilities);
        AbilityIconsWheel.gameObject.SetActive(true);
        TargetIconWheel.gameObject.SetActive(false);
    }

    void AbilityIconsWheel_OnAbilitySelection(Ability ability)
    {
        if (selectedPlayerCharacter.GetCurrentEnergy() >= ability.cost)
        {
            selectedAbility = ability;
            AbilityIconsWheel.gameObject.SetActive(false);
            TargetIconWheel.gameObject.SetActive(true);
            if (ability.IsFriendly)
                TargetIconWheel.ClearAndAdd(BattleScene.Allies);
            else
                TargetIconWheel.ClearAndAdd(BattleScene.Enemies);
            TargetIconWheel.NoteSelected(ability);
        }
    }

    void TargetIconsWheel_OnCharacterSelection(Character obj)
    {
        selectedPlayerCharacter.Stats[LilithStats.Energy].CurrentValue -= selectedAbility.cost;
        selectedPlayerCharacter.QueueAbility(new IntendedAction(selectedAbility, obj.gameObject));
        TargetIconWheel.gameObject.SetActive(false);
        AbilityIconsWheel.gameObject.SetActive(false);
    }

}
