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
        }
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
}
