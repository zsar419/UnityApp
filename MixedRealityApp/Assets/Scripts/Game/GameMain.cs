using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameMain : MonoBehaviour {

	public GameObject endgameMenu;
	public Text timeRun;
	public int warmupTime;
	public int distanceFromZombie;
	public GameObject zombie;
	public GameObject player;

	void Start () {
		endgameMenu.SetActive (false);	// For toggling
		//zombie.active = false;
		Invoke ("ProcessGame", warmupTime);
	}

	private void ProcessGame(){
		// Set game difficulty
		// Need to spawn zombie at random direction and distance behind player
		Vector3 zombieDirection ;
		if(player.transform.rotation.eulerAngles.normalized == new Vector3(0,0,0)){
			zombieDirection = new Vector3 (0, 0, 1) * -distanceFromZombie;
		}else{
			zombieDirection = player.transform.rotation.eulerAngles.normalized * -distanceFromZombie;
		}
		Debug.Log (zombieDirection);
		Vector3 finalPos = player.transform.position + zombieDirection;
		finalPos.y = -1.3f;
		Debug.Log (finalPos);
		Instantiate(zombie, finalPos, Quaternion.identity);

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
