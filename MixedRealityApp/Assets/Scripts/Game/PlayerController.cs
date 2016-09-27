using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float metresPerStep;
	//public AudioClip caughtAudio;
	//public AudioSource caughtAudioSource;

	private GameObject gameUIManager;
	private int currSteps = 0, lastSteps = 0;
	private float distance = 0;

	void Start(){
		gameUIManager = GameObject.Find ("GameUIManager");
	}

	void FixedUpdate(){
        lastSteps = currSteps;
		currSteps = Int32.Parse(""+gameUIManager.GetComponent<Pedometer> ().GetSteps());
        
		// PC Controls
		float vertical = Input.GetAxis ("Vertical");
		Vector3 forwardZ = new Vector3(0,0,vertical * Time.fixedDeltaTime * metresPerStep);
		this.transform.Translate (forwardZ, Space.World);
        
		// To make it realistic we can get user rotation and translate in that direction so player is not confined to Z axis
		forwardZ.z = (currSteps - lastSteps) * metresPerStep;
        this.transform.Translate(forwardZ, Space.Self);
		distance = (float)Math.Round ((float)this.transform.position.magnitude, 2);
	}

	public void OnTriggerEnter(Collider other){
		if(other.tag == "Zombie"){
			gameUIManager.GetComponent<GameMain> ().ProcessEndGame();

            //Stops the background music and plays a final audio clip when the player is caught. 
            //caughtAudioSource.clip = caughtAudio;
            //caughtAudioSource.Play();

		}
	}

	public float GetPlayerDistance(){ return distance; }
		
}
