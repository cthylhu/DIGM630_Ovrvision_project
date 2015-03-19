using UnityEngine;
using System.Collections;
using Leap;
public class PlantingSeed : MonoBehaviour {
	Controller Controller = new Controller ();
	
	public enum gesturestate {none,begin,end};
	public gesturestate digging = gesturestate.none;
	public gesturestate fingerdig = gesturestate.none;
	private float cooldownTime;
	public float MaxcooldownTime;

	public static bool isDiggingHole = false;
	public static bool isLooking = false;
	
	public Righthand Righthand;
	
	//	audio narration check numbers
	public int raycastcount;
	public int digholecount;
	public int waterplantcount;
	public int plantatreecount;
	public int plantgrow;
	public narration narration;
	public sounds sounds;

	// GameObjects to hold to currently viewed AR object
	public GameObject basePlanet;
	public GameObject basePlanetParent;
	public static GameObject lastSeenARObject;
	public static GameObject currentARObject;
	GameObject holeModel;
	GameObject sproutModel;
	GameObject budModel;

	// For keeping track of stuff
	public static int buttonPressed = 0;
	public int plantSubType = 0;
	public bool canStartTimer = true;
	public bool haveStartedTimer = false;
	public float startTime = 0.0f;
	public float endTime = 0.0f;
	public static string[] CurrentVRPlantList = new string[10];
	public static bool isDropping = false;
	GameObject[] AllPortals;
	public static GameObject[] PlanetList;
	public static int numberOfPlanets;	
	Color toonOutlineColor = Color.green;
	public int handlayermask;
	public static bool doneDropping = false;

	// Leap Hand position variables
	static Frame frame;
	static Frame perviousframe3;
	static Frame perviousframe10;
	static Frame perviousframe6;
	static Hand rightmost;
	static Hand leftmost;
	static float pitch;
	static float transPitch;
	static float Grab_L;
	static float Grab_R;
	static float Pinch_L;
	static float Pinch_R;
	static float roll_l;
	static float roll_r;
	static bool palmdown_r;
	
		
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
		//Instantiate (Resources.Load ("Comet"));
		PlanetList = GameObject.FindGameObjectsWithTag ("ARPlanet");
		//PlanetList = GameObject.Find ("Planets").GetComponentsInChildren<Transform>();
		foreach(GameObject p in PlanetList){
			Debug.Log (p.gameObject.name);
		}
		numberOfPlanets = 0;
		handlayermask = ~(1 << 18) & ~(1 << 19);
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
		
		frame = Controller.Frame ();
		perviousframe3 = Controller.Frame (3);
		perviousframe10 = Controller.Frame (10);
		perviousframe6 = Controller.Frame (6);
		rightmost = frame.Hands.Rightmost;
		leftmost = frame.Hands.Leftmost;
		pitch = rightmost.Direction.Pitch * 180.0f / Mathf.PI;
		transPitch = perviousframe3.Hands.Rightmost.Direction.Pitch - pitch;
		Grab_L = leftmost.GrabStrength;
		Grab_R = rightmost.GrabStrength;
		Pinch_L = rightmost.PinchStrength;
		Pinch_R = leftmost.PinchStrength;
		roll_l = leftmost.PalmNormal.Roll * 180.0f / Mathf.PI;
		roll_r = rightmost.PalmNormal.Roll * 180.0f / Mathf.PI;
		palmdown_r = roll_r <= -140 || roll_r >= 140;
		
		// Check for digging gesture
		
