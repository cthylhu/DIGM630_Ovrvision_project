using UnityEngine;
using System.Collections;
using Leap;


public class Button : MonoBehaviour {
	
	//scripts
	public sounds sounds;
	public AudioClip seed;
	public static bool seedGenerated = false;
	public Grab Grab;
	public FallandFloat FallandFloat;
	public Vector3 ButtonPosition;
	public int buttonType;
	public static string CurrentSeed;


	// Use this for initialization
	void Start () {


	}
	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){
		
		//Debug.Log ("Button pressed: " + this.name);

		if ((other.name == ("L_index_bone3")) || (other.name == ("R_index_bone3"))) {

			audio.PlayOneShot (seed);
			Debug.Log ("Button pressed: "+this.name);

			//other.SendMessage("LastSeed",buttonType);

			if (this.name == "GlowSeedButton") {
				CurrentSeed = "GlowSeed";
				GenerateSeed(CurrentSeed);
				Debug.Log ("Glow Seed Generated!");
			}
			if (this.name == "GhostSeedButton") {
				CurrentSeed = "GhostSeed";
				GenerateSeed(CurrentSeed);
				Debug.Log ("Ghost Seed Generated!");
			}
			if (this.name == "CandySeedButton") {
				CurrentSeed = "CandySeed";
				GenerateSeed(CurrentSeed);
				Debug.Log ("Candy Seed Generated!");
			}

			seedGenerated = true;
			PlantingSeed.buttonPressed = buttonType; 
			/*if (GameObject.Find ("GlowSeedButton").GetComponent<Button>().seednumber == 0 && 
			   //GameObject.Find ("GhostSeedButton").GetComponent<Button>().seednumber == 0 &&

			    GameObject.Find ("CandySeedButton").GetComponent<Button> ().seednumber == 0) {

				//seednumber ++;
                
				if (seednumber == 1) {
					GenerateSeed();
				}
			}*/


		}
	}

	void GenerateSeed(string seedName){

		GameObject.Find (seedName).transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - 2f, this.transform.position.z); 
		//GameObject.Find (seedName).transform.rotation = this.transform.rotation;
		//DisableSeedRenders ();
		EnableSeedRenders(seedName);

		GameObject.Find (seedName).GetComponent<FallandFloat> ().SendMessage ("Fall", ButtonPosition);
	}


	void DisableSeedRenders (){
		// Disable existing seeds
		Renderer[] ghostList = GameObject.Find ("GhostSeed").GetComponentsInChildren<Renderer>();
		foreach (Renderer r in ghostList){
			r.enabled = false;
		}
		Renderer[] glowList = GameObject.Find ("GlowSeed").GetComponentsInChildren<Renderer>();
		foreach (Renderer r in glowList){
			r.enabled = false;
		}
		Renderer[] candyList = GameObject.Find ("CandySeed").GetComponentsInChildren<Renderer>();
		foreach (Renderer r in candyList){
			r.enabled = false;
		}
	}

	void EnableSeedRenders(string seedname){
		// Enable new seed
		Renderer[] list = GameObject.Find (seedname).GetComponentsInChildren<Renderer>();
		foreach (Renderer r in list) {
			Debug.Log ("Enable Renderer: "+r.name);
			r.enabled = true;
		}
	}

	// Update is called once per frame
	void Update () {


		//ButtonPosition = this.transform.position;
//		if (Grab.Grabbed) {
//			DisableSeedRenders();
//		}
	}
}
