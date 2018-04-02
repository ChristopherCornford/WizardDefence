using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {

	public bool playerLoses = false;
	public int doorStrength;
	public int currentDoorHealth;
	public int Score;


	public TMP_Text currentScore;
	public TMP_Text highScore;
	public TMP_Text playerLossText;
	public TMP_Text currentDoorHealthText;
	public Slider doorHealth;




	// Use this for initialization
	void Start () {
		highScore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore", 0).ToString ();
		doorHealth.maxValue = doorStrength;
		currentDoorHealth = doorStrength;
	}
	
	// Update is called once per frame
	void Update () {
		currentScore.text = "Score: " + Score.ToString ();
		if (Score > PlayerPrefs.GetInt ("HighScore", 0)) {
			PlayerPrefs.SetInt ("HighScore", Score);
			highScore.text = "High Score: " + Score.ToString ();
		}
		doorHealth.value = currentDoorHealth;
		currentDoorHealthText.text = "(" + currentDoorHealth + " / " + doorStrength + ")";
		if (currentDoorHealth <= 0) {
			playerLoses = true;
		}
		if (playerLoses == true) {
			playerLossText.text = "The Goblins Have Overrun Your Tower!!";
			Time.timeScale = 0;
		} else {
			playerLossText.text = "";
		}
	}
}
