using UnityEngine;
using System.Collections;
using Leap;
[RequireComponent(typeof(AudioSource))]

public class watchtapping : MonoBehaviour {

	
	// Use this for initialization
	
	public AudioClip spaceswitch;

	//GameObject OVRObject = GameObject.Find ("OvrvisionViewObject");

	
	private float cooldownTime;
	public float MaxcooldownTime;
	public static bool setorigin = false;
	private Vector3 Gameworld;
	bool switchHappened = false;

	void Start () {

	}

	public static void HideRenderRecursively(GameObject go) {						//Turn off render recursively
		if (go == null) return;
		foreach (Transform trans in go.GetComponentsInChildren<Transform>(true)) {
			if (trans.gameObject.renderer == null) return;
			trans.gameObject.renderer.enabled = false;
		}
	}
/*
	void OnTriggerEnter(Collider other) {
		//Debug.Log("Collided watch object: " + this.name);
		if (other.name == ("L_index_bone3")) {
		
			audio.PlayOneShot (spaceswitch);

			// CHANGE CODE HERE ******
			audio.PlayOneShot (spaceswitch);
			
			if (!switchHappened) {
				GameObject.Find ("OVRCameraRig").GetComponent<OVRCameraRig>().AR_is_Active = false;
				
				switchHappened = true;
			}
			else {
				GameObject.Find ("OVRCameraRig").GetComponent<OVRCameraRig>().AR_is_Active = true;
				switchHappened = false;
			}

		}
	}
			
	*/		
	
	// Update is called once per frame
	void Update () {
		Gameworld = GameObject.Find ("VREnvironment").transform.localScale;

		if (Input.GetKeyDown ("m")) {
			audio.PlayOneShot (spaceswitch);

			if (!switchHappened) {
				GameObject.Find ("OVRCameraRig").GetComponent<OVRCameraRig>().AR_is_Active = false;
				spawnVRTrees();
				switchHappened = true;
			}
			else {
				GameObject.Find ("OVRCameraRig").GetComponent<OVRCameraRig>().AR_is_Active = true;
				switchHappened = false;
			}

		}
		
		//Frame startframe = Controller.Frame ();
		//Hand rightmost = startframe.Hands.Rightmost;
		//float wrist_x = rightmost.Arm.WristPosition.x;
		//float wrist_y = rightmost.Arm.WristPosition.y;
		//float wrist_z = rightmost.Arm.WristPosition.z;
		//wristcenter = new Vector3 ( wrist_x, wrist_y, -wrist_z);
		
		//if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			
			
			//transform.position = GameObject.Find("R_wristjoint").transform.position * 3.0f;

			
			
	}
		
	public void spawnVRTrees(){
		int count = 1;
		foreach (string plant in PlantingSeed.CurrentVRPlantList){
			if (plant != null){
				Debug.Log (plant);
				GameObject newPlant = Instantiate(Resources.Load(plant), PlantingSeed.PlanetList[count].position, PlantingSeed.PlanetList[count].rotation) as GameObject;
				newPlant.transform.parent = PlantingSeed.PlanetList[count];
				count++;
			}
		}
	}

}