using UnityEngine;
using System.Collections;

public class ZombieCubeBehaviourScript : MonoBehaviour {

    public float movementSpeed = 0.1F;
	//public float maxForce = 500;
    public GameObject player;	// ARCamera
    private Rigidbody rb;		// Cube

	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
		Vector3 heading = player.transform.position - this.transform.position;	// Get direction vector of movement
        //heading.y = 0;
        var distance = heading.magnitude;		// Vector magnitude
        var direction = heading / distance;		// Normalized vector
		Vector3 totalForce = direction * movementSpeed * Time.deltaTime;
		// Need to fix total force, need cap for vector
		rb.AddForce (totalForce);
    }

}
