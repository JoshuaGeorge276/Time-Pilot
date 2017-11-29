using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {

	public Transform[] spawnPositions;
	public EnemyController[] enemy;
    public EnemyBlimp blimp;

    private int numberOfEnemiesToSpawn;
	private int initalDelayForEnemies = 3;
	private static float delayPerWave = 1f;
    private EnemyBlimp spawnBlimp;
    
	// Use this for initialization
	void Start () {
		numberOfEnemiesToSpawn += GameManager.GetLevel() * 10;

        if (!PlayerPrefs.HasKey("DelayPerWave")) { PlayerPrefs.SetInt("DelayPerWave", 1); } // Sets DelayPerWave to 1 second if no value is selected or saved.
            
        InvokeRepeating ("SpawnWave", initalDelayForEnemies, PlayerPrefs.GetInt("DelayPerWave"));

        Vector3 blimpIdlePosition = new Vector3(-10, 25, 0);
        spawnBlimp = Instantiate(blimp, blimpIdlePosition, Quaternion.identity) as EnemyBlimp;

        Debug.Log("Enemies will spawn every " + PlayerPrefs.GetInt("DelayPerWave") + " second(s)");
        
    }
	
	// Update is called once per frame
	void Update () {
        if(GameManager.GetAmountOfEnemiesToDestroy() <= 0 && spawnBlimp.GetHealth() <= 0) {
            CancelInvoke();
            GameManager.GoToNextLevel();
        }
        
        if (GameManager.GetAmountOfEnemiesToDestroy() < 5 && spawnBlimp) {
            spawnBlimp.StartMoving();
        }
	}

	void SpawnWave(){
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemiesInScene.Length < 21) {
            int enemiesThisWave = Random.Range(3, 6);
            int enemyWaveToSpawn = Random.Range(1, 10);
            if (enemyWaveToSpawn < 7) {
                for (int i = 0; i < enemiesThisWave; i++) {
                    SpawnEnemy();
                }
            }
            else {
                SpawnMulitpleEnemies(enemiesThisWave);
            }
            Transform helicopterParent = spawnPositions[Random.Range(0, spawnPositions.Length - 1)];
            Instantiate(enemy[2], helicopterParent.position, Quaternion.identity, helicopterParent);
        }    
    }

    void SpawnEnemy() {
        Transform enemyParent = spawnPositions[Random.Range(0, spawnPositions.Length)];
        Vector3 enemyPosition = enemyParent.position;
        Instantiate(enemy[0], enemyPosition, Quaternion.identity, enemyParent);
    }

	void SpawnMulitpleEnemies(int gang){
		int spawner = Random.Range (6, 8);
		Transform enemyParent = spawnPositions [spawner];
		Vector3 enemyPosition = new Vector3 (Random.Range (1, 41), enemyParent.position.y, enemyParent.position.z);
		for (int i = 0; i < gang; i++) {
			Instantiate (enemy[1], new Vector3(enemyPosition.x + (i * 2), enemyPosition.y, enemyPosition.y), Quaternion.identity, enemyParent);
		}
    }

    public static void SetDelayPerWave(int delay) {
        PlayerPrefs.SetInt("DelayPerWave", delay);
    }
}
