using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccelerometerMovementScript : MonoBehaviour {
	public Text accelerationText;
	public float maxX;
	public float maxY;
	public float maxZ;

	public float minX;
	public float minY;
	public float minZ;

	void Start () {
		Input.gyro.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		//Vector3 accelerationlessgravity = Input.acceleration - Input.gyro.gravity;
		//Vector3 acceleration = Input.gyro.userAcceleration;
		//accelerationText.text = Input.acceleration.ToString();
		maxX = Mathf.Max(maxX, Input.gyro.userAcceleration.x);
		maxY = Mathf.Max(maxY, Input.gyro.userAcceleration.y);
		maxZ = Mathf.Max(maxZ, Input.gyro.userAcceleration.z);
	
		minX = Mathf.Min(minX, Input.gyro.userAcceleration.x);
		minY = Mathf.Min(minY, Input.gyro.userAcceleration.y);
		minZ = Mathf.Min(minZ, Input.gyro.userAcceleration.z);

		string minimum = minX.ToString("0.00") + ", " + minY.ToString("0.00") + ", " + minZ.ToString("0.00");
		string maximum = maxX.ToString("0.00") + ", " + maxY.ToString("0.00") + ", " + maxZ.ToString("0.00");
		string diff = (minX + maxX).ToString("0.00") + ", " + (minY + maxY).ToString("0.00") + ", " + (minZ + maxZ).ToString("0.00");
		accelerationText.text = "Accelerometer: " + 
			"\n Acc: " + Input.gyro.userAcceleration.ToString() +
			"\n Min: " + minimum +
			"\n Max: " + maximum +
			"\n Diff: " + diff;
	}
}
