﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GPS : MonoBehaviour {
	public Text position;
	private float latitude, longitude;
	private double timestamp;

	void Start () {
		Input.location.Start();
		// Ticks (every 1 secs) the current gps position (reduce performance)
		InvokeRepeating("RetrieveGPSData", 0, 1);
	}
	
	void RetrieveGPSData() {
		if (!Input.location.isEnabledByUser)
			position.text = "Please enable your location!";
		else {
			if (Input.location.status == LocationServiceStatus.Failed) {
				position.text = "Unable to determine device location";
			} else {
				latitude = Input.location.lastData.latitude;
				longitude = Input.location.lastData.longitude;
				timestamp = Input.location.lastData.timestamp;

				position.text = "Location: " +
				"\n Latitude: " + latitude +
				"\n Longitude: " + longitude +
				"\n Altitude: " + Input.location.lastData.altitude +
				"\n Horizontal Accuracy: " + Input.location.lastData.horizontalAccuracy +
				"\n Timestamp: " + timestamp;
			}
		}
	
	}
}

