using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AbilityIconsWheel : MonoBehaviour
{
    private PositionButtons _positionButtons;
    private Character selectedCharacter;
    public event Action<Ability> OnAbilitySelection;
    public Dictionary<Ability, GameObject> abilityDictionary = new Dictionary<Ability, GameObject>();


    GameObject selectedCharacterPortrait;

    // Use this for initialization
    void Awake()
    {
        _positionButtons = this.EnsureComponent<PositionButtons>();
    }

    void Update()
    {
        //foreach (var item in abilityDictionary)
        //{
        //    UIButton buttonUI = item.Value.GetComponent<UIButton>();
        //    BoxCollider buttonBoxCollider = item.Value.GetComponent<BoxCollider>();
        //    if (item.Key.cost > selectedCharacter.GetCurrentEnergy())
        //    {
        //        buttonUI.isEnabled = false;
        //        buttonBoxCollider.enabled = false;
        //    }
        //    else
        //    {
        //        buttonUI.isEnabled = true;
        //        buttonBoxCollider.enabled = true;
        //    }
        //}
    }

    public void ClearAndAdd(IEnumerable<Ability> abilities)
    {
        abilityDictionary.Clear();
        _positionButtons.Clear();
        foreach (Ability ability in abilities)
        {
            AddAbilityButton(ability);
        }
        _positionButtons.Layout();
    }

    private void AddAbilityButton(Ability ability)
    {
        GameObject button = _positionButtons.Add(ability.TextureName, () => selectedCharacter.GetEnergy().CurrentValue >= ability.cost, () =>
        {
            if (OnAbilitySelection != null)
                OnAbilitySelection(ability);
        });
        abilityDictionary.Add(ability, button);
        EnergyIconWheel energyWheel = button.EnsureComponent<EnergyIconWheel>();
        energyWheel.energy = ability;
        energyWheel.constant = true;
    }
    public void NoteSelected(Character character)
    {
        selectedCharacter = character;
        if (selectedCharacterPortrait == null)
            selectedCharacterPortrait = new GameObject();

        selectedCharacterPortrait.transform.parent = this.gameObject.transform;
        selectedCharacterPortrait.transform.localPosition = Vector3.zero;
        selectedCharacterPortrait.transform.localScale = Vector3.one;
        DaemonButton button = selectedCharacterPortrait.EnsureComponent<DaemonButton>();
        button.EnsureComponent<UITexture>().mainTexture = Resources.Load<Texture2D>("Textures/" + character.TextureName);
    }

}
