using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    [Header("Gun Variables")]
    public bool isFiring;
    public float bulletSpeed;
    public float timeBetweenShots;

    [Header("GameObjects")]
    public Transform fireFrom;
    public GameObject[] currentBullet;

    [Header("Script References")]
    public ColourSelectManager colourSelectManager;

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
        //When you left click, the gun fires
        if (Input.GetMouseButton (0) || Input.GetKey (KeyCode.Q)) {
            if (colourSelectManager.GetBulletToShoot() == null) {
				//Making a LogError incase something is null
				Debug.Log ("Its null captain! Please fix it");
				//Returning
                return;
            }
			//Print to check if something is working
			Debug.Log("It is instantiating, all is good");
            //Instantiate the bullet and set it as a gameObject
            //additionally, give it a fireFrom position and rotation [Which is an empty object]
            GameObject bulletToShoot = colourSelectManager.GetBulletToShoot();
            bullet = (GameObject)Instantiate(bulletToShoot, fireFrom.position, fireFrom.rotation);
        }
    }



    /*
     * TESTING RAYCASTING
    */

    //Where is the bullet coming from
    public Transform shootFrom;
    public enum GunType { Semi, Burst, Auto };
    public GunType gunType;

    public void Shoot () {
        //Create a new ray
        Ray ray = new Ray(shootFrom.position, shootFrom.forward);
        //What we hit
        RaycastHit hit;
        //Distance of raycast
        float shootDistance = 20;
        //Implement the raycast
        if (Physics.Raycast(ray, out hit, shootDistance)) {
            shootDistance = hit.distance;
        }
        //Draw ray to see it with gizmos
        Debug.DrawRay(ray.origin, ray.direction * shootDistance, Color.blue, 1f);
    }

    public void ShootContinious () {
        if (gunType == GunType.Auto) {
            Shoot();
        }

    }
    
}
