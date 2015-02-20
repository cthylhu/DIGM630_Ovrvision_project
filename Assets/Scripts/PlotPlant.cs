using UnityEngine;
using System.Collections;
using Leap;

public class PlotPlant : MonoBehaviour {
	public Righthand righthand;
	public int planted;
	public Vector3 plantsize;
	public float popfromdirt;

	private GameObject plantSeed;
	// Use this for initialization
	void Start () {
		planted = 0;
		plantSeed = GameObject.Find ("Test_Plant_Planet");
		Debug.Log (plantSeed.transform.localScale);
	}


 void NormalGrow(){
		plantSeed.transform.localScale -= new Vector3 (0.01f,0.01f,0.01f) ;
		plantsize = plantSeed.transform.localScale;
		
		righthand.normalgrow = false;  
	}
	
	
	void ReverseGrow(){
		plantsize =new Vector3 (0.01f,0.01f,0.01f);
		plantSeed.transform.localScale += plantsize ;
		righthand.reversegrow = false;  

	}




	// Update is called once per frame
	void Update () {




		//popfromdirt = GameObject.Find ("Test_Plant_Planet").transform.localPosition.y;


		//if (Input.GetKeyDown ("a")) {
		if (righthand.plotplant) {
						//Debug.Log ("You are planting!");
						Renderer[] children = GetComponentsInChildren<Renderer> ();
						foreach (Renderer r in children) {
								//Debug.Log("Child: " + r.name);
								if (r.name == "Test_Plant_Planet") {
										 r.renderer.enabled = true;
					                     planted = 1;
								} 
				                else {
										r.renderer.enabled = false;
								}
						}
				}



		if (planted==1) {
				if (righthand.normalgrow) {
					NormalGrow();
				 }
				if (righthand.reversegrow){
					ReverseGrow();
				}

	}
  }
}
