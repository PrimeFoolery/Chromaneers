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

    [Header("Manager Variables")]
    public bool isItSingleplayer;

    [Header("Bullet Prefabs")]
    public GameObject blueBulletPrefab;
    public GameObject redBulletPrefab;
    public GameObject yellowBulletPrefab;
    public GameObject purpleBulletPrefab;
    public GameObject greenBulletPrefab;
    public GameObject orangeBulletPrefab;
    public GameObject rainbowBulletBluePrefab;

    void Start () {
        //If the game is played singleplayer
        if (isItSingleplayer == true) {
            //The game will start with the blue bullet
            bulletToShoot = blueBulletPrefab;
        }
        //If the game is player coop
        if (isItSingleplayer == false) {
            blueBulletToShoot = blueBulletPrefab;
            redBulletToShoot = redBulletPrefab;
			yellowBulletToShoot = yellowBulletPrefab;
			purpleBulletToShoot = purpleBulletPrefab;
			orangeBulletToShoot = orangeBulletPrefab;
			greenBulletToShoot = greenBulletPrefab;
            rainbowBulletBlueToShoot = rainbowBulletBluePrefab;
        }
	}

    //Variable to set current bullet to shoot
    private GameObject bulletToShoot;
    private GameObject blueBulletToShoot;
    private GameObject redBulletToShoot;
	private GameObject yellowBulletToShoot;
	private GameObject purpleBulletToShoot;
	private GameObject orangeBulletToShoot;
	private GameObject greenBulletToShoot;
    private GameObject rainbowBulletBlueToShoot;

    //GameObject function to return which bullet to shoot in singleplayer
    public GameObject GetBulletToShoot () {
        return bulletToShoot;
    }

    //GameObject function to return which bullet to shoot for player one in coop
    public GameObject GetBulletBlueToShoot () {
        return blueBulletToShoot;
    }

    //GameObject function to return which bullet to shoot for player one in coop
    public GameObject GetBulletRedToShoot () {
        return redBulletToShoot;
    }

    //GameObject function to return which bullet to shoot for player one in coop
    public GameObject GetBulletYellowToShoot () {
        return yellowBulletToShoot;
    }

	//GameObject function to return which bullet to shoot for player one in coop
	public GameObject GetBulletPurpleToShoot () {
		return purpleBulletToShoot;
	}

	//GameObject function to return which bullet to shoot for player one in coop
	public GameObject GetBulletOrangeToShoot () {
		return orangeBulletToShoot;
	}

	//GameObject function to return which bullet to shoot for player one in coop
	public GameObject GetBulletGreenToShoot () {
		return greenBulletToShoot;
	}

    //GameObject function to return which bullet to shoot for player one in coop
    public GameObject GetBulletRainbowBlueToShoot()
    {
        return rainbowBulletBlueToShoot;
    }

    //Setting the private gameObject to a certain bullet
    public void SetBulletToShoot (GameObject bullet) {
        bulletToShoot = bullet;
    }
}
