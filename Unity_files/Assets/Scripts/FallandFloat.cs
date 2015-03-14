using UnityEngine;
using System.Collections;

public class FallandFloat : MonoBehaviour {
	public float xSpeed;
	public float ySpeed;
	public float zSpeed;

	public float minRotationSpeed;
	public float maxRotationSpeed;
	public float minMovementSpeed;
	public float maxMovementSpeed;

	private float rotationSpeed=1f;
	private float movementSpeed=1f;// Degrees per second
	//private float movementSpeed =2f; // Units per second;

	public Vector3 ButtonPosition;

	//private Transform target2;
	//private Transform target3;
	private Quaternion qTo;
	public Button Button;
	public Grab Grab;
	public sounds sounds;
	public bool droptosoil;
	public string SeedName;

	public void DisableSeedRenders(string seedname){
		Renderer[] list = GameObject.Find (seedname).GetComponentsInChildren<Renderer>();
		foreach (Renderer r in list) {
			r.enabled = false;
		}
	}
	// Use this for initialization
	void Start () {
		droptosoil = false;
		//target = GameObject.Find ("GlowSeedButton").transform;  	
		//target2 = GameObject.Find ("GhostSeedButton").transform; 
		//target3 = GameObject.Find ("CandySeedButton").transform;  
		rotationSpeed = Random.Range (minRotationSpeed, maxRotationSpeed);
		movementSpeed = Random.Range (minMovementSpeed, maxMovementSpeed);
	
	}

	void Fall (Vector3 position){
		ButtonPosition = position;
		Debug.Log ("Set target button position!");
		
	}

	// Update is called once per frame
	void Update () {
		         
		if (Button.seedGenerated) {

			if (!Grab.Grabbed) {
				if (PlantingSeed.buttonPressed == 1){
					SeedName = "CandySeed";
				}
				if (PlantingSeed.buttonPressed == 2){
					SeedName = "GhostSeed";
				}
				if (PlantingSeed.buttonPressed == 3){
					SeedName = "GlowSeed";
				}
				//Debug.Log (SeedName);

				//Vector3 v3 = ButtonPosition - GameObject.Find (SeedName).transform.position;
				//float angle = Mathf.Atan2 (v3.z, v3.x) * Mathf.Rad2Deg;
				//qTo = Quaternion.AngleAxis (angle, Vector3.down);

				transform.Rotate(0f, ySpeed * Time.deltaTime, 0f);
				
				//transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
				//transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);

				//transform.Rotate (Vector3.right * Time.deltaTime);
			}
		}
	}

	void OnTriggerExit(Collider other){
		
		//Debug.Log("Fall into Object: " + other.name);
		
		if (other.name == "FallCollider") {
			
			// if(this.name =="GlowSeed(Clone") {
			
			//GameObject.Find ("GlowSeed").GetComponent<Grab>().Grabbed = false;
			
			Destroy (this.rigidbody);
			
			//Destroy(this);
			
			Debug.Log ("Seed Planted");
			
			Button.seedGenerated = false;
			
			Grab.Grabbed = false;
			
			other.audio.PlayOneShot (sounds.planted);
			
			if (this.name == "CandySeed") {
				DisableSeedRenders ("CandySeed");
			}
			if (this.name == "GhostSeed") {
				DisableSeedRenders ("GhostSeed");
			}
			if (this.name == "GlowSeed") {
				DisableSeedRenders ("GlowSeed");
			}
			
			GameObject.Find ("AREnvironment").GetComponent<PlantingSeed> ().SendMessage ("CheckSeed", droptosoil = true);
			
		}

	}
}
