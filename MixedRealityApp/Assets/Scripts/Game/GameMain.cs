using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class GameMain : MonoBehaviour {

	public GameObject endgameMenu;
	public Text gameEndTimeRun, gameEndScoreText, gameEndDistanceText, gameEndOutrun;
	public Text maxScoreText, maxDistanceText, maxOutrunText;
	public Button mainMenu, restart; 

	public Text timeRun, score, distance, zombiesOutrun;
	private bool processScore;

	public int warmupTime;
	public int maxZombies;
	public float zombieSpawnDist;
	public float maxZombieDist;
	public GameObject player, zombie;
	private List<UnityEngine.Object> zombieList = new List<UnityEngine.Object>();
	public int outrun = 0;

	void Start () {
		endgameMenu.SetActive (false);
		mainMenu.onClick.AddListener (() => ToMenu());
		restart.onClick.AddListener (() => RestartGame());

		InvokeRepeating ("ProcessGame", warmupTime, 10);
		Invoke ("UpdateScore", warmupTime);
	}

	public void Update(){
		timeRun.text = "Time run: "+ GetTime() + " s";
		if (processScore)
			score.text = "Score: " + (GetTime () - warmupTime);
		zombiesOutrun.text = "Outrun: " + outrun;
		distance.text = "Distance: " + player.GetComponent<PlayerController>().GetPlayerDistance() + "m";
	}

	private void UpdateScore(){
		processScore = true;
	}

	private double GetTime(){
		return Math.Round (Time.timeSinceLevelLoad, 1);
	}

	private void ProcessGame(){
		if (zombieList.Count <= maxZombies) {
			// Set game difficulty
			// Need to spawn zombie at random direction and distance behind player
			Vector3 zombieDirection;
			if (player.transform.rotation.eulerAngles.normalized == new Vector3 (0, 0, 0)) {
				zombieDirection = new Vector3 (0, 0, 1) * -zombieSpawnDist;
			} else {
				zombieDirection = player.transform.rotation.eulerAngles.normalized * -zombieSpawnDist;
			}
			Vector3 finalPos = player.transform.position + zombieDirection;
			finalPos.y = -1.3f;
			var zombieInstance = Instantiate (zombie, finalPos, Quaternion.identity);
			zombieList.Add (zombieInstance);
		}

		// Removing from list if zombie(s) exceed max distance
		Vector3 playerPos = player.transform.position;
		for(int i = 0;i<zombieList.Count;i++){
			var z = (GameObject)zombieList [i];
			float zombieDist = (playerPos - z.transform.position).magnitude;
			if(zombieDist>maxZombieDist){
				outrun++;
				zombieList.RemoveAt (i);
				DestroyObject (z);
			}
		}
	}

    /// Gets the distance of the closest zombie to the player. 
    public float GetClosestDist(){
		float closest = Mathf.Infinity;
		Vector3 playerPos = player.transform.position;
		//zombieList.ForEach (z => closest = Mathf.Min ((playerPos - ((GameObject)z).transform.position).magnitude, closest));

        foreach (var zombie in zombieList)
        {
            var z = (GameObject)zombie;
            float dist = (playerPos - z.transform.position).magnitude;
            if (dist < closest)
            {
                closest = dist;
            }
        }

        return closest;
    }

	public void ProcessEndGame(){
		// Hiding all in game stats
		timeRun.GetComponent<Text>().enabled = false;
		score.GetComponent<Text>().enabled = false;
		distance.GetComponent<Text>().enabled = false;
		zombiesOutrun.GetComponent<Text>().enabled = false;

		// Displaying current game stats
		float currTime = (float)Math.Round (Time.timeSinceLevelLoad, 1);
		float currScore = currTime - warmupTime;	// Set score based on parameters
		float currDistance = player.GetComponent<PlayerController>().GetPlayerDistance();

		gameEndTimeRun.text += currTime + " s";
		gameEndScoreText.text += currScore;
		gameEndDistanceText.text += currDistance + " m";
		gameEndOutrun.text += outrun;

		// Calculating if current game stats > previous maximum stats
		GameManager.Instance.maxScore = Mathf.Max (currScore, GameManager.Instance.maxScore);
		GameManager.Instance.maxDistance = Mathf.Max (currDistance, GameManager.Instance.maxDistance);
		GameManager.Instance.maxZombiesOutrun = Mathf.Max (outrun, GameManager.Instance.maxZombiesOutrun);
		GameManager.Instance.Save (GameManager.Instance.maxScore, GameManager.Instance.maxDistance, GameManager.Instance.maxZombiesOutrun);

		// Settings max stats based on max performance
		maxScoreText.text += GameManager.Instance.maxScore;
		maxDistanceText.text += GameManager.Instance.maxDistance + " m";
		maxOutrunText.text += GameManager.Instance.maxZombiesOutrun;

		// Showing endgame menu and pausing game
		endgameMenu.SetActive (true);
		Time.timeScale = 0;
	}

	private void RestartGame(){
		Time.timeScale = 1;	// Resuming time
		SceneManager.LoadScene ("VRMode");
	}

	private void ToMenu(){
		Time.timeScale = 1;	// Resuming time
		SceneManager.LoadScene ("MainMenu");
	}
	
}
