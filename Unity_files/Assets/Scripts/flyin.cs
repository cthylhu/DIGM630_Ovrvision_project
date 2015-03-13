using UnityEngine;
using System.Collections;

public class flyin : MonoBehaviour {
	public AudioClip appear;
	public AudioClip diappear;
	private float step;
	public float movespeed;
	
	public Vector3 target1;
	public Vector3 target2;

	public enum flystate {none,flyin,flyout,flyback}
	public flystate cometfly = flystate.none;

	// Use this for initialization
	void Start () {
		
		step = movespeed * Time.deltaTime;

		target1 = new Vector3 (0, -1, 0);
		target2 = new Vector3 (1000, -1, 0);
	}


	
	// Update is called once per frame
	void Update () {

		if ((GameObject.Find ("CandySeed") != null) || (GameObject.Find ("GhostSeed") != null) || (GameObject.Find ("GlowSeed") != null)) {

						if ((GameObject.Find ("CandySeed").GetComponent<FallandFloat> ().droptosoil == true) ||
								(GameObject.Find ("GhostSeed").GetComponent<FallandFloat> ().droptosoil == true) ||
								(GameObject.Find ("GlowSeed").GetComponent<FallandFloat> ().droptosoil == true)) {

								transform.position = Vector3.MoveTowards (transform.position, target1, step);
						}
//								
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
				
//	
		
				}
}



