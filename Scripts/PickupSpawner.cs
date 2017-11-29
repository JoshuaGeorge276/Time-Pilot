using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour {

    public GameObject pickup;

    private Vector3 spawnPosOne, spawnPosTwo;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");

        InvokeRepeating("SpawnPickup", 15, Random.Range(30, 50));
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SpawnPickup() {
        spawnPosOne = player.transform.position + new Vector3(-3.5f, 6, 0);
        spawnPosTwo = player.transform.position + new Vector3(3.5f, 6, 0);
        int coin = Random.Range(0, 2);
        if(coin == 0) {
            Instantiate(pickup, spawnPosOne, Quaternion.identity);
        }else {
            Instantiate(pickup, spawnPosTwo, Quaternion.identity);
        }
        
    }
}
