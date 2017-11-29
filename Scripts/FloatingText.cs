using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Text scoreText;
    public void SetText(int text) {
        scoreText.text = text.ToString();
    }

    public void DestroyText() {
        Destroy(gameObject);
    }
}
