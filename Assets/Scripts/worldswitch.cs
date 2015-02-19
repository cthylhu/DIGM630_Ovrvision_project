using UnityEngine;
using System.Collections;
using Leap;
[RequireComponent(typeof(AudioSource))]

public class worldswitch : MonoBehaviour {
	public AudioClip spaceswitch;

	
	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			Debug.Log ("Button pressed!");
			audio.PlayOneShot (spaceswitch);
		}
	}

}
