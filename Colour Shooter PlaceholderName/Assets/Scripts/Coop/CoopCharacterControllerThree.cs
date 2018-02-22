using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopCharacterControllerThree : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;
    public float shootingSpeed;
    [Space(10)]
    public bool usingXboxController;
    public bool isShooting;

    [Header("Script References")]
    public CharacterThreeGunController coopCharacterControllerThree;

    //Private variables
    private Rigidbody myRB;
    private GameObject mainCamera;
    private CameraScript mainCameraScript;
    private float cameraBorderPushbackSpeed = 0f;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float XDistBetweenPlayerAndAveragePlayerPos;
    private float ZDistBetweenPlayerAndAveragePlayerPos;

    void Start () {
        //Getting the Rigidbody from the object attached to this script
        myRB = GetComponent<Rigidbody>();
        //Getting the mainCamera from the current scene
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraScript = mainCamera.GetComponent<CameraScript>();
    }
	
	void Update () {
        XDistBetweenPlayerAndAveragePlayerPos = Mathf.Abs(transform.position.x - mainCameraScript.averagePos.x);//CALCULATES DISTANCE ON X AXIS BETWEEN THIS PLAYER AND THE AVERAGE PLAYER POS THE CAMERA IS POINTED AT
        ZDistBetweenPlayerAndAveragePlayerPos = Mathf.Abs(transform.position.z - mainCameraScript.averagePos.z);//CALCULATES DISTANCE ON Z AXIS BETWEEN THIS PLAYER AND THE AVERAGE PLAYER POS THE CAMERA IS POINTED AT
        if (XDistBetweenPlayerAndAveragePlayerPos >= 19.75f)
        {
            cameraBorderPushbackSpeed += 0.01f;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(mainCameraScript.averagePos.x, transform.position.y, transform.position.z), cameraBorderPushbackSpeed);//PUSHES THE PLAYER BACK IF THE GO TOO FAR
        }
        if (ZDistBetweenPlayerAndAveragePlayerPos >= 11.15f)
        {
            cameraBorderPushbackSpeed += 0.01f;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y, mainCameraScript.averagePos.z), cameraBorderPushbackSpeed);//PUSHES THE PLAYER BACK IF THE GO TOO FAR
        }
        if (XDistBetweenPlayerAndAveragePlayerPos < 19.75f && ZDistBetweenPlayerAndAveragePlayerPos < 11.15f)
        {
            cameraBorderPushbackSpeed = 0f;
        }

	    //Checking whether an Xbox or Playstation controller is being used
        if (!usingXboxController) {
	        //Making a vector3 to store the characters inputs
	        moveInput = new Vector3(Input.GetAxisRaw("Joystick3LHorizontal"), 0f, Input.GetAxisRaw("Joystick3LVertical"));
            if (!isShooting) {
                //Multiply the moveInput by the moveVelocity to give it speed whilst walking
                moveVelocity = moveInput * moveSpeed;
            } else if (isShooting) {
                //Multiply the moveInput by the moveVelocity to give it speed and divide whilst shooting
                moveVelocity = moveInput * shootingSpeed;
            }

            //Making a new vector3 to do rotations with joystick
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Joystick3RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick3RVertical");
	        //Checking if the vector3 has got a value inputed
	        if (playerDirection.sqrMagnitude > 0.0f) {
	            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
	        }

	        //Shooting the bullet
	        if (Input.GetKeyDown(KeyCode.Joystick3Button7)) {
                mainCameraScript.SmallScreenShake();
                coopCharacterControllerThree.isFiring = true;
	        }
	        //Not shootings the bullet
	        if (Input.GetKeyUp(KeyCode.Joystick3Button7)) {
	            coopCharacterControllerThree.isFiring = false;
	        }
	    }

		if (usingXboxController) {
			//Making a vector3 to store the characters inputs
			moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick3LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick3LVertical"));
		    if (!isShooting) {
		        //Multiply the moveInput by the moveVelocity to give it speed whilst walking
		        moveVelocity = moveInput * moveSpeed;
		    } else if (isShooting) {
		        //Multiply the moveInput by the moveVelocity to give it speed and divide whilst shooting
		        moveVelocity = moveInput * shootingSpeed;
		    }

            //Making a new vector3 to do rotations with joystick
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick3RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick3RVertical");
			//Checking if the vector3 has got a value inputed
			if (playerDirection.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
			}

			//Shooting the bullet
			if (Input.GetButtonDown("Fire3")) {
                mainCameraScript.SmallScreenShake();
                coopCharacterControllerThree.isFiring = true;
			}
			//Not shootings the bullet
			if (Input.GetButtonUp("Fire3")) {
				coopCharacterControllerThree.isFiring = false;
			}
		}
    }

    void FixedUpdate () {
        //Set the Rigidbody to retreieve the moveVelocity;
        myRB.velocity = moveVelocity;

    }
}
