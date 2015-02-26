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
		//if (GameObject.Find("CenterEyeAnchor"))
			//this.transform.parent = GameObject.Find("CenterEyeAnchor").transform;
	}

	// UpdateTracker
	public void UpdateTransform (float[] markerGet, int elementNo) {
		int i = elementNo * MARKERGET_ARG10;

		/*Matrix4x4 T_camera = Matrix4x4.zero;			//Transform matrix of Camera
		Matrix4x4 T_marker = Matrix4x4.zero;			//Transform matrix of Marker

		//T_camera = GameObject.Find ("LeftEyeAnchor").transform.localToWorldMatrix;
		Vector3 V_camera = GameObject.Find ("LeftEyeAnchor").transform.localPosition;
		Quaternion R_camera = GameObject.Find ("LeftEyeAnchor").transform.localRotation;
		Vector3 S_camera = GameObject.Find ("LeftEyeAnchor").transform.localScale;
		Debug.Log ("Camera OLD Translation: " + V_camera);
		Debug.Log ("Camera OLD Rotation: " + R_camera);
		Debug.Log ("Camera OLD Scale: " + S_camera);

		T_camera.SetTRS (V_camera, R_camera, S_camera);
		Debug.Log ("Camera matrix: " + T_camera);


		Vector3 V_marker = new Vector3 (markerGet [i + 1], markerGet [i + 2], markerGet [i + 3]);							//Marker Translation
		Quaternion R_marker = new Quaternion (markerGet [i + 4], markerGet [i + 5], markerGet [i + 6], markerGet [i + 7]);	//Marker Rotation
		Vector3 S_marker = Vector3.one;																						//Marker Scale
		Debug.Log ("Marker OLD Translation: " + V_marker);
		Debug.Log ("Marker OLD Rotation: " + R_marker);
		Debug.Log ("Marker OLD Scale: " + S_marker);

		T_marker.SetTRS (V_marker, R_marker, S_marker);
		Debug.Log ("Marker matrix: " + T_marker);

		Matrix4x4 T_marker_world = T_camera * T_marker;				//Multiply matrices
		Debug.Log ("Marker WORLD matrix: " + T_marker_world);


		float new_marker_position_x = (float)T_marker_world [0,0];
		float new_marker_position_y = (float)T_marker_world [1,1];
		float new_marker_position_z = (float)T_marker_world [2,2];

		Vector3 Marker_position_NEW = new Vector3 (new_marker_position_x, new_marker_position_y, new_marker_position_z );

		float new_marker_quaternion_1 = (float)(T_marker_world [0,3]);
		float new_marker_quaternion_2 = (float)(T_marker_world [1,3]);
		float new_marker_quaternion_3 = (float)(T_marker_world [2,3]);
		float new_marker_quaternion_4= (float)(T_marker_world [3,3]);


		Quaternion Marker_rotation_NEW = new Quaternion (new_marker_quaternion_1, new_marker_quaternion_2, new_marker_quaternion_3, new_marker_quaternion_4);
		Debug.Log ("NEW POSITION: " + Marker_position_NEW);
		Debug.Log ("NEW ROTATION: " + Marker_rotation_NEW);

		this.transform.localPosition = Marker_position_NEW;
		this.transform.localRotation = Marker_rotation_NEW;

		Debug.Log ("===============");

		//Debug.Log ("Transform Vector: " + markerGet [i + 1]+", "+ markerGet [i + 2]+", "+ markerGet [i + 3]);
		//Debug.Log ("Transform Quat: " + markerGet [i + 4]+", "+ markerGet [i + 5]+", "+ markerGet [i + 6]+", "+ markerGet [i + 7]);


*/
		this.transform.localPosition = new Vector3(markerGet[i + 1], markerGet[i + 2], markerGet[i + 3]);
		this.transform.localRotation = new Quaternion (markerGet[i+4],markerGet[i+5],markerGet[i+6],markerGet[i+7]);

		//Debug.Log ("Position of LeftEyeAnchor: " + GameObject.Find ("LeftEyeAnchor").transform.position);
		//this.transform.position = new Vector3(markerGet[i + 1], markerGet[i + 2], markerGet[i + 3]) + GameObject.Find("CenterEyeAnchor").transform.position;
		//Debug.Log ("Position of Cube: " + this.transform.position);
		//this.transform.rotation =  new Quaternion (markerGet[i+4],markerGet[i+5],markerGet[i+6],markerGet[i+7]);
	
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
