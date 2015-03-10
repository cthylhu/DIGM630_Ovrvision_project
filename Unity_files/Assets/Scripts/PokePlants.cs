using UnityEngine;
using System.Collections;
using Leap;


public class PokePlants : MonoBehaviour {
	
	//scripts
	public sounds sound; 
	public AudioClip movement_poke;
	
	// Use this for initialization
	void Start () {
		
		
		
	}

	void Retrigger(){
		this.GetComponent<Animator>().enabled = false;

		}
	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){
		Debug.Log("Collided plant object: " + this.name);
		if ((other.name == ("L_index_bone3"))||(other.name == ("R_index_bone3"))) {
			
			audio.PlayOneShot(movement_poke,1.0f);
			this.GetComponent<Animator>().enabled = true;

			Invoke ("Retrigger",5);
		}
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}
