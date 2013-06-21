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
    
    // Use this for initialization
    void Start()
    {
        abilityBar = GetComponent<AbilityBar>();
        team1EntityBar = GetComponents<EntityBar>().First(bar => !bar.HugRightWall);
        team2EntityBar = GetComponents<EntityBar>().First(bar => bar.HugRightWall);
        
        team1EntityBar.SelectedEntityChanged += () => {
            abilityBar.Abilities = team1EntityBar.SelectedEntity.abilities; };
        
        tasks = GameObject.FindGameObjectWithTag("Components").GetComponent<TimedTaskManager>();
    }

    void OnGUI()
    {
        int screenCenterX = Screen.width / 2;
        int screenCenterY = Screen.height / 2;

        GUI.Button(new Rect(screenCenterX, 0, 0, Screen.height), "A");

        if(team1EntityBar.SelectedEntity != null)
        {
            if(GUI.Button(new Rect(screenCenterX - buttonWidth - buttonPadding / 2, screenCenterY - buttonHeight / 2, buttonWidth, buttonHeight), team1EntityBar.SelectedEntity.Name))
            {

            }
        }

        if(team2EntityBar.SelectedEntity != null)
        {
            if(GUI.Button(new Rect(screenCenterX + buttonPadding / 2, screenCenterY - buttonHeight / 2, buttonWidth, buttonHeight), team2EntityBar.SelectedEntity.Name))
            {

            }
        }

        if(abilityBar.SelectedAbility != null)
        {
            if(GUI.Button(new Rect(screenCenterX - buttonWidth / 2, screenCenterY - buttonHeight - buttonHeight / 2 - buttonPadding / 2, buttonWidth, buttonHeight), abilityBar.SelectedAbility.DisplayName))
            {

            }
        }

        if(abilityBar.SelectedAbility != null && team2EntityBar.SelectedEntity != null && team1EntityBar.SelectedEntity != null)
        {
            if(GUI.Button(new Rect(screenCenterX - buttonWidth / 2, screenCenterY + buttonHeight / 2 + buttonPadding / 2, buttonWidth, buttonHeight), "Go!"))
            {
                abilityBar.SelectedAbility.Do(team1EntityBar.SelectedEntity, team2EntityBar.SelectedEntity);
                
                IntTween tween = new IntTween();
                tween.From = buttonWidth * 3;
                tween.To = buttonWidth;
                tween.Duration = 1000;
                tween.CurrentValueChanged += (e) => buttonWidth = e.NewValue;
                tasks.Add(tween);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
