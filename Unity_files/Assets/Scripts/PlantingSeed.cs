﻿using UnityEngine;
using System.Collections;

public class PlantingSeed : MonoBehaviour {

	public Righthand Righthand;

	GameObject basePlanet;
	GameObject basePlanetParent;
	GameObject holeModel;
	GameObject sproutModel;
	GameObject budModel;

	public static int buttonPressed = 0;
	public int plantSubType = 0;
	public bool canStartTimer = true;
	public bool haveStartedTimer = false;
	public float startTime = 0.0f;
	public float endTime = 0.0f;
	public static string[] CurrentVRPlantList = new string[10];


	GameObject[] AllPortals;
	public static Transform[] PlanetList;
	public static int numberOfPlanets = 1;

	int hitcount = 1;




	public static GameObject FindInChildren(GameObject gameObject, string name)
	{
		foreach(Transform t in gameObject.GetComponentsInChildren<Transform>())
		{
			if(t.name == name)
				return t.gameObject;
		}
		return null;
	}

	public void TurnOffAllPortals (){
		AllPortals = GameObject.FindGameObjectsWithTag("Portal1");
		foreach (GameObject p in AllPortals) {
			p.transform.Find ("Active").gameObject.renderer.enabled = false;
			p.transform.Find ("Inactive").gameObject.renderer.enabled = false;
		}
	}

	// Use this for initialization
	void Start () {

		Instantiate (Resources.Load ("Comet"));

		
		PlanetList = GameObject.Find ("Planets").GetComponentsInChildren<Transform>();
		foreach(Transform p in PlanetList){
			Debug.Log (p.gameObject.name);


		}
		//TurnOffAllPortals ();

	}

	// Check whether there is seed in soil

	void CheckSeed(bool droptosoil){

		//basePlanetParent.GetComponent<PlanetInfo>().seedinsoil = droptosoil;

		}
	// Tell the colorscheme type you have picked for the planet

