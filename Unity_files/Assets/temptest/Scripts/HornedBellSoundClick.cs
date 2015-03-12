using UnityEngine;
using System.Collections;

public class HornedBellSoundClick : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown ()
	{
		
		string debugScriptString = "Blah bell";
		
		
		audio.Play();
		Debug.Log(debugScriptString);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
