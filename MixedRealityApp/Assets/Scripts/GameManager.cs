using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class GameManager : MonoBehaviour {
	// For storing game information

	private static GameManager instance;
	public static GameManager Instance{ get{return instance; }}

	public float  maxTimerun, maxSteps, maxDistance;

	void Awake () {
		if(instance == null) 
			instance = this;
		else if(instance != this){
			Destroy(gameObject);
		}

		// Object persists through scenes
		DontDestroyOnLoad (gameObject);

		// Check if we have saved data before
		if (PlayerPrefs.HasKey ("MaxSteps")) Load ();
		else Save ();
	}


	private void Load(){
		// We have had a previous session - thus load saved data
		maxTimerun = PlayerPrefs.GetFloat("MaxTimeRun");
		maxSteps = PlayerPrefs.GetFloat("MaxSteps");
		maxDistance= PlayerPrefs.GetFloat("MaxDistance");
	}

	public void Save(float time= 0, float steps = 0, float distance = 0){
		// We dont have a saved session - load new game data
		PlayerPrefs.SetFloat("MaxTimeRun", time);
		PlayerPrefs.SetFloat("MaxSteps", steps);
		PlayerPrefs.SetFloat("MaxDistance", distance);
	}

	public void ResetStats(){
		Save ();
		Load ();
	}

}
