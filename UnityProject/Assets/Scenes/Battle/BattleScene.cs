using UnityEngine;
using System.Collections;

public class BattleScene : MonoBehaviour {

	void Start () {
        var clip = Resources.Load("sounds/onewingedangel") as AudioClip;
        audio.loop = true;
        audio.PlayOneShot(clip, .01f);

        Screen.orientation = ScreenOrientation.Landscape;
	}
	
	void Update () {
	
	}
}
