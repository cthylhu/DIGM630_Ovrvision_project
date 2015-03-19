using UnityEngine;
using System.Collections;

public class PlanetInfo : MonoBehaviour {
	
	public int trackerNum;
	public bool isPlanted = false;
	public bool hasHole = false;
	public string assigncolor;
	public bool canDig = true;

	public bool VRplanetSpawned = false;
	public bool watered = false;
	public bool firstTimeWatered = true;
	
	public int familyType = 0;
	public int plantType = 0;


	Vector3 currentEuler;
	public float startTime = 0.0f;
	public float endTime = 0.0f;
	public bool timerStarted = false;
	public float totalWateredTime;

	
	// Use this for initialization
	void Start () {
		totalWateredTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {


		currentEuler = GameObject.Find ("watercan").transform.localRotation.eulerAngles;

		if (PlantingSeed.currentARObject != null && (currentEuler.x > 300f || currentEuler.x < 30f)) {
			//Debug.Log ("Started pouring!");
			//this.transform.Find("watercan_Prefab").renderer.material.SetColor("_Color", Color.blue);
			//if (firstTimeWatered){
			//	totalWateredTime = 0.0f;
			//	firstTimeWatered = false;
			//}
			//else{
				if (!timerStarted && isPlanted){
					//isPouring = true;

					startTime = Time.time;
					timerStarted = true;
					GameObject.Find ("WaterParticles").GetComponent<ParticleSystem>().enableEmission = true;
				}
			//}
		}
		else {
			//this.transform.Find("watercan_Prefab").renderer.material.SetColor("_Color", Color.white);
			if (timerStarted){
				//isPouring = false;
				endTime = Time.time;
				totalWateredTime = totalWateredTime + (endTime - startTime);
				timerStarted = false;
				startTime = 0.0f;
				endTime = 0.0f;
				GameObject.Find ("WaterParticles").GetComponent<ParticleSystem>().enableEmission = false;
			}
			
		}
	}


}
