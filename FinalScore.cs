using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour {

    public Text scoreText;
    public Text hiScoreText;
    public Text newHiScoreIndicator;

	// Use this for initialization
	void Start () {
        if(GameManager.GetScore() > PlayerPrefs.GetInt("Hi-Score")) {
            PlayerPrefs.SetInt("Hi-Score", GameManager.GetScore());
            newHiScoreIndicator.enabled = true;
            
        }
        hiScoreText.text = "Hi-Score: " + PlayerPrefs.GetInt("Hi-Score").ToString();
        scoreText.text = "Score: " + GameManager.GetScore();
	}

    private void Update() {
        newHiScoreIndicator.color = Color.Lerp(Color.blue, Color.yellow, Mathf.PingPong(Time.time, 1));
    }
}
