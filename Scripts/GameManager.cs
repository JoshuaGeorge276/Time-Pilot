using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Image[] livesIcon;
	public static GameManager instance = null;
    public AudioClip introSound;

    private BoardManager boardScript;
	private static int level = 1;
    private static int originalPlayerScore = 0;
    private static int originalPlayerLives = 3;
    private static int originalAmountOfEnemiesToDestroy = 20 + (level * 10);
	private static int playerScore, playerLives, amountOfEnemiesToDestroy;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
            AudioSource.PlayClipAtPoint(introSound, new Vector3(25, 25, 0));
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

		boardScript = GetComponent<BoardManager> ();

        playerScore = originalPlayerScore;
        playerLives = originalPlayerLives;

        amountOfEnemiesToDestroy = originalAmountOfEnemiesToDestroy;

		InitGame ();

        if(playerLives < 3) {
            livesIcon[0].enabled = (playerLives > 0);
            livesIcon[1].enabled = (playerLives > 1);
            livesIcon[2].enabled = (playerLives > 2);
        }
	}

    public static void ResetScript() {
        level = 1;
        originalPlayerScore = 0;
        originalPlayerLives = 3;
        playerScore = 0;
        playerLives = 4;
        originalAmountOfEnemiesToDestroy = 20 + (level * 10);
        amountOfEnemiesToDestroy = originalAmountOfEnemiesToDestroy;
    }

    void InitGame(){
		boardScript.SetupScene ();
	}

	public static void AddScore(int score){
		playerScore += score;
	}

    public static int GetScore() {
        return playerScore;
    }

    public static void DecrementLives(){
		playerLives--;
		if(!CheckIfLost()) {
            ResetLevel();
        }else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

	}

	public static int GetLevel(){
		return level;
	}

    public static void GoToNextLevel() {
        level++;
        amountOfEnemiesToDestroy = 20 + (level * 10);
        ResetLevel();
    }

    public static int GetAmountOfEnemiesToDestroy() {
        return amountOfEnemiesToDestroy;
    }

    public static void DecrementAmountOfEnemiesToDestroy() {
        if(amountOfEnemiesToDestroy > 0) {
            amountOfEnemiesToDestroy--;
        }
    }

    public static bool CheckIfLost() {
        if(playerLives >= 0) {
            return false;
        }
        return true;
    }

    public static void ResetLevel() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        originalPlayerScore = playerScore;
        originalPlayerLives = playerLives;
        originalAmountOfEnemiesToDestroy = amountOfEnemiesToDestroy;
    }
}
