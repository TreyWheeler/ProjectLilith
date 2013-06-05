using UnityEngine;
using System.Collections;

public class CardButton : MonoBehaviour
{

    public string buttonLabel;

    public int cardNumber = 1;
    public int cardWidth = 64;
    public int cardHeight = 64;
    private GameObject[] _userCardsOnScreen;

    private CommandCenter commandCenter;

    private const short MARGIN = 10;

    private int leftFloat = 0;

    private bool IsEnemyCard
    {
        get { return gameObject.tag == "EnemyCard"; }
    }

    // Use this for initialization
    void Start()
    {
        if (IsEnemyCard)
            leftFloat = Screen.width - cardWidth;

        commandCenter = GameObject.FindGameObjectWithTag("CC").GetComponent<CommandCenter>();
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(leftFloat, Screen.height - (cardHeight * cardNumber) - (MARGIN * cardNumber), cardWidth, cardHeight), buttonLabel))
        {
            if (IsEnemyCard)
            {
                commandCenter.SelectedEnemy = this;
            }
            else
            {
                commandCenter.SelectedPlayer = this;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