	void ColorPicked(string colorname){

		basePlanetParent.GetComponent<PlanetInfo>().assigncolor = colorname;

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

		// Detect if you're looking at a planet
		Vector3 fwd = GameObject.Find("CenterEyeAnchor").transform.forward;
		RaycastHit hit;

		if (Physics.Raycast(GameObject.Find("CenterEyeAnchor").transform.position, fwd, out hit, 50)){
			//Debug.Log ("Hit #: "+hitcount+", Collider: "+hit.collider.name);
			//Debug.DrawLine(GameObject.Find("CenterEyeAnchor").transform.position, hit.point);

			if (hit.collider.name == "ARPlanetObject"){									// If raycast hits AR object, highlight it
				basePlanet = hit.collider.transform.Find("BasePlanet").gameObject;				// Set specific baseplanet
				basePlanetParent = basePlanet.transform.parent.gameObject;

				holeModel = basePlanetParent.transform.Find("Planet_with_hole").gameObject;		// Set specific holeModel
				sproutModel = basePlanetParent.transform.Find("Planet_with_plant").gameObject;	// Set specific sproutModel

				budModel = sproutModel.transform.Find ("bud").gameObject;

				//Debug.Log ("Parent: "+childOfCollider);
				basePlanet.renderer.material.SetColor ("_OutlineColor", Color.green);
				holeModel.renderer.material.SetColor ("_OutlineColor", Color.green);
				sproutModel.renderer.material.SetColor ("_OutlineColor", Color.green);

				hitcount++;
			

				// 1. Dig a hole in the ground
				// Detect finger-poke collision with planet
				//if (Righthand.dighole) {
				if (Input.GetKeyDown ("a")) {

					
					GameObject.Find ("purple").collider.enabled = true;
					GameObject.Find ("green").collider.enabled = true;
					GameObject.Find ("blue").collider.enabled = true;

					
					if (!(basePlanetParent.GetComponent<PlanetInfo>().dighole)) {
						Debug.Log ("Diggy diggy hole!");
						
						basePlanet.renderer.enabled = false;
						holeModel.renderer.enabled = true;
						sproutModel.renderer.enabled = false;
						budModel.renderer.enabled = false;
						basePlanetParent.GetComponent<PlanetInfo>().dighole = true;
						
					}
					
				}


				// 2. Pick seed

				// Button.cs: Pick a plant family, spawn a seed
				// Detect what COLOR of plant you want to plant

				// Some seed color picking code here



				// 3. Planting the seed
				// Enable buttons
				if (basePlanetParent.GetComponent<PlanetInfo>().dighole) {

					GameObject.Find ("CandySeedButton").collider.enabled= true;
					//GameObject.Find ("GhostSeedButton").collider.enabled= true;
					GameObject.Find ("GlowSeedButton").collider.enabled= true;

					basePlanetParent.GetComponent<PlanetInfo>().familyType = buttonPressed;

					//if (basePlanetParent.GetComponent<PlanetInfo>().seedinsoil) {
					if (Input.GetKeyDown ("s")) {
						basePlanet.renderer.enabled = false;
						holeModel.renderer.enabled = false;
						sproutModel.renderer.enabled = true;
						budModel.renderer.enabled = true;
						basePlanetParent.GetComponent<PlanetInfo>().planted = true;
						//GameObject.Find ("watercan").SendMessage ("WaterHere",basePlanet.GetComponent<OvrvisionTracker>().markerID );

						basePlanetParent.GetComponent<PlanetInfo>().seedinsoil = false;
						basePlanetParent.GetComponent<PlanetInfo>().dighole = false;
						//GameObject.Find ("CandySeedButton").collider.enabled= false;
						//GameObject.Find ("GhostSeedButton").collider.enabled= true;
						//GameObject.Find ("GlowSeedButton").collider.enabled= false;
						/*
						GameObject.Find ("GlowSeed").GetComponent<FallandFloat>().droptosoil = false;
						GameObject.Find ("GhostSeed").GetComponent<FallandFloat>().droptosoil = false;
						GameObject.Find ("CandySeed").GetComponent<FallandFloat>().droptosoil = false;
		           */
						
					}
				}

				// 4. The Watering Can will determine seed type

				//if(basePlanetParent.GetComponent<PlanetInfo>().planted && GameObject.Find ("watercan").GetComponent<watercan>().canbewatered){

					//GameObject.Find ("watercan").SendMessage ( "WaterComes", basePlanetParent.GetComponent<PlanetInfo>().planted = true); 

					//Choosing seed type with keys
					/*if (Input.GetKeyDown ("z")) {
						basePlanetParent.GetComponent<PlanetInfo>().plantType = 1;
						Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
						basePlanetParent.GetComponent<PlanetInfo>().watered = true; 
					}
					if (Input.GetKeyDown ("x")) {
						basePlanetParent.GetComponent<PlanetInfo>().plantType = 2;
						Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
						basePlanetParent.GetComponent<PlanetInfo>().watered = true; 
					}
					if (Input.GetKeyDown ("c")) {
						basePlanetParent.GetComponent<PlanetInfo>().plantType = 3;
						Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
						basePlanetParent.GetComponent<PlanetInfo>().watered = true; 
					}
					if (Input.GetKeyDown ("v")) {
						basePlanetParent.GetComponent<PlanetInfo>().plantType = 4;
						Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
						basePlanetParent.GetComponent<PlanetInfo>().watered = true; 
					}
					if (Input.GetKeyDown ("b")) {
						basePlanetParent.GetComponent<PlanetInfo>().plantType = 5;
						Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
						basePlanetParent.GetComponent<PlanetInfo>().watered = true; 
					}
					*/
				
				//}

				// 4. The Watering Can will determine seed type
				if (basePlanetParent != null){
					if (GameObject.Find ("watercan").GetComponent<WatercanTilt>().isPouring) {
						
						if (canStartTimer) {
							startTime = Time.time;
							canStartTimer = false; 
							haveStartedTimer = true;
						}
						
					}
					else {
						canStartTimer = true;
						if (haveStartedTimer){
							endTime = Time.time;
							basePlanetParent.GetComponent<PlanetInfo>().totalTimeElapsed = 
								basePlanetParent.GetComponent<PlanetInfo>().totalTimeElapsed + (endTime - startTime);
							haveStartedTimer = false;
							
							
							basePlanetParent.GetComponent<PlanetInfo>().planetSpawned = false;
							
						}
						// If the time elapsed since the timer was started is greater than __ seconds, plant type will be __
						if (basePlanetParent.GetComponent<PlanetInfo>().totalTimeElapsed > 5.0f && plantSubType != 2 && plantSubType != 3){
							plantSubType = 1;
							//Debug.Log ("Subtype: "+plantSubType);
							basePlanetParent.GetComponent<PlanetInfo>().watered = true;
						}
						if (basePlanetParent.GetComponent<PlanetInfo>().totalTimeElapsed > 10.0f && plantSubType != 3){
							plantSubType = 2;
							//Debug.Log ("Subtype: "+plantSubType);
							basePlanetParent.GetComponent<PlanetInfo>().watered = true;
						}
						if (basePlanetParent.GetComponent<PlanetInfo>().totalTimeElapsed > 15.0f){
							plantSubType = 3;
							//Debug.Log ("Subtype: "+plantSubType);
							basePlanetParent.GetComponent<PlanetInfo>().watered = true;
						}
						
					}
					
					// Spawn a VR preview tree at object location
					if (basePlanetParent.GetComponent<PlanetInfo>().watered){
						if (!(basePlanetParent.GetComponent<PlanetInfo>().planetSpawned)){
							
							
							Debug.Log ("# of planets: "+numberOfPlanets);
							//Debug.Log ("Going to spawn next at: " + PlanetList [numberOfPlanets].name);
							
							if (basePlanetParent.GetComponent<PlanetInfo>().familyType == 1 && plantSubType == 1){
								spawnVRtrees ("VRObjectLollipop", 1);
							}
							if (basePlanetParent.GetComponent<PlanetInfo>().familyType == 1 && plantSubType == 2){
								spawnVRtrees ("VRObjectHornbell", 2); //
							}
							if (basePlanetParent.GetComponent<PlanetInfo>().familyType == 1 && plantSubType == 3){
								spawnVRtrees ("VRObjectHornbell", 3); //
							}
							if (basePlanetParent.GetComponent<PlanetInfo>().familyType == 2 && plantSubType == 1){
								spawnVRtrees ("VRObjectSkull", 4);
							}
							if (basePlanetParent.GetComponent<PlanetInfo>().familyType == 2 && plantSubType == 2){
								spawnVRtrees ("VRObjectGhost", 5);
							}
							if (basePlanetParent.GetComponent<PlanetInfo>().familyType == 2 && plantSubType == 3){
								spawnVRtrees ("VRObjectCandyfaces", 6);
							}
							if (basePlanetParent.GetComponent<PlanetInfo>().familyType == 3 && plantSubType == 1){
								spawnVRtrees ("VRObjectHornbell", 7);
							}
							if (basePlanetParent.GetComponent<PlanetInfo>().familyType == 3 && plantSubType == 2){
								spawnVRtrees ("VRObjectHornbell", 8); //
							}
							if (basePlanetParent.GetComponent<PlanetInfo>().familyType == 3 && plantSubType == 3){
								spawnVRtrees ("VRObjectHornbell", 9); //
							}
							
							basePlanetParent.GetComponent<PlanetInfo>().watered = false;
						}
						
					}
				}
 
			}
		}
		else{
			if (basePlanet != null) {
				basePlanet.renderer.material.SetColor ("_OutlineColor", Color.clear);
				holeModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
				sproutModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
			}
			basePlanetParent = null;

		}


	}
	

