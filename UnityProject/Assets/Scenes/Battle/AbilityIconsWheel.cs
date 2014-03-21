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
        selectedCharacterPortrait.transform.localRotation = Quaternion.identity;
        DaemonButton button = selectedCharacterPortrait.EnsureComponent<DaemonButton>();
        var texture = button.EnsureComponent<UITexture>();
        texture.mainTexture = Resources.Load<Texture2D>("Textures/" + character.TextureName);
        texture.depth = 10;

        EnergyIconWheel energyWheel = button.EnsureComponent<EnergyIconWheel>();
        energyWheel.energy = character;

        var energyOutline = selectedCharacterPortrait.AddComponent<EnergyIconOutline>();
        energyOutline.Name = "Energy Outline";
        energyOutline.Color = new Color(1, .82f, .3f);//#FFD24D
        energyOutline.ProgressDelegate = () => { return character.Stats[LilithStats.Energy].CurrentValue % 1f; };
        energyOutline.Clockwise = false;
        energyOutline.DegreeStart = 270;
        energyOutline.DegreeEnd = 360;
        energyOutline.Depth = 5;
        energyOutline.Diameter = 122;

        var energyVialOutline = selectedCharacterPortrait.AddComponent<EnergyIconOutline>();
        energyVialOutline.Name = "Energy Vial Outline";
        energyVialOutline.Color = new Color(0, 0, 0, .8f);
        energyVialOutline.ProgressDelegate = () => { return 1f; };
        energyVialOutline.DegreeStart = 0;
        energyVialOutline.DegreeEnd = 90;
        energyVialOutline.Depth = 3;
        energyVialOutline.Diameter = 122;


        var healthVialOutline = selectedCharacterPortrait.AddComponent<EnergyIconOutline>();
        healthVialOutline.Name = "Health Vial Outline";
        healthVialOutline.Color = new Color(0, 0, 0, .8f);
        healthVialOutline.ProgressDelegate = () => { return 1f; };
        healthVialOutline.DegreeStart = 90;
        healthVialOutline.DegreeEnd = 180;
        healthVialOutline.Depth = 3;
        healthVialOutline.Diameter = 122;

        var healthOutline = selectedCharacterPortrait.AddComponent<EnergyIconOutline>();
        healthOutline.Name = "Health Outline";
        healthOutline.Color = new Color(1, 0, 0);
        healthOutline.ProgressDelegate = () => { return character.Stats[LilithStats.Health].CurrentRatio; };
        healthOutline.DegreeStart = 90;
        healthOutline.DegreeEnd = 180;
        healthOutline.Depth = 4;
        healthOutline.Diameter = 122;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        this.GetComponent<UIPlayTween>().Play(true);
    }

}
