﻿using UnityEngine;
using System.Collections;
using Leap;


public class Plants : MonoBehaviour {
	
	//scripts
	public sounds sounds;
	public AudioClip movement_poke;
	
	// Use this for initialization
	void Start () {
		
		
		
	}

	void Retrigger(){
		this.GetComponent<Animator>().enabled = false;

		}
	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){
		Debug.Log("Object: " + this.name);
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
