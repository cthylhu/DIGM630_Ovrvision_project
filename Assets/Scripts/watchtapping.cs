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
	
	private Vector3 Gameworld;

	void Start () {

	}
	
	void OnTriggerEnter(Collider other) {
		
		if (other.name == ("L_index_bone3")) {
	
			audio.PlayOneShot (spaceswitch);

			if (Gameworld == new Vector3 (0, 0, 0)) {
		
				GameObject.Find ("VREnvironment").transform.localScale = new Vector3 (1, 1, 1);

				GameObject.Find("VRHornedbellTree").renderer.enabled = true;
				GameObject.Find("VRJellyFishTree").renderer.enabled = true;
				GameObject.Find("VRHornedbellTree").collider.enabled = true;
				GameObject.Find("VRJellyFishTree").collider.enabled = true;

				GameObject.Find("GhostTree").renderer.enabled = true;
				GameObject.Find("GhostTree").collider.enabled = true;

				GameObject.Find("Planet").renderer.enabled = true;
				GameObject.Find("Planet2").renderer.enabled = true;
				GameObject.Find ("TestPlanet").renderer.enabled = false;
				GameObject.Find ("TestPlanet").GetComponent<PlotPlant>().enabled = false;

				/*GameObject.Find ("GlowSeedButton").renderer.enabled= false;
				GameObject.Find ("GhostSeedButton").renderer.enabled= false;
				GameObject.Find ("CandySeedButton").renderer.enabled= false;

				GameObject.Find ("GlowSeedButtonCollide").collider.enabled= false;
				GameObject.Find ("GhostSeedButtonCollide").collider.enabled= false;
				GameObject.Find ("CandySeedButtonCollide").collider.enabled= false;*/

				cooldownTime -= Time.deltaTime;
	
				if (cooldownTime <= 0) {
					cooldownTime = MaxcooldownTime;
				}
		
			}
	

		
			if (Gameworld == new Vector3 (1, 1, 1)) {
	
				GameObject.Find ("VREnvironment").transform.localScale = new Vector3 (0, 0, 0);
				GameObject.Find("VRHornedbellTree").renderer.enabled = false;
				GameObject.Find("VRJellyFishTree").renderer.enabled = false;
				GameObject.Find("VRHornedbellTree").collider.enabled = false;
				GameObject.Find("VRJellyFishTree").collider.enabled = false;

				GameObject.Find("GhostTree").renderer.enabled = false;
				GameObject.Find("GhostTree").collider.enabled = false;

				GameObject.Find("Planet").renderer.enabled = false;
				GameObject.Find("Planet2").renderer.enabled = false;
				GameObject.Find ("TestPlanet").renderer.enabled = true;
				GameObject.Find ("TestPlanet").GetComponent<PlotPlant>().enabled = true;
			
				/*GameObject.Find ("GlowSeedButton").renderer.enabled= true;
				GameObject.Find ("GhostSeedButton").renderer.enabled= true;
				GameObject.Find ("CandySeedButton").renderer.enabled= true;

				GameObject.Find ("GlowSeedButtonCollide").collider.enabled= true;
				GameObject.Find ("GhostSeedButtonCollide").collider.enabled= true;
				GameObject.Find ("CandySeedButtonCollide").collider.enabled= true;*/

				cooldownTime -= Time.deltaTime;
				if (cooldownTime <= 0) {
					cooldownTime = MaxcooldownTime;
				}
	
			}
		}
	}
			
			
	
	// Update is called once per frame
	void Update () {
		Gameworld = GameObject.Find ("VREnvironment").transform.localScale;

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