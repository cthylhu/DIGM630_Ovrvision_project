using UnityEngine;
using System.Collections;
using Leap;

public class PlotPlant : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("a")) {
			//Debug.Log ("Button pressed!");
			Renderer[] children = GetComponentsInChildren<Renderer>();
			foreach(Renderer r in children) {
				//Debug.Log("Child: " + r.name);
				if (r.name == "Test_Plant_Planet"){
					r.renderer.enabled = true;
				}
				else {
					r.renderer.enabled = false;
				}
			}

		}
	}
}
