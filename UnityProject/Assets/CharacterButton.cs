using UnityEngine;
using System.Collections;

public class CharacterButton : MonoBehaviour {

    public Character character;
    public RotateScript rotator;

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
        if (rotator)
            rotator.OnPress();
    }

    public void OnDrag()
    {
        if (rotator)
            rotator.OnDrag();
    }
}
