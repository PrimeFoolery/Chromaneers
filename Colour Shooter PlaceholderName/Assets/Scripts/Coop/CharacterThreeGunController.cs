using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterThreeGunController : MonoBehaviour {

    [Header("Gun Variables")]
    public float bulletSpeed;
    public float timeBetweenShots;
    [Space(10)]
    public bool isFiring;
    public bool usingXboxController;

    [Header("GameObjects")]
    public Transform fireFrom;
    public GameObject[] currentBullet;

    [Header("Script References")]
    public ColourSelectManager colourSelectManager;
    public CoopCharacterControllerThree coopCharacterControllerThree;

    //Private variables
    private float shotCounter;
    //private ColourSelectmanager colourSelectManager;
    private GameObject bullet;

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
    }

    //Function that handles the bullets and which ones to instantiate
    void CurrentBulletFiring () {
        if (!coopCharacterControllerThree.usingXboxController) {
            //When you left click, the gun fires
            if (Input.GetKey(KeyCode.Joystick3Button7)) {
                if (colourSelectManager.GetBulletYellowToShoot() == null) {
                    print("anything");
                    return;
                }
                print("Blue bullet firing");
                //Instantiate the bullet and set it as a gameObject
                //additionally, give it a fireFrom position and rotation [Which is an empty object]
                GameObject bulletToShoot = colourSelectManager.GetBulletYellowToShoot();
                bullet = (GameObject)Instantiate(bulletToShoot, fireFrom.position, fireFrom.rotation);
            }
        } 
        if (coopCharacterControllerThree.usingXboxController) {
            //When you left click, the gun fires
			if (Input.GetButton("Fire3")) {
                if (colourSelectManager.GetBulletYellowToShoot() == null) {
                    print("anything");
                    return;
                }
                print("Blue bullet firing");
                //Instantiate the bullet and set it as a gameObject
                //additionally, give it a fireFrom position and rotation [Which is an empty object]
                GameObject bulletToShoot = colourSelectManager.GetBulletYellowToShoot();
                bullet = (GameObject)Instantiate(bulletToShoot, fireFrom.position, fireFrom.rotation);
            }
        } 
    }
}
