using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public GameObject projectile;
	public float speed, projectileSpeed;
    public AudioClip projectileSound;
    public GameObject explosion;
    public AudioClip playerLoseSound;

	private bool isIdle = true;
	private Transform projectileOffset;
	private Vector3 projectileVelocity;
    private float firingRate = 0.125f;
	private float playerWidth = 0.355f;
	private float playerHeight = 0.355f;
	private Rigidbody2D myBody;
	private BoardManager boardManager;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
        sprite = GetComponent<SpriteRenderer>();
		projectileOffset = transform.Find ("ProjectileOffset");
		boardManager = GameObject.Find ("Game Manager").GetComponent<BoardManager> ();
        transform.position = new Vector3(boardManager.GetRows() / 2, boardManager.GetColumns() / 2, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		ShipMovement();
		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical")) * speed;
		ClampPlayerInGameSpace ();
		if (moveVec == new Vector2(0, 0))
			isIdle = true;
		else
			isIdle = false;

		if(!isIdle)
			myBody.AddForce (moveVec);

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ();
		}
	}

	public void PlayerFire(bool fire){
		if (fire)
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		else
			CancelInvoke ("Fire");
	}

    private void Fire() {
		Vector3 projectileSpawnPosition = projectileOffset.transform.position;
        AudioSource.PlayClipAtPoint(projectileSound, projectileSpawnPosition);

        Vector2 projectileVelocity = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * projectileSpeed;
		GameObject bullet = Instantiate(projectile, projectileSpawnPosition, Quaternion.identity) as GameObject;
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
		bulletRigidbody.velocity = (isIdle ? new Vector2(0, projectileSpeed) : projectileVelocity);
    }

    private void ShipMovement() {
		if (isIdle) {
			transform.rotation = Quaternion.identity;
		} else {
			float rotation = Mathf.Atan2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"));
			transform.rotation = Quaternion.Euler (0, 0, rotation * Mathf.Rad2Deg * -1);
		}
    }

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.GetComponent<EnemyController> () || coll.gameObject.CompareTag("EnemyProjectile")) {
            AudioSource.PlayClipAtPoint(playerLoseSound, transform.position);
            sprite.enabled = false;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Invoke("DecrementLives", 1);
		}
	}

	void ClampPlayerInGameSpace(){
		int gameWidth = boardManager.GetRows ();
		int gameHeight = boardManager.GetColumns ();

		float xPos = Mathf.Clamp (transform.position.x, playerWidth, gameWidth - playerWidth);
		float yPos = Mathf.Clamp (transform.position.y, playerHeight, gameHeight - playerHeight);

		transform.position = new Vector3 (xPos, yPos, transform.position.z);
	}

    void DecrementLives() {
        GameManager.DecrementLives();
    }

}
