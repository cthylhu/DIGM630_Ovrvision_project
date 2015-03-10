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

	void Start () {

	}

	public static void HideRenderRecursively(GameObject go) {						//Turn off render recursively
		if (go == null) return;
		foreach (Transform trans in go.GetComponentsInChildren<Transform>(true)) {
			if (trans.gameObject.renderer == null) return;
			trans.gameObject.renderer.enabled = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		
		/*if (other.name == ("L_index_bone3")) {
		
			audio.PlayOneShot (spaceswitch);



			if (Gameworld == new Vector3 (0, 0, 0)) {
				Debug.Log ("Switch triggered!");
				setorigin = true;

				GameObject.Find ("VREnvironment").transform.localScale = new Vector3 (1, 1, 1);
				GameObject.Find ("AREnvironment").transform.localScale = new Vector3 (0, 0, 0);
				//GameObject.Find ("Buttons").transform.localScale = new Vector3 (0, 0, 0);
				//GameObject.Find ("BGM").audio.Play ();

				cooldownTime -= Time.deltaTime;
	
				if (cooldownTime <= 0) {
					cooldownTime = MaxcooldownTime;
				}
		
			}
	

		
			if (Gameworld == new Vector3 (1, 1, 1)) {
	
				GameObject.Find ("VREnvironment").transform.localScale = new Vector3 (0, 0, 0);
				GameObject.Find ("AREnvironment").transform.localScale = new Vector3 (1, 1, 1);
				//GameObject.Find ("Buttons").transform.localScale = new Vector3 (1, 1, 1);
				//GameObject.Find ("BGM").audio.Stop ();

				cooldownTime -= Time.deltaTime;
				if (cooldownTime <= 0) {
					cooldownTime = MaxcooldownTime;
				}
	
			}
		}*/
	}
			
			
	
	// Update is called once per frame
	void Update () {
		Gameworld = GameObject.Find ("VREnvironment").transform.localScale;

		if (Input.GetKeyDown ("m")) {
			
			audio.PlayOneShot (spaceswitch);
			
			
			
			if (Gameworld == new Vector3 (0, 0, 0)) {
				Debug.Log ("Switch triggered!");
				setorigin = true;
				
				GameObject.Find ("VREnvironment").transform.localScale = new Vector3 (1, 1, 1);
				GameObject.Find ("AREnvironment").transform.localScale = new Vector3 (0, 0, 0);
				GameObject.Find ("Buttons").transform.localScale = new Vector3 (0, 0, 0);
				//GameObject.Find ("BGM").audio.Play ();
				
				cooldownTime -= Time.deltaTime;
				
				if (cooldownTime <= 0) {
					cooldownTime = MaxcooldownTime;
				}
				
			}
			
			
			
			if (Gameworld == new Vector3 (1, 1, 1)) {
				
				GameObject.Find ("VREnvironment").transform.localScale = new Vector3 (0, 0, 0);
				GameObject.Find ("AREnvironment").transform.localScale = new Vector3 (1, 1, 1);
				GameObject.Find ("Buttons").transform.localScale = new Vector3 (1, 1, 1);
				//GameObject.Find ("BGM").audio.Stop ();
				
				cooldownTime -= Time.deltaTime;
				if (cooldownTime <= 0) {
					cooldownTime = MaxcooldownTime;
				}
				
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
		
		

}