using UnityEngine;
using System.Collections;

public class PlanetJunkRotation : MonoBehaviour {

    public Vector3 r;
    void Start(){
       // r = new Vector3 (0,0,0);
    }
	// Update is called once per frame
	void Update () {
	gameObject.transform.Rotate(r*Time.deltaTime);
	}
}
