using UnityEngine;
using System.Collections;

public class narration : MonoBehaviour {
	public AudioClip Ending;
	public AudioClip Intro1;
	public AudioClip Intro2;
	public AudioClip Intro3;
	public AudioClip Intro4;
	public AudioClip Intro5;
	public AudioClip Intro6;
	public AudioClip Intro7;
	public AudioClip Intro8;
	public AudioClip Intro9;
	public AudioClip Intro10;
	public AudioClip Intro11;
	public AudioClip Intro12;
	public AudioClip Intro13;
	public AudioClip Intro14;


	public float audiolength;
	public bool Nar_Check;



	// Use this for initialization
	void Start () {

		audio.PlayOneShot (Intro1);
		//Invoke ("Intro2Play", 18);
		//Invoke ("Intro3Play", 35);
		//Invoke ("Intro4Play", 43);

		/*
		Invoke ("Intro5Play", 5);
		Invoke ("Intro6Play", 5);
		Invoke ("Intro7Play", 5);
		Invoke ("Intro8Play", 5);
		Invoke ("Intro9Play", 5);
		Invoke ("Intro10Play", 5);
		Invoke ("Intro11Play", 5);
		Invoke ("Intro12Play", 5);
		Invoke ("Intro13Play", 5);
		Invoke ("Intro14Play", 5);
		Invoke ("Intro15Play", 5);
		*/
		Nar_Check = false;

	}
	void Intro2Play(){
				audio.PlayOneShot (Intro2);
		}
	void Intro3Play(){
		audio.PlayOneShot (Intro3);
	}
	void Intro4Play(){
		audio.PlayOneShot (Intro4);
	}
	void Intro11Play(){
		audio.PlayOneShot (Intro11);
	}
	void Intro8Play(){
		audio.PlayOneShot (Intro8);
		}
	void Intro9Play(){
		audio.PlayOneShot (Intro9);
	}
	// Update is called once per frame
	void Update () {



			            
				}

	
	}

