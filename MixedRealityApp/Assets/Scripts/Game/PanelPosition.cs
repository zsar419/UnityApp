using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelPosition : MonoBehaviour {

	public GameObject panelMain;
	public GameObject panelBorder;
	public GameObject reverseCamera;

	void Start () {
		//panelMain.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (int)(Screen.height * 0.35));
		//panelBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (int)(Screen.height * 0.35));
	}
	
	void Update () {
	
	}
}