		if (rightmost.IsValid && rightmost.IsRight ) {
			if(GameObject.Find ("CleanRobotFullRightHand(Clone)") !=null){
				// Now we can do index raycast!
				Vector3 indexfwd = fingerbone.fingerfwd;
				RaycastHit fingerhit;
				
/*				switch (fingerdig){
				case gesturestate.none:
					isDiggingHole = false;
					
					if (palmdown_r) {
						fingerdig = gesturestate.begin;
					}
					break;
				case gesturestate.begin:
					if(pitch>=40){
						//if(fingerbone.fingerdig ){
						//if(GameObject.Find ("R_index_bone3").transform.position.y
						audio.PlayOneShot(sounds.digging);
						//audio.PlayOneShot (narration.Intro10);
						Debug.Log("Digging!");
						fingerdig = gesturestate.end;
					}
					
					break;
				case gesturestate.end:
					cooldownTime -= Time.deltaTime;
					if (cooldownTime <= 0) {
						fingerdig = gesturestate.none;
						cooldownTime = MaxcooldownTime;
					}
					break;
				}
				*/
				
				switch (digging) {
				case gesturestate.none:


					if(!doneDropping){ 

						if((!GameObject.Find ("ARPlanetObject").GetComponent<PlanetInfo> ().hasHole)){
							if (palmdown_r) {
								digging = gesturestate.begin;
							}
						}
					}


					break;
					
				case gesturestate.begin:
					//if (Physics.Raycast(GameObject.Find("R_index_bone3").transform.position, indexfwd, out fingerhit, 20f)){
						//if (fingerhit.collider.name == "ARPlanetObject") {
							//Debug.DrawLine(GameObject.Find("CenterEyeAnchor").transform.position, fingerhit.point);
					if(transPitch <-50){
						//isDiggingHole = true;
						//audio.PlayOneShot (sounds.hole);
						audio.PlayOneShot(sounds.digging,2f);
						digging = gesturestate.end;
					}

					//}
					
					break;
				case gesturestate.end:
					
					//if (Physics.Raycast(GameObject.Find("R_index_bone3").transform.position, indexfwd, out fingerhit, 20f)){
						//if(Righthand.dighole){
						
						//if (fingerhit.collider.name != "ARPlanetObject") {
							if(pitch>=70){
								isDiggingHole = true;
								audio.PlayOneShot (sounds.hole);
								//audio.PlayOneShot(sounds.digging,2f);
								digging = gesturestate.none;
							}
						//}
					//}            
					break;
				}
				
			}
		}



		Vector3 fwd = GameObject.Find("CenterEyeAnchor").transform.forward;
		// Vector3 fwd = GameObject.Find ("R_index_bone3").transform.forward;
		RaycastHit hit;
		
