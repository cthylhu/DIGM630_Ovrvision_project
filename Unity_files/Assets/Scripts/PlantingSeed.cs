using UnityEngine;
using System.Collections;

public class PlantingSeed : MonoBehaviour {
	bool planted = false;
	bool dighole = false;
	public int plantType = 0;
	
	Transform holeModel;
	Transform sproutModel;
	int hitcount = 0;

	// Use this for initialization
	void Start () {
		//Debug.Log (gameObject.name);

		holeModel = gameObject.transform.Find ("Planet_with_hole");
		sproutModel = gameObject.transform.Find ("Planet_with_plant");

	}
	
	// Update is called once per frame
	void Update () {

		// 1. Detect which planet you're looking at
		Vector3 fwd = GameObject.Find("CenterEyeAnchor").transform.forward;
		RaycastHit hit;
		if (Physics.Raycast(GameObject.Find("CenterEyeAnchor").transform.position, fwd, out hit, 9)){
			Debug.Log ("Hit #: "+hitcount+", Collider: "+hit.collider.name);
			hitcount++;
			gameObject.renderer.material.SetColor ("_OutlineColor", Color.green);
			holeModel.renderer.material.SetColor ("_OutlineColor", Color.green);
			sproutModel.renderer.material.SetColor ("_OutlineColor", Color.green);

			//Debug.Log ("hParent: "+holeModel.parent);
			//Debug.Log ("sParent: "+sproutModel.parent);

			// 2. Dig a hole in the ground
			// Detect finger-poke collision with planet
			if (Input.GetKeyDown ("a")) {
				
				if (!dighole) {
					Debug.Log ("Diggy diggy hole!");
					
					gameObject.renderer.enabled = false;
					holeModel.renderer.enabled = true;
					sproutModel.renderer.enabled = false;
					dighole = true;
					
				}
				
			}

			// 3. Detect what type of plant you want to plant
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

			//4. Plant the seed
			if (Input.GetKeyDown ("s")) {
				if (dighole) {
					gameObject.renderer.enabled = false;
					holeModel.renderer.enabled = false;
					sproutModel.renderer.enabled = true;
					
				}
			}
		}
		else{
			gameObject.renderer.material.SetColor ("_OutlineColor", Color.clear);
			holeModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
			sproutModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
		}


	}
}
