using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject trackedObject;

	private Vector3 offset; 
	private float cameraWidth = 7.6f;
	private float cameraHeight = 4;
	private BoardManager boardManager;
    private new Camera camera;

    // Use this for initialization
    void Start () {
		offset = transform.position - trackedObject.transform.position;
		boardManager = GameObject.Find ("Game Manager").GetComponent<BoardManager> ();

        Color darkBlue = new Color(0, 90, 150, 0);
        Color turquoise = new Color(3, 120, 150, 0);
        Color purple = new Color(95, 1, 100, 0);
        Color cameraBackground;

        int level = GameManager.GetLevel();
        switch (level % 3) {
            case 0: cameraBackground = purple/255;
                break;
            case 1: cameraBackground = darkBlue/255;
                break;
            case 2: cameraBackground = turquoise/255;
                break;
            default: cameraBackground = new Color(0, 0, 100);
                break;
        }

        camera = gameObject.GetComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = cameraBackground;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		int gameWidth = boardManager.GetRows ();
		int gameHeight = boardManager.GetColumns ();

		float xPos = Mathf.Clamp (trackedObject.transform.position.x + offset.x, cameraWidth, gameWidth-cameraWidth);
		float yPos = Mathf.Clamp (trackedObject.transform.position.y + offset.y, cameraHeight, gameHeight-cameraHeight);
		float zPos = trackedObject.transform.position.z + offset.z;
		transform.position = new Vector3(xPos, yPos, zPos);
	}
}
