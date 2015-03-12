using UnityEngine;
using System.Collections;



public class CandyFacesSoundClick : MonoBehaviour 
{
	
	public AudioClip SFX_plantmusic_candyfaces_lowsynth_v1;
	
	// Use this for initialization
	void OnMouseDown ()
	{
		
		string debugScriptString = "Meow";
		
		
		audio.Play();
		Debug.Log(debugScriptString);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
