using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    [Header("Gun Variables")]
    public float bulletSpeed;
    public float bulletSpread;
    public float timeBetweenShots;
    public float timeBetweenSplats;
    [Space(10)]
    public bool isFiring;

    public bool isSplatting;
    public bool usingXboxController;
    private Vector3 startingPosition;
    private Vector3 recoiledPosition;

    [Header("GameObjects")]
    public Transform fireFrom;
    public GameObject[] currentBullet;
    public GameObject paintSplatProjector;

    [Header("Script References")]
    public ColourSelectManager colourSelectManager;

    public ColourPicker colourPicker;
    

    //Private variables
    private float shotCounter;
    private GameObject bullet;
    private float bulletSpreadWidth;
    private GameObject mainCamera;
    private CameraScript mainCameraScript;
    private float gunRecoilSpeed = 1f;
    private GameObject newestPaintSplat;

    void Start ()
    {
        startingPosition = transform.localPosition;
        recoiledPosition = new Vector3(0,0,0.3f);
        //Calling the ColourSelectManager
        colourSelectManager = ColourSelectManager.instance;
        colourPicker = GameObject.FindGameObjectWithTag("ColourPicker").GetComponent<ColourPicker>();
        //Getting the mainCamera from the current scene
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraScript = mainCamera.GetComponent<CameraScript>();
    }
	
	void Update () {
        //Debug.Log("startingPosition:   " +startingPosition);
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
            if (shotCounter > 0)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, startingPosition, gunRecoilSpeed*Time.deltaTime);
            }
        } else if (isSplatting)
	    {
	        shotCounter -= Time.deltaTime;
	        if (shotCounter <= 0)
	        {
	            shotCounter = timeBetweenSplats;
	            Splat();
	        }
	    }
        else {
            shotCounter = 0;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startingPosition, gunRecoilSpeed * Time.deltaTime);
        }

	    
	    //Giving the bullets a bit of spread
	    bulletSpreadWidth = Random.Range(-bulletSpread, bulletSpread);
	    
	}

    //Function that handles the bullets and which ones to instantiate
    void CurrentBulletFiring () {
        //When you left click, the gun fires
        if (Input.GetMouseButton (0) || Input.GetKey(KeyCode.Joystick1Button7)) {
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
            //adds camera shake when the bullet spawn
            //Finally gives a rotation to the bullet to give a bulletSpread affect
            transform.localPosition = recoiledPosition;
            GameObject bulletToShoot = colourSelectManager.GetBulletToShoot();
            bullet = (GameObject)Instantiate(bulletToShoot, fireFrom.position, fireFrom.rotation);
            mainCameraScript.SmallScreenShake();
            bullet.transform.Rotate(0f, bulletSpreadWidth, 0f);
        }
    }

    void Splat()
    {
        Debug.Log("Splat is Happening");
        float randomRotationXAndW = Random.Range(0, 360);
        float randomRotationZ = Random.Range(0, 360);
        float rotationY = -randomRotationZ;
        if (colourPicker.currentColourHighligted=="Blue")
        {
            Debug.Log("Blue Splat is Happening");
            newestPaintSplat = Instantiate(paintSplatProjector,transform.position,new Quaternion(randomRotationXAndW, rotationY, randomRotationZ, randomRotationXAndW));
            //newestPaintSplat.GetComponent<paintProjectorController>().ChangeToBlue();
        } else
        if (colourPicker.currentColourHighligted == "Red")
        {
            newestPaintSplat = Instantiate(paintSplatProjector, transform.position, new Quaternion(randomRotationXAndW, rotationY, randomRotationZ, randomRotationXAndW));
            //newestPaintSplat.GetComponent<paintProjectorController>().ChangeToRed();
        } else
        if (colourPicker.currentColourHighligted == "Yellow")
        {
            newestPaintSplat = Instantiate(paintSplatProjector, transform.position, new Quaternion(randomRotationXAndW, rotationY, randomRotationZ, randomRotationXAndW));
            //newestPaintSplat.GetComponent<paintProjectorController>().ChangeToYellow();
        }

    }
}
