using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour {
	bool opened = false;
	GameObject PortalObject;

	// Use this for initialization
	void Start () {
		PortalObject = GameObject.Find ("Portal1");
		PortalObject.transform.Find ("Active").gameObject.renderer.enabled = false;
		PortalObject.transform.Find ("Inactive").gameObject.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {


		/*if (!opened){
			// If player <insert gesture> while looking at a planet, activate Portal
			if (Input.GetKeyDown ("o")) {
				PortalObject.transform.Find ("Active").gameObject.renderer.enabled = true;
				PortalObject.transform.Find ("Inactive").gameObject.renderer.enabled = true;
				opened = true;
			}

		}
		else{
			// If player <insert other gesture> while looking at a planet, deactivate Portal
			if (Input.GetKeyDown ("p")) {
				PortalObject.transform.Find ("Active").gameObject.renderer.enabled = false;
				PortalObject.transform.Find ("Inactive").gameObject.renderer.enabled = false;
				opened = false;
			}
		}*/
	}

	public void OpenPortal (){
		PortalObject.transform.Find ("Active").gameObject.renderer.enabled = true;
		PortalObject.transform.Find ("Inactive").gameObject.renderer.enabled = true;

	}

	public void ClosePortal (){
		PortalObject.transform.Find ("Active").gameObject.renderer.enabled = false;
		PortalObject.transform.Find ("Inactive").gameObject.renderer.enabled = false;

	}
}
