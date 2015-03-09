﻿using UnityEngine;
using System.Collections;

public class PlantingSeed : MonoBehaviour {
	 public bool planted = false;
	public  bool  dighole = false;
	 public bool seedinsoil = false;
	public int plantType = 0;
	public Righthand Righthand;
	
	Transform holeModel;
	Transform sproutModel;
	Material holeMat;
	Material sproutMat;
	int hitcount = 0;

	// Use this for initialization
	void Start () {
		//Debug.Log (gameObject.name);

		holeModel = this.transform.FindChild ("Planet_with_hole");
		sproutModel = this.transform.FindChild ("Planet_with_plant");
		holeMat = holeModel.renderer.material;
		sproutMat = sproutModel.renderer.material;
		Debug.Log ("Hole mat: "+holeMat.name);
		Debug.Log ("Sprout mat: "+sproutMat.name);
	}

	// Check whether there is seed in soil

	void CheckSeed(bool droptosoil){

		seedinsoil = droptosoil;

		}

	void NormalGrow(){

		this.transform.localScale -= new Vector3 (0.01f,0.01f,0.01f) ;
		//plantsize = plantSeed.transform.localScale;
		
		Righthand.normalgrow = false;  
	}
	
	
	void ReverseGrow(){
		//plantsize =plantSeed.transform.localScale;
		this.transform.localScale += new Vector3 (0.01f,0.01f,0.01f);
		Righthand.reversegrow = false;  
		
	}
	// Update is called once per frame

		void Update () {

		// 1. Detect which planet you're looking at
		Vector3 fwd = GameObject.Find("CenterEyeAnchor").transform.forward;
		RaycastHit hit;
		if (Physics.Raycast(GameObject.Find("CenterEyeAnchor").transform.position, fwd, out hit, 9)){
//			Debug.Log ("Hit #: "+hitcount+", Collider: "+hit.collider.name);
			hitcount++;
			this.renderer.material.SetColor ("_OutlineColor", Color.green);
			holeMat.SetColor ("_OutlineColor", Color.green);
			sproutMat.SetColor ("_OutlineColor", Color.green);

			//Debug.Log ("hParent: "+holeModel.parent);
			//Debug.Log ("sParent: "+sproutModel.parent);

			// 2. Dig a hole in the ground
			// Detect finger-poke collision with planet
			if (Righthand.dighole) {
				
				if (!dighole) {
					Debug.Log ("Diggy diggy hole!");
					
					this.renderer.enabled = false;
					holeModel.renderer.enabled = true;
					sproutModel.renderer.enabled = false;
					dighole = true;
					
				}
				
			}

			// 3. Detect what type of plant you want to plant
			// Detect finger-poke collision with button
			if (Input.GetKeyDown ("z")) {
				plantType = 1;
			}
			if (Input.GetKeyDown ("x")) {
				plantType = 2;
			}
			if (Input.GetKeyDown ("c")) {
				plantType = 3;
			}

			//4. Plant the seed
			if (dighole) {
				if (seedinsoil) {
					this.renderer.enabled = false;
					holeModel.renderer.enabled = false;
					sproutModel.renderer.enabled = true;
					planted = true;
					seedinsoil = false;

					/*
					GameObject.Find ("GlowSeed").GetComponent<FallandFloat>().droptosoil = false;
					GameObject.Find ("GhostSeed").GetComponent<FallandFloat>().droptosoil = false;
					GameObject.Find ("CandySeed").GetComponent<FallandFloat>().droptosoil = false;
                   */
						
				}
			}

			if (planted) {
				if (Righthand.normalgrow ) {
					NormalGrow();
				}
				if (Righthand.reversegrow ){
					ReverseGrow();
				}
				
			}

		}
		else{
			this.renderer.material.SetColor ("_OutlineColor", Color.clear);
			holeModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
			sproutModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
		}


	}











}
