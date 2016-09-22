using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GPS : MonoBehaviour {
	public Text distanceText;

	private float distance = 0;
	private float latitude, longitude;
	private double timestamp;

	void Start () {
		Input.location.Start();
		// Ticks (every 1 secs) the current gps position (reduce performance)
		//InvokeRepeating("RetrieveGPSData", 0, 1);
	}
	
	void RetrieveGPSData() {
		latitude = Input.location.lastData.latitude;
				longitude = Input.location.lastData.longitude;
				timestamp = Input.location.lastData.timestamp;

				distanceText.text = "Location: " +
				"\n Latitude: " + latitude +
				"\n Longitude: " + longitude +
				"\n Altitude: " + Input.location.lastData.altitude +
				"\n Horizontal Accuracy: " + Input.location.lastData.horizontalAccuracy +
				"\n Timestamp: " + timestamp;
	}

	void Update(){
		RetrieveGPSData ();
		//distanceText.text = "Distance: " + distance;
	}
}

