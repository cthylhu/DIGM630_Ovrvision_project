using UnityEngine;
using System.Collections;

public class WatercanTilt : MonoBehaviour {
	Vector3 currentEuler;
	public static bool isPouring = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentEuler = this.transform.localRotation.eulerAngles;

		if (Input.GetKeyDown("q")) {
			this.transform.Find ("Particle System").GetComponent<ParticleSystem>().enableEmission = false;
		}

		if (currentEuler.x > 300f || currentEuler.x < 30f) {
			//Debug.Log ("Started pouring!");
			//this.transform.Find("watercan_Prefab").renderer.material.SetColor("_Color", Color.blue);
			GameObject.Find ("WaterParticles").GetComponent<ParticleSystem>().enableEmission = true;
			isPouring = true;
		}
		else {
			//this.transform.Find("watercan_Prefab").renderer.material.SetColor("_Color", Color.white);
			GameObject.Find ("WaterParticles").GetComponent<ParticleSystem>().enableEmission = false;
			isPouring = false;
		}
	}
}
