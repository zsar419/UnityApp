using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMain : MonoBehaviour {




	void Start () {

	}
	
<<<<<<< HEAD
	void Update () {
=======
	// Update is called once per frame
	void FixedUpdate() {
		//checkIsMovement ();
		stepDetector();
		counterText.text = "Counter: " + steps;
	}

	/*private bool hasChanged;
	private int counter;


	float x_old = 0;
	float y_old = 0;
	float z_old = 0;

	private void checkIsMovement(){
		
		float x = Input.acceleration.x;
		float y = Input.acceleration.y;
		float z = Input.acceleration.z;
		float oldValue = ((x_old * x) + (y_old * y)) + (z_old * z);
		float oldValueSqrT = Mathf.Abs(Mathf.Sqrt((float) (((x_old * x_old) + (y_old * y_old)) + (z_old * z_old))));
		float newValue = Mathf.Abs(Mathf.Sqrt((float) (((x * x) + (y * y)) + (z * z))));
		oldValue /= oldValueSqrT * newValue;
		if ((oldValue <= 0.994) && (oldValue > 0.9)){
			if (!hasChanged){
				hasChanged = true;
				counter++; //here the counter
			}
			else{
				hasChanged = false;
			}
		}
		x_old = x;
		y_old = y;
		z_old = z;
	}*/

	private float loLim = 0.005F;
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
>>>>>>> 932907ef60b907ae84b15a22d6d523557ddcf2cb

	}


}
