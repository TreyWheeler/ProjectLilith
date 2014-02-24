using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AbilityIconsWheel : MonoBehaviour
{

    private PositionButtons _positionButtons;
    public event Action<Ability> OnAbilitySelection;
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
}
