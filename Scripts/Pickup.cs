using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pickup : MonoBehaviour {

    public AudioClip pickupSound;

    private int[] scoreAwarded = { 1500, 2000, 4000 };
    
	void OnTriggerEnter2D(Collider2D col){
		if (col.GetComponent<PlayerController> ()) {
            int randomScore = Random.Range(0, 3);
			GameManager.AddScore (scoreAwarded[randomScore]);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            FloatingTextController.CreateFloatingText(scoreAwarded[randomScore]);
            Destroy (gameObject);
		}
	}
}
