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
	}

	public void RestartGame(){
		SceneManager.LoadScene ("VRMode", LoadSceneMode.Single);
	}

	public void ToMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
	
}
