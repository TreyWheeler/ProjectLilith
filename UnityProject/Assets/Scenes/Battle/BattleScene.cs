using UnityEngine;
using System.Collections;

public class BattleScene : MonoBehaviour {

	void Start () {
        var clip = Resources.Load("sounds/onewingedangel") as AudioClip;
        audio.PlayOneShot(clip, .1f);
	}
	
	void Update () {
	
	}
}
