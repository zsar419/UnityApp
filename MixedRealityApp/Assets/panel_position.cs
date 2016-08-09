using UnityEngine;
using System.Collections;

public class panel_position : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (int)(Screen.height * 0.35));
        Debug.Log("UI: Panel positioned");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