	public void spawnVRtrees (string prefabName, int ptype) {
		basePlanetParent.GetComponent<PlanetInfo>().plantType = ptype;
		Transform[] destroylist = PlanetList[numberOfPlanets].GetComponentsInChildren<Transform>();

		if (destroylist.Length > 1) {

			/*foreach (Transform t in destroylist) {
				if (t.name != PlanetList[numberOfPlanets].name){
					//Debug.Log ("Destroying: "+t.name);
					Destroy(t.gameObject);
				}
			}*/
			destroylist = basePlanetParent.transform.Find ("VRPreviewContainer").GetComponentsInChildren<Transform> ();
			foreach (Transform t in destroylist) {
				if (t.name != "VRPreviewContainer"){
					//Debug.Log ("Destroying: "+t.name);
					Destroy(t.gameObject);
				}
			}
			if (numberOfPlanets != 1){
				numberOfPlanets--;
			}
		}
		// spawn a planet inside gameobject at array index numberOfPlanets
		//GameObject newPlanet = Instantiate(Resources.Load(prefabName), PlanetList[numberOfPlanets].position, PlanetList[numberOfPlanets].rotation) as GameObject;
		//newPlanet.transform.parent = PlanetList[numberOfPlanets];

		GameObject newPreviewPlanet = Instantiate(Resources.Load(prefabName), basePlanetParent.transform.Find ("VRPreviewContainer").position, basePlanet.transform.rotation) as GameObject;
		newPreviewPlanet.transform.parent = basePlanetParent.transform.Find ("VRPreviewContainer");
		newPreviewPlanet.transform.localScale = Vector3.one;

		Transform[] childs;
		childs = newPreviewPlanet.GetComponentsInChildren<Transform>();
		foreach (Transform t in childs) {
			t.gameObject.layer = LayerMask.NameToLayer("PlantLayer");
		}

		CurrentVRPlantList [numberOfPlanets - 1] = prefabName;
		numberOfPlanets++;
		basePlanetParent.GetComponent<PlanetInfo>().planetSpawned = true;
	}





}
