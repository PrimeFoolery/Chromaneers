using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTwoGunController : MonoBehaviour {

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
    [Space(5)]
    [Header("SMG Weapon")]
    public Transform fireFromSMG;
    [Range(0, 50)] public float bulletSpeedSMG;
    [Range(10, 20)] public float bulletSpreadSMG;
    [Range(0, 1)] public float timeBetweenShotsSMG;
    [Header("Rainbow Weapon")]
    public Transform fireFromRainbow;
    [Range(0, 50)] public float bulletSpeedRainbow;
    [Range(0, 10)] public float bulletSpreadRainbow;
    [Range(0, 1)] public float timeBetweenShotsRainbow;

    public CharacterOneGunController.currentWeapon stateOfWeapon;
    private bool weaponPickedUp = false;
     
    [Header("Gun Variables")]
    public bool isFiring;
    public bool usingXboxController;

    [Header("GameObjects")]
    public GameObject[] currentBullet;

    [Header("Script References")]
    public ColourSelectManager colourSelectManager;
    public CoopCharacterControllerTwo coopCharacterControllerTwo;

    [Header("Controller Prompts")]
    public Image controllerPrompt;
    public Sprite[] controlSprite;
    public string controlState;

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
        controllerPrompt.enabled = false;
        controlState = "Idle";
    }

    void Update () {
        Vector2 ContPos = Camera.main.WorldToScreenPoint((this.transform.position));
        ContPos.x = ContPos.x - (Screen.width / 2);
        ContPos.y = ContPos.y - (Screen.height * 0.37963f);
        controllerPrompt.transform.localPosition = ContPos;
        if(controlState == "Idle")
        {
            controllerPrompt.sprite = controlSprite[0];
        }
        if (controlState == "Blue")
        {
            controllerPrompt.sprite = controlSprite[1];
        }
        //Checking whether or not the player is firing
        if (isFiring) {
            //We calculate when he shot
            shotCounter -= Time.deltaTime;
            //Then say when he can and cant shoot, so it isnt a stream of bullets
            if (shotCounter <= 0) {
                if (stateOfWeapon == CharacterOneGunController.currentWeapon.OriginalWeapon) {
                    shotCounter = timeBetweenShots;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.TrishotWeapon) {
                    shotCounter = timeBetweenShotsTri;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.SniperWeapon) {
                    shotCounter = timeBetweenShotsSniper;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.SMGWeapon) {
                    shotCounter = timeBetweenShotsSMG;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.RainbowWeapon) {
                    shotCounter = timeBetweenShotsRainbow;
                }
                //Calling function CurrentBulletFiring() which handles the bullets
                CurrentBulletFiring();
            }
            if (shotCounter > 0)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, startingPosition, gunRecoilSpeed*Time.deltaTime);
			}
        } else {
            shotCounter = 0;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, startingPosition, gunRecoilSpeed * Time.deltaTime);
        }
        //Giving the bullets a bit of spread
        if (stateOfWeapon == CharacterOneGunController.currentWeapon.OriginalWeapon) {
            bulletSpreadWidth = Random.Range(-bulletSpread, bulletSpread);
        } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.TrishotWeapon) {
            bulletSpreadWidth = Random.Range(-bulletSpreadTri, bulletSpreadTri);
        } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.SniperWeapon) {
            bulletSpreadWidth = Random.Range(-bulletSpreadSniper, bulletSpreadSniper);
        } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.SMGWeapon) {
            bulletSpreadWidth = Random.Range(-bulletSpreadSMG, bulletSpreadSMG);
        } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.RainbowWeapon) {
            bulletSpreadWidth = Random.Range(-bulletSpreadRainbow, bulletSpreadRainbow);
        }
        
        if (stateOfWeapon == CharacterOneGunController.currentWeapon.SniperWeapon || stateOfWeapon == CharacterOneGunController.currentWeapon.TrishotWeapon
            || stateOfWeapon == CharacterOneGunController.currentWeapon.SMGWeapon || stateOfWeapon == CharacterOneGunController.currentWeapon.RainbowWeapon)
        {
            if (coopCharacterControllerTwo.usingXboxController ==false)
            {
                if ((Input.GetKey(KeyCode.Joystick2Button0)) && weaponPickedUp == false)
                {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.OriginalWeapon;
                }
            } else
            if (coopCharacterControllerTwo.usingXboxController ==true)
            {
                if ((Input.GetButton("Pickup2")) && weaponPickedUp == false)
                {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.OriginalWeapon;
                }
            }
            
        }

        if (coopCharacterControllerTwo.usingXboxController == false)
        {
            if (Input.GetKeyUp(KeyCode.Joystick2Button0))
            {
                weaponPickedUp = false;
                controllerPrompt.enabled = false;
                controlState = "Idle";
            }
        } else
        if (coopCharacterControllerTwo.usingXboxController == true)
        {
            if (Input.GetButtonUp("Pickup2"))
            {
                weaponPickedUp = false;
                controllerPrompt.enabled = false;
                controlState = "Idle";
            }
        }
    }

    //Function that handles the bullets and which ones to instantiate
    void CurrentBulletFiring () {
        if (!coopCharacterControllerTwo.usingXboxController) {
            //When you left click, the gun fires
            if (Input.GetKey(KeyCode.Joystick2Button7)) {
                if (colourSelectManager.GetBulletRedToShoot() == null) {
                    //print("I am null! Check ColourSelectManager");
                    return;
                }
                //print("Red bullet firing");
                //Instantiate the bullet and set it as a gameObject
                //additionally, give it a fireFrom position and rotation [Which is an empty object]
                //adds camera shake when the bullet spawn
                //Finally gives a rotation to the bullet to give a bulletSpread affect
				transform.localPosition = recoiledPosition;
                GameObject bulletToShoot = colourSelectManager.GetBulletRedToShoot();
                if (stateOfWeapon == CharacterOneGunController.currentWeapon.OriginalWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromOriginal.position, fireFromOriginal.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.OriginalWeapon;
                    bullet.GetComponent<RedBulletController>().speedOriginal = bulletSpeedOriginal;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.TrishotWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromL.position, fireFromL.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.TrishotWeapon;
                    bullet.GetComponent<RedBulletController>().speedTri = bulletSpreadTri;
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromM.position, fireFromM.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.TrishotWeapon;
                    bullet.GetComponent<RedBulletController>().speedTri = bulletSpreadTri;
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromR.position, fireFromR.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.TrishotWeapon;
                    bullet.GetComponent<RedBulletController>().speedTri = bulletSpreadTri;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.SniperWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromSniper.position, fireFromSniper.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.SniperWeapon;
                    bullet.GetComponent<RedBulletController>().speedSniper = bulletSpeedSniper;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.SMGWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromSMG.position, fireFromSMG.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.SMGWeapon;
                    bullet.GetComponent<RedBulletController>().speedSMG = bulletSpeedSMG;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.RainbowWeapon) {
                    GameObject rainbowBulletToShoot = colourSelectManager.GetBulletRainbowRedToShoot();
                    bullet = (GameObject)Instantiate(rainbowBulletToShoot, fireFromRainbow.position, fireFromRainbow.rotation);
                    bullet.GetComponent<RainbowBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.RainbowWeapon;
                    bullet.GetComponent<RainbowBulletController>().speedRainbow = bulletSpeedRainbow;
                }
                mainCameraScript.SmallScreenShake();
                bullet.transform.Rotate(0f, bulletSpreadWidth, 0f);
                this.GetComponent<AudioSource>().Play();
            }
        } 
        if (coopCharacterControllerTwo.usingXboxController) {
            //When you left click, the gun fires
			if (Input.GetAxis("Fire2Right")>0.1f) {
                if (colourSelectManager.GetBulletRedToShoot() == null) {
                    //print("I am null! Check ColourSelectManager");
                    return;
                }
                //print("Red bullet firing");
                //Instantiate the bullet and set it as a gameObject
                //additionally, give it a fireFrom position and rotation [Which is an empty object]
			    //adds camera shake when the bullet spawn
                //Finally gives a rotation to the bullet to give a bulletSpread affect
				transform.localPosition = recoiledPosition;
                GameObject bulletToShoot = colourSelectManager.GetBulletRedToShoot();
                if (stateOfWeapon == CharacterOneGunController.currentWeapon.OriginalWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromOriginal.position, fireFromOriginal.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.OriginalWeapon;
                    bullet.GetComponent<RedBulletController>().speedOriginal = bulletSpeedOriginal;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.TrishotWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromL.position, fireFromL.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.TrishotWeapon;
                    bullet.GetComponent<RedBulletController>().speedTri = bulletSpreadTri;
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromM.position, fireFromM.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.TrishotWeapon;
                    bullet.GetComponent<RedBulletController>().speedTri = bulletSpreadTri;
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromR.position, fireFromR.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.TrishotWeapon;
                    bullet.GetComponent<RedBulletController>().speedTri = bulletSpreadTri;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.SniperWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromSniper.position, fireFromSniper.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.SniperWeapon;
                    bullet.GetComponent<RedBulletController>().speedSniper = bulletSpeedSniper;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.SMGWeapon) {
                    bullet = (GameObject)Instantiate(bulletToShoot, fireFromSMG.position, fireFromSMG.rotation);
                    bullet.GetComponent<RedBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.SMGWeapon;
                    bullet.GetComponent<RedBulletController>().speedSMG = bulletSpeedSMG;
                } else if (stateOfWeapon == CharacterOneGunController.currentWeapon.RainbowWeapon) {
                    GameObject rainbowBulletToShoot = colourSelectManager.GetBulletRainbowRedToShoot();
                    bullet = (GameObject)Instantiate(rainbowBulletToShoot, fireFromRainbow.position, fireFromRainbow.rotation);
                    bullet.GetComponent<RainbowBulletController>().currentWeapon = CharacterOneGunController.currentWeapon.RainbowWeapon;
                    bullet.GetComponent<RainbowBulletController>().speedRainbow = bulletSpeedRainbow;
                }
                mainCameraScript.SmallScreenShake();
                bullet.transform.Rotate(0f, bulletSpreadWidth, 0f);
                this.GetComponent<AudioSource>().Play();
            }
        } 
    }
    
    void OnTriggerStay (Collider theCol) {
        if (theCol.gameObject.CompareTag("TrishotWeapon"))
        {
            controllerPrompt.enabled = true;
            controlState = "Blue";
            if (coopCharacterControllerTwo.usingXboxController==false)
            {
                if (Input.GetKey(KeyCode.Joystick2Button0))
                {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.TrishotWeapon;
                    weaponPickedUp = true;
                    Destroy(theCol.gameObject);
                }
            } else 
            if (coopCharacterControllerTwo.usingXboxController==true)
            {
                if (Input.GetButton("Pickup2"))
                {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.TrishotWeapon;
                    weaponPickedUp = true;
                    Destroy(theCol.gameObject);
                }
            }
        }
        else if (theCol.gameObject.CompareTag("SniperWeapon"))
        {
            controllerPrompt.enabled = true;
            controlState = "Blue";
            if (coopCharacterControllerTwo.usingXboxController==false)
            {
                if (Input.GetKey(KeyCode.Joystick2Button0))
                {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.SniperWeapon;
                    weaponPickedUp = true;
                    Destroy(theCol.gameObject);
                }
            } else 
            if (coopCharacterControllerTwo.usingXboxController==true)
            {
                if (Input.GetButton("Pickup2"))
                {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.SniperWeapon;
                    weaponPickedUp = true;
                    Destroy(theCol.gameObject);
                }
            }
        } 
        else if (theCol.gameObject.CompareTag("SMGWeapon")) {
            controllerPrompt.enabled = true;
            controlState = "Blue";
            if (coopCharacterControllerTwo.usingXboxController == false) {
                if (Input.GetKey(KeyCode.Joystick2Button0)) {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.SMGWeapon;
                    weaponPickedUp = true;
                    Destroy(theCol.gameObject);
                }
            } else
            if (coopCharacterControllerTwo.usingXboxController == true) {
                if (Input.GetButton("Pickup2")) {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.SMGWeapon;
                    weaponPickedUp = true;
                    Destroy(theCol.gameObject);
                }
            }
        } 
        else if (theCol.gameObject.CompareTag("RainbowWeapon")) {
            controllerPrompt.enabled = true;
            controlState = "Blue";
            if (coopCharacterControllerTwo.usingXboxController == false) {
                if (Input.GetKey(KeyCode.Joystick2Button0)) {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.RainbowWeapon;
                    weaponPickedUp = true;
                    Destroy(theCol.gameObject);
                }
            } else
            if (coopCharacterControllerTwo.usingXboxController == true) {
                if (Input.GetButton("Pickup2")) {
                    stateOfWeapon = CharacterOneGunController.currentWeapon.RainbowWeapon;
                    weaponPickedUp = true;
                    Destroy(theCol.gameObject);
                }
            }
        }
    }
    private void OnTriggerExit(Collider theCol)
    {
        if (theCol.gameObject.CompareTag("TrishotWeapon"))
        {
            controllerPrompt.enabled = false;
            controlState = "Idle";
        }
        else if (theCol.gameObject.CompareTag("SniperWeapon"))
        {
            controllerPrompt.enabled = false;
            controlState = "Idle";
        } 
        else if (theCol.gameObject.CompareTag("SMGWeapon")) {
            controllerPrompt.enabled = false;
            controlState = "Idle";
        } 
        else if (theCol.gameObject.CompareTag("RainbowWeapon")) {
            controllerPrompt.enabled = false;
            controlState = "Idle";
        }
    }
}