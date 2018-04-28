using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOneGunController : MonoBehaviour {

    [Header("Weapon variables, and their stats")]
    [Header("Original Weapon")]
    public Transform fireFromOriginal;
    [Range(0, 50)] public float bulletSpeedOriginal;
    [Range(0, 10)] public float bulletSpread;
    [Range(0, 1)] public float timeBetweenShots;
    [Space(5)]
    [Header("Trishot Weapon")]
    public Transform fireFromL;
    public Transform fireFromM;
    public Transform fireFromR;
    [Range(0, 50)] public float bulletSpeedTrishot;
    [Range(0, 10)] public float bulletSpreadTri;
    [Range(0, 1)] public float timeBetweenShotsTri;
    [Space(5)]
    [Header("Sniper Weapon")]
    public Transform fireFromSniper;
    [Range(0, 50)] public float bulletSpeedSniper;
    [Range(0, 10)] public float bulletSpreadSniper;
    [Range(0, 1)] public float timeBetweenShotsSniper;


    public enum currentWeapon { OriginalWeapon, TrishotWeapon, SniperWeapon }
    public currentWeapon stateOfWeapon;

    [Header("Gun Variables")]
    public bool isFiring;
    public bool usingXboxController;

    [Header("GameObject")]
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
                if (stateOfWeapon == currentWeapon.OriginalWeapon) {
                    shotCounter = timeBetweenShots;
                } else if (stateOfWeapon == currentWeapon.TrishotWeapon) {
                    shotCounter = timeBetweenShotsTri;
                } else if (stateOfWeapon == currentWeapon.SniperWeapon) {
                    shotCounter = timeBetweenShotsSniper;
                }
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
        if (stateOfWeapon == currentWeapon.OriginalWeapon) {
            bulletSpreadWidth = Random.Range(-bulletSpread, bulletSpread);
        } else if (stateOfWeapon == currentWeapon.TrishotWeapon) {
            bulletSpreadWidth = Random.Range(-bulletSpreadTri, bulletSpreadTri);
        } else if (stateOfWeapon == currentWeapon.SniperWeapon) {
            bulletSpreadWidth = Random.Range(-bulletSpreadSniper, bulletSpreadSniper);
        }
    }

    //Function that handles the bullets and which ones to instantiate
    void CurrentBulletFiring () {
		if (!coopCharacterControllerOne.usingXboxController) {
            //When you left click, the gun fires
            if (Input.GetKey(KeyCode.Joystick1Button5)) {
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
                if (stateOfWeapon == currentWeapon.OriginalWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromOriginal.position, fireFromOriginal.rotation);
                    bullet.GetComponent<BlueBulletController>().currentWeapon = currentWeapon.OriginalWeapon;
                    bullet.GetComponent<BlueBulletController>().speedOriginal = bulletSpeedOriginal;
                } else if (stateOfWeapon == currentWeapon.TrishotWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromL.position, fireFromL.rotation);
                    bullet.GetComponent<BlueBulletController>().currentWeapon = currentWeapon.TrishotWeapon;
                    bullet.GetComponent<BlueBulletController>().speedTri = bulletSpreadTri;
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromM.position, fireFromM.rotation);
                    bullet.GetComponent<BlueBulletController>().currentWeapon = currentWeapon.TrishotWeapon;
                    bullet.GetComponent<BlueBulletController>().speedTri = bulletSpreadTri;
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromR.position, fireFromR.rotation);
                    bullet.GetComponent<BlueBulletController>().currentWeapon = currentWeapon.TrishotWeapon;
                    bullet.GetComponent<BlueBulletController>().speedTri = bulletSpreadTri;
                } else if (stateOfWeapon == currentWeapon.SniperWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromSniper.position, fireFromSniper.rotation);
                    bullet.GetComponent<BlueBulletController>().currentWeapon = currentWeapon.SniperWeapon;
                    bullet.GetComponent<BlueBulletController>().speedSniper = bulletSpeedSniper;
                }
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
                if (stateOfWeapon == currentWeapon.OriginalWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromOriginal.position, fireFromOriginal.rotation);
                } else if (stateOfWeapon == currentWeapon.TrishotWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromL.position, fireFromL.rotation);
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromM.position, fireFromM.rotation);
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromR.position, fireFromR.rotation);
                } else if (stateOfWeapon == currentWeapon.SniperWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromSniper.position, fireFromSniper.rotation);
                }
                mainCameraScript.SmallScreenShake();
                bullet.transform.Rotate(0f, bulletSpreadWidth, 0f);
                this.GetComponent<AudioSource>().Play();
            }
        } 
    }

    void OnTriggerStay (Collider theCol) {
        if (theCol.gameObject.CompareTag("TrishotWeapon")) {
            if (Input.GetKey(KeyCode.Joystick1Button0)) {
                stateOfWeapon = currentWeapon.TrishotWeapon;
            }
        } else if (!theCol.gameObject.CompareTag("TrishotWeapon") && Input.GetKey (KeyCode.Joystick1Button0)) {
            stateOfWeapon = currentWeapon.OriginalWeapon;
        }

        if (theCol.gameObject.CompareTag("SniperWeapon")) {
            if (Input.GetKey(KeyCode.Joystick1Button0)) {
                stateOfWeapon = currentWeapon.SniperWeapon;
            }
        } else if (!theCol.gameObject.CompareTag("SniperWeapon") && Input.GetKey(KeyCode.Joystick1Button0)) {
            stateOfWeapon = currentWeapon.OriginalWeapon;
        }
    }
}
