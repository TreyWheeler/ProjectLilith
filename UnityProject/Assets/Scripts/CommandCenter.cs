using UnityEngine;
using System.Collections;

public class CommandCenter : MonoBehaviour
{

    public int buttonWidth = 64;
    public int buttonHeight = 64;

    public IAbility SelectedAbility;
    public CardButton SelectedPlayer;
    public CardButton SelectedEnemy;

    public int buttonPadding = 50;

    // Use this for initialization
    void Start()
    {

    }

    void OnGUI()
    {
        int screenCenterX = Screen.width / 2;
        int screenCenterY = Screen.height / 2;


        if (SelectedPlayer != null)
        {
            if (GUI.Button(new Rect(screenCenterX - buttonWidth - buttonPadding / 2, screenCenterY - buttonHeight / 2, buttonWidth, buttonHeight), SelectedPlayer.buttonLabel))
            {

            }
        }

        if (SelectedEnemy != null)
        {
            if (GUI.Button(new Rect(screenCenterX + buttonPadding / 2, screenCenterY - buttonHeight / 2, buttonWidth, buttonHeight), SelectedEnemy.buttonLabel))
            {

            }
        }

        if (SelectedAbility != null)
        {
            if (GUI.Button(new Rect(screenCenterX - buttonWidth / 2, screenCenterY - buttonHeight - buttonHeight / 2 - buttonPadding / 2, buttonWidth, buttonHeight), SelectedAbility.DisplayName))
            {

            }
        }

        if (SelectedAbility != null && SelectedEnemy != null && SelectedPlayer != null)
        {
            if (GUI.Button(new Rect(screenCenterX - buttonWidth / 2, screenCenterY + buttonHeight / 2 + buttonPadding / 2, buttonWidth, buttonHeight), "Go!"))
            {
                SelectedAbility.Do(SelectedPlayer, SelectedEnemy);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
