using UnityEngine;
using System.Collections;

public class SkullFacesSoundClick : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown ()
	{
		
		string debugScriptString = "I'm a Skull";
		
		
		audio.Play();
		Debug.Log(debugScriptString);
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
