using UnityEngine;
using System.Collections;

public class EntityBar : MonoBehaviour
{
    private int margin = 10;
    private int cardHeight = 64;
    private int cardWidth = 64;
    private CombatEntity[] CombatEntities;
    private CommandCenter commandCenter;
    public bool HugRightWall;
    
    // Use this for initialization
    void Start()
    {
        commandCenter = GameObject.FindGameObjectWithTag("CC").GetComponent<CommandCenter>();
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
        int XCoordinate = HugRightWall ? Screen.width - margin - cardWidth : margin;
        
        for(int i = 0; i < CombatEntities.Length; i++)
        {
            if(GUI.Button(new Rect(XCoordinate, Screen.height - ((cardHeight + margin) * (i + 1)), cardWidth, cardHeight), CombatEntities[i].Name))
            {
                if(HugRightWall)                
                    commandCenter.SelectedEnemy = CombatEntities[i];
                else
                    commandCenter.SelectedPlayer = CombatEntities[i];
            }
        }
    }
}
