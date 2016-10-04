using UnityEngine;

public class ZombieCubeBehaviourScript : MonoBehaviour {
	public float movementModifier = 1f;

	private float playerSpeed;
    private GameObject player;	// ARCamera
    private Rigidbody rb;		// Cube
   	private Animator zAnim;

	void Start () {
        rb = GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
		zAnim = GetComponent<Animator>();
		//player = GameObject.FindGameObjectsWithTag ("MainCamera")[0];
	}

    void Update(){
		Vector3 pos = player.transform.position;
		pos.y = this.transform.position.y;
		transform.LookAt(pos);
    }

    void FixedUpdate(){
		Vector3 heading = player.transform.position - this.transform.position;	// Get direction vector of movement
        //heading.y = 0;
		float distance = heading.magnitude;		// Vector magnitude
        zAnim.SetFloat("ZombieDist", distance);
        // Vector3 direction = heading / distance;		// Normalized vector
		// Vector3 totalForce = direction * movementSpeed * Time.deltaTime;
		//rb.AddForce (totalForce);
		Vector3 zombieDir = new Vector3(transform.forward.x, 0f, transform.forward.z);
		rb.velocity = zombieDir * playerSpeed * movementModifier;
    }

	public void SetSpeed(float speed){
		playerSpeed = speed;
	}

    public float getDistanceFromPlayer(){
        //Vector3 heading = player.transform.position - this.transform.position;
        return (player.transform.position - this.transform.position).magnitude;
    }

}
