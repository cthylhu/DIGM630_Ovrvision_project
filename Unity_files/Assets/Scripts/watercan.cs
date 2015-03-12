using UnityEngine;
using System.Collections;

public class watercan : MonoBehaviour {

	public AudioClip watering1;
	public AudioClip watering2;
	public float step1;
	public float step2;
	public float movespeed;
	public float rotatespeed;
	private float cooldownTime;
	private Quaternion qTo;
	public float MaxcooldownTime;
	public Vector3 target;
	public enum gesturestate { none, detected, ing, end}
	public gesturestate waterplant = gesturestate.none;
	public  bool canbewatered = false;

	
	// Use this for initialization
	void Start () {

		step1 = movespeed * Time.deltaTime;
		step2 = rotatespeed * Time.deltaTime;
	}
	/*
	void WaterComes(bool planted){

		canbewatered = planted;


	}
	*/

	void WaterHere(int MarkerID){
		

		gameObject.AddComponent <OvrvisionTracker> ().markerID = MarkerID; 
		canbewatered = true;

		}


	//		 Update is called once per frame
	void Update () {


		Vector3 v3 = new Vector3 (1,1,1);

		float angle = Mathf.Atan2 (v3.z, v3.x) * Mathf.Rad2Deg;

		qTo = Quaternion.AngleAxis (angle, Vector3.down);


		if (canbewatered) {

						switch (waterplant) {

						case gesturestate.none:

								transform.position = transform.localPosition;

								if (Righthand.fist) {

										waterplant = gesturestate.detected;
		
								}
								break;

						case gesturestate.detected:

						//transform.position += new Vector3 (0, 3, 0) * speed * Time.deltaTime; 

				transform.position = Vector3.MoveTowards(transform.position, GameObject.Find ("rightpalm").transform.position, step1);

								if (Righthand.waterplant) {

										transform.position = transform.localPosition;
					transform.rotation = Quaternion.RotateTowards (transform.rotation,GameObject.Find ("rightpalm").transform.rotation, step2);
										audio.PlayOneShot (watering1, 10f);

										waterplant = gesturestate.ing;
								}
								break;

						case gesturestate.ing:

								if (!Righthand.waterplant) {


					transform.rotation = Quaternion.RotateTowards (transform.rotation, GameObject.Find ("rightpalm").transform.rotation, step2);
										audio.PlayOneShot (watering2, 10f);

										waterplant = gesturestate.end;
								}
								break;

						case gesturestate.end:
			
								if (Righthand.openhand) {

										transform.position -= new Vector3 (0, 7, 0) * step1; 
								}

								waterplant = gesturestate.none;
								break;
						}
				}
	}
}