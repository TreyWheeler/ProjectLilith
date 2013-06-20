using UnityEngine;
using System.Collections;
using System;

public class EntityBar : MonoBehaviour
{
    private int margin = 10;
    private int cardHeight = 64;
    private int cardWidth = 64;
    private CombatEntity[] CombatEntities;
    public bool HugRightWall;
    
    public event Action SelectedEntityChanged;
    
    public CombatEntity _selectedEntity;

    public CombatEntity SelectedEntity
    {
        get
        {
            return _selectedEntity;
        }
        
        set
        {
            if(_selectedEntity != value)
            {
                _selectedEntity = value;
                if(SelectedEntityChanged != null)
                    SelectedEntityChanged();
            }
        }
    }
    
    
    // Use this for initialization
    void Start()
    {
        CombatEntities = new CombatEntity[]
        { 
            new CombatEntity() 
            { 
                Name = "Trey", 
                abilities = new Ability[] 
                {
                    new Ability() 
                    { 
                        DisplayName = "Procrastinate" 
                    }, 
                    new Ability() 
                    { 
                        DisplayName = "Weeeeeeeeeeeeee" 
                    }, 
                    new Ability() 
                    { 
                        DisplayName = "Get Fat" 
                    }, 
                    new Ability() 
                    { 
                        DisplayName = "Wiz on the Electric Fence" 
                    }
                }
            },
            
            new CombatEntity() 
            { 
                Name = "Roy", 
                abilities = new Ability[] 
                {
                    new Ability() 
                    { 
                        DisplayName = "Shrug" 
                    }, 
                    new Ability() 
                    { 
                        DisplayName = "Scratch" 
                    }, 
                    new Ability() 
                    { 
                        DisplayName = "Code" 
                    } 
                }
            } 
        };
    }
 
    void OnGUI()
    {
        if(CombatEntities != null)
        {
            int XCoordinate = HugRightWall ? Screen.width - margin - cardWidth : margin;
        
            for(int i = 0; i < CombatEntities.Length; i++)
            {
                //string healthAsPercent = 
                
                if(GUI.Button(new Rect(XCoordinate, Screen.height - ((cardHeight + margin) * (i + 1)), cardWidth, cardHeight), CombatEntities[i].Name))
                {
                    SelectedEntity = CombatEntities[i];
                }
            }
        }
    }
}
