using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Es.InkPainter;

public class CoopCharacterControllerThree : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;
    public float shootingSpeed;
    public float timeToShoot;
    public Vector3 playerDirection;
	private Vector2 playerLookDirection;
    [Space(10)]
    public bool usingXboxController;
    public bool isShooting;
    public GameObject paintProjector;
    public bool CanPlayerMove = true;

    public bool isCameraZoomedOut = false;

    private EnemyManager listManager;
    public ColourPicker colourPicker;
    public ParticleSystem walkingPuff;
    private float walkingPuffCooldown = 0.2f;
    public bool currentlyDodging = false;
    private Vector3 dodgeDirection;
    private float dodgeDuration = 0.325f;
    public float RollSpeed;
    private float dodgeCooldown = 0f;
    public Slider dodgeSlider;

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
    private List<Vector3> savedPosition = new List<Vector3>();
    private float floorSavingTimer = 1f;

    private float splatTimer = 0f;
    public GameObject paintBlob;

    private float specialAttackCooldown = 0f;
    private bool specialAttackOn = false;
    private float specialAttackDuration = 0f;

    public Animator anim;

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

    private bool isFalling = false;

    public AudioClip playerWalking;
    public AudioClip playerDashing;
    AudioSource audio;

    public Vector3 thisPlayersReviveSpot;

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
        audio = GetComponent<AudioSource>();
	    
	    playerLookDirection.x = 0f;
	    playerLookDirection.y = 1f;
    }
	
	void Update () {



        dodgeSlider.value = (dodgeCooldown);
        //Checking whether an Xbox or Playstation controller is being used
        if (!usingXboxController) {
	        var xLeft = Input.GetAxis("Joystick3LHorizontal");
	        var yLeft = Input.GetAxis("Joystick3LVertical");
	        var xRight = Input.GetAxis("Joystick3RHorizontal");
	        var yRight = Input.GetAxis("Joystick3RVertical");
	        Move(xLeft, yLeft, xRight, yRight);

            //Making a vector3 to store the characters inputs
            if (gameObject.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
            {
                moveInput = new Vector3(Input.GetAxisRaw("Joystick3LHorizontal"), 0f, Input.GetAxisRaw("Joystick3LVertical"));
            }
            else
            {
                moveInput = new Vector3(0, 0, 0);
                moveVelocity = new Vector3(0, 0, 0);
                audio.Stop();
            }

            if (isCameraZoomedOut==false)
            {
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
            }
            else
            {
                
                        if (transform.position.x - mainCameraScript.averagePos.x <= -35f || transform.position.x - redPlayer.gameObject.transform.position.x <= -45f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -45f)
                        {
                            if (moveInput.x <= 0)
                            {
                                moveInput.x = 0;
                            }
                        }
                        else if (transform.position.x - mainCameraScript.averagePos.x >= 35f || transform.position.x - redPlayer.gameObject.transform.position.x >= 45f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 45f)
                        {
                            if (moveInput.x >= 0)
                            {
                                moveInput.x = 0;
                            }
                        }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -25f || transform.position.z - redPlayer.gameObject.transform.position.z <= -35f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -35f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 25f || transform.position.z - redPlayer.gameObject.transform.position.z >= 35f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 35f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                
            }
            

            if (CanPlayerMove==true)
            {
                if (!isShooting)
                {
                    //Multiply the moveInput by the moveVelocity to give it speed whilst walking
                    if (moveInput != new Vector3(0, 0, 0))
                    {
                        if (colourPlayerIsStandingOn != "yellow")
                        {
                            if (walkingPuffCooldown <= 0)
                            {
                                walkingPuff.Play();
                                walkingPuffCooldown = 0.2f;
                                audio.Play();
                            }

                            walkingPuffCooldown -= Time.deltaTime;
                            if (moveSpeed <= 6f)
                            {
                                moveSpeed = moveSpeed * movingAcceleration;
                            }
                            if (moveSpeed >= 6f)
                            {
                                moveSpeed = 6f;
                            }
                        }
                        else
                            if (colourPlayerIsStandingOn == "yellow")
                        {
                            if (moveSpeed <= 7f)
                            {
                                moveSpeed = moveSpeed * movingAcceleration;
                            }
                            if (moveSpeed >= 7f)
                            {
                                moveSpeed = 7f;
                            }
                        }
                        moveVelocity = moveInput * moveSpeed;
                    }
                    if (moveInput == new Vector3(0, 0, 0))
                    {
                        walkingPuff.Stop();
                        audio.Stop();
                        if (moveSpeed >= 2f)
                        {
                            moveSpeed = moveSpeed * movingDecceleration;
                        }
                        if (moveSpeed <= 2f)
                        {
                            moveSpeed = 2f;
                        }
                        moveVelocity = moveVelocity * movingDecceleration;
                    }
                }
                else if (isShooting)
                {
                    //Multiply the moveInput by the moveVelocity to give it speed and divide whilst shooting
                    if (colourPlayerIsStandingOn == "orange")
                    {
                        moveVelocity = moveInput * -1 * shootingSpeed;
                    }
                    else
                    {
                        moveVelocity = moveInput * shootingSpeed;
                    }
                    if (moveInput != new Vector3(0, 0, 0))
                    {
                        if (walkingPuffCooldown <= 0)
                        {
                            walkingPuff.Play();
                            walkingPuffCooldown = 0.2f;
                        }

                        walkingPuffCooldown -= Time.deltaTime;
                        if (moveSpeed <= 4f)
                        {
                            moveSpeed = moveSpeed * movingAcceleration;
                        }
                        if (moveSpeed >= 4f && moveSpeed <= 2.5f)
                        {
                            moveSpeed = 4f;
                        }
                        if (moveSpeed >= 2.5f)
                        {
                            moveSpeed = moveSpeed * shootingDecceleration;
                        }
                        //moveVelocity = moveInput * moveSpeed;
                    }
                    if (moveInput == new Vector3(0, 0, 0))
                    {
                        walkingPuff.Stop();
                        audio.Stop();
                        if (moveSpeed >= 2f)
                        {
                            moveSpeed = moveSpeed * movingDecceleration;
                        }
                        if (moveSpeed <= 2f)
                        {
                            moveSpeed = 2f;
                        }
                        moveVelocity = moveVelocity * movingDecceleration;
                    }
                }
            }
            

	        //Making a new vector3 to do rotations with joystick
	        playerDirection = Vector3.right * Input.GetAxisRaw("Joystick3RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick3RVertical");
	        //Checking if the vector3 has got a value inputed
	        if (playerDirection.sqrMagnitude > 0.0f) {
		        transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
		        playerLookDirection.x = playerDirection.x;
		        playerLookDirection.y = playerDirection.z;
	        }
	        else
	        {
		        Vector3 playerDirectionAlt = Vector3.right * Input.GetAxisRaw("Joystick3LHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick3LVertical");
		        if (playerDirectionAlt.sqrMagnitude > 0.0f) {
			        transform.rotation = Quaternion.LookRotation(playerDirectionAlt, Vector3.up);
			        playerLookDirection.x = playerDirectionAlt.x;
			        playerLookDirection.y = playerDirectionAlt.z;
		        }
	        }

            //Stops people from spam clicking to shoot faster
            timeToShoot -= Time.deltaTime;
            if (timeToShoot <= 0) {
                //Shooting the bullet
                if (playerDirection != new Vector3(0, 0, 0) && canPlayerShoot==true) {
                    coopCharacterControllerThree.isFiring = true;
                    isShooting = true;
                    timeToShoot = 0.5f;
                }
            }
	        //Not shootings the bullet
	        if (playerDirection == new Vector3(0, 0, 0)) {
                coopCharacterControllerThree.isFiring = false;
	            isShooting = false;
            }
            if (Input.GetKeyDown(KeyCode.Joystick3Button5) && currentlyDodging == false && dodgeCooldown <= 0f && gameObject.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
            {
                if (moveInput != new Vector3(0, 0, 0))
                {
                    gameObject.GetComponent<ParticleSystem>().Play();
                    dodgeDirection = moveInput;
                    Roll(dodgeDirection);
                    audio.PlayOneShot(playerDashing, 1f);
                }
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
                CanPlayerMove = true;
                dodgeDuration = 0.325f;
                dodgeCooldown = 1f;
            }

            if (currentlyDodging == false)
            {
                dodgeCooldown -= Time.deltaTime;
            }/*
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
            }*/
            //Debug.Log(specialAttackCooldown);
            //Debug.Log(specialAttackOn);
            if (Input.GetKey(KeyCode.Joystick3Button4) && specialAttackCooldown <= 0&&specialAttackOn==false)
            {
                //Debug.Log("USE Yellows SPECIAL ATTACK");
                //specialAttackDuration = 5f;
                //specialAttackOn = true;

            }

            if (specialAttackOn==true&&specialAttackDuration>=0)
            {
                Debug.Log("yellows Special Attack being used");
                specialAttackDuration -= Time.deltaTime;
            }

            if (specialAttackOn==true&&specialAttackDuration<0)
            {
                
                specialAttackCooldown = 45f;
                specialAttackOn = false;
            }
            if (specialAttackCooldown >= 0)
            {
                specialAttackCooldown -= Time.deltaTime;
            }
        }

		if (usingXboxController) {
			var xLeft = Input.GetAxis("XboxJoystick3LHorizontal");
			var yLeft = Input.GetAxis("XboxJoystick3LVertical");
			var xRight = Input.GetAxis("XboxJoystick3RHorizontal");
			var yRight = Input.GetAxis("XboxJoystick3RVertical");
			Move(xLeft, yLeft, xRight, yRight);
            //Making a vector3 to store the characters inputs
		    if (gameObject.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
		    {
		        moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick3LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick3LVertical"));
		    }
		    else
		    {
		        moveInput = new Vector3(0, 0, 0);
		        moveVelocity = new Vector3(0, 0, 0);
		        audio.Stop();
            }
            if (isCameraZoomedOut == false)
            {
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
            }
            else
            {

                if (transform.position.x - mainCameraScript.averagePos.x <= -35f || transform.position.x - redPlayer.gameObject.transform.position.x <= -45f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -45f)
                {
                    if (moveInput.x <= 0)
                    {
                        moveInput.x = 0;
                    }
                }
                else if (transform.position.x - mainCameraScript.averagePos.x >= 35f || transform.position.x - redPlayer.gameObject.transform.position.x >= 45f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 45f)
                {
                    if (moveInput.x >= 0)
                    {
                        moveInput.x = 0;
                    }
                }

                if (transform.position.z - mainCameraScript.averagePos.z <= -25f || transform.position.z - redPlayer.gameObject.transform.position.z <= -35f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -35f)
                {
                    if (moveInput.z <= 0)
                    {
                        moveInput.z = 0;
                    }
                }
                else if (transform.position.z - mainCameraScript.averagePos.z >= 25f || transform.position.z - redPlayer.gameObject.transform.position.z >= 35f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 35f)
                {
                    if (moveInput.z >= 0)
                    {
                        moveInput.z = 0;
                    }
                }

            }

            if (CanPlayerMove==true)
		    {
                if (!isShooting)
                {
                    //Multiply the moveInput by the moveVelocity to give it speed whilst walking
                    if (moveInput != new Vector3(0, 0, 0))
                    {
                        if (walkingPuffCooldown <= 0)
                        {
                            walkingPuff.Play();
                            walkingPuffCooldown = 0.2f;
                            audio.Play();
                        }

                        walkingPuffCooldown -= Time.deltaTime;
                        if (colourPlayerIsStandingOn != "yellow")
                        {
                            if (moveSpeed <= 6f)
                            {
                                moveSpeed = moveSpeed * movingAcceleration;
                            }
                            if (moveSpeed >= 6f)
                            {
                                moveSpeed = 6f;
                            }
                        }
                        else
                        if (colourPlayerIsStandingOn == "yellow")
                        {
                            if (moveSpeed <= 7f)
                            {
                                moveSpeed = moveSpeed * movingAcceleration;
                            }
                            if (moveSpeed >= 7f)
                            {
                                moveSpeed = 7f;
                            }
                        }

                        moveVelocity = moveInput * moveSpeed;
                    }
                    if (moveInput == new Vector3(0, 0, 0))
                    {
                        walkingPuff.Stop();
                        audio.Stop();
                        if (moveSpeed >= 2f)
                        {
                            moveSpeed = moveSpeed * movingDecceleration;
                        }
                        if (moveSpeed <= 2f)
                        {
                            moveSpeed = 2f;
                        }
                        moveVelocity = moveVelocity * movingDecceleration;
                    }
                }
                else if (isShooting)
                {
                    //Multiply the moveInput by the moveVelocity to give it speed and divide whilst shooting
                    if (colourPlayerIsStandingOn == "orange")
                    {
                        moveVelocity = moveInput * -1 * shootingSpeed;
                    }
                    else
                    {
                        moveVelocity = moveInput * shootingSpeed;
                    }
                    if (moveInput != new Vector3(0, 0, 0))
                    {
                        if (walkingPuffCooldown <= 0)
                        {
                            walkingPuff.Play();
                            walkingPuffCooldown = 0.2f;
                        }

                        walkingPuffCooldown -= Time.deltaTime;
                        if (moveSpeed <= 4f)
                        {
                            moveSpeed = moveSpeed * movingAcceleration;
                        }
                        if (moveSpeed >= 4f && moveSpeed <= 2.5f)
                        {
                            moveSpeed = 4f;
                        }
                        if (moveSpeed >= 2.5f)
                        {
                            moveSpeed = moveSpeed * shootingDecceleration;
                        }
                        //moveVelocity = moveInput * moveSpeed;
                    }
                    if (moveInput == new Vector3(0, 0, 0))
                    {
                        walkingPuff.Stop();
                        audio.Stop();
                        if (moveSpeed >= 2f)
                        {
                            moveSpeed = moveSpeed * movingDecceleration;
                        }
                        if (moveSpeed <= 2f)
                        {
                            moveSpeed = 2f;
                        }
                        moveVelocity = moveVelocity * movingDecceleration;
                    }
                }
            }
            

			//Making a new vector3 to do rotations with joystick
			//Setting it where player can move and shoot whilst looking around
			playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick3RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick3RVertical");
			//Checking if the vector3 has got a value inputed
			if (playerDirection.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
				playerLookDirection.x = playerDirection.x;
				playerLookDirection.y = playerDirection.z;
			}
			//Setting it where player can rotate whilst moving but not shoot
			else
			{
				Vector3 playerDirectionAlt = Vector3.right * Input.GetAxisRaw("XboxJoystick3LHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick3LVertical");
				if (playerDirectionAlt.sqrMagnitude > 0.0f) {
					transform.rotation = Quaternion.LookRotation(playerDirectionAlt, Vector3.up);
					playerLookDirection.x = playerDirectionAlt.x;
					playerLookDirection.y = playerDirectionAlt.z;
				}
			}

            //Stops people from spam clicking to shoot faster
		    timeToShoot -= Time.deltaTime;
		    if (timeToShoot <= 0) {
		        //Shooting the bullet
		        if (playerDirection != new Vector3(0, 0, 0) && canPlayerShoot == true) {
		            coopCharacterControllerThree.isFiring = true;
		            isShooting = true;
		            timeToShoot = 0.5f;
                }
            }
			
			//Not shootings the bullet
			if (playerDirection == new Vector3(0, 0, 0)) {
				coopCharacterControllerThree.isFiring = false;
			    isShooting = false;
            }
		    if (Input.GetButtonDown("Roll3") && currentlyDodging == false && dodgeCooldown <= 0f && gameObject.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
		    {
		        if (moveInput != new Vector3(0, 0, 0))
		        {
		            gameObject.GetComponent<ParticleSystem>().Play();
		            dodgeDirection = moveInput;
		            Roll(dodgeDirection);
		            audio.PlayOneShot(playerDashing, 1f);
		        }
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
		        CanPlayerMove = true;
		        dodgeDuration = 0.325f;
		        dodgeCooldown = 1f;
		    }
		    if (currentlyDodging == false)
		    {
		        dodgeCooldown -= Time.deltaTime;
		    }/*
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
		    }*/
		    if (Input.GetButton("Fire3Left") && specialAttackCooldown <= 0 && specialAttackOn == false)
		    {
		        //Debug.Log("USE Yellows SPECIAL ATTACK");
		        //specialAttackDuration = 5f;
		        //specialAttackOn = true;

		    }

		    if (specialAttackOn == true && specialAttackDuration >= 0)
		    {
		        Debug.Log("yellows Special Attack being used");
		        specialAttackDuration -= Time.deltaTime;
		    }

		    if (specialAttackOn == true && specialAttackDuration < 0)
		    {

		        specialAttackCooldown = 45f;
		        specialAttackOn = false;
		    }
		    if (specialAttackCooldown >= 0)
		    {
		        specialAttackCooldown -= Time.deltaTime;
		    }
        }
	
	    RaycastHit floorHit;
	    Ray floorRay = new Ray(transform.position, Vector3.down);
	    if (Physics.Raycast(floorRay, out floorHit, 20f))
	    {

	        if (floorHit.collider)
	        {
	            tagUnderPlayer = floorHit.collider.gameObject.tag;
	            floorSavingTimer -= Time.deltaTime;
	            if (floorSavingTimer < 0)
	            {
	                savedPosition.Add(transform.position);
	                if (savedPosition.Count > 4)
	                {
	                    savedPosition.RemoveAt(0);
	                }
	                floorSavingTimer = 1;
	            }
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

	    if (tagUnderPlayer == "FallingArea" && currentlyDodging == false)
	    {
	        isFalling = true;
	    }

	    if (isFalling == true)
	    {
	        transform.position = Vector3.MoveTowards(transform.position,
	            new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z), 5f * Time.deltaTime);
	    }
	    if (transform.position.y <= -5f)
	    {
	        gameObject.GetComponent<CoopCharacterHealthControllerThree>().Die();
	        isFalling = false;
	        transform.position = thisPlayersReviveSpot;
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
            //moveSpeed = 0;
            canPlayerShoot = false;
            CanPlayerMove = false;
            currentlyDodging = true;
            transform.Translate(currentDirection * RollSpeed * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ReviveArea")
        {
            thisPlayersReviveSpot = other.transform.position;
        }
    }

	private void Move(float xLeft, float yLeft, float xRight, float yRight)
	{
		//Setting the float of the animation in the beginning 
		anim.SetFloat("velX", xLeft);
		anim.SetFloat("velZ", yLeft);

		//Creating two new vectors, a move and look vector
		Vector2 moveVector = new Vector2(xLeft, yLeft);
		Vector2 lookVector = new Vector2(playerDirection.x, playerDirection.z);

		//Creating a value that is based between two vectors
		float dotMovement = Vector2.Dot(moveVector, lookVector);
	    
		float lookVelocity = Mathf.Sqrt(Vector2.SqrMagnitude(lookVector));
		float moveVelocity = Mathf.Sqrt(Vector2.SqrMagnitude(moveVector));

		if (lookVelocity > 0 && moveVelocity > 0)
		{
			//Both sticks are moving
			anim.SetFloat("dotMovement", dotMovement);
			anim.SetFloat("lookVelocity", lookVelocity);
		} else if (lookVelocity > 0f)
		{
			//Idle but shooting
			anim.SetFloat("dotMovement", 0f);
			anim.SetFloat("lookVelocity", -1f);
		} else if (moveVelocity > 0f)
		{
			//moving but not shooting
			lookVector = playerLookDirection;
			lookVector.Normalize();
			dotMovement = Vector2.Dot(moveVector, lookVector);
			anim.SetFloat("dotMovement", dotMovement);
			anim.SetFloat("lookVelocity", 1f);
		}
		else
		{
			anim.SetFloat("dotMovement", 0f);
			anim.SetFloat("lookVelocity", -1f);
		}
	}
}
