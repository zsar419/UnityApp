using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameMain : MonoBehaviour {

	public GameObject endgameMenu;
	public Text timeRun;

	void Start () {
		endgameMenu.SetActive (false);	// For toggling
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
