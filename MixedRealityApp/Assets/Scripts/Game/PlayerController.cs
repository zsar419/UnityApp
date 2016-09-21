using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float movespeed = 0.0f;
	public GameObject gameUIManager;

	public Text gameEndTimeRun;
	public Text gameEndStepsText;
	public Text gameEndDistanceText;

	public Text maxTimeRun;
	public Text maxtepsText;
	public Text maxDistanceText;

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
		// Game end panel data
		if(other.tag == "Zombie"){
			Debug.Log ("COLLISION");

			// Displaying current game stats
			float currTime = (float)gameUIManager.GetComponent<GameMain> ().GetTime ();
			//float currSteps = gameUIManager.GetComponent<Pedometer>().GetSteps();
			float currSteps = gameUIManager.GetComponent<Pedometer>().stepDetector();
			float currDistance = gameUIManager.GetComponent<GPS> ().GetDistance ();

			gameEndTimeRun.text += currTime + " s";
			gameEndStepsText.text += currSteps;
			gameEndDistanceText.text += currDistance + " m";

			// Calculating if current game stats > previous maximum stats

			GameManager.Instance.maxTimerun = Mathf.Max (currTime, GameManager.Instance.maxTimerun);
			GameManager.Instance.maxSteps = Mathf.Max (currSteps, GameManager.Instance.maxSteps);
			GameManager.Instance.maxDistance = Mathf.Max (currDistance, GameManager.Instance.maxDistance);
			GameManager.Instance.Save ((float)GameManager.Instance.maxTimerun, GameManager.Instance.maxSteps, GameManager.Instance.maxDistance);

			// Comparison in relation to max stats
			maxTimeRun.text += GameManager.Instance.maxTimerun + " s";
			maxtepsText.text += GameManager.Instance.maxSteps;
			maxDistanceText.text += GameManager.Instance.maxDistance + " m";

			gameUIManager.GetComponent<GameMain>().ShowEndGameMenu();

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
