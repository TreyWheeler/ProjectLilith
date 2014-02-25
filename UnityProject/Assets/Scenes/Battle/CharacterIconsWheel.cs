using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterIconsWheel : MonoBehaviour
{

    private PositionButtons _positionButtons;
    public event Action<Character> OnCharacterSelection;
    
    GameObject selectedCharacterPortrait;

    void Awake()
    {
        _positionButtons = this.EnsureComponent<PositionButtons>();
    }
    public void ClearAndAdd(IEnumerable<Character> characters)
    {
        List<ButtonDetails> details = new List<ButtonDetails>();
        foreach (Character character in characters)
        {
            ButtonDetails newDetail = new ButtonDetails();
            newDetail.textureName = character.TextureName;
            newDetail.isEnabled = character.IsAlive;
            AddClick(character, newDetail);
            details.Add(newDetail);
        }
        _positionButtons.ClearAndAdd(details);
    }

    private void AddClick(Character character, ButtonDetails newDetail)
    {
        newDetail.buttonClick += () =>
        {
            if (OnCharacterSelection != null && character.IsAlive)
                OnCharacterSelection(character);
        };
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
