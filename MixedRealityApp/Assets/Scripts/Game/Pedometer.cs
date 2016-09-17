using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Pedometer : MonoBehaviour {
	public Text counterText;

	/*private float loLim = 0.005F;
	private float hiLim = 0.1F;
	private int steps = 0;
	private bool stateH = false;
	private float fHigh = 10.0F;
	private float curAcc= 0F;
	private float fLow = 0.2F;
	private float avgAcc;

	public int stepDetector(){
		curAcc = Mathf.Lerp (curAcc, Input.acceleration.magnitude, Time.deltaTime * fHigh);
		avgAcc = Mathf.Lerp (avgAcc, Input.acceleration.magnitude, Time.deltaTime * fLow);
		float delta = curAcc - avgAcc;

		if (!stateH) {
			if (delta > hiLim) {
				stateH = true;
				steps++;
			}
		} else {
			if (delta < loLim) {
				stateH = false;
			}
		}

		avgAcc = curAcc;
		return steps;
	}

	void FixedUpdate() {
		//checkIsMovement ();
		stepDetector();
		counterText.text = "Counter: " + steps;
	} */

	private AndroidJavaObject plugin;
	private float initialStep, steps;
	void Start () {
		#if UNITY_ANDROID
		plugin = new AndroidJavaClass("jp.kshoji.unity.sensor.UnitySensorPlugin").CallStatic<AndroidJavaObject>("getInstance");
		plugin.Call("setSamplingPeriod", 100 * 1000); // refresh sensor 100 mSec eachc
		plugin.Call("startSensorListening", "stepcounter");
		initialStep = plugin.Call<float[]>("getSensorValues", "stepcounter")[0];
		#endif
	}

	void OnApplicationQuit () {
		#if UNITY_ANDROID
		if (plugin != null) {
			plugin.Call("terminate");
			plugin = null;
		}
		#endif
	}

	void Update () {
		#if UNITY_ANDROID
		if (plugin != null) {
			steps =  plugin.Call<float[]>("getSensorValues", "stepcounter")[0] - initialStep;
			counterText.text = "Steps: " + steps; 
			//displayVals("stepcounter");
		}
		#endif
	}

	// For displaying other android sensor properties if required
	private void displayVals(string input){
		counterText.text += input+": ";
		plugin.Call<float[]>("getSensorValues", input).ToList().ForEach(s => counterText.text += s.ToString("0.00") + ", ");
		counterText.text += "\n";
	}

}
