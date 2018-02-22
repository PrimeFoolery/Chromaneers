﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTwoGunController : MonoBehaviour {

    [Header("Gun Variables")]
    public float bulletSpeed;
    public float bulletSpread;
    public float timeBetweenShots;
    [Space(10)]
    public bool isFiring;
    public bool usingXboxController;

    [Header("GameObjects")]
    public Transform fireFrom;
    public GameObject[] currentBullet;

    [Header("Script References")]
    public ColourSelectManager colourSelectManager;
    public CoopCharacterControllerTwo coopCharacterControllerTwo;

    //Private variables
    private float shotCounter;
    private GameObject bullet;
    private float bulletSpreadWidth;

    void Start () {
        //Calling the ColourSelectManager
        colourSelectManager = ColourSelectManager.instance;
    }

    void Update () {
        //Checking whether or not the player is firing
        if (isFiring) {
            //We calculate when he shot
            shotCounter -= Time.deltaTime;
            //Then say when he can and cant shoot, so it isnt a stream of bullets
            if (shotCounter <= 0) {
                shotCounter = timeBetweenShots;
                //Calling function CurrentBulletFiring() which handles the bullets
                CurrentBulletFiring();
            }
        } else {
            shotCounter = 0;
        }
        //Giving the bullets a bit of spread
        bulletSpreadWidth = Random.Range(-bulletSpread, bulletSpread);
    }

    //Function that handles the bullets and which ones to instantiate
    void CurrentBulletFiring () {
        if (!coopCharacterControllerTwo.usingXboxController) {
            //When you left click, the gun fires
            if (Input.GetKey(KeyCode.Joystick2Button7)) {
                if (colourSelectManager.GetBulletRedToShoot() == null) {
                    print("I am null! Check ColourSelectManager");
                    return;
                }
                print("Red bullet firing");
                //Instantiate the bullet and set it as a gameObject
                //additionally, give it a fireFrom position and rotation [Which is an empty object]
                //Finally gives a rotation to the bullet to give a bulletSpread affect
                GameObject bulletToShoot = colourSelectManager.GetBulletRedToShoot();
                bullet = (GameObject)Instantiate(bulletToShoot, fireFrom.position, fireFrom.rotation);
                bullet.transform.Rotate(0f, bulletSpreadWidth, 0f);
            }
        } 
        if (coopCharacterControllerTwo.usingXboxController) {
            //When you left click, the gun fires
			if (Input.GetButton("Fire2")) {
                if (colourSelectManager.GetBulletRedToShoot() == null) {
                    print("I am null! Check ColourSelectManager");
                    return;
                }
                print("Red bullet firing");
			    //Instantiate the bullet and set it as a gameObject
			    //additionally, give it a fireFrom position and rotation [Which is an empty object]
			    //Finally gives a rotation to the bullet to give a bulletSpread affect
			    GameObject bulletToShoot = colourSelectManager.GetBulletRedToShoot();
			    bullet = (GameObject)Instantiate(bulletToShoot, fireFrom.position, fireFrom.rotation);
			    bullet.transform.Rotate(0f, bulletSpreadWidth, 0f);
            }
        } 
    }
}