using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour {
	bool openPortal = false;

	// Use this for initialization
	void Start () {
		GameObject.Find ("Portal1").transform.Find ("Active").gameObject.renderer.enabled = false;
		GameObject.Find ("Portal1").transform.Find ("Inactive").gameObject.renderer.enabled = false;
		//GameObject.Find("Portal1").transform.parent = GameObject.Find("LeftEyeAnchor").transform;
		//GameObject.Find("Portal1").transform.parent.transform.localPosition = new Vector3(0.0f, 0.0f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {


		if (!openPortal){
			// If player <insert gesture> while looking at a planet, activate Portal
			if (Input.GetKeyDown ("o")) {
				GameObject.Find ("Portal1").transform.Find ("Active").gameObject.renderer.enabled = true;
				GameObject.Find ("Portal1").transform.Find ("Inactive").gameObject.renderer.enabled = true;
				openPortal = true;
			}

		}
		else{
			// If player <insert other gesture> while looking at a planet, deactivate Portal
			if (Input.GetKeyDown ("p")) {
				GameObject.Find ("Portal1").transform.Find ("Active").gameObject.renderer.enabled = false;
				GameObject.Find ("Portal1").transform.Find ("Inactive").gameObject.renderer.enabled = false;
				openPortal = false;
			}
		}
	}
}
