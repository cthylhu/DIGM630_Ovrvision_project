using UnityEngine;
using System.Collections;
using Leap;

public class fingerbone : MonoBehaviour
{
	Controller Controller = new Controller ();
	public Finger.FingerType fingerType;
	public Bone.BoneType BoneType ;
	public static bool fingerdig;
	static Frame startframe;
	static Hand rightmost;
	static Finger finger_ ;
	static Bone bone;
	static float Roll;
	static float Pitch;
	static float Yaw;

	public static Vector3 fingerfwd ;
	public float fingerfwd_x;
	public float fingerfwd_y;
	public float fingerfwd_z;


	
	// Use this for initialization
	void Start ()
	{
		Controller = new Controller ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		startframe = Controller.Frame ();
		Frame perviousframe3 = Controller.Frame (3);
		Frame perviousframe10 = Controller.Frame (10);
		Frame perviousframe6 = Controller.Frame (6);
		rightmost = startframe.Hands.Rightmost;
		/*
		Finger finger_ = startframe.Fingers [(int)fingerType];
		Bone bone = finger_.Bone (BoneType);
*/
		//if (script.levelcount != 2 && script.levelcount >= 0) {
		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			finger_ = rightmost.Fingers [(int)fingerType];
			bone = finger_.Bone (BoneType);
			
			Roll = bone.Direction.Roll * 180.0f / Mathf.PI;
			Yaw = bone.Basis.yBasis.Yaw * 180.0f / Mathf.PI;
			Pitch = bone.Direction.Pitch * 180.0f / Mathf.PI;
			fingerfwd_x = bone.Basis.zBasis.x;
			fingerfwd_y = bone.Basis.zBasis.y;
			fingerfwd_z = bone.Basis.zBasis.z;
			fingerfwd = new Vector3 (fingerfwd_x,fingerfwd_y,fingerfwd_z);

			Quaternion twist = Quaternion.Euler (Pitch, Yaw, -Roll);
			//float bonemove_x= bone.Direction.
			
			float boneEnd_x = bone.NextJoint.x;
			float boneEnd_y = bone.NextJoint.y;
			float boneEnd_z = bone.NextJoint.z;
			float boneLength = bone.Length;
			Vector3 boneEnd = new Vector3 (boneEnd_x, boneEnd_y, -boneEnd_z);


			fingerdig = perviousframe3.Hands.Rightmost.Fingers[1].TipPosition.y - fingerfwd_y >170 ;
			//transform.rotation = Quaternion.Slerp (transform.rotation, twist, Time.deltaTime * smooth); 
			//transform.position = boneEnd * 0.05f;
		}
		//}
	}	
}

