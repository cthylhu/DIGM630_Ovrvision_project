using UnityEngine;
using System.Collections;
using Leap;


public class Button : MonoBehaviour {
	
	//scripts
	public sounds sounds;
	public AudioClip seed;

	// Use this for initialization
	void Start () {

		
		
	}
	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){
		Debug.Log("Object: " + this.name);
		if (other.name == ("L_index_bone3")) {
						
			audio.PlayOneShot(seed,0.1f);

				}
			
	}
	// Update is called once per frame
	void Update () {

	}
}
