using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class GameManager : MonoBehaviour {
	// For storing game information

	private static GameManager instance;
	public static GameManager Instance{ get{return instance; }}

	public int maxSteps;
	public int maxDistance;
	public Text maxStepText;
	public Text maxDistanceText;

	void Awake () {
		instance = this;

		// Object persists through scenes
		DontDestroyOnLoad (gameObject);

		// Check if we have saved data before
		if (PlayerPrefs.HasKey ("MaxSteps")) Load ();
		else Save ();

		maxStepText.text = "Max steps: "+ maxSteps;
		maxDistanceText.text = "Max distance: " + maxDistance;

	}

	void Update () {
		
	}

	private void Load(){
		// We have had a previous session - thus load saved data
		maxSteps = PlayerPrefs.GetInt("MaxSteps");
		maxDistance= PlayerPrefs.GetInt("MaxDistance");
	}

	private void Save(){
		// We dont have a saved session - load new game data
		PlayerPrefs.SetInt("MaxSteps", 0);
		PlayerPrefs.SetInt("MaxDistance", 0);
	}

	public void ResetStats(){
		Save ();
		Load ();

		maxStepText.text = "Max steps: " + maxSteps;
		maxDistanceText.text = "Max distance: " + maxDistance;
	}

}
