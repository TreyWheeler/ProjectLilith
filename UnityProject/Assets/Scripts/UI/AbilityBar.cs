using UnityEngine;
using System.Collections;
using System;

public class AbilityBar : MonoBehaviour
{
    private int _margin = 10;
    private int width = 64;
    private int height = 64;
    
    public event Action SelectedAbilityChanged;
    
    private Ability[] _abilities;

    public Ability[] Abilities
    { 
        get
        {
            return _abilities;
        }
        set
        {
            if(_abilities != value)
            {
                _abilities = value;
                SelectedAbility = null;
            }            
        }
    }
    
    private Ability _selectedAbility;

    public Ability SelectedAbility
    {
        get
        {
            return _selectedAbility;
        }
        set
        {
            if(_selectedAbility != value)
            {
                _selectedAbility = value;
                if(SelectedAbilityChanged != null)
                    SelectedAbilityChanged();
            }
        }
    }
    
    
    // Use this for initialization
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void Update()
    { 
    }
 
    void OnGUI()
    {            
        if(Abilities != null)
        {
            int totalWidth = (width * Abilities.Length) + ((Abilities.Length - 1) * _margin); 
            int startX = (Screen.width - totalWidth) / 2;
            
            for(int i = 0; i < Abilities.Length; i++)
            {
                Ability currentAbility = Abilities[i];
                
                if(GUI.Button(new Rect(startX + (i * (width + _margin)), _margin, width, height), currentAbility.DisplayName))
                {
                    SelectedAbility = currentAbility;
                }
            }
        }
    }
}
