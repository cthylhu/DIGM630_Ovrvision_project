using UnityEngine;
using System.Collections;

public class FallandFloat : MonoBehaviour {
	Vector3 ButtonPosition;
	// Use this for initialization
	void Start () {
		//droptosoil = false;
		//DisableSeedRenders (this.name);
	}

	// Update is called once per frame
	void Update () {
		      
		/*if (Button.seedGenerated) {

			if (!Grab.Grabbed) {
				if (PlantingSeed.buttonPressed == 1){
					SeedName = "CandySeed";
				}
				if (PlantingSeed.buttonPressed == 2){
					SeedName = "GhostSeed";
				}
				if (PlantingSeed.buttonPressed == 3){
					SeedName = "GlowSeed";
				}
				EnableSeedRender(SeedName);
				transform.Rotate(0f, ySpeed * Time.deltaTime, 0f);
						
			}
		}
		else {
			DisableSeedRenders (this.name);
		}*/
	}
//	void OnTriggerExit(Collider other){
//
//
//
//		if (other.name == "FallCollider") {
//
//			// if(this.name =="GlowSeed(Clone") {
//
//			//GameObject.Find ("GlowSeed").GetComponent<Grab>().Grabbed = false;
//
//			Destroy (this.rigidbody);
//
////			//Destroy(this);
////			
//			Debug.Log ("Seed Planted");
//			Debug.Log("Fall into Object: " + other.name);
////
//			Button.seedGenerated = false;
//
//			Grab.Grabbed = false;
////			
//			other.audio.PlayOneShot(sounds.planted);
////			
//			Renderer[] fallseed = this.GetComponentsInChildren<Renderer>();
//			foreach (Renderer r in fallseed){
//				r.enabled = false;
//			}
////			
////			//GameObject.Find ("AREnvironment").GetComponent<PlantingSeed> ().SendMessage ("CheckSeed", droptosoil = true);
////			
//		}
////
//	}
}
