﻿using UnityEngine;
using System.Collections;
using Leap;
[RequireComponent(typeof(AudioSource))]

public class watchtapping : MonoBehaviour {
	Controller Controller = new Controller();
	
	// Use this for initialization
	
	public AudioClip spaceswitch;

	
	public Vector3 wristcenter;
	
	private float cooldownTime;
	public float MaxcooldownTime;
	
	private Vector3 Gameworld;

	void Start () {

	}
	
	void OnTriggerEnter(Collider other) {
		
				if (other.name == ("L_index_bone3")) {
			
						audio.PlayOneShot (spaceswitch, 5.0f);

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

		//Frame startframe = Controller.Frame ();
		//Hand rightmost = startframe.Hands.Rightmost;
		//float wrist_x = rightmost.Arm.WristPosition.x;
		//float wrist_y = rightmost.Arm.WristPosition.y;
		//float wrist_z = rightmost.Arm.WristPosition.z;
		//wristcenter = new Vector3 ( wrist_x, wrist_y, -wrist_z);
		
		//if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			
			
			//transform.position = GameObject.Find("R_wristjoint").transform.position * 3.0f;
		wristcenter = this.transform.localPosition;
			
			
		}
		
		

}