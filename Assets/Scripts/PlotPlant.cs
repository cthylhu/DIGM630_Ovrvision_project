using UnityEngine;
using System.Collections;
using Leap;

public class PlotPlant : MonoBehaviour {
	//public Righthand righthand;
	public int planted;
	public Vector3 plantsize;
	public Vector3 plantheight;
	public AudioClip pouringseed;

	private GameObject plantSeed;
	// Use this for initialization
	void Start () {
		planted = 0;
		plantSeed = GameObject.Find ("TestPlanet");
		//Debug.Log (plantSeed.transform.localScale);


	}


 void NormalGrow(){
		plantSeed.transform.localScale -= new Vector3 (0.01f,0.01f,0.01f) ;
		plantsize = plantSeed.transform.localScale;
		
		Righthand.normalgrow = false;  
	}
	
	
	void ReverseGrow(){
		plantsize =plantSeed.transform.localScale;
		plantSeed.transform.localScale += new Vector3 (0.01f,0.01f,0.01f);
		Righthand.reversegrow = false;  

	}

	void OnTriggerEnter(Collider other ){
		Debug.Log ("Planted? : " + planted);
		if (other.name == "R_index_bone3" /*&& planted == 0*/) {
			Debug.Log ("Hand Collision detected!!");
			Debug.Log ("Name: " + other.name);

			if (Righthand.openhand) {

				Renderer[] children = GetComponentsInChildren<Renderer> ();
				foreach (Renderer r in children) {
					//Debug.Log("Child: " + r.name);
					if (r.name == "TestPlanet") {
						r.renderer.enabled = true;
						plantheight = plantSeed.transform.localPosition;
						plantSeed.transform.localPosition += new Vector3 (0, 0.02f, 0);
						planted = 1;
						audio.PlayOneShot(pouringseed,3.0f);

					} else {
						r.renderer.enabled = false;
					}
				}

			}
		}

	}


	// Update is called once per frame
	void Update () {




		//popfromdirt = GameObject.Find ("Test_Plant_Planet").transform.localPosition.y;


		//if (Input.GetKeyDown ("a")) {
		/*
		if (righthand.plotplant) {
						if (planted == 0) {
								//Debug.Log ("You are planting!");
								Renderer[] children = GetComponentsInChildren<Renderer> ();
								foreach (Renderer r in children) {
										//Debug.Log("Child: " + r.name);
										if (r.name == "Test_Plant_Planet") {
												r.renderer.enabled = true;
												plantheight = plantSeed.transform.localPosition;
												plantSeed.transform.localPosition += new Vector3 (0, 0.02f, 0);
												planted = 1;
										} else {
												r.renderer.enabled = false;
										}
								}
						}
				}
*/


		if (planted==1) {
				if (Righthand.normalgrow && Righthand.plotplant) {
					NormalGrow();
				 }
			if (Righthand.reversegrow && Righthand.plotplant){
					ReverseGrow();
				}

	}
  }
}
