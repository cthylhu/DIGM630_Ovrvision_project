using UnityEngine;
using System.Collections;

public class DropSeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = GameObject.Find ("leftpalm")
		transform.Translate(Vector3.down * Time.deltaTime * 3f);
		GameObject.Find ("Marker").transform.position = new Vector3 (PlantingSeed.lastSeenARObject.transform.position.x, 
		                                                             PlantingSeed.lastSeenARObject.transform.position.y+0.9f, 
		                                                             PlantingSeed.lastSeenARObject.transform.position.z);
		PlantingSeed.isDropping = true;
		
	}

	void OnTriggerEnter(Collider other){

		if (other.name == "Marker") {

			GameObject.Find ("sounds").audio.Play ();
			Debug.Log ("Seed Planted!");
			Grab.Grabbed = false;
			if (PlantingSeed.isLooking){
				PlantingSeed.isDropping = false;
				PlantingSeed.doneDropping = true;
			}
			Destroy (this.gameObject);

			/*	
			Renderer[] fallseed1 = GameObject.Find ("L_CandySeed_prefab").GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in fallseed1) {
				r.enabled = false;
			}
			Renderer[] fallseed3 = GameObject.Find ("L_GhostSeed_prefab").GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in fallseed3) {
				r.enabled = false;
			}
			Renderer[] fallseed2 = GameObject.Find ("L_GlowSeed_prefab").GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in fallseed2) {
				r.enabled = false;
			}
			Renderer[] fallseed4 = GameObject.Find ("R_CandySeed_prefab").GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in fallseed4) {
				r.enabled = false;
			}
			Renderer[] fallseed5 = GameObject.Find ("R_GhostSeed_prefab").GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in fallseed5) {
				r.enabled = false;
			}
			Renderer[] fallseed6 = GameObject.Find ("R_GlowSeed_prefab").GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in fallseed6) {
				r.enabled = false;
			}
			*/
	/*
			Renderer[] fallseed = this.GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in fallseed) {
				r.enabled = false;
			}
			*/
	//droptosoil = true;
	//GameObject.Find ("AREnvironment").GetComponent<PlantingSeed>().SendMessage ("CheckSeed",droptosoil);
	//GameObject.Find ("Comet").GetComponent<flyin>().SendMessage ("Comes",droptosoil);
	
		}

	}
}
