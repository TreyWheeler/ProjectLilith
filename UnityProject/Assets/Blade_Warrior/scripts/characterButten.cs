
	
using UnityEngine;
using System.Collections;

public class characterButten : MonoBehaviour {

	public GameObject frog;
	
	
	
	private Rect FpsRect ;
	private string frpString;
	
	private GameObject instanceObj;
	public GameObject[] gameObjArray=new GameObject[9];
	public AnimationClip[] AniList  = new AnimationClip[4];
	
	float minimum = 2.0f;
	float maximum = 50.0f;
	float touchNum = 0f;
	string touchDirection ="forward"; 
	private GameObject Villarger_A_Girl_prefab;
	
	// Use this for initialization
	void Start () {
		
		//frog.animation["dragon_03_ani01"].blendMode=AnimationBlendMode.Blend;
		//frog.animation["dragon_03_ani02"].blendMode=AnimationBlendMode.Blend;
		//Debug.Log(frog.GetComponent("dragon_03_ani01"));
		
		//Instantiate(gameObjArray[0], gameObjArray[0].transform.position, gameObjArray[0].transform.rotation);
	}
	
	
 void OnGUI() {
	  if (GUI.Button(new Rect(20, 20, 70, 40),"Idle")){
		 frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Idle");
	  }
	    //if (GUI.Button(new Rect(90, 20, 70, 40),"Greeting")){
		 // frog.animation.wrapMode= WrapMode.Loop;
		  //	frog.animation.CrossFade("Greeting");
	 // }
		   if (GUI.Button(new Rect(90, 20, 70, 40),"Walk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Walk");
	  }
		   if (GUI.Button(new Rect(160, 20, 70, 40),"L_Walk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("L_Walk");
	  }
		   if (GUI.Button(new Rect(230, 20, 70, 40),"R_Walk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("R_Walk");
			
	  }
		   if (GUI.Button(new Rect(300, 20, 70, 40),"B_Walk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("B_Walk");
	  }
	     if (GUI.Button(new Rect(370, 20, 70, 40),"Talk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Talk");
	  } 
		if (GUI.Button(new Rect(440, 20, 70, 40),"Run")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Run00");
	  } 
			if (GUI.Button(new Rect(510, 20, 70, 40),"L_Run")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("L_Run00");
	  }
			if (GUI.Button(new Rect(580, 20, 70, 40),"R_Run")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("R_Run00");
	  }
			if (GUI.Button(new Rect(650, 20, 70, 40),"B_Run")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("B_Run00");
	  }
			if (GUI.Button(new Rect(720, 20, 70, 40),"Jump")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Jump00");
	  }
			if (GUI.Button(new Rect(20, 60, 70, 40),"DrawBlade")){
		  frog.animation.wrapMode= WrapMode.Once;
		  	frog.animation.CrossFade("DrawBlade");
	  } 
			if (GUI.Button(new Rect(90, 60, 70, 40),"ATK_standy")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Attack_standy");
	  }
			if (GUI.Button(new Rect(160, 60, 70, 40),"Attack")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Attack");
	  }
			if (GUI.Button(new Rect(230, 60, 70, 40),"Attack01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Attack01");
	  }
		if (GUI.Button(new Rect(300, 60, 70, 40),"Block")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Block");
	  }
			if (GUI.Button(new Rect(370, 60, 70, 40),"Attack02")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Attack02");
	  }
			if (GUI.Button(new Rect(440, 60, 70, 40),"Combo")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Combo");
	  }
				if (GUI.Button(new Rect(510, 60, 70, 40),"Skill")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  frog.animation.CrossFade("Skill");
		
	  }
				if (GUI.Button(new Rect(580, 60, 70, 40),"M_Avoid")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  frog.animation.CrossFade("M_Avoid");
		
	  }
			if (GUI.Button(new Rect(650, 60, 70, 40),"L_Avoid")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("L_Avoid");
	  }
				if (GUI.Button(new Rect(720, 60, 70, 40),"R_Avoid")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  frog.animation.CrossFade("R_Avoid");
		
	  }
				if (GUI.Button(new Rect(20, 100, 70, 40),"Buff")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  frog.animation.CrossFade("Buff");
		
	  }
			if (GUI.Button(new Rect(90, 100, 70, 40),"Run01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Run");
	  }
		if (GUI.Button(new Rect(160, 100, 70, 40),"L_Run01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("L_Run");
	  }
		if (GUI.Button(new Rect(230, 100, 70, 40),"R_Run01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("R_Run");
	  }
		if (GUI.Button(new Rect(300, 100, 70, 40),"B_Run01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("B_Run");
	  }
		if (GUI.Button(new Rect(370, 100, 70, 40),"Jump01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Jump");
	  }
		if (GUI.Button(new Rect(440, 100, 70, 40),"PickUp")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("PickUp");
	  }
			if (GUI.Button(new Rect(510, 100, 70, 40),"Damage")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Damage");
	  }
			if (GUI.Button(new Rect(580, 100, 70, 40),"Death")){
		  frog.animation.wrapMode= WrapMode.Once;
		  	frog.animation.CrossFade("Death");
	  }
		if (GUI.Button(new Rect(650, 100, 140, 40),"Gentleman")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Gentleman");
	  }
	//		if (GUI.Button(new Rect(230, 60, 140, 40),"GangnamStyle")){
	//	  frog.animation.wrapMode= WrapMode.Loop;
	//	  	frog.animation.CrossFade("GangnamStyle");
	//  }  
		//		if (GUI.Button(new Rect(580, 440, 140, 40),"V  1.2")){
		//  frog.animation.wrapMode= WrapMode.Loop;
		//  	frog.animation.CrossFade("Idle");
	 // } 
				if (GUI.Button(new Rect(640, 480, 140, 40),"Ver 2.0")){
		 frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Idle");
	  }
		
		
 }
	
	// Update is called once per frame
	void Update () {
		
		//if(Input.GetMouseButtonDown(0)){
		
			//touchNum++;
			//touchDirection="forward";
		 // transform.position = new Vector3(0, 0,Mathf.Lerp(minimum, maximum, Time.time));
			//Debug.Log("touchNum=="+touchNum);
		//}
		/*
		if(touchDirection=="forward"){
			if(Input.touchCount>){
				touchDirection="back";
			}
		}
	*/
		 
		//transform.position = Vector3(Mathf.Lerp(minimum, maximum, Time.time), 0, 0);
	if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		//frog.transform.Rotate(Vector3.up * Time.deltaTime*30);
	}
	
}
