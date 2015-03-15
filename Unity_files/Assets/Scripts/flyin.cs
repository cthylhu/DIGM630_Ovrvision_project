using UnityEngine;
using System.Collections;

public class flyin : MonoBehaviour {
	public AudioClip appear;
	public AudioClip diappear;
	private float step;
	public float movespeed;
	public narration narration;
	
	public Vector3 target1;
	public Vector3 target2;
	public bool goin;
	public enum flystate {none,flyin,flyout,flyback}
	public flystate cometfly = flystate.none;

	public int pickcolorcount;
	// Use this for initialization
	void Start () {
		
		step = movespeed * Time.deltaTime;

		target1 = new Vector3 (0, -1, 0);
		target2 = new Vector3 (1000, -1, 0);
	}

	void Comes(bool comes){
		goin = comes;
		}
	
	// Update is called once per frame
	void Update () {


		//Debug.Log(" color comes!");

/*						 
	    if (PlantingSeed.lastSeensARObject != null && PlantingSeed.lastSeensARObject.GetComponent<PlanetInfo>().isPlanted) {

			transform.position = Vector3.MoveTowards (transform.position, target1, step);

			//pick color for the first time, give narration!
			
			if(pickcolorcount == 0){
				GameObject.Find ("narration").SendMessage ("Intro8Play");

				pickcolorcount=1;
			}

	   	}

		if (transform.localPosition == target1) {

			movespeed = 0;
			//Debug.Log ("Pick a color!");
//									
		}
//					

		if ((GameObject.Find ("purple").GetComponent<Colorpicker> ().colorchanged == true) ||
				(GameObject.Find ("blue").GetComponent<Colorpicker> ().colorchanged == true) ||
				(GameObject.Find ("green").GetComponent<Colorpicker> ().colorchanged == true)) {

			movespeed = 300;
			//transform.position = target2;
			transform.position = Vector3.MoveTowards (transform.position, target2, step);

		}
		
//	*/

		}
}



