/*
 * Ignore the name (it's a holdover from early development), this is the controller script for the zombies
 */

using UnityEngine;

public class ZombieCubeBehaviourScript : MonoBehaviour {
	public float movementModifier = 1f;

	public float playerSpeed;
    private GameObject player;	// ARCamera
    private Rigidbody rb;		// Cube
   	private Animator zAnim;

	void Start () {
        rb = GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
		zAnim = GetComponent<Animator>();
	}

    void Update(){
		Vector3 pos = player.transform.position;
		pos.y = this.transform.position.y;
		transform.LookAt(pos);
    }

    void FixedUpdate(){
		Vector3 heading = player.transform.position - this.transform.position;	// Get direction vector of movement
		float distance = heading.magnitude;		// Vector magnitude
        zAnim.SetFloat("ZombieDist", distance);  // Set zombie distance from player, for animation purposes
		Vector3 zombieDir = new Vector3(transform.forward.x, 0f, transform.forward.z);
		rb.velocity = zombieDir * playerSpeed * movementModifier;
        print("playerSpeed = " + playerSpeed);
        print("zombieDir = " + zombieDir.ToString());
    }

    // This function is currently unused - the player speed is set directly
	public void SetSpeed(float speed){
		playerSpeed = speed;
        playerSpeed = 2.0F;
	}

    public float getDistanceFromPlayer(){
        return (player.transform.position - this.transform.position).magnitude;
    }

}
