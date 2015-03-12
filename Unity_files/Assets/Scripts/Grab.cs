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


		
		switch (GrabSeed) {
		
		
		case GestureState.start:

			// Poke Glow Seed Button
			if(Button.seednumber==1) 
			{
				if (Grab_L > 0.8 ) {
					
					Grabbed = true;
					GrabSeed = GestureState.middle_L;
				} 
				
				if (Grab_R > 0.8 ) {
					
					Grabbed = true;
					GrabSeed = GestureState.middle_R;
				} 
			}


		
			break;
			
		case GestureState.middle_R:

			
			this.transform.position = GameObject.Find ("rightpalm").transform.position;

			if (Grab_R == 0) {
				


				GrabSeed = GestureState.end;

				gameObject.AddComponent <Rigidbody>().useGravity = true;
				
			}



			break;

		case GestureState.middle_L:
			
			this.transform.position = GameObject.Find ("leftpalm").transform.position;
			
			if (Grab_L == 0) {
					


				GrabSeed = GestureState.end;
					
				gameObject.AddComponent <Rigidbody>().useGravity = true;
					
				}

			
			
			break;
			
		case GestureState.end:



			if( Button.seednumber == 0){
			
			     GrabSeed = GestureState.start;	 
			
			}

			break;
			
		}
		
	}
}
