using UnityEngine;
using System.Collections;
using Leap;

public class Righthand : MonoBehaviour {
	Controller Controller = new Controller();
	public static bool plotplant;
	public static bool openhand;
	public static bool normalgrow;
	public static bool reversegrow;
	public static bool magicgrow;
	public static bool fist;


	
	// Use this for initialization
	void Start () {
		Controller = new Controller ();
	}
	
	// Update is called once per frame
	void Update () {
		// Frame 
		Frame startframe = Controller.Frame ();
		Frame perviousframe3 = Controller.Frame (3);
		Frame perviousframe10 = Controller.Frame (10);
		Frame perviousframe6 = Controller.Frame (6);
		
		// Right Hand variables
		Hand rightmost = startframe.Hands.Rightmost;
		Arm arm = rightmost.Arm;
		
		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			
			Finger thumb = rightmost.Fingers [0];
			Finger index = rightmost.Fingers [1];
			Finger middle = rightmost.Fingers [2];
			Finger ring = rightmost.Fingers [3];
			Finger pinky = rightmost.Fingers [4];
			
			
			float ringtipSpeed_x = ring.TipVelocity.x;
			float ringtipSpeed_y = ring.TipVelocity.y;
			float ringtipSpeed_z = ring.TipVelocity.y;
			
			//float trans_ringtipSpeed_z = perviousframe.Hands.Rightmost.Fingers [3].TipVelocity.z - ringtipSpeed_z;
			
			float pitch = rightmost.Direction.Pitch * 180.0f / Mathf.PI;
			float roll = rightmost.PalmNormal.Roll * 180.0f / Mathf.PI;
			float yaw = rightmost.Direction.Yaw * 180.0f / Mathf.PI;
			float Grab = rightmost.GrabStrength;
			float Pinch = rightmost.PinchStrength;
			float radius = rightmost.SphereRadius;
			float handmove_x = rightmost.PalmPosition.x;
			float handmove_y = rightmost.PalmPosition.y;
			float handmove_z = rightmost.PalmPosition.z;
			
			float wrist_x = rightmost.Arm.WristPosition.x;
			float wrist_y = rightmost.Arm.WristPosition.y;
			float wrist_z = rightmost.Arm.WristPosition.z;
			float elbow_x = rightmost.Arm.ElbowPosition.x;
			float elbow_y = rightmost.Arm.ElbowPosition.y;
			float elbow_z = rightmost.Arm.ElbowPosition.z;
			
			Vector3 handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);
			Vector3 wristposition= new Vector3 ( wrist_x ,wrist_y ,-wrist_z );

			
			float transRadius = perviousframe6.Hands.Rightmost.SphereRadius - radius;
			float transPitch = perviousframe10.Hands.Rightmost.Direction.Pitch - pitch;
			float transYaw = perviousframe10.Hands.Rightmost.Direction.Yaw - yaw;
			float transRoll = perviousframe10.Hands.Rightmost.PalmNormal.Roll * 180.0f / Mathf.PI - roll;
			float transWave_y_10 = perviousframe10.Hands.Rightmost.PalmPosition.y - handmove_y;
			float transWave_z_10 = perviousframe10.Hands.Rightmost.PalmPosition.z - handmove_z;
			float transWave_x_10 = perviousframe10.Hands.Rightmost.PalmPosition.x - handmove_x;
			float transWave_y_6 = perviousframe6.Hands.Rightmost.PalmPosition.y - handmove_y;
			float transWave_z_6 = perviousframe6.Hands.Rightmost.PalmPosition.z - handmove_z;
			float transWave_x_6 = perviousframe6.Hands.Rightmost.PalmPosition.x - handmove_x;
			float transWave_y_3 = perviousframe3.Hands.Rightmost.PalmPosition.y - handmove_y;
			float transWave_z_3 = perviousframe3.Hands.Rightmost.PalmPosition.z - handmove_z;
			float transWave_x_3 = perviousframe3.Hands.Rightmost.PalmPosition.x - handmove_x;

			// Gesture booleans
					
		    plotplant = !thumb.IsExtended && index.IsExtended && middle.IsExtended && !ring.IsExtended && !pinky.IsExtended;
			openhand = Grab<0.2;
			fist = Grab >0.8;
			//Debug.Log ("Open hand?: " +openhand);
			reversegrow = transWave_y_3 >5;
			normalgrow = transWave_y_3 <-5;

		}
		
	}

}
