using UnityEngine;
using System.Collections;

public class PlantingSeed : MonoBehaviour {
	bool planted = false;
	bool dighole = false;
	public int plantType = 0;

	Transform emptyPlanet;
	Transform holeModel;
	Transform sproutModel;
	int hitcount = 0;

	// Use this for initialization
	void Start () {
		emptyPlanet = this.transform.Find ("TestPlanet");
		holeModel = this.transform.Find ("Planet_with_hole");
		sproutModel = this.transform.Find ("Planet_with_plant");
	}
	
	// Update is called once per frame
	void Update () {

		// First, dig a hole in the ground
		// Detect finger-poke collision with planet
		if (Input.GetKeyDown ("a")) {
		
			if (!dighole) {
				Debug.Log ("Diggy diggy hole!");
				
				emptyPlanet.renderer.enabled = false;
				holeModel.renderer.enabled = true;
				sproutModel.renderer.enabled = false;
				dighole = true;

			}

		}

		// Second, detect what type of plant you want to plant
		// Detect finger-poke collision with button
		if (Input.GetKeyDown ("z")) {
			plantType = 1;
		}
		if (Input.GetKeyDown ("x")) {
			plantType = 2;
		}
		if (Input.GetKeyDown ("c")) {
			plantType = 3;
		}

		// Third, plant the seed into the hole
		// Detect which planet you're looking at and want to plant a seed in

		Vector3 fwd = GameObject.Find("CenterEyeAnchor").transform.forward;
		RaycastHit hit;
		if (Physics.Raycast(GameObject.Find("CenterEyeAnchor").transform.position, fwd, out hit, 9)){
			Debug.Log ("Hit something: "+hit.collider.name);
			Debug.Log ("Hit #: "+hitcount);
			hitcount++;
		}


		if (Input.GetKeyDown ("s")) {
			if (dighole) {
				emptyPlanet.renderer.enabled = false;
				holeModel.renderer.enabled = false;
				sproutModel.renderer.enabled = true;
				
			}
		}
	}
}
