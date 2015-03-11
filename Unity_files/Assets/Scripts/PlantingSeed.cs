using UnityEngine;
using System.Collections;

public class PlantingSeed : MonoBehaviour {

	public Righthand Righthand;

	GameObject basePlanet;
	GameObject basePlanetParent;
	GameObject holeModel;
	GameObject sproutModel;
	GameObject budModel;
	
	GameObject[] AllPortals;
	Transform[] PlanetList;
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
		PlanetList = GameObject.Find ("Planets").GetComponentsInChildren<Transform>();
		foreach(Transform p in PlanetList){
			Debug.Log (p.gameObject.name);
		}
		//TurnOffAllPortals ();

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

			if (hit.collider.name == "ARPlanetObject"){
				basePlanet = hit.collider.transform.Find("BasePlanet").gameObject;										// Set specific baseplanet
				basePlanetParent = basePlanet.transform.parent.gameObject;

				holeModel = basePlanetParent.transform.Find("Planet_with_hole").gameObject;		// Set specific holeModel
				sproutModel = basePlanetParent.transform.Find("Planet_with_plant").gameObject;	// Set specific sproutModel

				budModel = sproutModel.transform.Find ("bud").gameObject;

				//Debug.Log ("Parent: "+childOfCollider);
				basePlanet.renderer.material.SetColor ("_OutlineColor", Color.green);
				holeModel.renderer.material.SetColor ("_OutlineColor", Color.green);
				sproutModel.renderer.material.SetColor ("_OutlineColor", Color.green);

				hitcount++;
			
				// 2. Detect what type of plant you want to plant
				// Detect finger-poke collision with button
				if (Input.GetKeyDown ("z")) {
					basePlanetParent.GetComponent<PlanetInfo>().plantType = 1;
					Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
				}
				if (Input.GetKeyDown ("x")) {
					basePlanetParent.GetComponent<PlanetInfo>().plantType = 2;
					Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
				}
				if (Input.GetKeyDown ("c")) {
					basePlanetParent.GetComponent<PlanetInfo>().plantType = 3;
					Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
				}
				if (Input.GetKeyDown ("v")) {
					basePlanetParent.GetComponent<PlanetInfo>().plantType = 4;
					Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
				}
				if (Input.GetKeyDown ("b")) {
					basePlanetParent.GetComponent<PlanetInfo>().plantType = 5;
					Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
				}



				// 3. Dig a hole in the ground
				// Detect finger-poke collision with planet
				//if (Righthand.dighole) {
				if (Input.GetKeyDown ("a")) {

					if (!(basePlanetParent.GetComponent<PlanetInfo>().dighole)) {
						Debug.Log ("Diggy diggy hole!");
						
						basePlanet.renderer.enabled = false;
						holeModel.renderer.enabled = true;
						sproutModel.renderer.enabled = false;
						budModel.renderer.enabled = false;
						basePlanetParent.GetComponent<PlanetInfo>().dighole = true;
						
					}
					
				}


				//4. Plant the seed
				if (basePlanetParent.GetComponent<PlanetInfo>().dighole) {
					//if (seedinsoil) {
					if (Input.GetKeyDown ("s")) {
						basePlanet.renderer.enabled = false;
						holeModel.renderer.enabled = false;
						sproutModel.renderer.enabled = true;
						budModel.renderer.enabled = true;
						basePlanetParent.GetComponent<PlanetInfo>().planted = true;
						basePlanetParent.GetComponent<PlanetInfo>().seedinsoil = false;
						if (!(basePlanetParent.GetComponent<PlanetInfo>().planetSpawned)){
							Debug.Log (numberOfPlanets);
							Debug.Log ("Going to spawn next at: " + PlanetList [numberOfPlanets].name);
							
							switch (basePlanetParent.GetComponent<PlanetInfo> ().plantType) {
							
							case 0:
								//	Display nothing
								break;
							case 1:
								spawnVRtrees ("VRObjectHornbell");
								break;
							case 2:
								spawnVRtrees ("VRObjectCandyfaces");
								break;
							case 3:
								spawnVRtrees ("VRObjectGhost");
								break;
							case 4:
								spawnVRtrees ("VRObjectLollipop");
								break;
							case 5:
								spawnVRtrees ("VRObjectSkull");
								break;
							case 6:
								spawnVRtrees ("VRObjectGhost");
								break;
							case 7:
								spawnVRtrees ("VRObjectGhost");
								break;
							case 8:
								spawnVRtrees ("VRObjectGhost");
								break;
							case 9:
								spawnVRtrees ("VRObjectGhost");
								break;
							}
							numberOfPlanets++;
						}
						/*
						GameObject.Find ("GlowSeed").GetComponent<FallandFloat>().droptosoil = false;
						GameObject.Find ("GhostSeed").GetComponent<FallandFloat>().droptosoil = false;
						GameObject.Find ("CandySeed").GetComponent<FallandFloat>().droptosoil = false;
		           */
							
					}
				}

				// 5. Check if player wants to open a portal preview
				if (basePlanetParent.GetComponent<PlanetInfo>().planted) {

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
			if (basePlanet != null) {
				basePlanet.renderer.material.SetColor ("_OutlineColor", Color.clear);
				holeModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
				sproutModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
			}
		}

		// Player can close any portal at any time
		if (Input.GetKeyDown ("p")) {
			ClosePortal();
		}

	}

	public void spawnVRtrees (string prefabName) {
		// spawn a planet inside gameobject at array index numberOfPlanets
		GameObject newPlanet = Instantiate(Resources.Load(prefabName), PlanetList[numberOfPlanets].position, PlanetList[numberOfPlanets].rotation) as GameObject;
		newPlanet.transform.parent = PlanetList[numberOfPlanets];

		GameObject newPreviewPlanet = Instantiate(Resources.Load(prefabName), basePlanetParent.transform.position, basePlanetParent.transform.rotation) as GameObject;
		newPreviewPlanet.transform.parent = basePlanetParent.transform;
		newPreviewPlanet.transform.localScale = Vector3.one;

		Transform[] childs;
		childs = newPreviewPlanet.GetComponentsInChildren<Transform>();
		foreach (Transform t in childs) {
			t.gameObject.layer = LayerMask.NameToLayer("PlantLayer");
		}
		
		basePlanetParent.GetComponent<PlanetInfo>().planetSpawned = true;
	}

	public void OpenPortal (){
		// Parent portal under the appropriate view object
		//Debug.Log ("Plant type: "+basePlanet.GetComponent<PlanetInfo> ().plantType);

		//PortalObject.transform.Find ("Active").gameObject.renderer.enabled = true;
		//PortalObject.transform.Find ("Inactive").gameObject.renderer.enabled = true;
		
	}
	
	public void ClosePortal (){
		//PortalObject.transform.Find ("Active").gameObject.renderer.enabled = false;
		//PortalObject.transform.Find ("Inactive").gameObject.renderer.enabled = false;
		
	}







}
