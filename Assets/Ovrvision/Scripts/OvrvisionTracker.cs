using UnityEngine;
using System.Collections;

/// <summary>
/// This class provides main interface to the Ovrvision Ex
/// </summary>
public class OvrvisionTracker : MonoBehaviour {

	//var
	public int markerID = 0;
	//define
	private const int MARKERGET_ARG10 = 10; 
	
	// ------ Function ------

	// Tracker initialization
	public void Start()
	{
		/*if (GameObject.Find("LeftEyeAnchor"))
			this.transform.parent = GameObject.Find("LeftEyeAnchor").transform;*/
	}

	// UpdateTracker
	public void UpdateTransform (float[] markerGet, int elementNo) {
		int i = elementNo * MARKERGET_ARG10;

		//Debug.Log ("Transform Vector: " + markerGet [i + 1]+", "+ markerGet [i + 2]+", "+ markerGet [i + 3]);
		//Debug.Log ("Transform Quat: " + markerGet [i + 4]+", "+ markerGet [i + 5]+", "+ markerGet [i + 6]+", "+ markerGet [i + 7]);



		//this.transform.localPosition = new Vector3(markerGet[i + 1], markerGet[i + 2], markerGet[i + 3]);
		//this.transform.localRotation = new Quaternion (markerGet[i+4],markerGet[i+5],markerGet[i+6],markerGet[i+7]);

		//Debug.Log ("Position of LeftEyeAnchor: " + GameObject.Find ("LeftEyeAnchor").transform.position);
		this.transform.position = new Vector3(markerGet[i + 1], markerGet[i + 2], markerGet[i + 3]) + GameObject.Find("LeftEyeAnchor").transform.position;
		//Debug.Log ("Position of Cube: " + this.transform.position);

		this.transform.rotation =  new Quaternion (markerGet[i+4],markerGet[i+5],markerGet[i+6],markerGet[i+7]);
	
	}

	public void Update(){
		if (Input.GetKeyDown ("a")) {
			Debug.Log ("Current Vector: "+this.transform.position);
		}
	}

	public void UpdateTransformNone () {
		this.transform.localPosition = new Vector3 (-10000.0f, -10000.0f, -10000.0f);
	}
}
