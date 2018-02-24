using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerCharacterController : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;
    public float shootingSpeed;
    public float timeToShoot;
    [Space (10)]
    public bool usingController;
    public bool usingXboxController;
    public bool isShooting;

    public Vector3 pointToLook;

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
	    if (!isShooting) {
	        //Multiply the moveInput by the moveVelocity to give it speed whilst walking
	        moveVelocity = moveInput * moveSpeed;
        } else if (isShooting) {
	        //Multiply the moveInput by the moveVelocity to give it speed and divide whilst shooting
	        moveVelocity = moveInput * shootingSpeed;
        }

        //Checking if it is using the mouse
        if (!usingController) {
            //Creating a line from the Camera to the Mouse
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            //It intersects with a mathematical plane, not a physical plane
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            //Create a new float, which is a value for how far the camera is to the ground
            float rayLength;

            //Creating a raycast
            if (groundPlane.Raycast(cameraRay, out rayLength)) {
                //Set a point for the camera to look at
                pointToLook = cameraRay.GetPoint(rayLength);
                //Debug drawLine
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

                //Make the player to look towards the mouse
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            //Stops people from spam clicking to shoot faster
            timeToShoot -= Time.deltaTime;
            if (timeToShoot <= 0) {
                //Shooting the bullet
                if (Input.GetMouseButtonDown(0)) {
                    gunController.isFiring = true;
                    isShooting = true;
                    timeToShoot = 0.5f;
                }
            }
            //Not shootings the bullet
            if (Input.GetMouseButtonUp(0)) {
                gunController.isFiring = false;
                isShooting = false;
            }
        }
        
        //Checking if it is using a controller, and which controller, whether its Playstation or Xbox
        if (usingController) {
            if (!usingXboxController) {
                //Making a new vector3 to do rotations with joystick
                Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Joystick1RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick1RVertical");
                //Checking if the vector3 has got a value inputed
                if (playerDirection.sqrMagnitude > 0.0f) {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                }

                //Stops people from spam clicking to shoot faster
                timeToShoot -= Time.deltaTime;
                if (timeToShoot <= 0) {
                    //Shooting the bullet
                    if (Input.GetKeyDown(KeyCode.Joystick1Button7)) {
                        gunController.isFiring = true;
                        isShooting = true;
                        timeToShoot = 0.5f;
                    }
                }
                //Not shootings the bullet
                if (Input.GetKeyUp(KeyCode.Joystick1Button7)) {
                    gunController.isFiring = false;
                    isShooting = false;
                }
            }

            if (usingXboxController) {
                //Making a new vector3 to do rotations with joystick
                Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick1RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick1RVertical");
                //Checking if the vector3 has got a value inputed
                if (playerDirection.sqrMagnitude > 0.0f) {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                }

                //Stops people from spam clicking to shoot faster
                timeToShoot -= Time.deltaTime;
                if (timeToShoot <= 0) {
                    //Shooting the bullet
                    if (Input.GetButtonDown("Fire1")) {
                        gunController.isFiring = true;
                        isShooting = true;
                        timeToShoot = 0.5f;
                    }
                }
                //Not shootings the bullet
                if (Input.GetButtonUp("Fire1")) {
                    gunController.isFiring = false;
                    isShooting = false;
                }
            }
        }
	}

    void FixedUpdate () {
        //Set the Rigidbody to retreieve the moveVelocity;
        myRB.velocity = moveVelocity;
    }
}
