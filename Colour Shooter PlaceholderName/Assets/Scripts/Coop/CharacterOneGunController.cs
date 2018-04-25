using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOneGunController : MonoBehaviour {

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
    public CoopCharacterControllerOne coopCharacterControllerOne;

	private Vector3 startingPosition;
	private Vector3 recoiledPosition;
	private float gunRecoilSpeed = 1f;

    //Private variables
    private float shotCounter;
    private GameObject bullet;
    private float bulletSpreadWidth;
    private GameObject mainCamera;
    private CameraScript mainCameraScript;

    void Start () {
        //Calling the ColourSelectManager
		startingPosition = transform.localPosition;
		recoiledPosition = new Vector3(0,0,0.3f);
        colourSelectManager = ColourSelectManager.instance;
        //Getting the mainCamera from the current scene
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraScript = mainCamera.GetComponent<CameraScript>();
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
			}if (shotCounter > 0)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, startingPosition, gunRecoilSpeed*Time.deltaTime);
			}
        } else {
            shotCounter = 0;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, startingPosition, gunRecoilSpeed * Time.deltaTime);
        }
        //Giving the bullets a bit of spread
        bulletSpreadWidth = Random.Range(-bulletSpread, bulletSpread);
    }

    //Function that handles the bullets and which ones to instantiate
    void CurrentBulletFiring () {
		if (!coopCharacterControllerOne.usingXboxController) {
            //When you left click, the gun fires
            if (Input.GetKey(KeyCode.Joystick1Button7)) {
                if (colourSelectManager.GetBulletBlueToShoot() == null) {
                    //print("I am null! Check ColourSelectManager");
                    return;
                }
                //print("Blue bullet firing");
                //Instantiate the bullet and set it as a gameObject
                //additionally, give it a fireFrom position and rotation [Which is an empty object]
                //adds camera shake when the bullet spawn
                //Finally gives a rotation to the bullet to give a bulletSpread affect
				transform.localPosition = recoiledPosition;
                GameObject bulletToShoot = colourSelectManager.GetBulletBlueToShoot();
                bullet = (GameObject)Instantiate(bulletToShoot, fireFrom.position, fireFrom.rotation);
                mainCameraScript.SmallScreenShake();
                bullet.transform.Rotate(0f, bulletSpreadWidth, 0f);
                this.GetComponent<AudioSource>().Play();
            }
        } 
		if (coopCharacterControllerOne.usingXboxController) {
            //When you left click, the gun fires
			if (Input.GetButton("Fire1Right")) {
                if (colourSelectManager.GetBulletBlueToShoot() == null) {
                    //print("I am null! Check ColourSelectManager");
                    return;
                }
                //print("Blue bullet firing");
                //Instantiate the bullet and set it as a gameObject
                //additionally, give it a fireFrom position and rotation [Which is an empty object]
			    //adds camera shake when the bullet spawn
                //Finally gives a rotation to the bullet to give a bulletSpread affect
				transform.localPosition = recoiledPosition;
                GameObject bulletToShoot = colourSelectManager.GetBulletBlueToShoot();
                bullet = (GameObject)Instantiate(bulletToShoot, fireFrom.position, fireFrom.rotation);
			    mainCameraScript.SmallScreenShake();
                bullet.transform.Rotate(0f, bulletSpreadWidth, 0f);
            }
        } 
    }
}
