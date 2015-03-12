using UnityEngine;
using System.Collections;

public class EyeBlink : MonoBehaviour {

	//A texture object that will output the animation  
	private Texture texture;  
	//With this Material object, a reference to the game object Material can be stored  
	private Material goMaterial;  
	//An integer to advance frames  
	private int frameCounter = 0;  
	
	//A string that holds the name of the folder which contains the image sequence  
	public string folderName;  
	//The name of the image sequence  
	public string imageSequenceName;  
	//The number of frames the animation has  
	public int numberOfFrames;  
	public float countDown;
	//The base name of the files of the sequence  
	private string baseName ;  
	
	void Awake()  
	{   
		this.goMaterial = this.renderer.material;   
		this.baseName = this.folderName + "/" + this.imageSequenceName;  
	}  
	
	void Start ()  
	{   
		texture = (Texture)Resources.Load(baseName + "0" + ".png", typeof(Texture));  
		Debug.Log ("the texture loading now is " + baseName);
		countDown = 100f;
	}  
	
	void Update ()  
	{  
		print ("the frameconter in update is " + frameCounter);
		if (frameCounter < numberOfFrames - 1) {
			print ("it should play the loop");
			StartCoroutine ("PlayLoop", 0.04f);  
				} else {
			Waiting();
				}
	}  

	IEnumerator PlayLoop(float delay)  
	{   
		yield return new WaitForSeconds(delay);    

		frameCounter = (++frameCounter)%numberOfFrames;  

		this.texture = (Texture)Resources.Load(baseName + frameCounter.ToString(""), typeof(Texture)); 
		goMaterial.mainTexture = this.texture; 
		print ("textrue changed to " + texture.name + "in playloot");
		StopCoroutine("PlayLoop");  
	}  


	void Waiting(){
				if (countDown > 1) {
						countDown = countDown - 1;
			this.texture = (Texture)Resources.Load(baseName + "0", typeof(Texture)); 
			goMaterial.mainTexture = this.texture; 
				} else {
						frameCounter = -1;
			countDown = Random.Range(30, 120);
				}
		}

}
