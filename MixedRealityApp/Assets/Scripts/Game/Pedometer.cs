using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Pedometer : MonoBehaviour {

	private AndroidJavaObject plugin;
	private float initialStep, steps = 0;
	private float manualSteps;

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
			steps = plugin.Call<float[]>("getSensorValues", "stepcounter")[0] - initialStep + manualSteps;
		}
		#endif
	}

	public void ManualStepIncrement(){
		manualSteps++;
	}
	public float GetSteps(){ return steps; }

}
