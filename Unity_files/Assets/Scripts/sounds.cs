using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class sounds : MonoBehaviour {
	public AudioClip spaceswitch;
	public AudioClip GlowSeedButton;
	public AudioClip CandySeedButton;
	public AudioClip ColorPickerButton;
	public AudioClip GhostSeedButton;
	public AudioClip planted;


	void Awake() {
		//spaceswitch = Resources.Load ("/Sounds/spaceswitch") as AudioClip;
		//planted = Resources.Load ("/Sounds/planted") as AudioClip;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
