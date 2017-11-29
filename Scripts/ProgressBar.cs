using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProgressBar : MonoBehaviour {

    public Transform loadingBar;
    public Transform textIndicator;
    private float currentAmount;
    private float speed;
    private float amountOfEnemiesTotal;
    private GameObject enemyBlimp;
    private EnemyBlimp blimp;
    private int blimpMaxHealth;

	// Use this for initialization
	void Start () {
        amountOfEnemiesTotal = 20 + (GameManager.GetLevel() * 10);

        enemyBlimp = FindObjectOfType<EnemyBlimp>().gameObject;
        blimp = enemyBlimp.GetComponent<EnemyBlimp>();
        blimpMaxHealth = blimp.GetHealth();

        Debug.Log("Amount of enemies: " + GameManager.GetAmountOfEnemiesToDestroy() + " blimp health: " + blimpMaxHealth + " current level: " + GameManager.GetLevel());
    }
	
	// Update is called once per frame
	void Update () {
        currentAmount = ((GameManager.GetAmountOfEnemiesToDestroy() + blimp.GetHealth()) / (amountOfEnemiesTotal + blimpMaxHealth)) * 100;

        if (currentAmount >= 0) {
            textIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString();
        }
        
        loadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
		
	}
}
