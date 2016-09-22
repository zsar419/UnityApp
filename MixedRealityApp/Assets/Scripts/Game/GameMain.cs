using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMain : MonoBehaviour {

	public GameObject endgameMenu;
	public Text timeRun;
	public int warmupTime;
	public int maxZombies;
	public float zombieSpawnDist;
	public float maxZombieDist;
	public GameObject zombie;
	public GameObject player;
	private List<UnityEngine.Object> zombieList = new List<UnityEngine.Object>();

	void Start () {
		endgameMenu.SetActive (false);	// For toggling
		InvokeRepeating ("ProcessGame", warmupTime,2);
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
				zombieList.RemoveAt (i);
				DestroyObject (z);
			}
		}
	}

	public void Update(){
		timeRun.text = "Time run: "+ GetTime() + " s";
	}

	public double GetTime(){
		return Math.Round (Time.timeSinceLevelLoad, 1);
	}


	public void ShowEndGameMenu(){
		//endgameMenu.SetActive (!endgameMenu.activeSelf);	// For toggling
		endgameMenu.SetActive (true);
		Time.timeScale = 0;	// For pausing time
	}

	public void RestartGame(){
		Time.timeScale = 1;	// Resuming time
		SceneManager.LoadScene ("VRMode");
	}

	public void ToMenu(){
		Time.timeScale = 1;	// Resuming time
		SceneManager.LoadScene ("MainMenu");
	}
	
}
