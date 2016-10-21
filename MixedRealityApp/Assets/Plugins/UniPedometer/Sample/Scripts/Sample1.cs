using UnityEngine;
using System.Collections;
using UniPedometer;
using System;
using UnityEngine.UI;

public class Sample1 : MonoBehaviour {
	[SerializeField] Text text;
	[SerializeField] InputField fromHourInput;
	[SerializeField] InputField toHourInput;
	[SerializeField] Button queryButton;

	void Start () {
		queryButton.onClick.AddListener(() => QueryAndShow(Int32.Parse(fromHourInput.text), Int32.Parse(toHourInput.text)));
	}

	public void QueryAndShow(int fromHoursAgo, int toHoursAgo) {
		UniPedometerManager.IOS
			.QueryPedometerDataFromDate (
				DateTime.Now - TimeSpan.FromHours (fromHoursAgo),
				DateTime.Now - TimeSpan.FromHours(toHoursAgo),
				(CMPedometerData data, NSError error) => ShowPedometerData (data, error));
	}

	void ShowPedometerData(CMPedometerData data,  NSError error) {
		if (error != null)
			text.text = error.LocalizedDescription;
		else
			text.text = string.Format ("start date: {0}\nend date: {1}\n number of steps: {2}\ndistance: {3}",
				data.StartDate, data.EndDate, data.NumberOfSteps, data.Distance);
	}
}
