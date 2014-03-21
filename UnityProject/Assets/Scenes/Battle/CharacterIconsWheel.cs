using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterIconsWheel : MonoBehaviour
{

    private PositionButtons _positionButtons;
    public event Action<Character> OnCharacterSelection;
    public bool ShowEnergy;
    GameObject selectedCharacterPortrait;

    void Awake()
    {
        _positionButtons = this.EnsureComponent<PositionButtons>();
    }
    public void ClearAndAdd(IEnumerable<Character> characters)
    {
        _positionButtons.Clear();
        foreach (Character character in characters)
        {
            AddCharacterButton(character);
        }
        _positionButtons.Layout();
    }

    private void AddCharacterButton(Character character)
    {
        GameObject button = _positionButtons.Add(character.TextureName, () => character.IsAlive, () =>
        {
            if (OnCharacterSelection != null && character.IsAlive)
                OnCharacterSelection(character);
        });

        if (ShowEnergy)
        {
            EnergyIconWheel energyWheel = button.EnsureComponent<EnergyIconWheel>();
            energyWheel.energy = character;

            var energyOutline = button.AddComponent<EnergyIconOutline>();
            energyOutline.Name = "Energy Outline";
            energyOutline.Color = new Color(1, .82f, .3f);//#FFD24D
            energyOutline.ProgressDelegate = () => { return character.Stats[LilithStats.Energy].CurrentValue % 1f; };
            energyOutline.Clockwise = false;
            energyOutline.DegreeStart = 270;
            energyOutline.DegreeEnd = 360;
            energyOutline.Depth = 5;
            energyOutline.Diameter = 122;

            var energyVialOutline = button.AddComponent<EnergyIconOutline>();
            energyVialOutline.Name = "Energy Vial Outline";
            energyVialOutline.Color = new Color(0, 0, 0, .8f);
            energyVialOutline.ProgressDelegate = () => { return 1f; };
            energyVialOutline.DegreeStart = 0;
            energyVialOutline.DegreeEnd = 90;
            energyVialOutline.Depth = 3;
            energyVialOutline.Diameter = 122;
        }

        var healthVialOutline = button.AddComponent<EnergyIconOutline>();
        healthVialOutline.Name = "Health Vial Outline";
        healthVialOutline.Color = new Color(0, 0, 0, .8f);
        healthVialOutline.ProgressDelegate = () => { return 1f; };
        healthVialOutline.DegreeStart = 90;
        healthVialOutline.DegreeEnd = 180;
        healthVialOutline.Depth = 3;
        healthVialOutline.Diameter = 122;

        var healthOutline = button.AddComponent<EnergyIconOutline>();
        healthOutline.Name = "Health Outline";
        healthOutline.Color = new Color(1, 0, 0);
        healthOutline.ProgressDelegate = () => { return character.Stats[LilithStats.Health].CurrentRatio; };
        healthOutline.DegreeStart = 90;
        healthOutline.DegreeEnd = 180;
        healthOutline.Depth = 4;
        healthOutline.Diameter = 122;
    }


    public void NoteSelected(Ability value)
    {
        if (selectedCharacterPortrait == null)
            selectedCharacterPortrait = new GameObject();

        selectedCharacterPortrait.transform.parent = this.gameObject.transform;
        selectedCharacterPortrait.transform.localPosition = Vector3.zero;
        selectedCharacterPortrait.transform.localScale = Vector3.one;
        DaemonButton button = selectedCharacterPortrait.EnsureComponent<DaemonButton>();
        button.EnsureComponent<UITexture>().mainTexture = Resources.Load<Texture2D>("Textures/" + value.TextureName);
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
