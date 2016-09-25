using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class GameManager : MonoBehaviour {
	// For storing game information

	private static GameManager instance;
	public static GameManager Instance{ get{return instance; }}

	public float maxScore, maxDistance;
	public int maxZombiesOutrun;

	void Awake () {
		if(instance == null) 
			instance = this;
		else if(instance != this){
			Destroy(gameObject);
		}

		// Object persists through scenes
		DontDestroyOnLoad (gameObject);

		// Check if we have saved data before
		if (PlayerPrefs.HasKey ("MaxScore")) Load ();
		else Save ();
	}


	private void Load(){
		// We have had a previous session - thus load saved data
		maxScore = PlayerPrefs.GetFloat("MaxScore");
		maxDistance= PlayerPrefs.GetFloat("MaxDistance");
		maxZombiesOutrun = PlayerPrefs.GetInt("MaxOutrun");
	}

	public void Save(float score = 0, float distance = 0, int outrun = 0){
		// We dont have a saved session - load new game data
		PlayerPrefs.SetFloat("MaxScore", score);
		PlayerPrefs.SetFloat("MaxDistance", distance);
		PlayerPrefs.SetInt("MaxOutrun", outrun);
	}

	public void ResetStats(){
		Save ();
		Load ();
	}

}
