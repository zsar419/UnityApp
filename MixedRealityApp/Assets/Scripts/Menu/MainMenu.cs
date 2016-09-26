using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private Transform cameraTransform;
	private Transform cameraDesiredLookAt;
	private const float CAMERA_TRANSITION_SPEED = 3.5f;

	public Text maxStepText;
	public Text maxDistanceText;
	public Text maxZombiesOutrun;

	void Start () {
		cameraTransform = Camera.main.transform;
		SetStats ();
	}
	
	void Update () {
		// Smooth camera menu transitioning
		if(cameraDesiredLookAt != null)
			cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, CAMERA_TRANSITION_SPEED*Time.deltaTime);
	}

	// Allows buttons to control camera movement to look at different menus
	public void LookAtMenu(Transform menuTransform){
		cameraDesiredLookAt = menuTransform;
	}

	// Allows (in play menu) buttons to load scenes
	public void LoadVRMode(){
		SceneManager.LoadScene ("VRMode");
	}
	public void LoadHandheldMode(){
		SceneManager.LoadScene ("HandheldMode");
	}

	public void ResetStats(){
		GameManager.Instance.ResetStats ();
		SetStats ();
	}

	public void SetStats(){
		maxStepText.text = "Max score: "+ GameManager.Instance.maxScore;
		maxDistanceText.text = "Max distance: " + GameManager.Instance.maxDistance;
		maxZombiesOutrun.text = "Max zombies outrun: " + GameManager.Instance.maxZombiesOutrun;
	}
}
