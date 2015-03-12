using UnityEngine;
using System.Collections;
using Leap;

public class Lefthand : MonoBehaviour {
	Controller Controller = new Controller();
	public static bool plotplant;
	public static bool openhand;
	public static bool normalgrow;
	public static bool reversegrow;
	public static bool magicgrow;
	public static bool fist;
	public static bool sprinkle;
	public static bool palmdown;
	public Vector3 palmposition;
	
	
	
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
		Hand leftmost = startframe.Hands.Leftmost;
		Arm arm = leftmost.Arm;
		
		if ((leftmost.IsLeft) && (startframe.Hands.Count > 0)) {
			
			Finger thumb = leftmost.Fingers [0];
			Finger index = leftmost.Fingers [1];
			Finger middle = leftmost.Fingers [2];
			Finger ring = leftmost.Fingers [3];
			Finger pinky = leftmost.Fingers [4];

			float thumbtipSpeed= thumb.TipVelocity.x;
			float indextipSpeed= index.TipVelocity.x;
			float middletipSpeed = middle.TipVelocity.x;
			float ringtipSpeed= ring.TipVelocity.x;
			float pinkytipSpeed = pinky.TipVelocity.x;

			
			//float trans_ringtipSpeed_z = perviousframe.Hands.Rightmost.Fingers [3].TipVelocity.z - ringtipSpeed_z;
			
			float pitch = leftmost.Direction.Pitch * 180.0f / Mathf.PI;
			float roll = leftmost.PalmNormal.Roll * 180.0f / Mathf.PI;
			float yaw = leftmost.Direction.Yaw * 180.0f / Mathf.PI;
			float Grab = leftmost.GrabStrength;
			float Pinch = leftmost.PinchStrength;
			float radius = leftmost.SphereRadius;
			float handmove_x = leftmost.PalmPosition.x;
			float handmove_y = leftmost.PalmPosition.y;
			float handmove_z = leftmost.PalmPosition.z;
			
			float wrist_x = leftmost.Arm.WristPosition.x;
			float wrist_y = leftmost.Arm.WristPosition.y;
			float wrist_z = leftmost.Arm.WristPosition.z;
			float elbow_x = leftmost.Arm.ElbowPosition.x;
			float elbow_y = leftmost.Arm.ElbowPosition.y;
			float elbow_z = leftmost.Arm.ElbowPosition.z;
			
			Vector3 handcenter = new Vector3 (handmove_x, handmove_y, -handmove_z);
			
			palmposition = handcenter;
			
			Vector3 wristposition= new Vector3 ( wrist_x ,wrist_y ,-wrist_z );
			
			
			float transRadius = perviousframe6.Hands.Leftmost.SphereRadius - radius;
			float transPitch = perviousframe10.Hands.Leftmost.Direction.Pitch - pitch;
			float transYaw = perviousframe10.Hands.Leftmost.Direction.Yaw - yaw;
			float transRoll = perviousframe10.Hands.Leftmost.PalmNormal.Roll * 180.0f / Mathf.PI - roll;
			float transWave_y_10 = perviousframe10.Hands.Leftmost.PalmPosition.y - handmove_y;
			float transWave_z_10 = perviousframe10.Hands.Leftmost.PalmPosition.z - handmove_z;
			float transWave_x_10 = perviousframe10.Hands.Leftmost.PalmPosition.x - handmove_x;
			float transWave_y_6 = perviousframe6.Hands.Leftmost.PalmPosition.y - handmove_y;
			float transWave_z_6 = perviousframe6.Hands.Leftmost.PalmPosition.z - handmove_z;
			float transWave_x_6 = perviousframe6.Hands.Leftmost.PalmPosition.x - handmove_x;
			float transWave_y_3 = perviousframe3.Hands.Leftmost.PalmPosition.y - handmove_y;
			float transWave_z_3 = perviousframe3.Hands.Leftmost.PalmPosition.z - handmove_z;
			float transWave_x_3 = perviousframe3.Hands.Leftmost.PalmPosition.x - handmove_x;


			float transPinch = perviousframe3.Hands.Leftmost.PinchStrength - Pinch;
			
			// Gesture booleans
			palmdown = roll<20 && roll>-20;	

			plotplant = !thumb.IsExtended && index.IsExtended && middle.IsExtended && !ring.IsExtended && !pinky.IsExtended;
			openhand = Grab<0.2;
			fist = Grab >0.8;

			//Debug.Log ("Open hand?: " +openhand);
			reversegrow = transWave_y_3 >5;
			normalgrow = transWave_y_3 <-5;

			sprinkle = 	indextipSpeed>5;

			transform.position = palmposition;
		}
		
	}
	
}