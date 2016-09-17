using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float movespeed = 0.0f;
	public GameObject gameUIManager;

	public int currentSteps;
	public float currentDistance;
	public float time;

	void Start(){
		
	}

	void FixedUpdate(){
		// Get steps and replace it instead of vertical

		float vertical = Input.GetAxis ("Vertical");
		Vector3 forwardZ = new Vector3(0,0,vertical * Time.deltaTime*movespeed);
		this.transform.Translate (forwardZ, Space.World);
		//GetComponent<Transform>().Translate (forward, Space.World);
	}

	public void OnTriggerEnter(Collider other){
		if(other.tag == "Zombie"){
			//Destroy (other.gameObject);	// destroy zombie
			// other.GetComponent<MeshRenderer>().enabled = false;	// or Disable zombie rendering
			Debug.Log ("Collided");
			gameUIManager.GetComponent<GameMain>().ShowEndGameMenu();
			// Call function to stop game and raise menu
		}
	}

	/*
	public float movespeed = 5.0f;
	public float drag = 0.5f;

	private Rigidbody controller;

	void Start () {
		controller = GetComponent<Rigidbody> ();
		controller.drag = drag;
	}

	void Update () {
		// For keyboard input
		Vector3 dir = Vector3.zero;
		dir.z = Input.GetAxis ("Vertical");
		controller.AddForce (dir * movespeed);
	}*/
}
