using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyController {

    public float speed;
    public int timeToAttack;
    public GameObject projectile;

    private Transform missileOffset;
    private AudioSource clipperSound;

    protected override void Start() {
        base.Start();

        clipperSound = GetComponent<AudioSource>();

        InvokeRepeating("EnemyAttack", timeToAttack-5, timeToAttack);

        missileOffset = transform.Find("MissileOffset");

        SetScoreToGive(150);

        transform.rotation = Quaternion.Euler(0, 0, -10);
    }

    void Update() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GetPlayer().transform.position, step);

        if(GetPlayer().transform.position.x > transform.position.x) {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }else {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public override void EnemyAttack() {
        Instantiate(projectile, missileOffset.position, Quaternion.identity);
    }

    private void OnBecameVisible() {
        clipperSound.Play();
        clipperSound.loop = true;
    }

    private void OnBecameInvisible() {
        clipperSound.Stop();
    }
}
