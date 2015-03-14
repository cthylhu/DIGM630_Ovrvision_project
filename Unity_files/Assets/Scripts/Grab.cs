using UnityEngine;
using System.Collections;
using Leap;

using UnityEngine;
using System.Collections;
using Leap;

public class Grab : MonoBehaviour {
	
	Controller Controller = new Controller ();
	public  bool Grabbed;
	public Button Button;
	public FallandFloat FallandFloat;
	public Righthand Righthand;
	public Lefthand Lefthand;
	public sounds sounds;
	
	public enum GestureState
	{
		start,
		middle_L,
		middle_R,
		end
	}

	public GestureState GrabSeed = GestureState.start;

	// Use this for initialization
	void Start () {
		
	}

	/*
	void OnTriggerEnter(Collider other){
		
		Debug.Log ("Object: " + this.name);
		
		
		
		if ((other.name == ("rightpalm")) || (other.name == ("leftpalm"))) {
			
			
			this.GetComponent<FallandFloat> ().maxRotationSpeed = 100;
			this.GetComponent<FallandFloat> ().minRotationSpeed = 80;
			
			Debug.Log ("Slow down Orbiting!");
			
		}
	}
	
	
	
	void OnTriggerExit(Collider other){
		
		Debug.Log("Object: " + this.name);
		
		
		
		if ((other.name == ("rightpalm")) || (other.name == ("leftpalm"))) {
			
			
			this.GetComponent<FallandFloat>().maxRotationSpeed = 400;
			this.GetComponent<FallandFloat>().minRotationSpeed = 300;
			
			Debug.Log ("Speed up Orbiting!");
			
		}
	}
	
	*/
	
	
	// Update is called once per frame
	
