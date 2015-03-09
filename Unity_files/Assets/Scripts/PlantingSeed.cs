using UnityEngine;
using System.Collections;

public class PlantingSeed : MonoBehaviour {

	public Righthand Righthand;

	GameObject basePlanet;
	GameObject holeModel;
	GameObject sproutModel;
	GameObject PortalObject;
	GameObject[] AllPortals;

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
		basePlanet = FindInChildren (GameObject.Find ("AREnvironment"), "TestPlanet");
		holeModel = FindInChildren (GameObject.Find ("AREnvironment"), "Planet_with_hole");
		sproutModel = FindInChildren (GameObject.Find ("AREnvironment"), "Planet_with_plant");

		Debug.Log ("HoleModel: " + holeModel.name);
		Debug.Log ("SproutModel: " + sproutModel.name);

		PortalObject = FindInChildren (GameObject.Find ("AREnvironment"), "Portal1");
		TurnOffAllPortals ();

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

		if (Physics.Raycast(GameObject.Find("CenterEyeAnchor").transform.position, fwd, out hit, 50)){
			//Debug.Log ("Hit #: "+hitcount+", Collider: "+hit.collider.name);
			//Debug.DrawLine(GameObject.Find("CenterEyeAnchor").transform.position, hit.point);

			if (hit.collider.name == "CubeGameObject"){
				basePlanet = hit.collider.transform.Find("TestPlanet").gameObject;										// Set specific baseplanet
				holeModel = basePlanet.transform.parent.gameObject.transform.Find("Planet_with_hole").gameObject;		// Set specific holeModel
				sproutModel = basePlanet.transform.parent.gameObject.transform.Find("Planet_with_plant").gameObject;	// Set specific sproutModel
				PortalObject = basePlanet.transform.parent.gameObject.transform.Find("PortalContainer").gameObject.transform.Find ("Portal1").gameObject;

				//Debug.Log ("Parent: "+childOfCollider);
				basePlanet.renderer.material.SetColor ("_OutlineColor", Color.green);

				//this.renderer.material.SetColor ("_OutlineColor", Color.green);

				holeModel.renderer.material.SetColor ("_OutlineColor", Color.green);
				sproutModel.renderer.material.SetColor ("_OutlineColor", Color.green);

				hitcount++;
			
				// 2. Detect what type of plant you want to plant
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
				if (Input.GetKeyDown ("v")) {
					basePlanet.GetComponent<PlanetInfo>().plantType = 4;
				}
				if (Input.GetKeyDown ("b")) {
					basePlanet.GetComponent<PlanetInfo>().plantType = 5;
				}
				
				// 3. Dig a hole in the ground
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

				// 5. Check if player wants to open a portal preview
				if (basePlanet.GetComponent<PlanetInfo>().planted) {

					if (Input.GetKeyDown ("o")) {
						OpenPortal();
					}


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

		// Player can close any portal at any time
		if (Input.GetKeyDown ("p")) {
			ClosePortal();
		}

	}



	public void OpenPortal (){
		// Parent portal under the appropriate view object
		Debug.Log ("Plant type: "+basePlanet.GetComponent<PlanetInfo> ().plantType);

		switch (basePlanet.GetComponent<PlanetInfo> ().plantType) {
		
		case 0:
			GameObject.Find ("Portal2").transform.position = new Vector3(-1000.0f, -1000.0f, -1000.0f);
			break;
		case 1:
			GameObject.Find ("Portal2").transform.SetParent(GameObject.Find ("Portal_hornbell").transform.Find ("PortalContainer2"), false);
			break;
		case 2:
			GameObject.Find ("Portal2").transform.SetParent(GameObject.Find ("Portal_candyfaces").transform.Find ("PortalContainer2"), false);
			break;
		case 3:
			GameObject.Find ("Portal2").transform.SetParent(GameObject.Find ("Portal_skullshroom").transform.Find ("PortalContainer2"), false);
			break;
		case 4:
			GameObject.Find ("Portal2").transform.SetParent(GameObject.Find ("Portal_ghosttree").transform.Find ("PortalContainer2"), false);
			break;
		case 5:
			GameObject.Find ("Portal2").transform.SetParent(GameObject.Find ("Portal_lollipop").transform.Find ("PortalContainer2"), false);
			break;
		}


		PortalObject.transform.Find ("Active").gameObject.renderer.enabled = true;
		PortalObject.transform.Find ("Inactive").gameObject.renderer.enabled = true;
		
	}
	
	public void ClosePortal (){
		PortalObject.transform.Find ("Active").gameObject.renderer.enabled = false;
		PortalObject.transform.Find ("Inactive").gameObject.renderer.enabled = false;
		
	}







}
