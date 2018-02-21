using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopCharacterControllerOne : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;
    public bool usingXboxController;

    [Header("Script References")]
    public CharacterOneGunController coopCharacterControllerOne;

    //Private variables
    private Rigidbody myRB;
    private GameObject mainCamera;
    private CameraScript mainCameraScript;
    private float cameraBorderPushbackSpeed = 0.3f;
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
        //print (Input.GetAxisRaw("XboxRightTriggerPlayerOne"));
        XDistBetweenPlayerAndAveragePlayerPos = Mathf.Abs(transform.position.x - mainCameraScript.averagePos.x);//CALCULATES DISTANCE ON X AXIS BETWEEN THIS PLAYER AND THE AVERAGE PLAYER POS THE CAMERA IS POINTED AT
        ZDistBetweenPlayerAndAveragePlayerPos = Mathf.Abs(transform.position.z - mainCameraScript.averagePos.z);//CALCULATES DISTANCE ON Z AXIS BETWEEN THIS PLAYER AND THE AVERAGE PLAYER POS THE CAMERA IS POINTED AT
        if (XDistBetweenPlayerAndAveragePlayerPos>=19.75f)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3 (mainCameraScript.averagePos.x, transform.position.y, transform.position.z), cameraBorderPushbackSpeed);//PUSHES THE PLAYER BACK IF THE GO TOO FAR
        }
        if (ZDistBetweenPlayerAndAveragePlayerPos >= 11.15f)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y, mainCameraScript.averagePos.z), cameraBorderPushbackSpeed);//PUSHES THE PLAYER BACK IF THE GO TOO FAR
        }
        if (!usingXboxController) {
	        //Making a vector3 to store the characters inputs
	        moveInput = new Vector3(Input.GetAxisRaw("Joystick1LHorizontal"), 0f, Input.GetAxisRaw("Joystick1LVertical"));
	        //Multiply the moveInput by the moveVelocity to give it speed
	        moveVelocity = moveInput * moveSpeed;

	        //Making a new vector3 to do rotations with joystick
	        Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Joystick1RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick1RVertical");
	        //Checking if the vector3 has got a value inputed
	        if (playerDirection.sqrMagnitude > 0.0f) {
	            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
	        }

	        //Shooting the bullet
	        if (Input.GetKeyDown(KeyCode.Joystick1Button7)) {
	            coopCharacterControllerOne.isFiring = true;
	        }
	        //Not shootings the bullet
	        if (Input.GetKeyUp(KeyCode.Joystick1Button7)) {
	            coopCharacterControllerOne.isFiring = false;
	        }
	    }

		if (usingXboxController) {
			//Making a vector3 to store the characters inputs
			moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
			//Multiply the moveInput by the moveVelocity to give it speed
			moveVelocity = moveInput * moveSpeed;

			//Making a new vector3 to do rotations with joystick
			Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick1RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick1RVertical");
			//Checking if the vector3 has got a value inputed
			if (playerDirection.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
			}

			//Shooting the bullet
			if (Input.GetButtonDown("Fire1")) {
				print ("WOWOWOWOWOWOW");
				coopCharacterControllerOne.isFiring = true;
			}
			//Not shootings the bullet
			if (Input.GetButtonUp("Fire1")) {
				coopCharacterControllerOne.isFiring = false;
			}
		}
    }

    void FixedUpdate () {
        //Set the Rigidbody to retreieve the moveVelocity;
        myRB.velocity = moveVelocity;

    }
}
