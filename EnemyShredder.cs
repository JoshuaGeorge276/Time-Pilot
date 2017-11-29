using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShredder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.GetComponent<Enemy2> ()) {
			Destroy (col.gameObject);
		}

        if (col.GetComponent<Pickup>()) {
            Destroy(col.gameObject);
        }
	}
}
