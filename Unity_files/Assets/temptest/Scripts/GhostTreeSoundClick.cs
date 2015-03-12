using UnityEngine;
using System.Collections;

public class GhostTreeSoundClick : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown ()
	{
		
		string debugScriptString = "Boo!";
		
		
		audio.Play();
		Debug.Log(debugScriptString);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
