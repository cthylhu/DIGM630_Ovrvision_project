using UnityEngine;
using System.Collections;

public class raycasttest : MonoBehaviour {
	int counter;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 fwd = GameObject.Find("Main Camera").transform.forward;
		RaycastHit hit;
		if (Physics.Raycast(GameObject.Find("Main Camera").transform.position, fwd, out hit, 9)){
			Debug.Log ("Hit something! " + counter);
			Debug.Log("Collider: "+hit.collider.name);
			counter++;
		}
	}
}
