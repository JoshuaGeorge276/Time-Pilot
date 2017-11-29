using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBlimp : EnemyController {

    public float speed;
    public float min = 0f;
    public float max = 50f;

    private int health = 10;
    private float playerYPosition;
    private AudioSource approachingPlayerSound;
    private Rigidbody2D rigidbody;

    protected override void Start() {
        base.Start();

        rigidbody = GetComponent<Rigidbody2D>();

        approachingPlayerSound = GetComponent<AudioSource>();

        SetScoreToGive(1000);

        health = 10;

        min = transform.position.x;
        max = transform.position.x + 55;

        InvokeRepeating("EnemyAttack", 0, 20);
    }

    // Update is called once per frame
    void Update () {
        transform.up = rigidbody.velocity.normalized;
    }

    public override void EnemyAttack() {
        playerYPosition = GetPlayer().transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("PlayerProjectile")){
            health--;
            CheckIfDead();
            Destroy(coll.gameObject);
        }
        
    }

    void CheckIfDead() {
        if(health < 1) {
            CancelInvoke();
            GameManager.AddScore(GetScoreToGive());
            Vector3 enemyPosition = transform.position;
            Instantiate(explosion, enemyPosition, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            Destroy(gameObject);
        }
    }

    public int GetHealth() {
        return health;
    }

    public void StartMoving() {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, max - min) + min, transform.position.y, transform.position.z);
    }

    private void OnBecameVisible() {
        approachingPlayerSound.Play();
        approachingPlayerSound.loop = true;
    }

    private void OnBecameInvisible() {
        if (approachingPlayerSound.isPlaying) {
            approachingPlayerSound.Stop();
        }
    }

}
