using UnityEngine;
using System.Collections;
using Leap;

public class Grab : MonoBehaviour {
	
	Controller Controller = new Controller ();
	public static bool Grabbed = false;
	public static bool fallingSeedSpawned = false;
	string RenderThis;

	public Righthand Righthand;
	public Lefthand Lefthand;
	//public sounds sounds;
	public bool droptosoil;

	// Gesture Variables
	Frame frame;
	Hand rightmost;
	Hand leftmost;
	float Grab_L;
	float Grab_R;
	public static float Pinch_L;
	public static float Pinch_R;
	float roll_l;
	float roll_r;
	bool palmdown_l;
	bool palmdown_r;
	int handnum;
	
	public enum GestureState
	{
		start,
		middle_L,
		middle_R,
		end
	}

	public GestureState GrabSeed;

	// Use this for initialization
	void Start () {
		droptosoil = false;
		GrabSeed = GestureState.start;
	}
	
	// Update is called once per frame
	
	void Update () {
		
		frame = Controller.Frame ();
		rightmost = frame.Hands.Rightmost;
		leftmost = frame.Hands.Leftmost;
		Grab_L = leftmost.GrabStrength;
		Grab_R = rightmost.GrabStrength;
		Pinch_L = rightmost.PinchStrength;
		Pinch_R = leftmost.PinchStrength;
		roll_l = leftmost.PalmNormal.Roll * 180.0f / Mathf.PI;
		roll_r = rightmost.PalmNormal.Roll * 180.0f / Mathf.PI;
		palmdown_l = roll_l <= -140 || roll_l >= 140;
		palmdown_r = roll_r <= -140 || roll_r >= 140;
		handnum = frame.Hands.Count;


			
				switch (GrabSeed) {

				case GestureState.start:

					if (Button.seedGenerated && handnum > 0) {			// If a button was poked
						//Debug.Log ("At start gesture state!");
						if (Pinch_L > 0.8) {
							Grabbed = true;
							Button.seedGenerated = false;
							//GameObject.Find ("Button").SendMessage ("DisableSeedRenders");
							GrabSeed = GestureState.middle_L;
						} 

						if (Pinch_R > 0.8) {
							Grabbed = true;
							Button.seedGenerated = false;
							//GameObject.Find ("Button").SendMessage ("DisableSeedRenders");
							GrabSeed = GestureState.middle_R;
						} 
					}
					break;


				case GestureState.middle_R:

				if(GameObject.Find ("CleanRobotFullRightHand(Clone)") !=null){
					if (rightmost.IsRight || leftmost.IsLeft) {

						//Debug.Log ("Got to middle R gesture state!");
						//Debug.Log("Hand #: "+handnum);
						if (Button.CurrentSeed == "CandySeed") {
							RenderThis = "R_CandySeed_prefab";
							EnableHandSeedRender(RenderThis);
							//this.transform.position = GameObject.Find ("rightpalm").transform.position;
							Debug.Log ("CandySeed grabbed");

						}
						if (Button.CurrentSeed == "GhostSeed") {
							RenderThis = "R_GhostSeed_prefab";
							EnableHandSeedRender(RenderThis);
							Debug.Log ("GhostSeed grabbed");

						}
						if (Button.CurrentSeed == "GlowSeed") {
							RenderThis = "R_GlowSeed_prefab";
							EnableHandSeedRender(RenderThis);
							Debug.Log ("GlowSeed grabbed");

						}

						if (Pinch_R ==0 ) {
							GrabSeed = GestureState.end;
						}
					}
				}
				else {
					//Debug.Log ("Hand num: "+handnum);
					GrabSeed = GestureState.start;
					DisableHandSeedRender();
				}
				break;

				case GestureState.middle_L:

				if (handnum > 0) {
					if (Button.CurrentSeed == "CandySeed") {
						EnableHandSeedRender("L_CandySeed_prefab");

					}
					if (Button.CurrentSeed == "GhostSeed") {
						EnableHandSeedRender("L_GhostSeed_prefab");


					}
					if (Button.CurrentSeed == "GlowSeed") {
						EnableHandSeedRender("L_GlowSeed_prefab");

					}

					if (Pinch_L ==0 ) {
						GrabSeed = GestureState.end; 
						
					}
				}
				else {
					GrabSeed = GestureState.start;
				}
				break;

				case GestureState.end:
				//Debug.Log ("********Reached End state!*******");

				if (handnum > 0 && Grabbed) {
					//Debug.Log ("Falling Seed VVVV");

					if (!fallingSeedSpawned){
						//GameObject fallingSeed = Instantiate(Resources.Load("TestSphere"), GameObject.Find (RenderThis).transform.position, new Quaternion(0,0,0,0)) as GameObject;

						// SEED FALL TO GOUND!
						DisableHandSeedRender();
						if (Button.CurrentSeed == "GlowSeed") {
							//EnableHandSeedRender("R_Glowfall_prefab");

							RenderThis = "R_GlowSeed_prefab";
							GameObject fallingSeed = Instantiate(Resources.Load("R_Glowfall_prefab"), GameObject.Find (RenderThis).transform.position, new Quaternion(0,0,0,0)) as GameObject;
							Debug.Log("Glow Seed Ready to fall");
						}
						
						if (Button.CurrentSeed == "CandySeed") {
							//EnableHandSeedRender("R_Candyfall_prefab");
							RenderThis = "R_CandySeed_prefab";
							Debug.Log("Candy Seed Ready to fall");

							GameObject fallingSeed = Instantiate(Resources.Load("R_Candyfall_prefab"), GameObject.Find (RenderThis).transform.position, new Quaternion(0,0,0,0)) as GameObject;
							Debug.Log("Candy Seed Ready to fall");

						}
						
						if (Button.CurrentSeed == "GhostSeed") {
							//EnableHandSeedRender("R_Ghostfall_prefab");
							RenderThis = "R_GhostSeed_prefab";


							GameObject fallingSeed = Instantiate(Resources.Load("R_Ghostfall_prefab"), GameObject.Find (RenderThis).transform.position, new Quaternion(0,0,0,0)) as GameObject;
							Debug.Log("Ghost Seed Ready to fall");
						}

						fallingSeedSpawned = true;
					}
				}
				else {
					//Debug.Log ("RESTARTING");
					//DisableHandSeedRender();
				    
					fallingSeedSpawned = false;
				    GrabSeed = GestureState.start;
				}
				
				break;

			}

		}
//		else
//						return;
	


	public void DisableHandSeedRender(){
		//Debug.Log ("Hand Seed disabled!");
		GameObject[] existingSeeds = GameObject.FindGameObjectsWithTag ("HandSeed");
		foreach (GameObject s in existingSeeds) {
			Renderer[] elist = s.GetComponentsInChildren<Renderer> ();
				foreach (Renderer r in elist){
					r.enabled = false;
				}
		}
	}

	public void EnableHandSeedRender (string seedname){
		Debug.Log ("Hand Seed rendering!");
		DisableHandSeedRender();

		if (GameObject.Find (seedname) != null){
			Renderer[] seedparts = GameObject.Find (seedname).GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in seedparts) {
				r.enabled = true;
			}
		}
		/*GameObject[] newSeed = GameObject.FindGameObjectsWithTag ("HandSeed");
	
		foreach (GameObject b in newSeed) {
			Renderer[] newList = b.GetComponentsInChildren<Renderer> ();
			//Debug.Log ("Object in list: "+b.name);
			foreach (Renderer r in newList) {
				Debug.Log ("Render in list: "+r.name);
				if (r.name == seedname) {
					r.enabled = true;
					Debug.Log ("Render enabled: "+r.name);
				}
			}
		}*/

	}

}
