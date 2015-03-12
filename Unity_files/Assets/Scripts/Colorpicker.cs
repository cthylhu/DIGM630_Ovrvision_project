using UnityEngine;
using System.Collections;
using Leap;
public class Colorpicker : MonoBehaviour {
	Controller Controller = new Controller ();
	public sounds sounds;
	public AudioClip hit;
	public AudioClip pick;
	public AudioClip sprinkle;
	public static bool hitcomet;
	public static bool picked;
	public static bool colorchanged;

	public enum GestureState
	{
		
		start,
		middle_L,
		middle_R,
		end
	}
	public GestureState PickColor = GestureState.start;

	// Use this for initialization
	void Start () {
		

	
	}

	// pick a color by firstly touch the comet

	void OnTriggerEnter(Collider other){
		
				//Debug.Log ("Button pressed: " + this.name);
		
				if ((other.name == ("L_index_bone3")) || (other.name == ("R_index_bone3"))) {
			           hitcomet = true;
	                   audio.PlayOneShot (hit);
						
				}
		}


	
	// Update is called once per frame
	void Update () {



		Frame frame = Controller.Frame ();
		Hand rightmost = frame.Hands.Rightmost;
		Hand leftmost = frame.Hands.Leftmost;
		float Grab_L = leftmost.GrabStrength;
		float Grab_R = rightmost.GrabStrength;
		float Pinch_L = rightmost.PinchStrength;
		float Pinch_R = leftmost.PinchStrength;

		



		switch (PickColor){

		case GestureState.start:

			if (hitcomet)

			{
				//if (Grab_L > 0.8 ) {
				audio.PlayOneShot (pick);
				picked = true;
				
				if (Pinch_L > 0.8 ) {
					

					PickColor = GestureState.middle_L;
				} 
				
				//if (Grab_R > 0.8 ) {
				if (Pinch_R > 0.8 ) {
					

					PickColor= GestureState.middle_R;
				} 
			}

			break;
			
		case GestureState.middle_R:

	

			//this.transform.position = GameObject.Find ("rightpalm").transform.position;
			
			if (Righthand.sprinkle) {

				audio.PlayOneShot (sprinkle);
				
				PickColor = GestureState.end;
				
				colorchanged = true;
				
			}
			
			
			
			break;
			
		case GestureState.middle_L:
			
		
			//if (Grab_L == 0) {
			
			//this.transform.position = GameObject.Find ("leftpalm").transform.position;
			
			if (Lefthand.sprinkle) {
				
				audio.PlayOneShot (sprinkle);
				
				PickColor = GestureState.end;
				
				colorchanged = true;

			}
			
			
			
			break;
			
		case GestureState.end:
			
			
//			
//			if( this.name ="green"){
//
//			}
//			if( this.name ="blue"){
//
//			}
//			if( this.name ="purple"){
//
//			}

				
				PickColor = GestureState.start;	 
				

			
			break;
			
		}














	
	}
}
