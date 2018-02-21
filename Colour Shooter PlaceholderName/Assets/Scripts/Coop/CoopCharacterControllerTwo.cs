using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopCharacterControllerTwo : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;
    public bool usingXboxController;

    [Header("Script References")]
    public CharacterTwoGunController coopCharacterControllerTwo;

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
        XDistBetweenPlayerAndAveragePlayerPos = Mathf.Abs(transform.position.x - mainCameraScript.averagePos.x);//CALCULATES DISTANCE ON X AXIS BETWEEN THIS PLAYER AND THE AVERAGE PLAYER POS THE CAMERA IS POINTED AT
        ZDistBetweenPlayerAndAveragePlayerPos = Mathf.Abs(transform.position.z - mainCameraScript.averagePos.z);//CALCULATES DISTANCE ON Z AXIS BETWEEN THIS PLAYER AND THE AVERAGE PLAYER POS THE CAMERA IS POINTED AT
        if (XDistBetweenPlayerAndAveragePlayerPos >= 19.75f)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(mainCameraScript.averagePos.x, transform.position.y, transform.position.z), cameraBorderPushbackSpeed);//PUSHES THE PLAYER BACK IF THE GO TOO FAR
        }
        if (ZDistBetweenPlayerAndAveragePlayerPos >= 11.15f)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y, mainCameraScript.averagePos.z), cameraBorderPushbackSpeed);//PUSHES THE PLAYER BACK IF THE GO TOO FAR
        }
        if (!usingXboxController) {
	        //Making a vector3 to store the characters inputs
	        moveInput = new Vector3(Input.GetAxisRaw("Joystick2LHorizontal"), 0f, Input.GetAxisRaw("Joystick2LVertical"));
	        //Multiply the moveInput by the moveVelocity to give it speed
	        moveVelocity = moveInput * moveSpeed;

	        //Making a new vector3 to do rotations with joystick
	        Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Joystick2RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick2RVertical");
	        //Checking if the vector3 has got a value inputed
	        if (playerDirection.sqrMagnitude > 0.0f) {
	            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
	        }

	        //Shooting the bullet
	        if (Input.GetKeyDown(KeyCode.Joystick1Button7)) {
	            coopCharacterControllerTwo.isFiring = true;
	        }
	        //Not shootings the bullet
	        if (Input.GetKeyUp(KeyCode.Joystick1Button7)) {
	            coopCharacterControllerTwo.isFiring = false;
	        }
	    }

		if (usingXboxController) {
			//Making a vector3 to store the characters inputs
			moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick2LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick2LVertical"));
			//Multiply the moveInput by the moveVelocity to give it speed
			moveVelocity = moveInput * moveSpeed;

			//Making a new vector3 to do rotations with joystick
			Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick2RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick2RVertical");
			//Checking if the vector3 has got a value inputed
			if (playerDirection.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
			}

			//Shooting the bullet
			if (Input.GetButtonDown("Fire2")) {
				coopCharacterControllerTwo.isFiring = true;
			}
			//Not shootings the bullet
			if (Input.GetButtonUp("Fire2")) {
				coopCharacterControllerTwo.isFiring = false;
			}
		}
    }

    void FixedUpdate () {
        //Set the Rigidbody to retreieve the moveVelocity;
        myRB.velocity = moveVelocity;

    }
}
