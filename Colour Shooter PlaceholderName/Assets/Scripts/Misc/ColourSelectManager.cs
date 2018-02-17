using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSelectManager : MonoBehaviour {

    public static ColourSelectManager instance;

    //Handles singleton instnace
    void Awake () {
        if (instance != null) {
            Debug.LogError("More than one ColourSelectManager in Scene!");
            return;
        }
        instance = this;
    }

    [Header("Bullet Prefabs")]
    public GameObject blueBulletPrefab;
    public GameObject redBulletPrefab;
    public GameObject yellowBulletPrefab;

    void Start () {
        //The game will start with the blue bullet
        bulletToShoot = blueBulletPrefab;
	}

    //Variable to set current bullet to shoot
    private GameObject bulletToShoot;

    //GameObject function to return which bullet to shoot
    public GameObject GetBulletToShoot () {
        return bulletToShoot;
    }

    //Setting the private gameObject to a certain bullet
    public void SetBulletToShoot (GameObject bullet) {
        bulletToShoot = bullet;
    }
}
