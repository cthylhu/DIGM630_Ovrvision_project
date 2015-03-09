using UnityEngine;
using System.Collections;

public class PlantingSeed : MonoBehaviour {

	public Righthand Righthand;

	GameObject basePlanet;
	GameObject holeModel;
	GameObject sproutModel;

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
	
	// Use this for initialization
	void Start () {
		basePlanet = FindInChildren (GameObject.Find ("CubeGameObject"), "TestPlanet");
		holeModel = FindInChildren (GameObject.Find ("CubeGameObject"), "Planet_with_hole");
		sproutModel = FindInChildren (GameObject.Find ("CubeGameObject"), "Planet_with_plant");

		Debug.Log ("HoleModel: " + holeModel.name);
		Debug.Log ("SproutModel: " + sproutModel.name);

	}

	// Check whether there is seed in soil

	void CheckSeed(bool droptosoil){

		basePlanet.GetComponent<PlanetInfo>().seedinsoil = droptosoil;

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

		// 1. Detect if you're looking at a planet
		Vector3 fwd = GameObject.Find("CenterEyeAnchor").transform.forward;
		RaycastHit hit;

		if (Physics.Raycast(GameObject.Find("CenterEyeAnchor").transform.position, fwd, out hit, 20)){
			//Debug.Log ("Hit #: "+hitcount+", Collider: "+hit.collider.name);
			//Debug.DrawLine(GameObject.Find("CenterEyeAnchor").transform.position, hit.point);

			if (hit.collider.name == "CubeGameObject"){
				basePlanet = hit.collider.transform.Find("TestPlanet").gameObject;										// Set specific baseplanet
				holeModel = basePlanet.transform.parent.gameObject.transform.Find("Planet_with_hole").gameObject;		//Set specific holeModel
				sproutModel = basePlanet.transform.parent.gameObject.transform.Find("Planet_with_plant").gameObject;	//Set specific sproutModel

				//Debug.Log ("Parent: "+childOfCollider);
				basePlanet.renderer.material.SetColor ("_OutlineColor", Color.green);

				//this.renderer.material.SetColor ("_OutlineColor", Color.green);

				holeModel.renderer.material.SetColor ("_OutlineColor", Color.green);
				sproutModel.renderer.material.SetColor ("_OutlineColor", Color.green);

				hitcount++;
			

			// 2. Dig a hole in the ground
			// Detect finger-poke collision with planet
				//if (Righthand.dighole) {
				if (Input.GetKeyDown ("a")) {
					if (!(basePlanet.GetComponent<PlanetInfo>().dighole)) {
						Debug.Log ("Diggy diggy hole!");
						
						basePlanet.renderer.enabled = false;
						holeModel.renderer.enabled = true;
						sproutModel.renderer.enabled = false;
						basePlanet.GetComponent<PlanetInfo>().dighole = true;
						
					}
					
				}

			// 3. Detect what type of plant you want to plant
			// Detect finger-poke collision with button
				if (Input.GetKeyDown ("z")) {
					basePlanet.GetComponent<PlanetInfo>().plantType = 1;
				}
				if (Input.GetKeyDown ("x")) {
					basePlanet.GetComponent<PlanetInfo>().plantType = 2;
				}
				if (Input.GetKeyDown ("c")) {
					basePlanet.GetComponent<PlanetInfo>().plantType = 3;
				}

			//4. Plant the seed
				if (basePlanet.GetComponent<PlanetInfo>().dighole) {
					//if (seedinsoil) {
					if (Input.GetKeyDown ("s")) {
						basePlanet.renderer.enabled = false;
						holeModel.renderer.enabled = false;
						sproutModel.renderer.enabled = true;
						basePlanet.GetComponent<PlanetInfo>().planted = true;
						basePlanet.GetComponent<PlanetInfo>().seedinsoil = false;

						/*
						GameObject.Find ("GlowSeed").GetComponent<FallandFloat>().droptosoil = false;
						GameObject.Find ("GhostSeed").GetComponent<FallandFloat>().droptosoil = false;
						GameObject.Find ("CandySeed").GetComponent<FallandFloat>().droptosoil = false;
		           */
							
					}
				}

				if (basePlanet.GetComponent<PlanetInfo>().planted) {
					if (Righthand.normalgrow ) {
						NormalGrow();
					}
					if (Righthand.reversegrow ){
						ReverseGrow();
					}
					
				}
			}
		}
		else{
			basePlanet.renderer.material.SetColor ("_OutlineColor", Color.clear);
			holeModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
			sproutModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
		}


	}











}
