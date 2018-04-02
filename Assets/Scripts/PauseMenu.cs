using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	
	public Canvas gameCanvas;
	public Canvas pauseCanvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void Unpause() {
		gameCanvas.gameObject.SetActive (true);
		pauseCanvas.gameObject.SetActive (false);
		Time.timeScale = 1;
	}
}
