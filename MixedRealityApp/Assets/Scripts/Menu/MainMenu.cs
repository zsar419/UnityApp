using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private Transform cameraTransform;
	private Transform cameraDesiredLookAt;
	private const float CAMERA_TRANSITION_SPEED = 3.5f;

	public Text maxTimerunText;
	public Text maxStepText;
	public Text maxDistanceText;

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
		maxTimerunText.text = "Max time run: " + GameManager.Instance.maxTimerun;
		maxStepText.text = "Max steps: "+ GameManager.Instance.maxSteps;
		maxDistanceText.text = "Max distance: " + GameManager.Instance.maxDistance;
	}
}
