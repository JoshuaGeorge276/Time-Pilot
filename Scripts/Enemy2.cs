using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy2 : EnemyController {

	public float minSpeed;
	public float maxSpeed;

	private Vector3 direction;
	private float speed;

	protected override void Start ()
	{
		base.Start ();
		EnemyAttack ();
		SetScoreToGive (100);
		speed = Random.Range (minSpeed, maxSpeed);
	}
	// Update is called once per frame
	void Update () {
		transform.position += direction * speed * Time.deltaTime;
	}

	public override void EnemyAttack ()
	{
		float playerYPos = base.GetPlayer ().transform.position.y;
		float yPos = transform.position.y;

		if (playerYPos > yPos) {
			direction = Vector3.up;
		} else if (playerYPos < yPos) {
			direction = Vector3.down;
			transform.rotation = Quaternion.Euler (0, 0, 180);
		}
	}
}
