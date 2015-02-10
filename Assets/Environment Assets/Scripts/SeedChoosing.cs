using UnityEngine;
using System.Collections;

public class SeedChoosing : MonoBehaviour {

	public GameObject candySeed;
	public GameObject ghostSeed;
	public GameObject glowSeed;
	private Vector3 spawnPosition;

	void Start(){

		}

	public void SpawnCandySeed () {
		float z = Random.Range (290, 310);
		float x = Random.Range (-70,70);
		float y = Random.Range (-40,20);
		spawnPosition = new Vector3 (x, y, z);

		Instantiate (candySeed, spawnPosition, candySeed.transform.rotation);
	}
	public void SpawnGhostSeed () {
		float z = Random.Range (290, 310);
		float x = Random.Range (-70,70);
		float y = Random.Range (-40,20);
		spawnPosition = new Vector3 (x, y, z);

		Instantiate (ghostSeed, spawnPosition, ghostSeed.transform.rotation);
	}
	public void SpawnGlowSeed () {
		float z = Random.Range (290, 310);
		float x = Random.Range (-70,70);
		float y = Random.Range (-40,20);
		spawnPosition = new Vector3 (x, y, z);

		Instantiate (glowSeed, spawnPosition, glowSeed.transform.rotation);
	}

}
