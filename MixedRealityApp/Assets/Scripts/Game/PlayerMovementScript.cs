using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class PlayerMovementScript : MonoBehaviour {

    public float MovementSpeed = 1.0F;
    public GameObject player;
    private byte numAccelerations = 16;
    private Rigidbody rb;
    private Vector3[] accelerations;
    private byte accelerationsIndex;
    private Vector3 runningTotalAcceleration;

	// Use this for initialization
	void Start () {
        accelerations = new Vector3[numAccelerations];
        accelerationsIndex = 0;
        runningTotalAcceleration = new Vector3(0.0F, 0.0F, 0.0F);
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        //var userAcceleration = Input.gyro.userAcceleration;
        //accelerations[accelerationsIndex++] = Input.gyro.userAcceleration;
        //if (accelerationsIndex % 16 == 0)
        //{
        //    accelerationsIndex = 0;
        //}

        //rb.AddForce(averageVectorArray(accelerations) * MovementSpeed * Time.fixedDeltaTime);

        runningTotalAcceleration -= accelerations[accelerationsIndex];
        accelerations[accelerationsIndex] = Input.gyro.userAcceleration;
        runningTotalAcceleration += accelerations[accelerationsIndex++];
        if ((accelerationsIndex % 16) == 0)
        {
            accelerationsIndex = 0;
        }

        rb.AddForce((runningTotalAcceleration / numAccelerations) * MovementSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered the collision enter function of PBS");
        Debug.Log("You have been caught, and for you, The Chase is over");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        //Application.Quit();
    }

    //private Vector3 averageVectorArray(Vector3[] arr)
    //{
    //    Vector3 ave = new Vector3(0.0F, 0.0F, 0.0F);
    //    foreach (var acceleration in accelerations)
    //    {
    //        ave += acceleration;
    //    }

    //    return ave / numAccelerations;
    //}
}
