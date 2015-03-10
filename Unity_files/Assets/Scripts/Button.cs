using UnityEngine;
using System.Collections;
using Leap;


public class Button : MonoBehaviour {
	
	//scripts
	public sounds sounds;
	public AudioClip seed;
	public int seednumber;
	public Grab Grab;
	public FallandFloat FallandFloat;
	public Vector3  ButtonPosition;

	// Use this for initialization
	void Start () {


		
	}
	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){
		
		//Debug.Log ("Button pressed: " + this.name);

		if ((other.name == ("L_index_bone3")) || (other.name == ("R_index_bone3"))) {

			// Poke Glow Seed Button

			if (GameObject.Find ("GlowSeedButton").GetComponent<Button> ().seednumber == 0 && 
				GameObject.Find ("GhostSeedButton").GetComponent<Button> ().seednumber == 0 && 
				GameObject.Find ("CandySeedButton").GetComponent<Button> ().seednumber == 0) {

				audio.PlayOneShot (seed);

				seednumber ++;
                
                GenerateSeed();

						
				/*
				// Glow Seed Button Being Poked

				if(this.name == " GlowSeedButton"){

					Instantiate (GameObject.Find ("GlowSeed"), transform.position = this.transform.localPosition, transform.rotation = this.transform.localRotation);
					
					GameObject.Find ("GlowSeed(Clone)").GetComponent<FallandFloat>().SendMessage("Fall");
					//GameObject.Find ("GlowSeed(Clone)").GetComponent<FallandFloat>().SendMessage("Target",this.name);

					Debug.Log ("Glow Seed Generated!");
				
				}

				// Ghost Seed Button Being Poked

				if(this.name == " GhostSeedButton"){
					
					Instantiate (GameObject.Find ("GhostSeed"), transform.position = this.transform.localPosition, transform.rotation = this.transform.localRotation);
					
					GameObject.Find ("GhostSeed(Clone)").GetComponent<FallandFloat>().SendMessage("Fall");
					//GameObject.Find ("GlowSeed(Clone)").GetComponent<FallandFloat>().SendMessage("Target",this.name);

					
					Debug.Log ("Ghost Seed Generated!");
					
				}


				// Candy Seed Button Being Poked

				if(this.name == " CandySeedButton"){
					
					Instantiate (GameObject.Find ("CandySeed"), transform.position = this.transform.localPosition, transform.rotation = this.transform.localRotation);
					
					GameObject.Find ("CandySeed(Clone)").GetComponent<FallandFloat>().SendMessage("Fall");
					//GameObject.Find ("GlowSeed(Clone)").GetComponent<FallandFloat>().SendMessage("Target",this.name);

					
					Debug.Log ("Candy Seed Generated!");
					
				}
				*/
			}

		}
	}


	void GenerateSeed(){

		if (seednumber == 1) {
			
			if (this.name == " GlowSeedButton") {
				
				GameObject.Find ("GlowSeed").transform.position = this.transform.localPosition; 
				GameObject.Find ("GlowSeed").transform.rotation = this.transform.localRotation;
				
				GameObject.Find ("GlowSeed").GetComponent<FallandFloat> ().SendMessage ("Fall",ButtonPosition);
				//GameObject.Find ("GlowSeed(Clone)").GetComponent<FallandFloat>().SendMessage("Target",this.name);
				
				Debug.Log ("Glow Seed Generated!");
			}
			
			if (this.name == " GhostSeedButton") {
				
				GameObject.Find ("GhostSeed").transform.position = this.transform.localPosition; 
				GameObject.Find ("GhostSeed").transform.rotation = this.transform.localRotation;
				
				GameObject.Find ("GhostSeed").GetComponent<FallandFloat> ().SendMessage ("Fall",ButtonPosition);
				//GameObject.Find ("GlowSeed(Clone)").GetComponent<FallandFloat>().SendMessage("Target",this.name);
				
				Debug.Log ("Ghost Seed Generated!");
			}
			
			if (this.name == " CandySeedButton") {
				
				GameObject.Find ("CandySeed").transform.position = this.transform.localPosition; 
				GameObject.Find ("CandySeed").transform.rotation = this.transform.localRotation;
				
				GameObject.Find ("CandySeed").GetComponent<FallandFloat> ().SendMessage ("Fall", ButtonPosition);
				//GameObject.Find ("GlowSeed(Clone)").GetComponent<FallandFloat>().SendMessage("Target",this.name);
				
				Debug.Log ("Candy Seed Generated!");
			}
		}
	}

	// Update is called once per frame
	void Update () {

		ButtonPosition = this.transform.localPosition;

	}
}
