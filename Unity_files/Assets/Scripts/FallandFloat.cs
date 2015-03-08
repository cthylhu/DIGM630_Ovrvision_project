using UnityEngine;
using System.Collections;

public class FallandFloat : MonoBehaviour {
	public float minRotationSpeed;
	public float maxRotationSpeed;
	public float minMovementSpeed;
	public float maxMovementSpeed;
	private float rotationSpeed=4.0f; // Degrees per second
	private float movementSpeed =5.0f; // Units per second;

	public Vector3 target;

	//private Transform target2;
	//private Transform target3;
	private Quaternion qTo;
	public Button Button;
	public Grab Grab;
	public sounds sounds;
	public bool droptosoil;


	// Use this for initialization
	void Start () {

		//target = GameObject.Find ("GlowSeedButton").transform;  	
		//target2 = GameObject.Find ("GhostSeedButton").transform; 
		//target3 = GameObject.Find ("CandySeedButton").transform;  
		rotationSpeed = Random.Range (minRotationSpeed, maxRotationSpeed);
		movementSpeed = Random.Range (minMovementSpeed, maxMovementSpeed);
	
	}

	void Fall(Vector3 ButtonPosition){
		  
		 target = ButtonPosition;
	   
	     transform.localPosition -= new Vector3 (0, 1f, 0);
	    
			
		Debug.Log (" falling!");
		
	}



	void OnTriggerExit(Collider other){
		
		//Debug.Log("Object: " + this.name);
		
		
		
		if (other.name == "Marker"){







		   // if(this.name =="GlowSeed(Clone") {

			//GameObject.Find ("GlowSeed").GetComponent<Grab>().Grabbed = false;
			
			Destroy(this.rigidbody);

			//Destroy(this);

				Debug.Log ("Seed Planted");
				
				Button.seednumber = 0;
			    
			    Grab.Grabbed = false;
			
			    other.audio.PlayOneShot(sounds.planted);
			   
			    GameObject.Find ("TestPlanet2").GetComponent<PlantingSeed>().SendMessage("Check", droptosoil = true);
		  }


			/*if(this.name =="GhostSeed(Clone") {
				
				//GameObject.Find ("GhostSeed").GetComponent<Grab>().Grabbed = false;
				
				Destroy(this.rigidbody);
				
				//Destroy(this);

				Debug.Log ("Seed Planted");
				
				Button.seednumber = 0;
				
				other.audio.PlayOneShot(sounds.planted);
				
				
			}


			if(this.name =="CandySeed(Clone") {
				
				GameObject.Find ("CandySeed").GetComponent<Grab>().Grabbed = false;
				
				Destroy(GameObject.Find ("CandySeed").rigidbody);
				
				Destroy(this);

				Debug.Log ("Seed Planted");
				
				Button.seednumber = 0;
				
				other.audio.PlayOneShot(sounds.planted);
				
				
			}

		}*/
	}


	// Update is called once per frame
	void Update () {
		         
				if (Button.seednumber==1) {

				if (!Grab.Grabbed) {
								Vector3 v3 = target - transform.position;
								float angle = Mathf.Atan2 (v3.z, v3.x) * Mathf.Rad2Deg;
								qTo = Quaternion.AngleAxis (angle, Vector3.down);
								transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
								transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);
						}
				}
		/*

		if (GameObject.Find ("GhostSeedButton").GetComponent<Button>().seednumber==1) {
			
		if (!this.GetComponent<Grab>().Grabbed) {
				Vector3 v3 = target2.position - transform.position;
				float angle = Mathf.Atan2 (v3.z, v3.x) * Mathf.Rad2Deg;
				qTo = Quaternion.AngleAxis (angle, Vector3.down);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
				transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);
			}
		}


		if (GameObject.Find ("CandySeedButton").GetComponent<Button>().seednumber==1) {
			
		if (!this.GetComponent<Grab>().Grabbed) {
				Vector3 v3 = target3.position - transform.position;
				float angle = Mathf.Atan2 (v3.z, v3.x) * Mathf.Rad2Deg;
				qTo = Quaternion.AngleAxis (angle, Vector3.down);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
				transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);
			}
		}
		*/
		}
}
