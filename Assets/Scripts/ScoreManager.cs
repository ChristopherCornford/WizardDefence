using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


	public int Score;


	public Text currentScore;
	public Text highScore;



	// Use this for initialization
	void Start () {
		highScore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore", 0).ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		currentScore.text = "Score: " + Score.ToString ();
		if (Score > PlayerPrefs.GetInt ("HighScore", 0)) {
			PlayerPrefs.SetInt ("HighScore", Score);
			highScore.text = "High Score: " + Score.ToString ();
		}
	}
}
