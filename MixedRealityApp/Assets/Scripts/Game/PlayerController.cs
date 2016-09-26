using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float metresPerStep;
	public GameObject gameUIManager;
	private int currSteps = 0, lastSteps = 0;
	private float distance = 0;
	public Text distanceText;

	public Text gameEndTimeRun, gameEndScoreText, gameEndDistanceText, gameEndOutrun;
	public Text maxScoreText, maxDistanceText, maxOutrunText;

	public Text maxTimeRun;
	public Text maxtepsText;
	public Text maxDistanceText;
    public AudioClip caughtAudio;
    public AudioSource caughtAudioSource;

	void FixedUpdate(){
        lastSteps = currSteps;
		currSteps = Int32.Parse(""+gameUIManager.GetComponent<Pedometer> ().steps);
        
		// PC Controls
		float vertical = Input.GetAxis ("Vertical");
		Vector3 forwardZ = new Vector3(0,0,vertical * Time.fixedDeltaTime * metresPerStep);
		this.transform.Translate (forwardZ, Space.World);
        
		// To make it realistic we can get user rotation and translate in that direction so player is not confined to Z axis
		forwardZ.z = (currSteps - lastSteps) * metresPerStep;
        this.transform.Translate(forwardZ, Space.Self);
		distance = (float)Math.Round ((float)this.transform.position.magnitude, 2);
		distanceText.text = "Distance: " + distance + "m";
	}

	public void OnTriggerEnter(Collider other){
		// Game end panel data
		if(other.tag == "Zombie"){
			// Turn off other text
			gameUIManager.GetComponent<GameMain> ().TurnOffText();
			distanceText.GetComponent<Text>().enabled = false;


            //Stops the background music and plays a final audio clip when the player is caught. 
            caughtAudioSource.clip = caughtAudio;
            caughtAudioSource.Play();

			// Displaying current game stats
			float currTime = (float)gameUIManager.GetComponent<GameMain> ().GetTime ();
			//float currSteps = gameUIManager.GetComponent<Pedometer>().GetSteps();
			//float currSteps = gameUIManager.GetComponent<Pedometer>().steps;
			float currScore = currTime - (float)gameUIManager.GetComponent<GameMain> ().warmupTime;	// Set score based on parameters
			float currDistance = distance;
			int outrun = gameUIManager.GetComponent<GameMain> ().outrun;

			gameEndTimeRun.text += currTime + " s";
			gameEndScoreText.text += currScore;
			gameEndDistanceText.text += currDistance + " m";
			gameEndOutrun.text += outrun;

			// Calculating if current game stats > previous maximum stats
			GameManager.Instance.maxScore = Mathf.Max (currScore, GameManager.Instance.maxScore);
			GameManager.Instance.maxDistance = Mathf.Max (currDistance, GameManager.Instance.maxDistance);
			GameManager.Instance.maxZombiesOutrun = Mathf.Max (outrun, GameManager.Instance.maxZombiesOutrun);
			GameManager.Instance.Save (GameManager.Instance.maxScore, GameManager.Instance.maxDistance, GameManager.Instance.maxZombiesOutrun);

			// Comparison in relation to max stats
			maxScoreText.text += GameManager.Instance.maxScore;
			maxDistanceText.text += GameManager.Instance.maxDistance + " m";
			maxOutrunText.text += GameManager.Instance.maxZombiesOutrun;

			gameUIManager.GetComponent<GameMain>().ShowEndGameMenu();

		}
	}
		
}
