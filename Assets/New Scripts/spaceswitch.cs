﻿using UnityEngine;
using System.Collections;
using Leap;


public class spaceswitch : MonoBehaviour {
	
	//scripts
	public sounds sounds;
	
	
	private Vector3 Gameworld;
	private float cooldownTime;
	public float MaxcooldownTime;
	public enum WorldState {ARworld,VRworld,VRTransition,ARTransition}
	public WorldState world = WorldState.ARworld ;
	
	
	// Use this for initialization
	void Start () {
		
		
		
		
	}
	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){
		
		if (other.name == ("L_index_bone3")) {
			
			audio.PlayOneShot (sounds.spaceswitch);
			
			if (Gameworld == new Vector3 (0, 0, 0)) {
				
				GameObject.Find ("SkyBox").transform.localScale = new Vector3 (50, 50, 50);
				cooldownTime -= Time.deltaTime;
				
				if (cooldownTime <= 0) {
					cooldownTime = MaxcooldownTime;
				}
				
			}
			
			
			if (Gameworld == new Vector3 (50, 50, 50)) {
				
				GameObject.Find ("SkyBox").transform.localScale = new Vector3 (0, 0, 0);
				
				cooldownTime -= Time.deltaTime;
				if (cooldownTime <= 0) {
					cooldownTime = MaxcooldownTime;
				}
				
			}
		}
	}
	// Update is called once per frame
	void Update () {
		Gameworld = GameObject.Find ("SkyBox").transform.localScale;
	}
}
