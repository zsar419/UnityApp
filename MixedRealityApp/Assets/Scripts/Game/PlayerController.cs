using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public Text playerPos;
	public Text playerRot;
	public Text ARCameraRot;
	public GameObject ARCamera;

	public float metresPerStep;
	public AudioClip caughtAudio;
	public AudioSource caughtAudioSource;

	private GameObject gameUIManager;
	private int currSteps = 0, lastSteps = 0;
	private float distance = 0;

	void Start(){
		gameUIManager = GameObject.Find ("GameUIManager");
	}

	void FixedUpdate(){
		// Update player rotation based on camera
		transform.rotation = ARCamera.transform.rotation;

		// Debug text
		playerPos.text = "Player Pos: "+this.transform.position;
		playerRot.text = "Player Rot: "+this.transform.eulerAngles;
		ARCameraRot.text = "ARCamera Rot "+ARCamera.transform.eulerAngles;

		// Updating steps
		lastSteps = currSteps;
		currSteps = (int)gameUIManager.GetComponent<Pedometer> ().GetSteps();
		float movement = (currSteps - lastSteps) * metresPerStep;

		// PC Controls
		float pcMovementConst = 7 / 3f;
		float vertical = Input.GetAxis ("Vertical") * pcMovementConst;
		movement += vertical * Time.fixedDeltaTime * metresPerStep;


		transform.Translate (transform.forward * movement, Space.World);
		// Resetting y
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		// Absolute distance - total distance travelled
		distance += movement < 0 ? -movement : movement;

	}

	void LateUpdate(){
		ARCamera.transform.position = transform.position;
		Vector3 manualRotation = new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f);
		ARCamera.transform.Rotate (manualRotation * 30);
	}

	public void OnTriggerEnter(Collider other){
		if(other.tag == "Zombie"){
			gameUIManager.GetComponent<GameMain> ().ProcessEndGame();

			//Stops the background music and plays a final audio clip when the player is caught. 
			//caughtAudioSource.clip = caughtAudio;
			//caughtAudioSource.Play();

		}
	}

	public float GetPlayerDistance(){ return distance; }
		
}
