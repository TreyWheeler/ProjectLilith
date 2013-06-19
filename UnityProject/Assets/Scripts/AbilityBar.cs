using UnityEngine;
using System.Collections;

public class AbilityBar : MonoBehaviour
{

    private CommandCenter _commandCenter;
    private int _margin = 10;
    private Ability[] _abilities;
    private int width = 64;
    private int height = 64;

    // Use this for initialization
    void Start()
    {
        _commandCenter = GameObject.FindGameObjectWithTag("CC").GetComponent<CommandCenter>();
    }
 
    // Update is called once per frame
    void Update()
    {
 
    }
 
    void OnGUI()
    {
        if(_commandCenter.SelectedPlayer != null)
        {
            Ability[] abilities = _commandCenter.SelectedPlayer.abilities;
            
            int totalWidth = (width * abilities.Length) + ((abilities.Length - 1) * _margin); 
            int startX = (Screen.width - totalWidth) / 2;
            
            for(int i = 0; i < abilities.Length; i++)
            {
                Ability currentAbility = abilities[i];
                
                if(GUI.Button(new Rect(startX + (i * (width + _margin)), _margin, width, height), currentAbility.DisplayName))
                {
                    _commandCenter.SelectedAbility = currentAbility;
                }
            }
        }
    }
}
