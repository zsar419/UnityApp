using UnityEngine;

public class ZombieCubeBehaviourScript : MonoBehaviour {

	public float movementSpeed;
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
        var distance = heading.magnitude;		// Vector magnitude
        zAnim.SetFloat("ZombieDist", distance);
        var direction = heading / distance;		// Normalized vector
		Vector3 totalForce = direction * movementSpeed * Time.deltaTime;
		// Need to fix total force, need cap for vector
		rb.AddForce (totalForce);
    }

	public void SetSpeed(float speed){
		movementSpeed = speed;
	}

    public float getDistanceFromPlayer(){
        //Vector3 heading = player.transform.position - this.transform.position;
        return (player.transform.position - this.transform.position).magnitude;
    }

}
