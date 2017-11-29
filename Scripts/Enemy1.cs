using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy1 : EnemyController {

	public float speed;
	public int timeToAttack;
	public GameObject projectile;
    public float projectileSpeed;
    public AudioClip projectileSound;

	private Vector3 playerPosition;
	private Transform projectileOffset;

	protected override void Start (){
		base.Start ();

		InvokeRepeating ("EnemyAttack", 0, timeToAttack);

		projectileOffset = transform.Find ("ProjectileOffset");

		SetScoreToGive (100);
	}

	void Update(){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, step);
		transform.up = GetPlayer().transform.position - transform.position;

	}

	public override void EnemyAttack (){
        Vector3 randomPositioningOffset = new Vector3(Random.Range(0, 2), Random.Range(0, 2), 0);
        playerPosition = GetPlayer().transform.position + randomPositioningOffset;

    }

	void EnemyFire(){
		Vector2 projectileVelocity = new Vector2 (transform.up.x, transform.up.y) * projectileSpeed;
		GameObject bullet = Instantiate (projectile, projectileOffset.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(projectileSound, transform.position);
		Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
		bulletRigidbody.velocity = projectileVelocity;
	}

	void OnBecameVisible(){
        int initialShotTime = Random.Range(0, 30);
		InvokeRepeating ("EnemyFire", initialShotTime, 20);
	}

	void OnBecameInvisible(){
		CancelInvoke ("EnemyFire");
	}
}
