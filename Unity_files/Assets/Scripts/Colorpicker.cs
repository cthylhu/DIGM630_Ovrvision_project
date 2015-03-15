using UnityEngine;
using System.Collections;
using Leap;
public class Colorpicker : MonoBehaviour {
	//Controller Controller = new Controller ();
	public sounds sounds;
	public AudioClip pick;
	public AudioClip sprinkle;
	public bool  hitcomet;
	public bool picked;
	public bool colorchanged;
	public int colorplantcount;
	public narration narration;

	public enum GestureState
	{
		
		start,
		middle,
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
			         

						if (Righthand.pinching || Lefthand.pinching) {
								hitcomet = true;
								audio.PlayOneShot (pick, 0.5f);
						
						}

						//Colored ();

						//GameObject.Find ("AREnviroment").GetComponent<PlantingSeed> ().SendMessage ("ColorPicked", this.name); 
		
				}
		}

	void Colored(){

		GameObject.Find ("AREnviroment").GetComponent<PlantingSeed>().SendMessage("ColorPicked",this.name); 
	
	}
	// Update is called once per frame
	void Update () {



		switch (PickColor){

		case GestureState.start:

			if (hitcomet)

			{
					if (( Righthand.palmdown && Righthand.sprinkle) || (Lefthand.palmdown && Lefthand.sprinkle)) {
						
						audio.PlayOneShot (sprinkle,0.5f);
						
						PickColor = GestureState.end;
						
						colorchanged = true;
				
				
			}
				}

			break;

			
		case GestureState.end:
			//color plant for the first time, give narration!
			
//			if(colorplantcount == 0){
//				GameObject.Find ("narration").SendMessage ("Intro9Play");
//				colorplantcount=1;
//			}
			 hitcomet = false;
             PickColor = GestureState.start;	 

			break;
			
		}


	}
}
