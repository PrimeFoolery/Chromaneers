using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopCharacterControllerThree : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;
    public bool usingXboxController;

    [Header("Script References")]
    public CharacterThreeGunController coopCharacterControllerThree;

    //Private variables
    private Rigidbody myRB;
    private Camera mainCamera;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

	void Start () {
        //Getting the Rigidbody from the object attached to this script
        myRB = GetComponent<Rigidbody>();
        //Getting the mainCamera from the current scene
        mainCamera = FindObjectOfType<Camera>();
	}
	
	void Update () {
	    if (!usingXboxController) {
	        //Making a vector3 to store the characters inputs
	        moveInput = new Vector3(Input.GetAxisRaw("Joystick3LHorizontal"), 0f, Input.GetAxisRaw("Joystick3LVertical"));
	        //Multiply the moveInput by the moveVelocity to give it speed
	        moveVelocity = moveInput * moveSpeed;

	        //Making a new vector3 to do rotations with joystick
	        Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Joystick3RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick3RVertical");
	        //Checking if the vector3 has got a value inputed
	        if (playerDirection.sqrMagnitude > 0.0f) {
	            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
	        }

	        //Shooting the bullet
	        if (Input.GetKeyDown(KeyCode.Joystick1Button7)) {
	            coopCharacterControllerThree.isFiring = true;
	        }
	        //Not shootings the bullet
	        if (Input.GetKeyUp(KeyCode.Joystick1Button7)) {
	            coopCharacterControllerThree.isFiring = false;
	        }
	    }

		if (usingXboxController) {
			//Making a vector3 to store the characters inputs
			moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick3LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick3LVertical"));
			//Multiply the moveInput by the moveVelocity to give it speed
			moveVelocity = moveInput * moveSpeed;

			//Making a new vector3 to do rotations with joystick
			Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick3RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick3RVertical");
			//Checking if the vector3 has got a value inputed
			if (playerDirection.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
			}

			//Shooting the bullet
			if (Input.GetButtonDown("Fire3")) {
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
