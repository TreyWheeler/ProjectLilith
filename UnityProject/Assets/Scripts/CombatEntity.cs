using UnityEngine;
using System.Collections;

public class CombatEntity : MonoBehaviour
{

    public string buttonLabel;

    public int cardNumber = 1;
    public int cardWidth = 64;
    public int cardHeight = 64;
    public Ability[] abilities;
    private AbilityBar abilityBar;
    private CommandCenter commandCenter;

    private const short MARGIN = 10;

    private int leftFloat = 0;

    private bool IsEnemy
    {
        get { return gameObject.tag == "BlackTeam"; }
    }

    // Use this for initialization
    void Start()
    {
        if (buttonLabel == "Rock Lee")
        {
            Ability kick = new Ability() { DisplayName = "Kick" };
            Ability punch = new Ability() { DisplayName = "Punch" };
            Ability spit = new Ability() { DisplayName = "Spit" };
            Ability slap = new Ability() { DisplayName = "slap" };
            Ability knife = new Ability() { DisplayName = "knife" };
            Ability sword = new Ability() { DisplayName = "sword" };
            Ability club = new Ability() { DisplayName = "club" };
            Ability dagger = new Ability() { DisplayName = "dagger" };
            Ability bow = new Ability() { DisplayName = "bow" };
            abilities = new Ability[] { kick, punch, spit, slap, knife, sword, club, dagger, bow };
        }
        if (buttonLabel == "Ten Ten")
        {
            Ability kick = new Ability() { DisplayName = "balls" };
            Ability punch = new Ability() { DisplayName = "book" };
            Ability spit = new Ability() { DisplayName = "magic" };
            Ability slap = new Ability() { DisplayName = "show" };
            abilities = new Ability[] { kick, punch, spit, slap };
        }
        if (buttonLabel == "Neji")
        {
            Ability kick = new Ability() { DisplayName = "nothing" };
            Ability punch = new Ability() { DisplayName = "special" };
            Ability spit = new Ability() { DisplayName = "house" };
            abilities = new Ability[] { kick, punch, spit };
        }
        if (IsEnemy)
            leftFloat = Screen.width - cardWidth;

        abilityBar = GameObject.FindGameObjectWithTag("AB").GetComponent<AbilityBar>();
        commandCenter = GameObject.FindGameObjectWithTag("CC").GetComponent<CommandCenter>();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(leftFloat, Screen.height - (cardHeight * cardNumber) - (MARGIN * cardNumber), cardWidth, cardHeight), buttonLabel))
        {
            if (IsEnemy)
            {
                commandCenter.SelectedEnemy = this;
            }
            else
            {
                if (commandCenter.SelectedPlayer != this)
                {
                    commandCenter.SelectedPlayer = this;
                    commandCenter.SelectedAbility = null;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
