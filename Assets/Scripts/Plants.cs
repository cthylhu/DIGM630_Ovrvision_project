using UnityEngine;
using System.Collections;
using Leap;


public class Plants : MonoBehaviour {
	
	//scripts
	public sounds sounds;
	public AudioClip movement_poke;
	
	// Use this for initialization
	void Start () {
		
		
		
	}
	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){
		Debug.Log("Object: " + this.name);
		if ((other.name == ("L_index_bone3"))||(other.name == ("R_index_bone3"))) {
			
			audio.PlayOneShot(movement_poke,1.0f);
			
		}
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}
