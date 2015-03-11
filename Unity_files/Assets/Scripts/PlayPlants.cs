using UnityEngine;
using System.Collections;
using Leap;


public class PlayPlants : MonoBehaviour {
	
	//scripts
	//public sounds sound; 
	public AudioClip pokeSound;
	GameObject currentPlanet;
	int hitcount = 0;

	// Color for toon outline
	float r = 0f/255f;
	float g = 213f/255f;
	float b = 255f/255f;
	
	// Use this for initialization
	void Start () {
		//Debug.Log (r.ToString()+","+g.ToString()+","+b.ToString());
		
		
	}

	void Retrigger(){
		this.GetComponent<Animator>().enabled = false;

		}
	
	// left index poke the ghost then switch from AR to VR
	
	void OnTriggerEnter(Collider other){
		Debug.Log("Collided plant object: " + this.name);
		if ((other.name == ("L_index_bone3"))||(other.name == ("R_index_bone3"))) {
			
			audio.PlayOneShot(pokeSound,1.0f);
			this.GetComponent<Animator>().enabled = true;

			Invoke ("Retrigger",5);
		}
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 fwd = GameObject.Find("CenterEyeAnchor").transform.forward;
		RaycastHit hit;

		// Detect if player is looking at a plant collider
		if (Physics.Raycast(GameObject.Find("CenterEyeAnchor").transform.position, fwd, out hit, 50)) {
			if (hit.collider.name == "CHAR_hornedbellz_prefab" 
			    || hit.collider.name == "CHAR_lollipop_prefab" 
			    || hit.collider.name == "CHAR_ghosttree_prefab" 
			    || hit.collider.name == "CHAR_candyfaces_prefab" 
			    || hit.collider.name == "CHAR_SkullShroom_prefab"){

				Debug.Log ("Hit VR Plant #: "+hitcount+", Collider: "+hit.collider.name);
				Debug.DrawLine(GameObject.Find("CenterEyeAnchor").transform.position, hit.point);
				hitcount++;

				currentPlanet = hit.collider.transform.parent.gameObject;

				Debug.Log ("Current Planet: "+currentPlanet);

				Renderer[] renderers = currentPlanet.GetComponentsInChildren<Renderer>();
				foreach (Renderer rend in renderers) {

					rend.renderer.material.SetColor ("_OutlineColor", new Color(r, g, b, 1));
				}

				// If player makes a plant tapping gesture while looking at plant, plant will play its sound and animation
				if (Input.GetKeyDown(KeyCode.Space)){
					audio.PlayOneShot(hit.collider.transform.gameObject.GetComponent<PlayPlants>().pokeSound, 1.0f);
					this.GetComponent<Animator>().enabled = true;
					
					Invoke ("Retrigger",5);
				}

			}
			else{
				RenderAllTreeOutlinesNone();
			}
		}
		else{
			RenderAllTreeOutlinesNone();
		}
	}

	public void RenderAllTreeOutlinesNone() {
		if (currentPlanet != null) {
			Renderer[] renderers = GameObject.Find("Planets").GetComponentsInChildren<Renderer>();
			//Renderer[] renderers = GameObject.Find("TreeObjectsTest").GetComponentsInChildren<Renderer>();

			foreach (Renderer rend in renderers) {
				//Debug.Log ("R: "+rend.name);
				rend.renderer.material.SetColor ("_OutlineColor", new Color(r, g, b, 0));
			}
		}
	}









}
