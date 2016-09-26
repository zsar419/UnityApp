using UnityEngine;
using System.Collections;

public class ZombieCubeBehaviourScript : MonoBehaviour {

    public float movementSpeed = 0.1F;
	//public float maxForce = 500;
    private GameObject player;	// ARCamera
    private Rigidbody rb;		// Cube
    private Animator zAnim;

	void Start () {
        rb = GetComponent<Rigidbody>();
		player = GameObject.FindGameObjectsWithTag("Player")[0];
        // Set zombie position x units behind player
        zAnim = GetComponent<Animator>();
	}

    void Update()
    {
        transform.LookAt(player.transform);
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

    public float getDistanceFromPlayer()
    {
        //Vector3 heading = player.transform.position - this.transform.position;
        return (player.transform.position - this.transform.position).magnitude;
    }

}
