using UnityEngine;
using System.Collections;

public class WatercanTilt : MonoBehaviour {
	Vector3 currentEuler;
	public static bool isPouring = false;
	public float startTime = 0.0f;
	public float endTime = 0.0f;
	public bool timerStarted = false;
	public static float timeElapsed = 0.0f;

	// Use this for initialization
	void Awake () {
		transform.rotation = new Quaternion (0.7071067811865476f, 0f, 0.7071067811865476f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		/*currentEuler = this.transform.localRotation.eulerAngles;

		if (currentEuler.x > 300f || currentEuler.x < 30f) {
			//Debug.Log ("Started pouring!");
			//this.transform.Find("watercan_Prefab").renderer.material.SetColor("_Color", Color.blue);

			if (!timerStarted && PlantingSeed.isLooking && PlantingSeed.lastSeenARObject.GetComponent<PlanetInfo>().isPlanted){
				isPouring = true;
				startTime = Time.time;
				timerStarted = true;
				GameObject.Find ("WaterParticles").GetComponent<ParticleSystem>().enableEmission = true;
			}

		}
		else {
			//this.transform.Find("watercan_Prefab").renderer.material.SetColor("_Color", Color.white);
			if (timerStarted){
				isPouring = false;
				endTime = Time.time;
				timeElapsed = timeElapsed + (endTime - startTime);
				timerStarted = false;
				startTime = 0.0f;
				endTime = 0.0f;
				GameObject.Find ("WaterParticles").GetComponent<ParticleSystem>().enableEmission = false;
			}

		}*/


	}
}
