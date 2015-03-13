using UnityEngine;
using System.Collections;
using Leap;

using UnityEngine;
using System.Collections;
using Leap;

public class Grab : MonoBehaviour {
	
	Controller Controller = new Controller ();
	public bool Grabbed;
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

				if (Pinch_L == 1 ) {
					Grabbed = true;
					GrabSeed = GestureState.middle_L;
				} 
				
				//if (Grab_R > 0.8 ) {
				if (Pinch_R ==1 ) {
					
					Grabbed = true;
					GrabSeed = GestureState.middle_R;
				} 
			}

			break;
			
		case GestureState.middle_R:

			if(rightmost != null && handnum != 0 ){
				this.transform.parent = GameObject.Find ("seedContainer").transform;
				this.transform.localPosition = Vector3.zero;
				//this.transform.position = GameObject.Find ("rightpalm").transform.position;

				if (Grab_R == 0) {
				 	GrabSeed = GestureState.end;
					gameObject.AddComponent <Rigidbody>().useGravity = true;
					
				}



				if(rightmost == null){
					GrabSeed = GestureState.start;
				}

			}
			break;

		case GestureState.middle_L:

			if(leftmost != null && handnum != 0 ){
			 
				this.transform.parent = GameObject.Find ("seedContainer").transform;
				this.transform.localPosition = Vector3.zero;

				if (Grab_L == 0) {
				

				GrabSeed = GestureState.end;
					
				gameObject.AddComponent <Rigidbody>().useGravity = true;
					
				}

					if(rightmost == null){
						GrabSeed = GestureState.start;
				}

				}
			
			break;
			
		case GestureState.end:

			if(!(Button.seedGenerated)){
			
			     GrabSeed = GestureState.start;	 
			
			}

			break;
			
		}

	}
}
