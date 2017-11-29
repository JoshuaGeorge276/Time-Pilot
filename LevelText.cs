using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour {

    public Text playerText, dateText, levelText;

	// Use this for initialization
	void Start () {
        playerText.text = "Player 1";
        dateText.text = "A.D 19" + (GameManager.GetLevel() * 10);
        levelText.text = "Stage " + GameManager.GetLevel();

        Invoke("RemoveText", 4);
	}
	
	// Update is called once per frame
	void Update () {
        dateText.color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 1));
	}

    void RemoveText() {
        playerText.enabled = false;
        dateText.enabled = false;
        levelText.enabled = false;
    }
}
