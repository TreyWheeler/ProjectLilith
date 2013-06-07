using UnityEngine;
using System.Collections;

public class AbilityBar : MonoBehaviour {

    private CommandCenter _commandCenter;
    private int _margin = 10;
    private Ability[] _abilities;
    private int width = 64;
    private int height = 64;

	// Use this for initialization
	void Start () {

        _commandCenter = GameObject.FindGameObjectWithTag("CC").GetComponent<CommandCenter>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (_commandCenter.SelectedPlayer != null)
        {
            Ability[] abilities = _commandCenter.SelectedPlayer.abilities;
            int abilityCount = abilities.Length;
            if (abilityCount % 2 == 0)
            {
                for (int i = 0; i < abilityCount; i++)
                {
                    Ability currentAbility = abilities[i];
                    if (i % 2 == 0)
                    {
                        if (GUI.Button(new Rect(Screen.width / 2 + _margin + ((width + _margin) * (i / 2)), _margin, width, height), currentAbility.DisplayName))
                        {
                            _commandCenter.SelectedAbility = currentAbility;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(Screen.width / 2 - ((width + _margin) * ((i + 1) / 2)), _margin, width, height), currentAbility.DisplayName))
                        {
                            _commandCenter.SelectedAbility = currentAbility;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < abilityCount; i++)
                {
                    Ability currentAbility = abilities[i];
                    if (i % 2 == 0)
                    {
                        if (GUI.Button(new Rect(Screen.width / 2 - width / 2 + ((width + _margin) * (i / 2)), _margin, width, height), currentAbility.DisplayName))
                        {
                            _commandCenter.SelectedAbility = currentAbility;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(Screen.width / 2 - width / 2 - ((width + _margin) * ((i + 1) / 2)), _margin, width, height), currentAbility.DisplayName))
                        {
                            _commandCenter.SelectedAbility = currentAbility;
                        }
                    }
                }
            }
        }
    }
}
