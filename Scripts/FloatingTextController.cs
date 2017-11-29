using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    private static FloatingText popupTextPrefab;
    private static GameObject canvas;

    public static void Initialize() {
        canvas = GameObject.Find("MobileSingleStickControl");
        if (!popupTextPrefab) {
            popupTextPrefab = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
        }
        
    }

	public static void CreateFloatingText(int text) {
        FloatingText instance = Instantiate(popupTextPrefab);
        instance.transform.SetParent(canvas.transform, false);
        instance.SetText(text);
    }

}
