using UnityEngine;
using System.Collections;
using System.Linq;

public class CommandCenter : MonoBehaviour
{

    public int buttonWidth = 64;
    public int buttonHeight = 64;
    public int buttonPadding = 50;
    private AbilityBar abilityBar;
    private EntityBar team1EntityBar;
    private EntityBar team2EntityBar;
    TimedTaskManager tasks;
    TimedEvent test;
    // Use this for initialization
    void Start()
    {        
        abilityBar = GetComponent<AbilityBar>();
        team1EntityBar = GetComponents<EntityBar>().First(bar => !bar.HugRightWall);
        team2EntityBar = GetComponents<EntityBar>().First(bar => bar.HugRightWall);

        team1EntityBar.SelectedEntityChanged += () =>
        {
            abilityBar.Abilities = team1EntityBar.SelectedEntity.abilities;
        };

        tasks = GameObject.FindGameObjectWithTag("Components").GetComponent<TimedTaskManager>();
    }
    bool buttonEnabled = true;
    void OnGUI()
    {
        int screenCenterX = Screen.width / 2;
        int screenCenterY = Screen.height / 2;

        if (team1EntityBar.SelectedEntity != null)
        {
            if (GUI.Button(new Rect(screenCenterX - buttonWidth - buttonPadding / 2, screenCenterY - buttonHeight / 2, buttonWidth, buttonHeight), team1EntityBar.SelectedEntity.Name))
            {

            }
        }

        if (team2EntityBar.SelectedEntity != null)
        {
            if (GUI.Button(new Rect(screenCenterX + buttonPadding / 2, screenCenterY - buttonHeight / 2, buttonWidth, buttonHeight), team2EntityBar.SelectedEntity.Name))
            {

            }
        }

        if (abilityBar.SelectedAbility != null)
        {
            GUI.Label(new Rect(screenCenterX - buttonWidth / 2, screenCenterY - buttonHeight - buttonHeight / 2 - buttonPadding / 2, buttonWidth, buttonHeight), abilityBar.SelectedAbility.DisplayName, GUI.skin.button);
        }

        if (abilityBar.SelectedAbility != null && team2EntityBar.SelectedEntity != null && team1EntityBar.SelectedEntity != null)
        {
            GUI.enabled = buttonEnabled;
            if (GUI.Button(new Rect(screenCenterX - buttonWidth / 2, screenCenterY + buttonHeight / 2 + buttonPadding / 2, buttonWidth, buttonHeight), "Go!"))
            {
                buttonEnabled = false;
                TimedEvent  test = new TimedEvent(1000, () => 
                {
                    buttonEnabled = true; 
                });
                tasks.Add(test);
                abilityBar.SelectedAbility.Do(team1EntityBar.SelectedEntity, team2EntityBar.SelectedEntity);
            }
            GUI.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
