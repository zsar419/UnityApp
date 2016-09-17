using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private Transform cameraTransform;
	private Transform cameraDesiredLookAt;
	private const float CAMERA_TRANSITION_SPEED = 3.5f;


	void Start () {
		cameraTransform = Camera.main.transform;

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
	public void LoadFreerun(){
		SceneManager.LoadScene ("VRMode");
	}
	public void LoadCalibration(){
		SceneManager.LoadScene ("HandheldMode");
	}
}
