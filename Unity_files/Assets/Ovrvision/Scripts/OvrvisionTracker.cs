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

	//Matrix variables
	Matrix4x4 T_camera = Matrix4x4.zero;			//Transform matrix of Camera
	Matrix4x4 T_marker = Matrix4x4.zero;			//Transform matrix of Marker

	Vector3 V_camera = Vector3.zero;				//Camera Translation
	Quaternion R_camera = Quaternion.identity;		//Camera Rotation
	Vector3 S_camera = Vector3.one;					//Camera Scale

	Vector3 V_marker = 	Vector3.zero;				//Marker Translation
	Quaternion R_marker = Quaternion.identity;		//Marker Rotation
	Vector3 S_marker = Vector3.one;					//Marker Scale

	Matrix4x4 displayNewPosition = Matrix4x4.zero;
	public bool Parented = false;

	// ------ Function ------

	// Tracker initialization
	public void Start()
	{
		if (Parented) {
			if (GameObject.Find ("LeftEyeAnchor"))
				this.transform.parent = GameObject.Find ("LeftEyeAnchor").transform;
		}
	}

	// UpdateTracker
	public void UpdateTransform (float[] markerGet, int elementNo) {
		int i = elementNo * MARKERGET_ARG10;
		if (Parented) 
		{
			// Original transforms
			this.transform.localPosition = new Vector3 (markerGet [i + 1], markerGet [i + 2], markerGet [i + 3]);
			this.transform.localRotation = new Quaternion (markerGet [i + 4], markerGet [i + 5], markerGet [i + 6], markerGet [i + 7]);
			//Debug.Log ("Transform Vector: " + markerGet [i + 1]+", "+ markerGet [i + 2]+", "+ markerGet [i + 3]);
			//Debug.Log ("Transform Quat: " + markerGet [i + 4]+", "+ markerGet [i + 5]+", "+ markerGet [i + 6]+", "+ markerGet [i + 7]);
		}else
		{
			Debug.Log ("Camera matrix: " + T_camera);

			V_marker = new Vector3 (markerGet [i + 1], markerGet [i + 2], markerGet [i + 3]);
			R_marker = new Quaternion (markerGet [i + 4], markerGet [i + 5], markerGet [i + 6], markerGet [i + 7]);	
			S_marker = Vector3.one;
			T_marker.SetTRS (V_marker, R_marker, S_marker);
			//Debug.Log ("Marker OLD Translation: " + V_marker);
			//Debug.Log ("Marker OLD Rotation: " + R_marker);
			//Debug.Log ("Marker OLD Scale: " + S_marker);
			Debug.Log ("Marker matrix: " + T_marker);
			
			Matrix4x4 T_marker_world = T_camera * T_marker;				//Multiply matrices
			//Matrix4x4 T_marker_world = T_marker * T_camera;				//Multiply matrices
			Debug.Log ("Marker WORLD matrix: " + T_marker_world);
			
			Vector3 Marker_position_NEW = T_marker_world.GetColumn (3);

			Quaternion Marker_rotation_NEW = QuaternionFromMatrix (T_marker_world);

			Vector3 Marker_scale_NEW = new Vector3(
				T_marker_world.GetColumn(0).magnitude,
				T_marker_world.GetColumn(1).magnitude,
				T_marker_world.GetColumn(2).magnitude
				);

			// FOR DISPLAYING THE FLOAT VALUES BETTER, because Debug.Log for Vectors truncates float values
			Vector4 posVector = new Vector4 (Marker_position_NEW.x, Marker_position_NEW.y, Marker_position_NEW.z, 0);
			Vector4 rotVector = new Vector4 (Marker_rotation_NEW.w, Marker_rotation_NEW.x, Marker_rotation_NEW.y, Marker_rotation_NEW.z);
			Vector4 scaleVector = new Vector4(Marker_scale_NEW.x, Marker_scale_NEW.y, Marker_scale_NEW.z, 0);
			
			displayNewPosition.SetRow (0, posVector);
			displayNewPosition.SetRow (1, rotVector);
			displayNewPosition.SetRow (2, scaleVector);
			Debug.Log ("NEW POS/ROT/SCALE: " + displayNewPosition);


			// Apply new transformations
			this.transform.position = Marker_position_NEW;
			this.transform.rotation = Marker_rotation_NEW;
			this.transform.localScale = Marker_scale_NEW;
			
			Debug.Log ("===============");
		}



	}

	public static Quaternion QuaternionFromMatrix(Matrix4x4 m) { 
		return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1)); 	//Creates a rotation with the specified forward and upwards directions.
	}
	
	public void Update(){
		V_camera = GameObject.Find ("CenterEyeAnchor").transform.position;
		R_camera = GameObject.Find ("CenterEyeAnchor").transform.rotation;
		S_camera = GameObject.Find ("CenterEyeAnchor").transform.localScale;
		T_camera.SetTRS (V_camera, R_camera, S_camera);

	}

	public void UpdateTransformNone () {
		this.transform.localPosition = new Vector3 (-10000.0f, -10000.0f, -10000.0f);
	}
}
