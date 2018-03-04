using System.Collections;
using System.Collections.Generic;
using Es.InkPainter;
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
    public GameObject paintProjector;
    public ColourPicker colourPicker;

    public Vector3 pointToLook;

    [Header("Script References")]
    public GunController gunController;

    //Private variables
    private Rigidbody myRB;
    private Camera mainCamera;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    [System.Serializable]
    private enum UseMethodType
    {
        RaycastHitInfo,
    }
    [SerializeField]
    private Brush brush;
    [SerializeField]
    private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;
    [SerializeField]
    bool erase = false;

	void Start () {
        //Getting the Rigidbody from the object attached to this script
        myRB = GetComponent<Rigidbody>();
	    colourPicker = GameObject.FindGameObjectWithTag("ColourPicker").GetComponent<ColourPicker>();
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
                //Putting Splats down
                if (Input.GetMouseButton(1))
                {
                    //gunController.isSplatting = true;
                    //isShooting = true;
                    //timeToShoot = 0.5f;
                }
            }
            //Not shootings the bullet
            if (Input.GetMouseButtonUp(0)) {
                //gunController.isFiring = false;
                //isShooting = false;
            }
            //Not Splatting
            if (Input.GetMouseButtonUp(1))
            {
                gunController.isSplatting = false ;
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

	    if (colourPicker.currentColourHighligted=="Blue")
	    {
	        brush.Color = Color.blue;
	    }
	    if (colourPicker.currentColourHighligted == "Red")
	    {
	        brush.Color = Color.red;
	    }
	    if (colourPicker.currentColourHighligted == "Yellow")
	    {
	        brush.Color = Color.yellow;
	    }
	    if (colourPicker.currentColourHighligted == "Purple")
	    {
	        brush.Color = Color.magenta;
	    }
	    if (colourPicker.currentColourHighligted == "Orange")
	    {
	        brush.Color = Color.yellow;
	    }
	    if (colourPicker.currentColourHighligted == "Green")
	    {
	        brush.Color = Color.green;
	    }

        //Looking at the floor below player
        RaycastHit hit;
        Ray ray = new Ray(transform.position,Vector3.down);
        Debug.DrawRay(transform.position,Vector3.down, Color.yellow,20f);
	    if (Physics.Raycast(ray, out hit, 20f))
	    {
            Debug.Log(hit.collider.name);
	        if (hit.collider)
	        {
	            
                
                
	            if (Input.GetMouseButton(1))
	            {
	                
	            }

	            if (timeToShoot<=0)
	            {
	                if (Input.GetMouseButtonDown(1))
	                {
	                    bool success = true;
	                    var paintObject = hit.transform.GetComponent<InkCanvas>();
	                    if (paintObject != null)
	                    {
	                        if (useMethodType == UseMethodType.RaycastHitInfo)
	                        {
	                            brush.Scale = 0.068f;
	                            GameObject paintProjectionObject = Instantiate(paintProjector, transform.position, Quaternion.identity);
                                paintProjectionObject.GetComponent<paintProjectorController>().PaintStart(hit, paintObject,brush);
	                            //success = erase ? paintObject.Erase(brush, hit) : paintObject.Paint(brush, hit);
	                        }
	                    }

	                    if (!success)
	                    {
	                        Debug.Log("Paint not painted correctly");
	                    }
                        //timeToShoot = 0.5f;
                        //isShooting = true;

                    }
                }
	            if (Input.GetMouseButtonUp(1))
	            {
	                timeToShoot = 0.5f;
	                isShooting = true;
	            }


            }
           
	    }
	    
	}
    void PaintGround() { }
    void FixedUpdate () {
        //Set the Rigidbody to retreieve the moveVelocity;
        myRB.velocity = moveVelocity;
    }
}
