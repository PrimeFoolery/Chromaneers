﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBulletController : MonoBehaviour {

    [Header("Bullet variables")]
	[Range(0, 50)] public float speedOriginal;
	[Range(0, 50)] public float speedTri;
	[Range(0, 50)] public float speedSniper;
	[Range(0, 50)] public float speedSMG;
    public float deceleration;
    public float bulletLifeTime;
	public float trishotLifeTime;
    public float reboundBulletLifeTime;
    public int bulletDamage;

    private float directionTimer = 0f;
    private Vector3 savedDirection;

    [Header("Misc")]
    public GameObject paint;
    public float paintLifeTime;
	private GameObject bullet;
	public ColourSelectManager colourSelectManager;
	public CharacterOneGunController characterOneGunController;

    enum bulletState
    {
        normalBullet,
        reboundBullet
    }
    private bulletState stateOfBullet;
    
	public CharacterOneGunController.currentWeapon currentWeapon;
	
    //Private variables
    Vector3 previousBulletPosition;

    void Start () {
        //Tells the previous bullet position to be set to the current transform position
        previousBulletPosition = transform.position;
    }

    void Update () {

        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0f)
        {
            savedDirection = transform.position;
            directionTimer = 1f;
        }

        //Setting previous button position to the new updated position
        previousBulletPosition = transform.position;

	    if (stateOfBullet == bulletState.normalBullet) {
		    //Moving the bullet
			if (currentWeapon == CharacterOneGunController.currentWeapon.OriginalWeapon) {
				transform.Translate (Vector3.forward * speedOriginal * Time.deltaTime);
				speedOriginal = speedOriginal * deceleration;
			} else if (currentWeapon == CharacterOneGunController.currentWeapon.TrishotWeapon) {
				transform.Translate (Vector3.forward * speedTri * Time.deltaTime);
				speedTri = speedTri * deceleration;
			} else if (currentWeapon == CharacterOneGunController.currentWeapon.SniperWeapon) {
				transform.Translate (Vector3.forward * speedSniper * Time.deltaTime);
				speedSniper = speedSniper * deceleration;
			} else if (currentWeapon == CharacterOneGunController.currentWeapon.SMGWeapon) {
				transform.Translate (Vector3.forward * speedSMG * Time.deltaTime);
				speedSMG = speedSMG * deceleration;
			}

		    if (currentWeapon == CharacterOneGunController.currentWeapon.OriginalWeapon ||
		        currentWeapon == CharacterOneGunController.currentWeapon.SniperWeapon ||
		        currentWeapon == CharacterOneGunController.currentWeapon.SMGWeapon) {
			    //Bullets lifeTime
			    bulletLifeTime -= Time.deltaTime;
			    //Destroying the bullet after its time has reached 0
			    if (bulletLifeTime <= 0) {
				    Destroy(gameObject);
			    } 
		    } else if (currentWeapon == CharacterOneGunController.currentWeapon.TrishotWeapon) {
			    //Bullets lifeTime
			    trishotLifeTime -= Time.deltaTime;
			    //Destroying the bullet after its time has reached 0
			    if (trishotLifeTime <= 0) {
				    Destroy(gameObject);
			    } 
		    }
	    }

        if (stateOfBullet == bulletState.reboundBullet)
        {
            //Rebound the bullet
	        if (currentWeapon == CharacterOneGunController.currentWeapon.OriginalWeapon) {
		        transform.Translate(((Vector3.back * 0.8f * speedOriginal) + (Vector3.up * 0.2f * speedOriginal)) * Time.deltaTime);
		        speedOriginal = speedOriginal * deceleration;
	        } else if (currentWeapon == CharacterOneGunController.currentWeapon.TrishotWeapon) {
		        transform.Translate(((Vector3.back * 0.8f * speedTri) + (Vector3.up * 0.2f * speedTri)) * Time.deltaTime);
		        speedTri = speedTri * deceleration;
	        } else if (currentWeapon == CharacterOneGunController.currentWeapon.SniperWeapon) {
		        transform.Translate(((Vector3.back * 0.8f * speedSniper) + (Vector3.up * 0.2f * speedSniper)) * Time.deltaTime);
		        speedSniper = speedSniper * deceleration;
			} else if (currentWeapon == CharacterOneGunController.currentWeapon.SMGWeapon) {
				transform.Translate(((Vector3.back * 0.8f * speedSMG) + (Vector3.up * 0.2f * speedSMG)) * Time.deltaTime);
				speedSMG = speedSMG * deceleration;
			}
            
            //Bullets lifeTime
            reboundBulletLifeTime -= Time.deltaTime;
            //Destroying the bullet after its time has reached 0
            if (reboundBulletLifeTime <= 0) {
                Destroy(gameObject);
            }
        }

        

        //Paint LifeTime
        paintLifeTime -= Time.deltaTime;
        //Destroying the paint after its time has reached 0
        if (bulletLifeTime <= 0) {
            //Destroy (paint);
        }
    }

    void OnCollisionEnter (Collision theCol) {
        //Check if its the Enemy
        if (stateOfBullet==bulletState.normalBullet)
        {
            if (theCol.gameObject.tag == "RedEnemy")
            {
                //When it collides with the enemy, apply the damage
                theCol.gameObject.GetComponent<RedEnemyHealth>().EnemyDamaged(bulletDamage);
                //and destroy the bullet
                Destroy(gameObject);
            }

            if (theCol.gameObject.tag == "PurpleEnemy")
            {
                //When it collides with the enemy, apply the damage
                theCol.gameObject.GetComponent<PurpleEnemyHealth>().EnemyDamaged(bulletDamage);
                //and destroy the bullet
                Destroy(gameObject);
            }

            if (theCol.gameObject.tag == "OrangeEnemy")
            {
                //When it collides with the enemy, apply the damage
                theCol.gameObject.GetComponent<OrangeEnemyHealth>().EnemyDamaged(bulletDamage);
                //and destroy the bullet
                Destroy(gameObject);
            }
            if (theCol.gameObject.CompareTag("RedEnemy") || theCol.gameObject.CompareTag("YellowEnemy") || theCol.gameObject.CompareTag("OrangeEnemy") || theCol.gameObject.CompareTag("BlueEnemy") || theCol.gameObject.CompareTag("GreenEnemy") || theCol.gameObject.CompareTag("PurpleEnemy"))
            {
                theCol.gameObject.GetComponent<StandardEnemyBehaviour>().BulletKnockback(savedDirection);
            }
			if(theCol.gameObject.GetComponent<FastEnemy>()!=null){
				if(theCol.gameObject.GetComponent<FastEnemy>().colourOfEnemy == "blue" || theCol.gameObject.GetComponent<FastEnemy>().colourOfEnemy == "yellow") {
				    //Change bullet state to rebound
				    stateOfBullet = bulletState.reboundBullet;
				    //Randomly rotate the gameObject into the sky
				    transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
				    //Scaling the bullet down
				    transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
				    //Change trail width
				    this.GetComponent<TrailRenderer>().startWidth = 0.3f;

				    theCol.gameObject.GetComponent<FastEnemy>().BulletKnockback(savedDirection);
                }
			}
			if(theCol.gameObject.GetComponent<SpiderEnemyController>()!=null){
				if(theCol.gameObject.GetComponent<SpiderEnemyController>().bodyColour==2 || theCol.gameObject.GetComponent<SpiderEnemyController>().bodyColour==3){
					//Change bullet state to rebound
					stateOfBullet = bulletState.reboundBullet;
					//Randomly rotate the gameObject into the sky
					transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
					//Scaling the bullet down
					transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
					//Change trail width
					this.GetComponent<TrailRenderer>().startWidth = 0.3f;
				}
			}
			if(theCol.gameObject.GetComponent<SpiderLegScript>()!=null){
				if(theCol.gameObject.GetComponent<SpiderLegScript>().legColour=="blue" || theCol.gameObject.GetComponent<SpiderLegScript>().legColour=="yellow"){
					//Change bullet state to rebound
					stateOfBullet = bulletState.reboundBullet;
					//Randomly rotate the gameObject into the sky
					transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
					//Scaling the bullet down
					transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
					//Change trail width
					this.GetComponent<TrailRenderer>().startWidth = 0.3f;
				}
			}
			if(theCol.gameObject.GetComponent<SnakeEnemyScript>()!=null){
				if(theCol.gameObject.GetComponent<SnakeEnemyScript>().colourOfSnake == "blue" || theCol.gameObject.GetComponent<SnakeEnemyScript>().colourOfSnake == "yellow"){
					//Change bullet state to rebound
					stateOfBullet = bulletState.reboundBullet;
					//Randomly rotate the gameObject into the sky
					transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
					//Scaling the bullet down
					transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
					//Change trail width
					this.GetComponent<TrailRenderer>().startWidth = 0.3f;
				}
			}
            if (theCol.collider.name == "RightDoor")
            {
                // Debug.Log("hitting right wall");
                //Change bullet state to rebound
                stateOfBullet = bulletState.reboundBullet;
                //Randomly rotate the gameObject into the sky
                transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
                //Scaling the bullet down
                transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
                //Change trail width
                this.GetComponent<TrailRenderer>().startWidth = 0.3f;
            }
            else if (theCol.collider.name == "LeftDoor")
            {
                // Debug.Log("hitting left wall");
                //Change bullet state to rebound
                stateOfBullet = bulletState.reboundBullet;
                //Randomly rotate the gameObject into the sky
                transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
                //Scaling the bullet down
                transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
                //Change trail width
                this.GetComponent<TrailRenderer>().startWidth = 0.3f;

            }
            else if (theCol.collider.name == "Boss")
            {
                if (theCol.gameObject.transform.parent.GetComponent<BossController>().colourOfEnemy != "red")
                {
                    //Change bullet state to rebound
                    stateOfBullet = bulletState.reboundBullet;
                    //Randomly rotate the gameObject into the sky
                    transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
                    //Scaling the bullet down
                    transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
                    //Change trail width
                    this.GetComponent<TrailRenderer>().startWidth = 0.3f;
                    theCol.gameObject.transform.parent.GetComponent<BossController>().BulletKnockback(savedDirection);
                }
            }
            if (theCol.gameObject.CompareTag("BluePlayer"))
            {
                theCol.gameObject.GetComponent<CoopCharacterControllerOne>().Knockback(savedDirection);
            }
            if (theCol.gameObject.CompareTag("YellowPlayer"))
            {
                theCol.gameObject.GetComponent<CoopCharacterControllerThree>().Knockback(savedDirection);
            }
        }
        

        //Check if it collides with the blue enemy
        if (theCol.gameObject.CompareTag("BlueEnemy") || theCol.gameObject.CompareTag("YellowEnemy") || theCol.gameObject.CompareTag("GreenEnemy")||theCol.gameObject.CompareTag("Wall") 
			||theCol.gameObject.CompareTag("YellowPlayer") || theCol.gameObject.CompareTag("BluePlayer") || theCol.gameObject.CompareTag("BlueBullet") || theCol.gameObject.CompareTag("YellowBullet"))
        {
            //Change bullet state to rebound
            stateOfBullet = bulletState.reboundBullet;
            //Randomly rotate the gameObject into the sky
            transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
            //Scaling the bullet down
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            //Change trail width
            this.GetComponent<TrailRenderer>().startWidth = 0.3f;
            //Play audio
            this.GetComponent<AudioSource>().Play();
        }
        
    }
}
