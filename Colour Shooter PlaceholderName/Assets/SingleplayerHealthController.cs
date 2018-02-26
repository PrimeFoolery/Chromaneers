using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerHealthController : MonoBehaviour {

    public int health;

    public SingleplayerCharacterController singleplayerCharacterController;

    //Private variables
    private int currentHealth;

    void Start () {
	    //Setting the current health to be the health variable
	    //so that when we start the game, the enemy has full HP
	    currentHealth = health;
	}
	
	void Update () {
        print(currentHealth);
		//If the player reaches 0 HP, set speed to 0 and set material to something different
	    if (currentHealth <= 0) {
	        singleplayerCharacterController.moveSpeed = 0;
	    }
	}

    //Used to call this void in the bullet scripts
    //since currentHealth is a private variable
    public void EnemyDamaged(int damage) {
        currentHealth -= damage;
    }
}
