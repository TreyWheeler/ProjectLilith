using UnityEngine;
using System.Collections;
using System;

public class DaemonButton : MonoBehaviour {

    public event Action click;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        if (click != null)
            click();
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
