using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopCharacterControllerOne : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;

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
	}

    void FixedUpdate () {
        //Set the Rigidbody to retreieve the moveVelocity;
        myRB.velocity = moveVelocity;

    }
}
