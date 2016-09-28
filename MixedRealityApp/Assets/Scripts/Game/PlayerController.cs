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
		transform.eulerAngles = ARCamera.transform.eulerAngles;

		// Debug text
		playerPos.text = "Player Pos: "+this.transform.position;
		playerRot.text = "Player Rot: "+this.transform.eulerAngles;
		ARCameraRot.text = "ARCamera Rot "+ARCamera.transform.eulerAngles;

		// Updating steps
		lastSteps = currSteps;
		currSteps = (int)gameUIManager.GetComponent<Pedometer> ().GetSteps();
		Vector3 playerDir = new Vector3(transform.forward.x, 0, transform.forward.z);

		// PC Controls
		float vertical = Input.GetAxis ("Vertical");
		float movement = vertical * Time.fixedDeltaTime * metresPerStep;
		transform.Translate (playerDir * movement);

		movement += (currSteps - lastSteps) * metresPerStep;
		this.transform.Translate(playerDir * movement, Space.Self);
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
