using UnityEngine;
using System.Collections;

public class ZombieCubeBehaviourScript : MonoBehaviour {

    public float movementSpeed = 0.1F;
    public GameObject player;	// ARCamera
    private Rigidbody rb;		// Cube

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
		Vector3 heading = player.transform.position - transform.position;	// Get direction vector of movement
        //heading.y = 0;
        var distance = heading.magnitude;		// Vector magnitude
		heading.z = 0.0F;
        var direction = heading / distance;		// Normalized vector
        rb.AddForce(direction * movementSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision otherObj)
    {
        //EndGame();
    }
}
