using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public Transform canvas;

    public void PressPauseButton() {
        if (!canvas.gameObject.activeInHierarchy) {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            AudioListener.pause = true;
        }else {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
