using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float speed;
    public AudioClip explodingSound;

    private GameObject player;
    private float instatiationTime;
    private AudioSource trackingSound;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        instatiationTime = Time.time;

        trackingSound = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time < instatiationTime + 5) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.right = player.transform.position - transform.position;
        }else {
            transform.position += transform.right * speed * Time.deltaTime;
            trackingSound.Stop();
        }
    }

    private void OnBecameVisible() {
        trackingSound.Play();
        trackingSound.loop = true;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "PlayerProjectile") {
            AudioSource.PlayClipAtPoint(explodingSound, transform.position);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
