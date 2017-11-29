using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySetter : MonoBehaviour {

    public Text easyText, midText, hardText;

    private Text[] textList = new Text[3];

    private void Start() {
        textList[0] = easyText;
        textList[1] = midText;
        textList[2] = hardText;

        foreach (Text text in textList) {
            Color32 textColor = text.color;
            textColor.a = 120;
            text.color = textColor;
        }
    }

    public void SetDifficulty(int difficulty) {
        EnemySpawner.SetDelayPerWave(difficulty);
    }

    public void ChangeAlphaOnText(Text text) {
        Color32 selectedTextColor = text.color;
        selectedTextColor.a = 255;
        text.color = selectedTextColor;

        foreach(Text otherText in textList) {
            if (!text.Equals(otherText)) {
                Color32 textColor = otherText.color;
                textColor.a = 120;
                otherText.color = textColor;
            }
        }
    }

}
