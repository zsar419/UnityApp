using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GameMain : MonoBehaviour {
	// UI text
	public GameObject endgameMenu;
	public Text timeRun, score, distance, zombiesOutrun;
	public Text gameEndTimeRun, gameEndScoreText, gameEndDistanceText, gameEndOutrun;
	public Text maxScoreText, maxDistanceText, maxOutrunText;
	public Button mainMenu, restart, pauseBtn; 

	// Game setting variables
	public GameObject player, zombie;
	public int warmupTime;
	public int maxZombies;
	public float minZombieSpeed, zombieSpawnDist, maxZombieDist;

	private List<GameObject> zombieList = new List<GameObject>();
	private int outrun = 0;
	private float zombieSpeed;
	private float readyTime = 10f;
	private bool isPaused = false;

	void Start () {
		endgameMenu.SetActive (false);
		mainMenu.onClick.AddListener (() => ToMenu());
		restart.onClick.AddListener (() => RestartGame());
		pauseBtn.onClick.AddListener (() => ToggleGameState());
		Invoke ("EndTraining", warmupTime-1);	
		InvokeRepeating ("ProcessGame", warmupTime, 10);
	}

	public void Update(){
		timeRun.text = "Time run: "+ Math.Round(Time.timeSinceLevelLoad,2) + " s";
		if (Time.timeSinceLevelLoad>=warmupTime)
			score.text = "Score: " + Math.Round((Time.timeSinceLevelLoad - warmupTime),2);
		zombiesOutrun.text = "Outrun: " + outrun;
		distance.text = "Distance: " + Math.Round(player.GetComponent<PlayerController>().GetPlayerDistance(),2) + "m";
	}

	private void EndTraining(){
		// Setting zombie speed for game
		zombieSpeed = player.GetComponent<PlayerController>().GetPlayerDistance()/(Time.timeSinceLevelLoad-readyTime);
		zombie.GetComponent<ZombieCubeBehaviourScript>().playerSpeed = Mathf.Max(zombieSpeed, minZombieSpeed);
	}

	private void ProcessGame(){
		// Spawning zombies in the scene every x seconds (10)
		if (zombieList.Count < maxZombies) {
			Vector3 playerDir = new Vector3(player.transform.forward.x, 0, player.transform.forward.z);
			Vector3 direction = Quaternion.AngleAxis(UnityEngine.Random.Range(-45.0f, 45.0f), Vector3.up) * playerDir;
			Vector3 spawnPos = player.transform.position + direction * -zombieSpawnDist;
			spawnPos.y = -1.3f;
			GameObject zombieInstance = (GameObject)Instantiate (zombie, spawnPos, Quaternion.identity);
			zombieList.Add (zombieInstance);
		}

		// Removing from list if zombie(s) exceed max distance
		for(int i = 0;i<zombieList.Count;i++){
			GameObject z = zombieList [i];
			float zombieDist = (player.transform.position - z.transform.position).magnitude;
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
		foreach (var zombie in zombieList) {
			var z = (GameObject)zombie;
			float dist = (playerPos - z.transform.position).magnitude;
			if (dist < closest)
				closest = dist;
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
		float currTime = Time.timeSinceLevelLoad;
		float currScore = currTime - warmupTime;	// Set score based on parameters
		float currDistance = player.GetComponent<PlayerController>().GetPlayerDistance();

		gameEndTimeRun.text += Math.Round(currTime,2) + " s";
		gameEndScoreText.text += Math.Round(currScore,2);
		gameEndDistanceText.text += Math.Round(currDistance,2) + " m";
		gameEndOutrun.text += outrun;

		// Calculating if current game stats > previous maximum stats
		GameManager.Instance.maxScore = Mathf.Max (currScore, GameManager.Instance.maxScore);
		GameManager.Instance.maxDistance = Mathf.Max (currDistance, GameManager.Instance.maxDistance);
		GameManager.Instance.maxZombiesOutrun = Mathf.Max (outrun, GameManager.Instance.maxZombiesOutrun);
		GameManager.Instance.Save (GameManager.Instance.maxScore, GameManager.Instance.maxDistance, GameManager.Instance.maxZombiesOutrun);

		// Settings max stats based on max performance
		maxScoreText.text += Math.Round(GameManager.Instance.maxScore,2);
		maxDistanceText.text += Math.Round(GameManager.Instance.maxDistance,2) + " m";
		maxOutrunText.text += GameManager.Instance.maxZombiesOutrun;


        MusicScript mu = player.GetComponent<MusicScript>();
        mu.StopAllMusic();

		// Showing endgame menu and pausing game
		endgameMenu.SetActive (true);
		Time.timeScale = 0;
	}

	private void ToggleGameState(){
		isPaused = !isPaused;
		if(isPaused){
			pauseBtn.GetComponentInChildren<Text>().text = "Play";
			Time.timeScale = 0;
		}else{
			pauseBtn.GetComponentInChildren<Text>().text = "| |";
			Time.timeScale = 1;
		}
	}

	private void RestartGame(){
		Time.timeScale = 1;	// Resuming time
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	private void ToMenu(){
		Time.timeScale = 1;	// Resuming time
		SceneManager.LoadScene ("MainMenu");
	}
	
}
