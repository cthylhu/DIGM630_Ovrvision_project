using UnityEngine;
using System.Collections;
using Leap;
[RequireComponent(typeof(AudioSource))]

public class WorldSwitch : MonoBehaviour {

	
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
				//spawnVRTrees();
				switchHappened = true;
			}
			else {
				GameObject.Find ("OVRCameraRig").GetComponent<OVRCameraRig>().AR_is_Active = true;
				switchHappened = false;
			}

		}

		Vector3 fwd = GameObject.Find("CenterEyeAnchor").transform.forward;
		// Vector3 fwd = GameObject.Find ("R_index_bone3").transform.forward;
		RaycastHit hit;

		if (!switchHappened){
			if (Physics.Raycast(GameObject.Find("CenterEyeAnchor").transform.position, fwd, out hit, 100f)){

				//Debug.Log ("Hit #: "+hitcount+", Collider: "+hit.collider.name);
				Debug.DrawLine(GameObject.Find("CenterEyeAnchor").transform.position, hit.point, Color.red);

				if (hit.collider.name == "Portal1") {
					if (hit.distance < 0.2f) {
						Debug.Log ("VR world active");
						audio.PlayOneShot (spaceswitch);

						GameObject.Find ("OVRCameraRig").GetComponent<OVRCameraRig>().AR_is_Active = false;
						this.collider.enabled = false;
						switchHappened = true;

					}
					/*else {
						Debug.Log ("AR world active");
						GameObject.Find ("OVRCameraRig").GetComponent<OVRCameraRig>().AR_is_Active = true;
						switchHappened = false;
					}*/
				}
			}
		}
	}

		
	/*public void spawnVRTrees(){
		int count = 1;
		foreach (string plant in PlantingSeed.CurrentVRPlantList){
			if (plant != null){
				Debug.Log (plant);
				GameObject newPlant = Instantiate(Resources.Load(plant), PlantingSeed.PlanetList[count].position, PlantingSeed.PlanetList[count].rotation) as GameObject;
				newPlant.transform.parent = PlantingSeed.PlanetList[count];
				count++;
			}
		}
	}*/

}