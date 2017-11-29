using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static bool hasClicked = false;

    public void StartLevelInSeconds(int seconds) {
        GameManager.ResetScript();
        Invoke("LoadNextLevel", seconds);
    }

    public void ResetGame() {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadNextLevel() {
        GameManager.ResetScript();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetHasClicked() {
        hasClicked = true;
    }
}
