using UnityEngine;
using System.Collections;

public class SeedContainer : MonoBehaviour {
	public int family;
	public bool candy;
	public bool ghost;
	public bool glow;
	// Use this for initialization
	
	void Start () {
		candy = false;
		ghost = false;
		glow = false;
	}

	void LastSeed(int buttontype){

		family = buttontype;

	}
	


	// Update is called once per frame
	void Update () {



		if (family == 1) {

			GameObject.Find ("CandySeed").SendMessage("CanbePinched",candy = true);
			GameObject.Find ("GhostSeed").SendMessage("CanbePinched",ghost = false);
			GameObject.Find ("GlowSeed").SendMessage("CanbePinched",glow = false);

				}
		if (family == 2) {
			GameObject.Find ("CandySeed").SendMessage("CanbePinched",candy = false);
			GameObject.Find ("GhostSeed").SendMessage("CanbePinched",ghost = true);
			GameObject.Find ("GlowSeed").SendMessage("CanbePinched",glow = false);
		}

		if (family == 3) {
			GameObject.Find ("CandySeed").SendMessage("CanbePinched",candy = false);
			GameObject.Find ("GhostSeed").SendMessage("CanbePinched",ghost = false);
			GameObject.Find ("GlowSeed").SendMessage("CanbePinched",glow = true);
		}
	
	}

}