		if (Physics.Raycast(GameObject.Find("CenterEyeAnchor").transform.position, fwd, out hit, 100f, handlayermask)){
			//if (Physics.Raycast (GameObject.Find ("R_index_bone3").transform.position, fwd, out hit, 20f)) {
			//Debug.Log ("Hit #: "+hitcount+", Collider: "+hit.collider.name);
			Debug.DrawLine(GameObject.Find("CenterEyeAnchor").transform.position, hit.point);
			
			// --- Detect if you're looking at a planet ---


			if (hit.collider.name == "ARPlanetObject") {
				isLooking = true;
				//Debug.DrawLine(GameObject.Find("CenterEyeAnchor").transform.position, hit.point);

				// Temporarily disable all the colliders in the handmodels, so the colliders will not block the raycast from the planets any more, you can now do the digging
				//not far from the planets.
				
				if(GameObject.Find ("CleanRobotFullRightHand(Clone)") !=null){
					Collider[] righthandmodelcolliders = GameObject.Find ("CleanRobotFullRightHand(Clone)").GetComponentsInChildren<Collider> ();
					foreach (Collider c in righthandmodelcolliders) {
						c.enabled = false;
						//Debug.Log ("No colliders in right hand model now!");
					}
				}
				
				if(GameObject.Find ("CleanRobotFullLeftHand(Clone)") !=null){
					Collider[] lefthandmodelcolliders = GameObject.Find ("CleanRobotFullLeftHand(Clone)").GetComponentsInChildren<Collider> ();
					foreach (Collider c in lefthandmodelcolliders) {
						c.enabled = false;
						//Debug.Log ("No colliders in left hand model now!");
					}
				}
				

				// raycast detect for the first time , give narration!
				if (raycastcount == 0) {
					audio.PlayOneShot (narration.Intro5);
					raycastcount = 1;
				}
				
				// If raycast hits an AR object, highlight it
				
				//Debug.Log("Tracker #: "+hit.collider.transform.GetComponent<OvrvisionTracker>().markerID);
				basePlanet = hit.collider.transform.Find ("BasePlanet").gameObject;					// Set specific baseplanet
				basePlanetParent = basePlanet.transform.parent.gameObject;							// Set which ARPlanetObject you're looking at
				lastSeenARObject = basePlanetParent;												// Set last seen ARPlanetObject
				currentARObject = basePlanetParent;

				holeModel = basePlanetParent.transform.Find ("Planet_with_hole").gameObject;		// Set specific holeModel
				sproutModel = basePlanetParent.transform.Find ("Planet_with_plant").gameObject;		// Set specific sproutModel
				budModel = sproutModel.transform.Find ("bud").gameObject;
				
				if (!(basePlanetParent.GetComponent<PlanetInfo> ().hasHole)) {
					basePlanet.renderer.material.SetColor ("_OutlineColor", toonOutlineColor);
					holeModel.renderer.material.SetColor ("_OutlineColor", toonOutlineColor);
					sproutModel.renderer.material.SetColor ("_OutlineColor", toonOutlineColor);
					budModel.renderer.material.SetColor ("_OutlineColor", toonOutlineColor);
				}
				else {
					basePlanet.renderer.material.SetColor ("_OutlineColor", Color.magenta); 		// Highlight planets with holes in a different color
					holeModel.renderer.material.SetColor ("_OutlineColor", Color.magenta);
					sproutModel.renderer.material.SetColor ("_OutlineColor", Color.magenta);
				}
				
				
				// ***** THIS IS FOR DEBUG *****
				if (Input.GetKeyDown ("a")) {
					isDiggingHole = true;
				}
				if (Input.GetKeyDown ("s")) {
					doneDropping = true;
				}

				if (Input.GetKeyDown ("q")) {
					basePlanetParent.GetComponent<PlanetInfo> ().familyType = 1;
				}
				if (Input.GetKeyDown ("w")) {
					basePlanetParent.GetComponent<PlanetInfo> ().familyType = 2;
				}
				if (Input.GetKeyDown ("e")) {
					basePlanetParent.GetComponent<PlanetInfo> ().familyType = 3;
				}

				if (Input.GetKeyDown ("i")) {
					plantSubType = 1;
				}
				if (Input.GetKeyDown ("o")) {
					plantSubType = 2;
				}
				if (Input.GetKeyDown ("p")) {
					plantSubType = 3;
				}

				//Choosing seed type with keys
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
				if (Input.GetKeyDown ("n")) {
					basePlanetParent.GetComponent<PlanetInfo>().plantType = 6;
					Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
				}
				if (Input.GetKeyDown (",")) {
					basePlanetParent.GetComponent<PlanetInfo>().plantType = 7;
					Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
				}
				if (Input.GetKeyDown (".")) {
					basePlanetParent.GetComponent<PlanetInfo>().plantType = 8;
					Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
				}
				if (Input.GetKeyDown ("/")) {
					basePlanetParent.GetComponent<PlanetInfo>().plantType = 9;
					Debug.Log ("Plant type is: "+basePlanetParent.GetComponent<PlanetInfo>().plantType);
				}
				// ***** END DEBUG SECTION *****



				if (basePlanetParent != null ) {

					// --- Dig a hole in the ground ---

					if (isDiggingHole && !(basePlanetParent.GetComponent<PlanetInfo> ().hasHole) && basePlanetParent.GetComponent<PlanetInfo> ().canDig) {
						Debug.Log ("Diggy diggy hole!");
						audio.Play ();
						
						//dig hole for the first time, give narration!
						if (digholecount == 0) {
							audio.PlayOneShot (narration.Intro6);
							digholecount = 1;
						}
						
						basePlanet.renderer.enabled = false;
						holeModel.renderer.enabled = true;
						sproutModel.renderer.enabled = false;
						budModel.renderer.enabled = false;
						basePlanetParent.GetComponent<PlanetInfo> ().hasHole = true;
						
						//GameObject.Find ("purple").collider.enabled = true;
						//GameObject.Find ("green").collider.enabled = true;
						//GameObject.Find ("blue").collider.enabled = true;
						isDiggingHole = false;
						basePlanetParent.GetComponent<PlanetInfo> ().canDig = false;
						
					}
					
					
					
					//  ---  Planting the seed --- 
					
					if (doneDropping /*&& !isDiggingHole && basePlanetParent.GetComponent<PlanetInfo> ().hasHole*/) {
						Debug.Log ("attempting seed plant");
						//GameObject.Find ("CandySeedButton").collider.enabled= true;
						//GameObject.Find ("GhostSeedButton").collider.enabled= true;
						//GameObject.Find ("GlowSeedButton").collider.enabled= true;
						basePlanetParent.GetComponent<PlanetInfo> ().familyType = buttonPressed;
						
						basePlanet.renderer.enabled = false;
						holeModel.renderer.enabled = false;
						sproutModel.renderer.enabled = true;
						budModel.renderer.enabled = true;
						
						if (basePlanetParent.GetComponent<PlanetInfo> ().isPlanted && plantgrow == 0) {
							audio.PlayOneShot (narration.Intro9);
							plantgrow = 1;
						}
						
						basePlanetParent.GetComponent<PlanetInfo> ().isPlanted = true;
						basePlanetParent.GetComponent<PlanetInfo> ().hasHole = false;
						//isDropping = false;
						
						//GameObject.Find ("watercan").SendMessage ("WaterHere",basePlanet.GetComponent<OvrvisionTracker>().markerID );
						//GameObject.Find ("CandySeedButton").collider.enabled= false;
						//GameObject.Find ("GhostSeedButton").collider.enabled= true;
						//GameObject.Find ("GlowSeedButton").collider.enabled= false;
						
						//plant the first three trees, give narrations!
						
						plantatreecount ++;
						
						if (plantatreecount == 1) {
							audio.PlayOneShot (narration.Intro11);
						}
						if (plantatreecount == 2) {
							audio.PlayOneShot (narration.Intro12);
						}
						if (plantatreecount == 3) {
							audio.PlayOneShot (narration.Intro13);
						}
						doneDropping = false;

					}


					//  ---  The Watering Can will determine seed type --- 

					if (basePlanetParent.GetComponent<PlanetInfo> ().totalWateredTime != 0.0f && basePlanetParent.GetComponent<PlanetInfo> ().isPlanted == true) {
												
						// If the time elapsed since the timer was started is greater than __ seconds, plant type will be __

						if (basePlanetParent.GetComponent<PlanetInfo> ().totalWateredTime < 3.0f ) {
							plantSubType = 0;
						}
						else if (basePlanetParent.GetComponent<PlanetInfo> ().totalWateredTime >= 3.0f && basePlanetParent.GetComponent<PlanetInfo> ().totalWateredTime < 6.0f) {
							plantSubType = 1;
							if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 1) {
								spawnVRtrees ("VRObjectCandyplanet", 1);
								sproutModel.renderer.material.SetColor ("_OutlineColor", new Color(72f/255f, 0, 116f/255f, 1));
								budModel.renderer.material.SetColor ("_OutlineColor", new Color(157f/255f, 0, 1, 1));
							}
							if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 2) {
								spawnVRtrees ("VRObjectSkull", 4);
								sproutModel.renderer.material.SetColor ("_OutlineColor", new Color(16f/255f, 92f/255f, 10f/255f, 1));
								budModel.renderer.material.SetColor ("_OutlineColor", new Color(16f/255f, 92f/255f, 10f/255f, 1));
							}
							if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 3) {
								spawnVRtrees ("VRObjectJellyfish", 7);
								sproutModel.renderer.material.SetColor ("_OutlineColor", new Color(0, 94f/255f, 126f/255f, 1));
								budModel.renderer.material.SetColor ("_OutlineColor", new Color(0, 94f/255f, 126f/255f, 1));
							}
							//Debug.Log ("Subtype: "+plantSubType);
							basePlanetParent.GetComponent<PlanetInfo> ().watered = true;
							
							// water plant for the first time, give narration!
							if (waterplantcount == 0) {
								audio.PlayOneShot (narration.Intro10);
								
							}
							waterplantcount++;
						}
						else if (basePlanetParent.GetComponent<PlanetInfo> ().totalWateredTime >= 6.0f && basePlanetParent.GetComponent<PlanetInfo> ().totalWateredTime < 9.0f) {
							plantSubType = 2;
							if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 1) {
								spawnVRtrees ("VRObjectCakecity", 2);
								sproutModel.renderer.material.SetColor ("_OutlineColor", new Color(157f/255f, 0, 1, 1));
								budModel.renderer.material.SetColor ("_OutlineColor", new Color(157f/255f, 0, 1, 1));
							}
							if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 2) {
								spawnVRtrees ("VRObjectGhost", 5);
								sproutModel.renderer.material.SetColor ("_OutlineColor", new Color(16f/255f, 92f/255f, 10f/255f, 1));
								budModel.renderer.material.SetColor ("_OutlineColor", new Color(16f/255f, 92f/255f, 10f/255f, 1));
							}
							if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 3) {
								spawnVRtrees ("VRObjectSpaceneedle", 8);
								sproutModel.renderer.material.SetColor ("_OutlineColor", new Color(53f/255f, 205f/255f, 1, 1));
								budModel.renderer.material.SetColor ("_OutlineColor", new Color(53f/255f, 205f/255f, 1, 1));
							}
							//Debug.Log ("Subtype: "+plantSubType);
							basePlanetParent.GetComponent<PlanetInfo> ().watered = true;
							if (waterplantcount == 0) {
								audio.PlayOneShot (narration.Intro10);				
							}
							waterplantcount++;
						}
						else if (basePlanetParent.GetComponent<PlanetInfo> ().totalWateredTime >= 9.0f /*&& basePlanetParent.GetComponent<PlanetInfo> ().totalWateredTime < 12.0f*/) {
							plantSubType = 3;
							if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 1) {
								spawnVRtrees ("VRObjectLollipop", 3); 
								sproutModel.renderer.material.SetColor ("_OutlineColor", new Color(194f/255f, 98f/255f, 1, 1));
								budModel.renderer.material.SetColor ("_OutlineColor", new Color(194f/255f, 98/255f, 1, 1));
							}
							if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 2) {
								spawnVRtrees ("VRObjectCandyfaces", 6);
								sproutModel.renderer.material.SetColor ("_OutlineColor", new Color(101f/255f, 1, 89f/255f, 1));
								budModel.renderer.material.SetColor ("_OutlineColor", new Color(101f/255f, 1, 89f/255f, 1));
							}
							if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 3) {
								spawnVRtrees ("VRObjectHornbell", 9);
								sproutModel.renderer.material.SetColor ("_OutlineColor", new Color(53f/255f, 205f/255f, 1, 1));
								budModel.renderer.material.SetColor ("_OutlineColor", new Color(53f/255f, 205f/255f, 1, 1));
							}
							//Debug.Log ("Subtype: "+plantSubType);
							basePlanetParent.GetComponent<PlanetInfo> ().watered = true;
							if (waterplantcount == 0) {
								audio.PlayOneShot (narration.Intro10);
								
							}
							//basePlanetParent.GetComponent<PlanetInfo> ().totalWateredTime = 0.0f;
							waterplantcount++;
						}
							
						//}
						
						//  --- Spawn a VR preview tree at object location --- 

						/*if (plantSubType != 0 && basePlanetParent.GetComponent<PlanetInfo> ().familyType != 0) {
							if (!(basePlanetParent.GetComponent<PlanetInfo> ().VRplanetSpawned)) {
								//Debug.Log ("# of planets: "+numberOfPlanets);
								//Debug.Log ("Going to spawn next at: " + PlanetList [numberOfPlanets].name);
								
								if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 1 && plantSubType == 1) {
									spawnVRtrees ("VRObjectCandyplanet", 1);
								}
								if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 1 && plantSubType == 2) {
									spawnVRtrees ("VRObjectCakecity", 2);
								}
								if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 1 && plantSubType == 3) {
									spawnVRtrees ("VRObjectLollipop", 3); 
								}
								if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 2 && plantSubType == 1) {
									spawnVRtrees ("VRObjectSkull", 4);
								}
								if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 2 && plantSubType == 2) {
									spawnVRtrees ("VRObjectGhost", 5);
								}
								if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 2 && plantSubType == 3) {
									spawnVRtrees ("VRObjectCandyfaces", 6);
								}
								if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 3 && plantSubType == 1) {
									spawnVRtrees ("VRObjectJellyfish", 7);
								}
								if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 3 && plantSubType == 2) {
									spawnVRtrees ("VRObjectSpaceneedle", 8);
								}
								if (basePlanetParent.GetComponent<PlanetInfo> ().familyType == 3 && plantSubType == 3) {
									spawnVRtrees ("VRObjectHornbell", 9);
								}

								basePlanetParent.transform.Find ("planet_plain").renderer.enabled = false;
								//basePlanetParent.GetComponent<PlanetInfo> ().watered = false;
							}
							
						}*/
					}
				}
			} else {
				isLooking = false;
				//Debug.Log ("Not looking!");
				if (basePlanet != null) {
					basePlanet.renderer.material.SetColor ("_OutlineColor", Color.clear);
					holeModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
					sproutModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
					budModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
				
					basePlanet = null;
					holeModel = null;
					sproutModel = null;
					budModel = null;
					basePlanetParent = null;
					currentARObject = null;
				}
				
			}
		}else {
			isLooking = false;
			//Debug.Log ("Not looking!");
			if (basePlanet != null) {
				basePlanet.renderer.material.SetColor ("_OutlineColor", Color.clear);
				holeModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
				sproutModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
				budModel.renderer.material.SetColor ("_OutlineColor", Color.clear);
				
				basePlanet = null;
				holeModel = null;
				sproutModel = null;
				budModel = null;
				basePlanetParent = null;
				currentARObject = null;
			}
			
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Something touched me: " + other.name);
	}
	
	
	public void spawnVRtrees (string prefabName, int ptype) {
		basePlanetParent.GetComponent<PlanetInfo>().plantType = ptype;
		Transform[] destroylist = PlanetList[numberOfPlanets].GetComponentsInChildren<Transform>();
		
		if (destroylist.Length > 0) {
			/*foreach (Transform t in destroylist) {
				if (t.name != PlanetList[numberOfPlanets].name){
					//Debug.Log ("Destroying: "+t.name);
					Destroy(t.gameObject);
				}
			}*/
			Transform[] tdestroylist = basePlanetParent.transform.Find ("VRPreviewContainer").GetComponentsInChildren<Transform> ();
			foreach (Transform t in tdestroylist) {
				if (t.name != "VRPreviewContainer"){
					Debug.Log ("Destroying: "+t.name);
					Destroy(t.gameObject);
				}
			}
			if (numberOfPlanets != 0){
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
			t.gameObject.layer = LayerMask.NameToLayer("VRLayer");
		}
		
		CurrentVRPlantList [numberOfPlanets] = prefabName;
		numberOfPlanets++;
		basePlanetParent.GetComponent<PlanetInfo>().VRplanetSpawned = true;
	}
	
	
	
	
	
}
