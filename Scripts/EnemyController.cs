using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour {

	public GameObject explosion;
    public AudioClip explosionSound;

	private int scoreToGive;
	private GameObject player;

	// Use this for initialization, can be modified by subclasses
	protected virtual void Start () {
		try{
			player = GameObject.Find ("Player");
		}catch(MissingReferenceException e){
			Debug.Log ("Player cannot be found" + e);
		}

        FloatingTextController.Initialize();
	}

	// Method to implement behaviour in subclass
	public abstract void EnemyAttack();

	// Set score to award player when enemy is destroyed
	public void SetScoreToGive(int score){
		scoreToGive = score;
	}

    public int GetScoreToGive() {
        return scoreToGive;
    }

	// Returns the player in the current scene
	public GameObject GetPlayer (){
		return player;
	}

	// Destroys all enemies when hit by a projectile
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag("PlayerProjectile")) {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            FloatingTextController.CreateFloatingText(scoreToGive);
			GameManager.AddScore (scoreToGive);
            GameManager.DecrementAmountOfEnemiesToDestroy();
			Vector3 enemyPosition = transform.position;
			Instantiate (explosion, enemyPosition, Quaternion.identity);  
			Destroy (coll.gameObject);
			Destroy (gameObject);
		}
	}
}
