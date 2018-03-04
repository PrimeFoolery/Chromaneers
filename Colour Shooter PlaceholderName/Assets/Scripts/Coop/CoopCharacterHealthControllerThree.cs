using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoopCharacterHealthControllerThree : MonoBehaviour {

	[Header("Health Variables")]
	public int health;
	public float invincibility;
	public bool canBeDamaged;
	
	[Header("Materials")]
	public Material matOne;
	public Material matTwo;
	public Material deadMat;
	public float duration;
	public Renderer rend;

	[Header("UI")] 
	public Text currentHP;

	public CoopCharacterControllerThree coopCharacterControllerThree;

	//Private variables
	private int currentHealth;

	void Start () {
		//Setting the current health to be the health variable
		//so that when we start the game, the enemy has full HP
		currentHealth = health;
	    
		//Getting the renderer
		//And setting the main material to its origin material
		rend = GetComponent<Renderer>();
		rend.material = matOne;
	}
	
	void Update () {
		//Basic text to see HP
		currentHP.text = "Yellow Current Health = " + currentHealth.ToString(); 

		//If the player reaches 0 HP, set speed to 0 and set material to something different
		if (currentHealth <= 0) {
			coopCharacterControllerThree.moveSpeed = 0;
		}
		
		//Making a timer to decide when the player can or cant take damage
		//Also making a material lerp
		invincibility -= Time.deltaTime;
		float gettingDamaged = Mathf.PingPong(Time.time, duration) / duration;
		//If the time is 0 then the player can take damage
		//but if the number is larger than 0, then it cant
		if (invincibility <= 0) {
			canBeDamaged = true;
			rend.material = matOne;
			invincibility = 0;
			if (currentHealth <= 0) {
				rend.material = deadMat;
			}
		} else if (invincibility > 0) {
			canBeDamaged = false;
			rend.material.Lerp(matOne, matTwo, gettingDamaged);
		}
	}

	//Used to call this void in the bullet scripts
	//since currentHealth is a private variable
	public void EnemyDamaged(int damage) {
		if (canBeDamaged) {
			currentHealth -= damage;
		}
	}
}
