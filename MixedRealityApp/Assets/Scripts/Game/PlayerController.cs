using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float movespeed = 1.0f;
	public GameObject gameUIManager;
    private int currSteps;
    private int lastSteps;

	public Text gameEndTimeRun;
	public Text gameEndStepsText;
	public Text gameEndDistanceText;

	public Text maxTimeRun;
	public Text maxtepsText;
	public Text maxDistanceText;

	void Start(){
        currSteps = 0;
        lastSteps = 0;
	}

	void FixedUpdate(){
        // Get steps and replace it instead of vertical
        lastSteps = currSteps;
        currSteps = gameUIManager.GetComponent<Pedometer>().currSteps;
        float vertical = Input.GetAxis ("Vertical");
		Vector3 forwardZ = new Vector3(0,0,vertical * Time.fixedDeltaTime * movespeed);
		this.transform.Translate (forwardZ, Space.World);
        forwardZ.z = (currSteps - lastSteps) * Time.fixedDeltaTime * movespeed;
        this.transform.Translate(forwardZ, Space.Self);
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
