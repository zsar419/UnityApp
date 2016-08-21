using UnityEngine;
using System.Collections;

public class ZombieCubeBehaviourScript : MonoBehaviour {

    public float movementSpeed = 0.1F;
    public GameObject player;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        Vector3 heading = player.transform.position - transform.position;
        //heading.y = 0;
        var distance = heading.magnitude;
        heading.y = 0.0F;
        var direction = heading / distance;
        rb.AddForce(direction * movementSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision otherObj)
    {
        //EndGame();
    }
}