	void Update () {
		
		Frame frame = Controller.Frame ();
		Hand rightmost = frame.Hands.Rightmost;
		Hand leftmost = frame.Hands.Leftmost;
		float Grab_L = leftmost.GrabStrength;
		float Grab_R = rightmost.GrabStrength;
		float Pinch_L = rightmost.PinchStrength;
		float Pinch_R = leftmost.PinchStrength;
		int handnum = frame.Hands.Count;

		switch (GrabSeed) {
		
		case GestureState.start:

			// Poke Glow Seed Button
			if(Button.seedGenerated) 
			{
				//if (Grab_L > 0.8 ) {
				//Debug.Log("Got here! 1");
				if (Pinch_L == 1 ) {
					//Debug.Log("Got here! 2");
					Grabbed = true;
					//GameObject.Find ("Button").SendMessage ("DisableSeedRenders");
					GrabSeed = GestureState.middle_L;
				} 
				
				//if (Grab_R > 0.8 ) {
				if (Pinch_R ==1 ) {
					
					Grabbed = true;
					//GameObject.Find ("Button").SendMessage ("DisableSeedRenders");
					GrabSeed = GestureState.middle_R;
				} 
			}

			break;
			
		case GestureState.middle_R:

			if(rightmost.IsValid){
				Debug.Log ("Got here! 3");
				//this.transform.parent = GameObject.Find ("seedContainer").transform;
				//this.transform.localPosition = Vector3.zero;
//				this.transform.position = GameObject.Find ("rightpalm").transform.position;
//				this.transform.localScale  = GameObject.Find ("rightpalm").transform.localScale * 0.5f;

				
				if (Button.CurrentSeed == "CandySeed"){
					//EnableHandSeedRenders("CandySeed");
					Renderer[] rightnewSeed = GameObject.Find ("R_CandySeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in rightnewSeed) {
						Debug.Log ("Hand seed render: "+r.name);
						r.enabled = true;
					}
					Renderer[] rightseedList1 = GameObject.Find ("R_GhostSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in rightseedList1){
						r.enabled = false;
					}
					Renderer[] rightseedList2 = GameObject.Find ("R_GlowSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in rightseedList2){
						r.enabled = false;
					}
				}
				if (Button.CurrentSeed == "GhostSeed"){
					//EnableHandSeedRenders("GhostSeed");
					
					Renderer[] rightnewSeed = GameObject.Find ("R_GhostSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in rightnewSeed) {
						Debug.Log ("Hand seed render: "+r.name);
						r.enabled = true;
					}
					Renderer[] rightseedList1 = GameObject.Find ("R_CandySeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in rightseedList1){
						r.enabled = false;
					}
					Renderer[] rightseedList2 = GameObject.Find ("R_GlowSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in rightseedList2){
						r.enabled = false;
					}
				}
				if (Button.CurrentSeed == "GlowSeed"){
					//EnableHandSeedRenders("GlowSeed");
					
					Renderer[] rightnewSeed = GameObject.Find ("R_GlowSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in rightnewSeed) {
						Debug.Log ("Hand seed render: "+r.name);
						r.enabled = true;
					}
					Renderer[] rightseedList1 = GameObject.Find ("R_GhostSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in rightseedList1){
						r.enabled = false;
					}
					Renderer[] rightseedList2 = GameObject.Find ("R_CandySeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in rightseedList2){
						r.enabled = false;
					}
				}

				if (Grab_R == 0) {
				 	GrabSeed = GestureState.end;

					gameObject.AddComponent <Rigidbody>().useGravity = true;
					
				}



			}

			if(!rightmost.IsValid){
				GrabSeed = GestureState.start;
				
			}
			break;

		case GestureState.middle_L:

			if(leftmost.IsValid ){ ///
				Debug.Log ("Got here! 3");
				//this.transform.parent = GameObject.Find ("seedContainer").transform;
				//this.transform.localPosition = Vector3.zero;
	

				if (Button.CurrentSeed == "CandySeed"){
					//EnableHandSeedRenders("CandySeed");
					Renderer[] leftnewSeed = GameObject.Find ("L_CandySeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in leftnewSeed) {
						Debug.Log ("Hand seed render: "+r.name);
						r.enabled = true;
					}
					Renderer[] leftseedList1 = GameObject.Find ("L_GhostSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in leftseedList1){
						r.enabled = false;
					}
					Renderer[] leftseedList2 = GameObject.Find ("L_GlowSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in leftseedList2){
						r.enabled = false;
					}
				}
				if (Button.CurrentSeed == "GhostSeed"){
					//EnableHandSeedRenders("GhostSeed");

					Renderer[] leftnewSeed = GameObject.Find ("L_GhostSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in leftnewSeed) {
						Debug.Log ("Hand seed render: "+r.name);
						r.enabled = true;
					}
					Renderer[] leftseedList1 = GameObject.Find ("L_CandySeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in leftseedList1){
						r.enabled = false;
					}
					Renderer[] leftseedList2 = GameObject.Find ("L_GlowSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in leftseedList2){
						r.enabled = false;
					}
				}
				if (Button.CurrentSeed == "GlowSeed"){
					//EnableHandSeedRenders("GlowSeed");

					Renderer[] leftnewSeed = GameObject.Find ("L_GlowSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in leftnewSeed) {
						Debug.Log ("Hand seed render: "+r.name);
						r.enabled = true;
					}
					Renderer[] leftseedList1 = GameObject.Find ("L_GhostSeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in leftseedList1){
						r.enabled = false;
					}
					Renderer[] leftseedList2 = GameObject.Find ("L_CandySeed_prefab").GetComponentsInChildren<Renderer>();
					foreach (Renderer r in leftseedList2){
						r.enabled = false;
					}
				}

				if (Grab_L == 0) {

					GrabSeed = GestureState.end;
						
					gameObject.AddComponent <Rigidbody>().useGravity = true;		
				}

			}

			if(!leftmost.IsValid){
				GrabSeed = GestureState.start;
				
			}
			
			break;
			
		case GestureState.end:

			if(!Button.seedGenerated){
			
			     GrabSeed = GestureState.start;	 
			
			}

			break;
			
		}

	}
	/*
	void EnableHandSeedRenders(string seedname){

		Debug.Log ("Got here! 4");
		// Disable existing seeds
		Renderer[] leftseedList = GameObject.Find ("LeftseedContainer").GetComponentsInChildren<Renderer>();
		foreach (Renderer r in leftseedList){
			r.enabled = false;
		}
		Renderer[] rightseedList = GameObject.Find ("RightseedContainer").GetComponentsInChildren<Renderer>();
		foreach (Renderer r in rightseedList){
			r.enabled = false;
		}
		// Enable new seed
		Renderer[] leftnewSeed = GameObject.Find ("LeftseedContainer").Find (seedname).GetComponentsInChildren<Renderer>();
		foreach (Renderer r in leftnewSeed) {
			Debug.Log ("Hand seed render: "+r.name);
			r.enabled = true;
		}
		Renderer[] rightnewSeed = GameObject.Find ("RightseedContainer").Find (seedname).GetComponentsInChildren<Renderer>();
		foreach (Renderer r in rightnewSeed) {
			Debug.Log ("Hand seed render: "+r.name);
			r.enabled = true;
		}
		//seedGenerated = true;
		//PlantingSeed.buttonPressed = buttonType; 
	}
*/

}
