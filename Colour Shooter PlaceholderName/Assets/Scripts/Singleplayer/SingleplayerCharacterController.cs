﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerCharacterController : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;

    public GameObject paintBullet;//THIS IS THE BULLET PREFAB
    public GameObject bulletEmitter;//THIS IS WHERE THE BULLETS COME FROM

    [Header("Script References")]
    public GunController gunController;

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
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //Multiply the moveInput by the moveVelocity to give it speed
        moveVelocity = moveInput * moveSpeed;

        //Creating a line from the Camera to the Mouse
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        //It intersects with a mathematical plane, not a physical plane
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //Create a new float, which is a value for how far the camera is to the ground
        float rayLength;

        //Creating a raycast
        if (groundPlane.Raycast(cameraRay, out rayLength)) {
            //Set a point for the camera to look at
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //Debug drawLine
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            //Make the player to look towards the mouse
            transform.LookAt(new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
        }

        /*
        //Shooting the bullet
        if (Input.GetMouseButtonDown (0)) {
            //gunController.isFiring = true;
            gunController.Shoot();
        } else if (Input.GetMouseButton (0)) {
            gunController.ShootContinious();
        }
        */

        //Shooting the bullet
        if (Input.GetMouseButtonDown(0)) {
            gunController.isFiring = true;
        }
        //Not shootings the bullet
        if (Input.GetMouseButtonUp (0)) {
            gunController.isFiring = false;
        }
	}

    void FixedUpdate () {
        //Set the Rigidbody to retreieve the moveVelocity;
        myRB.velocity = moveVelocity;

    }
}