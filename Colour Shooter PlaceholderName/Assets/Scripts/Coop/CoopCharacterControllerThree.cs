﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

public class CoopCharacterControllerThree : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;
    public float shootingSpeed;
    public float timeToShoot;
    [Space(10)]
    public bool usingXboxController;
    public bool isShooting;
    public GameObject paintProjector;

    private EnemyManager listManager;
    public ColourPicker colourPicker;

    private bool currentlyDodging = false;
    private Vector3 dodgeDirection;
    private float dodgeDuration = 0.15f;
    public float RollSpeed;
    private float dodgeCooldown = 0f;

    [Header("Script References")]
    public CharacterThreeGunController coopCharacterControllerThree;

    private CoopCharacterControllerTwo redPlayer;
    private CoopCharacterControllerOne bluePlayer;

    public Color redColor = Color.red;
    public Color orangeColor = new Color(1, 0.75f, 0, 1);
    public Color yellowColor = Color.yellow;
    public Color greenColor = Color.green;
    public Color blueColor = Color.blue;
    public Color purpleColor = new Color(0.6f, 0, 1, 1);

    public string colourPlayerIsStandingOn;
    public bool canPlayerShoot = true;

    private string tagUnderPlayer;
    private Vector3 savedPosition;

    private float splatTimer = 0f;
    public GameObject paintBlob;

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

    //Private variables
    private Rigidbody myRB;
    private GameObject mainCamera;
    private CameraScript mainCameraScript;
    private float cameraBorderPushbackSpeed = 0f;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float XDistBetweenPlayerAndAveragePlayerPos;
	private float ZDistBetweenPlayerAndAveragePlayerPos;
	public float movingAcceleration = 1.1f;
	public float movingDecceleration = 0.9f;
	public float shootingDecceleration = 0.95f;
    private float poisonTimer = 3f;

    void Start () {
        //Getting the Rigidbody from the object attached to this script
        myRB = GetComponent<Rigidbody>();
        redPlayer = GameObject.FindGameObjectWithTag("RedPlayer").GetComponent<CoopCharacterControllerTwo>();
        bluePlayer = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<CoopCharacterControllerOne>();
        //Getting the mainCamera from the current scene
        colourPicker = GameObject.FindGameObjectWithTag("ColourPicker").GetComponent<ColourPicker>();
        listManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraScript = mainCamera.GetComponent<CameraScript>();
        brush.Color = yellowColor;
    }
	
	void Update () {

	    //Checking whether an Xbox or Playstation controller is being used
        if (!usingXboxController) {
	        //Making a vector3 to store the characters inputs
	        moveInput = new Vector3(Input.GetAxisRaw("Joystick3LHorizontal"), 0f, Input.GetAxisRaw("Joystick3LVertical"));
            if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - redPlayer.gameObject.transform.position.x <= -35f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -35f)
            {
                if (moveInput.x <= 0)
                {
                    moveInput.x = 0;
                }
            }
            else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - redPlayer.gameObject.transform.position.x >= 35f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 35f)
            {
                if (moveInput.x >= 0)
                {
                    moveInput.x = 0;
                }
            }

            if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - redPlayer.gameObject.transform.position.z <= -25f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -25f)
            {
                if (moveInput.z <= 0)
                {
                    moveInput.z = 0;
                }
            }
            else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - redPlayer.gameObject.transform.position.z >= 25f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 25f)
            {
                if (moveInput.z >= 0)
                {
                    moveInput.z = 0;
                }
            }
            if (!isShooting) {
				//Multiply the moveInput by the moveVelocity to give it speed whilst walking
				if(moveInput!= new Vector3(0,0,0)){
					if(colourPlayerIsStandingOn!="yellow"){
						if(moveSpeed<=5f){
							moveSpeed = moveSpeed * movingAcceleration;
						}
						if(moveSpeed>=5f){
							moveSpeed = 5f;
						}
					}else
						if(colourPlayerIsStandingOn=="yellow"){
							if(moveSpeed<=7f){
								moveSpeed = moveSpeed * movingAcceleration;
							}
							if(moveSpeed>=7f){
								moveSpeed = 7f;
							}
						}
					moveVelocity = moveInput * moveSpeed;
				}
				if(moveInput== new Vector3(0,0,0)){
					if(moveSpeed>=0.5f){
						moveSpeed = moveSpeed * movingDecceleration;
					}
					if(moveSpeed<=0.5f){
						moveSpeed = 0.5f;
					}
					moveVelocity = moveVelocity * movingDecceleration;
				}
			} else if (isShooting) {
                //Multiply the moveInput by the moveVelocity to give it speed and divide whilst shooting
			    if (colourPlayerIsStandingOn == "orange")
			    {
			        moveVelocity = moveInput * -1 * shootingSpeed;
			    }
			    else
			    {
			        moveVelocity = moveInput * shootingSpeed;
			    }
                if (moveInput!= new Vector3(0,0,0)){
					if(moveSpeed<=2f){
						moveSpeed = moveSpeed * movingAcceleration;
					}
					if(moveSpeed>=2f&&moveSpeed<=2.5f){
						moveSpeed = 2f;
					}
					if(moveSpeed>=2.5f){
						moveSpeed = moveSpeed * shootingDecceleration;
					}
					//moveVelocity = moveInput * moveSpeed;
				}
				if(moveInput== new Vector3(0,0,0)){
					if(moveSpeed>=0.5f){
						moveSpeed = moveSpeed * movingDecceleration;
					}
					if(moveSpeed<=0.5f){
						moveSpeed = 0.5f;
					}
					moveVelocity = moveVelocity * movingDecceleration;
				}
			}

            //Making a new vector3 to do rotations with joystick
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Joystick3RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick3RVertical");
	        //Checking if the vector3 has got a value inputed
	        if (playerDirection.sqrMagnitude > 0.0f) {
	            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
	        }

            //Stops people from spam clicking to shoot faster
            timeToShoot -= Time.deltaTime;
            if (timeToShoot <= 0) {
                //Shooting the bullet
                if (Input.GetKeyDown(KeyCode.Joystick3Button7)&&canPlayerShoot==true) {
                    coopCharacterControllerThree.isFiring = true;
                    isShooting = true;
                    timeToShoot = 0.5f;
                }
            }
	        //Not shootings the bullet
	        if (Input.GetKeyUp(KeyCode.Joystick3Button7)) {
                coopCharacterControllerThree.isFiring = false;
	            isShooting = false;
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button1) && currentlyDodging == false && dodgeCooldown <= 0f)
            {
                dodgeDirection = moveInput;
                Roll(dodgeDirection);
            }
            else if (currentlyDodging == true && dodgeDuration >= 0f)
            {
                Roll(dodgeDirection);
                dodgeDuration -= Time.deltaTime;
            }
            if (dodgeDuration < 0)
            {
                gameObject.GetComponent<CoopCharacterHealthControllerThree>().canBeDamaged = true;
				gameObject.GetComponent<CoopCharacterHealthControllerThree>().ChangeToMatOne();
                currentlyDodging = false;
				canPlayerShoot = true;
                dodgeDuration = 0.15f;
                dodgeCooldown = 1f;
            }

            if (currentlyDodging == false)
            {
                dodgeCooldown -= Time.deltaTime;
            }
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            //Debug.DrawRay(transform.position,Vector3.down, Color.yellow,20f);
            if (Physics.Raycast(ray, out hit, 20f))
            {
                //Debug.Log(hit.collider.name);
                float groundSizeX = hit.collider.gameObject.GetComponent<Renderer>().bounds.size.x;
                float neededBrushSize = 4.8228f * (Mathf.Pow(groundSizeX, -0.982f));
                brush.Scale = neededBrushSize;
                if (hit.collider)
                {



                    if (Input.GetKey(KeyCode.Joystick3Button6))
                    {
                        if (splatTimer <= 0f)
                        {
                            bool success = true;
                            var paintObject = hit.transform.GetComponent<InkCanvas>();
                            if (paintObject != null)
                            {
                                if (useMethodType == UseMethodType.RaycastHitInfo)
                                {
                                    GameObject tempPaintSplot = Instantiate(paintBlob, transform.position, Quaternion.identity);
                                    tempPaintSplot.GetComponent<paintSplatBlob>().SetPaintVariables(brush, hit, paintObject);
                                    tempPaintSplot.GetComponent<Renderer>().material.color = yellowColor;
                                    tempPaintSplot = null;
                                }
                            }
                            if (!success)
                            {
                                Debug.Log("Paint not painted correctly");
                            }

                            splatTimer = 0.5f;
                        }

                        splatTimer -= Time.deltaTime;
                    }
                }
            }
        }

		if (usingXboxController) {
			//Making a vector3 to store the characters inputs
			moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick3LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick3LVertical"));
		    if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - redPlayer.gameObject.transform.position.x <= -35f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -35f)
		    {
		        if (moveInput.x <= 0)
		        {
		            moveInput.x = 0;
		        }
		    }
		    else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - redPlayer.gameObject.transform.position.x >= 35f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 35f)
		    {
		        if (moveInput.x >= 0)
		        {
		            moveInput.x = 0;
		        }
		    }

		    if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - redPlayer.gameObject.transform.position.z <= -25f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -25f)
		    {
		        if (moveInput.z <= 0)
		        {
		            moveInput.z = 0;
		        }
		    }
		    else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - redPlayer.gameObject.transform.position.z >= 25f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 25f)
		    {
		        if (moveInput.z >= 0)
		        {
		            moveInput.z = 0;
		        }
		    }
            if (!isShooting) {
				//Multiply the moveInput by the moveVelocity to give it speed whilst walking
				if(moveInput!= new Vector3(0,0,0)){
					if(colourPlayerIsStandingOn!="yellow"){
						if(moveSpeed<=5f){
							moveSpeed = moveSpeed * movingAcceleration;
						}
						if(moveSpeed>=5f){
							moveSpeed = 5f;
						}
					}else
					if(colourPlayerIsStandingOn=="yellow"){
						if(moveSpeed<=7f){
							moveSpeed = moveSpeed * movingAcceleration;
						}
						if(moveSpeed>=7f){
							moveSpeed = 7f;
						}
					}

					moveVelocity = moveInput * moveSpeed;
				}
				if(moveInput== new Vector3(0,0,0)){
					if(moveSpeed>=0.5f){
						moveSpeed = moveSpeed * movingDecceleration;
					}
					if(moveSpeed<=0.5f){
						moveSpeed = 0.5f;
					}
					moveVelocity = moveVelocity * movingDecceleration;
				}
			} else if (isShooting) {
                //Multiply the moveInput by the moveVelocity to give it speed and divide whilst shooting
			    if (colourPlayerIsStandingOn == "orange")
			    {
			        moveVelocity = moveInput * -1 * shootingSpeed;
			    }
			    else
			    {
			        moveVelocity = moveInput * shootingSpeed;
			    }
                if (moveInput!= new Vector3(0,0,0)){
					if(moveSpeed<=2f){
						moveSpeed = moveSpeed * movingAcceleration;
					}
					if(moveSpeed>=2f&&moveSpeed<=2.5f){
						moveSpeed = 2f;
					}
					if(moveSpeed>=2.5f){
						moveSpeed = moveSpeed * shootingDecceleration;
					}
					//moveVelocity = moveInput * moveSpeed;
				}
				if(moveInput== new Vector3(0,0,0)){
					if(moveSpeed>=0.5f){
						moveSpeed = moveSpeed * movingDecceleration;
					}
					if(moveSpeed<=0.5f){
						moveSpeed = 0.5f;
					}
					moveVelocity = moveVelocity * movingDecceleration;
				}
			}

            //Making a new vector3 to do rotations with joystick
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick3RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick3RVertical");
			//Checking if the vector3 has got a value inputed
			if (playerDirection.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
			}

            //Stops people from spam clicking to shoot faster
		    timeToShoot -= Time.deltaTime;
		    if (timeToShoot <= 0) {
		        //Shooting the bullet
		        if (Input.GetButtonDown("Fire3Right") && canPlayerShoot == true) {
		            coopCharacterControllerThree.isFiring = true;
		            isShooting = true;
		            timeToShoot = 0.5f;
                }
            }
			
			//Not shootings the bullet
			if (Input.GetButtonUp("Fire3Right")) {
				coopCharacterControllerThree.isFiring = false;
			    isShooting = false;
            }
		    if (Input.GetButtonDown("Roll3") && currentlyDodging == false && dodgeCooldown <= 0f)
		    {
		        dodgeDirection = moveInput;
		        Roll(dodgeDirection);
		    }
		    else if (currentlyDodging == true && dodgeDuration >= 0f)
		    {
		        Roll(dodgeDirection);
		        dodgeDuration -= Time.deltaTime;
		    }
		    if (dodgeDuration < 0)
		    {
		        gameObject.GetComponent<CoopCharacterHealthControllerThree>().canBeDamaged = true;
				gameObject.GetComponent<CoopCharacterHealthControllerThree>().ChangeToMatOne();
                currentlyDodging = false;
				canPlayerShoot = true;
		        dodgeDuration = 0.15f;
		        dodgeCooldown = 1f;
		    }
		    if (currentlyDodging == false)
		    {
		        dodgeCooldown -= Time.deltaTime;
		    }
            RaycastHit hit;
		    Ray ray = new Ray(transform.position, Vector3.down);
		    //Debug.DrawRay(transform.position,Vector3.down, Color.yellow,20f);
		    if (Physics.Raycast(ray, out hit, 20f))
		    {
		        //Debug.Log(hit.collider.name);
				float groundSizeX = hit.collider.gameObject.GetComponent<Renderer>().bounds.size.x;
				float neededBrushSize = 4.8228f * (Mathf.Pow(groundSizeX, -0.982f));
				brush.Scale = neededBrushSize;
		        if (hit.collider)
		        {



		            if (Input.GetButton("Fire3Left"))
		            {
		                if (splatTimer <= 0f)
		                {
		                    bool success = true;
		                    var paintObject = hit.transform.GetComponent<InkCanvas>();
		                    if (paintObject != null)
		                    {
		                        if (useMethodType == UseMethodType.RaycastHitInfo)
		                        {
		                            colourPicker.currentColourHighligted = "Yellow";
                                    GameObject tempPaintSplot = Instantiate(paintBlob, transform.position, Quaternion.identity);
		                            tempPaintSplot.GetComponent<paintSplatBlob>().SetPaintVariables(brush, hit, paintObject);
		                            tempPaintSplot.GetComponent<Renderer>().material.color = yellowColor;
                                    tempPaintSplot = null;
		                        }
		                    }
		                    if (!success)
		                    {
		                        Debug.Log("Paint not painted correctly");
		                    }

		                    splatTimer = 0.5f;
		                }

		                splatTimer -= Time.deltaTime;
                    }
		        }
		    }
        }
	   
	    //Debug.Log(colourPlayerIsStandingOn);
	    if (colourPlayerIsStandingOn == "yellow")
	    {
	        moveSpeed = 6;
	    }
	    else
	    if (colourPlayerIsStandingOn == "blue")
	    {
	        moveSpeed = 2;
	    }
	    else
	    {
	        moveSpeed = 4;
	    }
	    if (colourPlayerIsStandingOn == "orange")
	    {
	        moveSpeed = -Mathf.Abs(moveSpeed);
	    }
	    if (colourPlayerIsStandingOn == "null")
	    {
	        moveSpeed = Mathf.Abs(moveSpeed);
	    }
	    if (colourPlayerIsStandingOn == "red")
	    {
	        Debug.Log("onRed:  " + poisonTimer);
	        poisonTimer -= Time.deltaTime;
	        if (poisonTimer <= 0)
	        {
	            gameObject.GetComponent<CoopCharacterHealthControllerThree>().GetHit();
	            poisonTimer = 3f;
	        }
	    }
	    RaycastHit floorHit;
	    Ray floorRay = new Ray(transform.position, Vector3.down);
	    if (Physics.Raycast(floorRay, out floorHit, 20f))
	    {

	        if (floorHit.collider)
	        {
	            tagUnderPlayer = floorHit.collider.gameObject.tag;
	            savedPosition = transform.position;
	        }
	        else
	        {
	            tagUnderPlayer = "null";
	        }
	    }
	    else
	    {
	        tagUnderPlayer = "null";
	    }
	    if (tagUnderPlayer != "Floor")
	    {
	        transform.position = Vector3.MoveTowards(transform.position,
	            new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z), 0.2f);
	    }

	    if (transform.position.y <= -5f)
	    {
	        gameObject.GetComponent<CoopCharacterHealthControllerThree>().Die();
            transform.position = savedPosition;
	    }
    }

    void FixedUpdate () {
        //Set the Rigidbody to retreieve the moveVelocity;
        myRB.velocity = moveVelocity;

    }
    public void Knockback(Vector3 bulletPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, bulletPosition, -0.6f);
    }
    private void Roll(Vector3 currentDirection)
    {
        if (currentDirection != new Vector3(0, 0, 0))
        {
            gameObject.GetComponent<CoopCharacterHealthControllerThree>().canBeDamaged = false;
            gameObject.GetComponent<CoopCharacterHealthControllerThree>().InvTimer = 1f;
            moveSpeed = 0;
            canPlayerShoot = false;
            currentlyDodging = true;
			transform.position = Vector3.MoveTowards(transform.position, transform.position+(currentDirection), 1f*RollSpeed*Time.deltaTime);
            moveVelocity = currentDirection * RollSpeed;
        }
    }
}
