using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyHealth : MonoBehaviour {

	public GameObject splat;
    public int health;
	public float deathTimer = 1;

    //Private variables
    private int currentHealth;

    void Start () {
        //Setting the current health to be the health variable
        //so that when we start the game, the enemy has full HP
        currentHealth = health;
    }

    void Update () {
        //If the enemy reaches 0 HP, destroy the enemy
        if (currentHealth <= 0) {
			gameObject.GetComponent<ParticleSystem> ().Play();
			deathTimer -= Time.deltaTime;
			if (deathTimer >= 0) {
				Destroy (gameObject);
			}
        }
    }

    //Used to call this void in the bullet scripts
    //since currentHealth is a private variable
    public void EnemyDamaged (int damage) {
		gameObject.GetComponent<ParticleSystem> ().Play();
        currentHealth -= damage;
    }
}
