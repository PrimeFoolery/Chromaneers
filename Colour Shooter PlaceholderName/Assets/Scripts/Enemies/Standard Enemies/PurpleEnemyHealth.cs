﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleEnemyHealth : MonoBehaviour {

    //public int health;

    //Private variables
    public int redHealth=3;
	public int blueHealth=3;
	private float recoveryTimer = 1f;
	public GameObject sphere;

	public Material redJellyMaterial;
	public Material blueJellyMaterial;
	public Material purpleJellyMaterial;

    private GameObject mainCamera;
    private EnemyManager enemyManagerScript;

    private GameObject thisEnemiesSpawnPoint;

    [Header("Splat")]
    public GameObject PurpleSplat;
    public GameObject enemyEmpty;

    void Start () {
        //Setting the current health to be the health variable
        //so that when we start the game, the enemy has full HP
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        thisEnemiesSpawnPoint = gameObject.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint;
    }

    void Update () {
        //If the enemy reaches 0 HP, destroy the enemy
		if(redHealth<=0&&blueHealth>0){
			GetComponent<Renderer> ().material = blueJellyMaterial;
			sphere.GetComponent<Renderer> ().material = blueJellyMaterial;
		}
		if(redHealth<0){
			redHealth=0;
		}
		if(blueHealth<=0&&redHealth>0){
			GetComponent<Renderer> ().material = redJellyMaterial;
			sphere.GetComponent<Renderer> ().material = redJellyMaterial;
		}
		if(blueHealth<0){
			blueHealth=0;
		}
		if(blueHealth<=0&&redHealth<=0){
		    enemyManagerScript.enemyList.Remove(gameObject);
		    mainCamera.GetComponent<CameraScript>().SmallScreenShake();
		    if (thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
		    {
		        thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList.Remove(gameObject);
		    }
		    else if (thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
		    {
		        thisEnemiesSpawnPoint.GetComponent<newSpawner>().ThisSpawnpointsEnemyList.Remove(gameObject);
		    }
            Instantiate(PurpleSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
            Destroy (this.gameObject);
		}
		recoveryTimer -= Time.deltaTime;
		if(recoveryTimer<=0){
			redHealth = 3;
			blueHealth = 3;
			GetComponent<Renderer> ().material = purpleJellyMaterial;
			sphere.GetComponent<Renderer> ().material = purpleJellyMaterial;
		}
    }

    //Used to call this void in the bullet scripts
    //since currentHealth is a private variable
    public void EnemyDamaged (int damage) {
        //currentHealth -= damage;
		GetComponent<ParticleSystem> ().Play ();
    }
    public void PoisonDamaged()
    {
        GetComponent<ParticleSystem>().Play();
        if (blueHealth > 0)
        {
            blueHealth -= 1;
            recoveryTimer = 1f;
        }
        else
        {
            redHealth -= 1;
            recoveryTimer = 1f;
        }
        if (gameObject.GetComponent<StandardEnemyBehaviour>().isAggroPlayer == false)
        {
            gameObject.GetComponent<StandardEnemyBehaviour>().AggroToggle();
        }
    }
    public void OnCollisionEnter (Collision other){
		if(other.gameObject.CompareTag("RedBullet")){
			redHealth -= 1;
		    if (gameObject.GetComponent<StandardEnemyBehaviour>().isAggroPlayer == false)
		    {
		        gameObject.GetComponent<StandardEnemyBehaviour>().AggroToggle();
		    }
            recoveryTimer = 1f;
		}
		if(other.gameObject.CompareTag("BlueBullet")){
			blueHealth -= 1;
		    if (gameObject.GetComponent<StandardEnemyBehaviour>().isAggroPlayer == false)
		    {
		        gameObject.GetComponent<StandardEnemyBehaviour>().AggroToggle();
		    }
            recoveryTimer = 1f;
		}
	}
}
