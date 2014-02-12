using UnityEngine;
using System.Collections;

public class CharacterButton : MonoBehaviour {

    public Character character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        Debug.Log("Click");
    }

    public void OnPress()
    {
        this.SendMessageUpwards("BubbledOnPress");
    }

    public void OnDrag()
    {
        this.SendMessageUpwards("BubbledOnDrag");
    }
}
