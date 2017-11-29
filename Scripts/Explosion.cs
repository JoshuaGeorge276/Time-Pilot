using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	void SelfDestroy(){
		Destroy (transform.parent.gameObject);
	}

    void Destoy() {
        Destroy(gameObject);
    }
}
