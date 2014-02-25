using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AbilityIconsWheel : MonoBehaviour
{
    private PositionButtons _positionButtons;
    public event Action<Ability> OnAbilitySelection;

    GameObject selectedCharacterPortrait;

    // Use this for initialization
    void Awake()
    {
        _positionButtons = this.EnsureComponent<PositionButtons>();
    }

    public void ClearAndAdd(IEnumerable<Ability> abilities)
    {
        List<ButtonDetails> details = new List<ButtonDetails>();
        foreach (Ability ability in abilities)
        {
            ButtonDetails newDetail = new ButtonDetails();
            newDetail.textureName = ability.TextureName;
            AddClick(ability, newDetail);
            details.Add(newDetail);
        }
        _positionButtons.ClearAndAdd(details);
    }

    private void AddClick(Ability ability, ButtonDetails newDetail)
    {
        newDetail.buttonClick += () =>
        {
            if (OnAbilitySelection != null)
                OnAbilitySelection(ability);
        };
    }
    public void NoteSelected(Character value)
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
