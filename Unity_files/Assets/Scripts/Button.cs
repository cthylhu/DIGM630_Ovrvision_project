using UnityEngine;
using System.Collections;
using Leap;


public class Button : MonoBehaviour {
	
	//scripts
	public narration narration;
	public sounds sounds;
	public AudioClip seed;
	public static bool seedGenerated = false;
	Vector3 ButtonPosition;
	public int buttonType;
	public static string CurrentSeed;
	public int pickseedcount;

	//public bool droptosoil;

	void DisableSeedRenders (){
		Renderer[] list = GameObject.Find ("AllSeeds").GetComponentsInChildren<Renderer>();
		foreach (Renderer r in list) {
			r.enabled = false;
		}
	}
	void EnableSeedRender (string name){
		DisableSeedRenders ();
		Renderer[] list = GameObject.Find (name).GetComponentsInChildren<Renderer>();
		foreach (Renderer r in list) {
			r.enabled = true;
			string buttonname = name+"Button";
			//Debug.Log ("Button name: "+buttonname);
			GameObject.Find (name).transform.position = new Vector3 (GameObject.Find (buttonname).transform.position.x, 
			                                                         GameObject.Find (buttonname).transform.position.y - 2f, 
			                                                         GameObject.Find (buttonname).transform.position.z); 
		}
	}
	
	void Fall (Vector3 position){
		ButtonPosition = position;
		Debug.Log ("Set target button position!");
		
	}
	
	// Use this for initialization
	void Start () {
		//droptosoil = false;
		DisableSeedRenders ();
	}
	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){
		
		//Debug.Log ("Button pressed: " + this.name);

		if ((other.name == ("L_index_bone3")) || (other.name == ("R_index_bone3"))) {
			audio.PlayOneShot (seed);
			//Debug.Log ("Button pressed: "+this.name);

			//other.SendMessage("LastSeed",buttonType);


			//pick seed for the first time, give narration!

			if(pickseedcount == 0){
				narration.audio.PlayOneShot(narration.Intro7);
				pickseedcount=1;
			}


			if (this.name == "GlowSeedButton") {
				CurrentSeed = "GlowSeed";
				Debug.Log ("Glow Seed Generated!");
			}
			if (this.name == "GhostSeedButton") {
				CurrentSeed = "GhostSeed";
				Debug.Log ("Ghost Seed Generated!");
			}
			if (this.name == "CandySeedButton") {
				CurrentSeed = "CandySeed";
				Debug.Log ("Candy Seed Generated!");
			}
			seedGenerated = true;
			PlantingSeed.buttonPressed = buttonType; 

		}
	}
	
	void Update () {
		
		if (seedGenerated) {
			if (!Grab.Grabbed) {
				//Debug.Log("Enabling button seed");
				EnableSeedRender(CurrentSeed);
				GameObject.Find (CurrentSeed).transform.Rotate(0f, 200f * Time.deltaTime, 0f);
			}
			else{
				DisableSeedRenders ();
			}
		}
		else {
			DisableSeedRenders ();
		}
	}

}
