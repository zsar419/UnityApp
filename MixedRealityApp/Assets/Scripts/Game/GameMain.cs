using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMain : MonoBehaviour {

	public GameObject endgameMenu;

	void Start () {
		endgameMenu.SetActive (false);	// For toggling
	}



	public void ShowEndGameMenu(){
		//endgameMenu.SetActive (!endgameMenu.activeSelf);	// For toggling
		endgameMenu.SetActive (true);
		//Time.timeScale = 0;	// For pausing time
	}

	public void RestartGame(){
		Time.timeScale = 1;	// Resuming time
		SceneManager.LoadScene ("VRMode", LoadSceneMode.Single);
	}

	public void ToMenu(){
		Time.timeScale = 1;	// Resuming time
		SceneManager.LoadScene ("MainMenu");
	}
	
}
