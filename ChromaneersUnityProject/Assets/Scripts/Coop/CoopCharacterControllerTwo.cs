using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Es.InkPainter;

public class CoopCharacterControllerTwo : MonoBehaviour {

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

    public bool isCameraZoomedOut = false;

    private EnemyManager listManager;
    public ColourPicker colourPicker;
    public ParticleSystem walkingPuff;

    public bool currentlyDodging = false;
    private Vector3 dodgeDirection;
    private float dodgeDuration = 0.325f;
    public float RollSpeed;
    private float dodgeCooldown = 0f;
    public Slider dodgeSlider;
    private bool isFalling = false;

    [Header("Script References")]
    public CharacterTwoGunController coopCharacterControllerTwo;

    private CoopCharacterControllerThree yellowPlayer;
    private CoopCharacterControllerOne bluePlayer;

    private string tagUnderPlayer;
    private List<Vector3> savedPosition = new List<Vector3>();
    private float floorSavingTimer = 1f;

    public Color redColor = Color.red;
    public Color orangeColor = new Color(1, 0.75f, 0, 1);
    public Color yellowColor = Color.yellow;
    public Color greenColor = Color.green;
    public Color blueColor = Color.blue;
    public Color purpleColor = new Color(0.6f, 0, 1, 1);
    private float walkingPuffCooldown = 0.2f;
    public string colourPlayerIsStandingOn;
    private float splatTimer = 0f;
    public GameObject paintBlob;
    public bool canPlayerShoot = true;
	public bool canPlayerMove = true;

    private float specialAttackCooldown = 0f;
    private bool specialAttackOn = false;
    private float specialAttackDuration = 5f;

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

    public AudioClip playerWalking;
    public AudioClip playerDashing;
    AudioSource audio;

    public Vector3 thisPlayersReviveSpot;

    private bool colourBlindModeActive = false;
    public GameObject cbIndicator;
    private GameObject cbCurrentIndicator;

    private GameObject settingsKeeper;
    public int thisPlayersControllerIndex;

    void Start () {
        settingsKeeper = GameObject.FindGameObjectWithTag("Settings");
        if (settingsKeeper.GetComponent<DoNotDestroy>().RedPlayerControllerType == "xbox")
        {
            usingXboxController = true;
        }
        else if (settingsKeeper.GetComponent<DoNotDestroy>().RedPlayerControllerType == "ps4")
        {
            usingXboxController = false;
        }
        thisPlayersControllerIndex = settingsKeeper.GetComponent<DoNotDestroy>().RedPlayerControllerIndex;
        //Getting the Rigidbody from the object attached to this script
        myRB = GetComponent<Rigidbody>();
        yellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer").GetComponent<CoopCharacterControllerThree>();
        bluePlayer = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<CoopCharacterControllerOne>();
        //Getting the mainCamera from the current scene
        colourPicker = GameObject.FindGameObjectWithTag("ColourPicker").GetComponent<ColourPicker>();
        listManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraScript = mainCamera.GetComponent<CameraScript>();
        brush.Color = redColor;
        audio = GetComponent<AudioSource>();

        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>().colourBlindMode == true)
        {
            SpawnColourBlindIndicator();
            colourBlindModeActive = true;
        }
        else
        {
            colourBlindModeActive = false;
        }

        playerLookDirection.x = 0f;
	    playerLookDirection.y = 1f;
    }
	
	void Update () {


	    if (Input.GetKeyUp(KeyCode.F1))
	    {
	        if (colourBlindModeActive == false)
	        {
	            SpawnColourBlindIndicator();
	            colourBlindModeActive = true;
	        }
	        else
	        {
	            Destroy(cbCurrentIndicator);
	            colourBlindModeActive = false;
	        }
	    }
        dodgeSlider.value = (dodgeCooldown);
	    if (thisPlayersControllerIndex==1)
	    {
            //Checking whether an Xbox or Playstation controller is being used
            if (!usingXboxController)
            {
                var xLeft = Input.GetAxis("Joystick1LHorizontal");
                var yLeft = Input.GetAxis("Joystick1LVertical");
                var xRight = Input.GetAxis("Joystick1RHorizontal");
                var yRight = Input.GetAxis("Joystick1RVertical");
                Move(xLeft, yLeft, xRight, yRight);
                //Making a vector3 to store the characters inputs
                if (gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    moveInput = new Vector3(Input.GetAxisRaw("Joystick1LHorizontal"), 0f, Input.GetAxisRaw("Joystick1LVertical"));
                }
                else
                {
                    moveInput = new Vector3(0, 0, 0);
                    moveVelocity = new Vector3(0, 0, 0);
                    audio.Stop();
                }

                if (isCameraZoomedOut == false)
                {
                    if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -35f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -35f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 35f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 35f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -25f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -25f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 25f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 25f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                }
                else
                {

                    if (transform.position.x - mainCameraScript.averagePos.x <= -40f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -50f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -50f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 40f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 50f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 50f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -30f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -40f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -40f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 30f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 40f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 40f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }

                }

                if (canPlayerMove == true)
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
                playerDirection = Vector3.right * Input.GetAxisRaw("Joystick1RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick1RVertical");
                //Checking if the vector3 has got a value inputed
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    Vector3 tempRotationValue = transform.rotation.eulerAngles;
                    tempRotationValue.y = tempRotationValue.y + 17;
                    transform.rotation = Quaternion.Euler(tempRotationValue);
                    playerLookDirection.x = playerDirection.x;
                    playerLookDirection.y = playerDirection.z;
                }
                else
                {
                    Vector3 playerDirectionAlt = Vector3.right * Input.GetAxisRaw("Joystick1LHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick1LVertical");
                    if (playerDirectionAlt.sqrMagnitude > 0.0f)
                    {
                        transform.rotation = Quaternion.LookRotation(playerDirectionAlt, Vector3.up);
                        Vector3 tempRotationValue = transform.rotation.eulerAngles;
                        tempRotationValue.y = tempRotationValue.y + 17;
                        transform.rotation = Quaternion.Euler(tempRotationValue);
                        playerLookDirection.x = playerDirectionAlt.x;
                        playerLookDirection.y = playerDirectionAlt.z;
                    }
                }

                //Stops people from spam clicking to shoot faster
                timeToShoot -= Time.deltaTime;
                if (timeToShoot <= 0)
                {
                    //Shooting the bullet
                    if (playerDirection != new Vector3(0, 0, 0) && canPlayerShoot == true)
                    {
                        coopCharacterControllerTwo.isFiring = true;
                        isShooting = true;
                        timeToShoot = 0.5f;
                    }
                }
                //Not shootings the bullet
                if (playerDirection == new Vector3(0, 0, 0))
                {
                    coopCharacterControllerTwo.isFiring = false;
                    isShooting = false;
                }
                if (Input.GetKeyDown(KeyCode.Joystick1Button5) && currentlyDodging == false && dodgeCooldown <= 0f && gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
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
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().canBeDamaged = true;
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().ChangeToMatOne();
                    currentlyDodging = false;
                    canPlayerMove = true;
                    canPlayerShoot = true;
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



                    if (Input.GetKey(KeyCode.Joystick2Button6))
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
                                    tempPaintSplot.GetComponent<Renderer>().material.color = redColor;
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
                if (Input.GetKey(KeyCode.Joystick1Button4) && specialAttackCooldown <= 0 && specialAttackOn == false)
                {
                    //Debug.Log("USE red SPECIAL ATTACK");
                    //specialAttackDuration = 5f;
                    //specialAttackOn = true;

                }

                if (specialAttackOn == true && specialAttackDuration >= 0)
                {
                    Debug.Log("red Special Attack being used");
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

            if (usingXboxController)
            {
                var xLeft = Input.GetAxis("XboxJoystick1LHorizontal");
                var yLeft = Input.GetAxis("XboxJoystick1LVertical");
                var xRight = Input.GetAxis("XboxJoystick1RHorizontal");
                var yRight = Input.GetAxis("XboxJoystick1RVertical");
                Move(xLeft, yLeft, xRight, yRight);
                //Making a vector3 to store the characters inputs
                if (gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
                }
                else
                {
                    moveInput = new Vector3(0, 0, 0);
                    moveVelocity = new Vector3(0, 0, 0);
                    audio.Stop();
                }
                if (isCameraZoomedOut == false)
                {
                    if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -35f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -35f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 35f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 35f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -25f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -25f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 25f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 25f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                }
                else
                {

                    if (transform.position.x - mainCameraScript.averagePos.x <= -40f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -50f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -50f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 40f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 50f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 50f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -30f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -40f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -40f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 30f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 40f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 40f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }

                }
                if (canPlayerMove == true)
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
                playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick1RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick1RVertical");
                //Checking if the vector3 has got a value inputed
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    Vector3 tempRotationValue = transform.rotation.eulerAngles;
                    tempRotationValue.y = tempRotationValue.y + 17;
                    transform.rotation = Quaternion.Euler(tempRotationValue);
                    playerLookDirection.x = playerDirection.x;
                    playerLookDirection.y = playerDirection.z;
                }
                //Setting it where player can rotate whilst moving but not shoot
                else
                {
                    Vector3 playerDirectionAlt = Vector3.right * Input.GetAxisRaw("XboxJoystick1LHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick1LVertical");
                    if (playerDirectionAlt.sqrMagnitude > 0.0f)
                    {
                        transform.rotation = Quaternion.LookRotation(playerDirectionAlt, Vector3.up);
                        Vector3 tempRotationValue = transform.rotation.eulerAngles;
                        tempRotationValue.y = tempRotationValue.y + 17;
                        transform.rotation = Quaternion.Euler(tempRotationValue);
                        playerLookDirection.x = playerDirectionAlt.x;
                        playerLookDirection.y = playerDirectionAlt.z;
                    }
                }

                //Stops people from spam clicking to shoot faster
                timeToShoot -= Time.deltaTime;
                if (timeToShoot <= 0)
                {
                    //Shooting the bullet
                    if (playerDirection != new Vector3(0, 0, 0) && canPlayerShoot == true)
                    {
                        coopCharacterControllerTwo.isFiring = true;
                        isShooting = true;
                        timeToShoot = 0.5f;
                    }
                }
                //Not shootings the bullet
                if (playerDirection == new Vector3(0, 0, 0))
                {
                    coopCharacterControllerTwo.isFiring = false;
                    isShooting = false;
                }
                if (Input.GetButtonDown("Roll1") && currentlyDodging == false && dodgeCooldown <= 0f && gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
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
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().canBeDamaged = true;
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().ChangeToMatOne();
                    currentlyDodging = false;
                    canPlayerMove = true;
                    canPlayerShoot = true;
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



		            if (Input.GetButton("Fire2Left"))
		            {
		                if (splatTimer <= 0f)
		                {
		                    bool success = true;
		                    var paintObject = hit.transform.GetComponent<InkCanvas>();
		                    if (paintObject != null)
		                    {
		                        if (useMethodType == UseMethodType.RaycastHitInfo)
		                        {
		                            colourPicker.currentColourHighligted = "Red";
                                    GameObject tempPaintSplot = Instantiate(paintBlob, transform.position, Quaternion.identity);
		                            tempPaintSplot.GetComponent<paintSplatBlob>().SetPaintVariables(brush, hit, paintObject);
		                            tempPaintSplot.GetComponent<Renderer>().material.color = redColor;
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
                if (Input.GetButton("Fire1Left") && specialAttackCooldown <= 0 && specialAttackOn == false)
                {
                    // Debug.Log("USE Yellows SPECIAL ATTACK");
                    // specialAttackDuration = 5f;
                    // specialAttackOn = true;

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
        }
        else if (thisPlayersControllerIndex == 2)
	    {
            //Checking whether an Xbox or Playstation controller is being used
            if (!usingXboxController)
            {
                var xLeft = Input.GetAxis("Joystick2LHorizontal");
                var yLeft = Input.GetAxis("Joystick2LVertical");
                var xRight = Input.GetAxis("Joystick2RHorizontal");
                var yRight = Input.GetAxis("Joystick2RVertical");
                Move(xLeft, yLeft, xRight, yRight);
                //Making a vector3 to store the characters inputs
                if (gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    moveInput = new Vector3(Input.GetAxisRaw("Joystick2LHorizontal"), 0f, Input.GetAxisRaw("Joystick2LVertical"));
                }
                else
                {
                    moveInput = new Vector3(0, 0, 0);
                    moveVelocity = new Vector3(0, 0, 0);
                    audio.Stop();
                }

                if (isCameraZoomedOut == false)
                {
                    if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -35f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -35f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 35f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 35f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -25f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -25f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 25f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 25f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                }
                else
                {

                    if (transform.position.x - mainCameraScript.averagePos.x <= -40f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -50f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -50f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 40f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 50f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 50f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -30f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -40f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -40f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 30f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 40f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 40f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }

                }

                if (canPlayerMove == true)
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
                playerDirection = Vector3.right * Input.GetAxisRaw("Joystick2RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick2RVertical");
                //Checking if the vector3 has got a value inputed
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    Vector3 tempRotationValue = transform.rotation.eulerAngles;
                    tempRotationValue.y = tempRotationValue.y + 17;
                    transform.rotation = Quaternion.Euler(tempRotationValue);
                    playerLookDirection.x = playerDirection.x;
                    playerLookDirection.y = playerDirection.z;
                }
                else
                {
                    Vector3 playerDirectionAlt = Vector3.right * Input.GetAxisRaw("Joystick2LHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick2LVertical");
                    if (playerDirectionAlt.sqrMagnitude > 0.0f)
                    {
                        transform.rotation = Quaternion.LookRotation(playerDirectionAlt, Vector3.up);
                        Vector3 tempRotationValue = transform.rotation.eulerAngles;
                        tempRotationValue.y = tempRotationValue.y + 17;
                        transform.rotation = Quaternion.Euler(tempRotationValue);
                        playerLookDirection.x = playerDirectionAlt.x;
                        playerLookDirection.y = playerDirectionAlt.z;
                    }
                }

                //Stops people from spam clicking to shoot faster
                timeToShoot -= Time.deltaTime;
                if (timeToShoot <= 0)
                {
                    //Shooting the bullet
                    if (playerDirection != new Vector3(0, 0, 0) && canPlayerShoot == true)
                    {
                        coopCharacterControllerTwo.isFiring = true;
                        isShooting = true;
                        timeToShoot = 0.5f;
                    }
                }
                //Not shootings the bullet
                if (playerDirection == new Vector3(0, 0, 0))
                {
                    coopCharacterControllerTwo.isFiring = false;
                    isShooting = false;
                }
                if (Input.GetKeyDown(KeyCode.Joystick2Button5) && currentlyDodging == false && dodgeCooldown <= 0f && gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
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
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().canBeDamaged = true;
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().ChangeToMatOne();
                    currentlyDodging = false;
                    canPlayerMove = true;
                    canPlayerShoot = true;
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



                    if (Input.GetKey(KeyCode.Joystick2Button6))
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
                                    tempPaintSplot.GetComponent<Renderer>().material.color = redColor;
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
                if (Input.GetKey(KeyCode.Joystick1Button4) && specialAttackCooldown <= 0 && specialAttackOn == false)
                {
                    //Debug.Log("USE red SPECIAL ATTACK");
                    //specialAttackDuration = 5f;
                    //specialAttackOn = true;

                }

                if (specialAttackOn == true && specialAttackDuration >= 0)
                {
                    Debug.Log("red Special Attack being used");
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

            if (usingXboxController)
            {
                var xLeft = Input.GetAxis("XboxJoystick2LHorizontal");
                var yLeft = Input.GetAxis("XboxJoystick2LVertical");
                var xRight = Input.GetAxis("XboxJoystick2RHorizontal");
                var yRight = Input.GetAxis("XboxJoystick2RVertical");
                Move(xLeft, yLeft, xRight, yRight);
                //Making a vector3 to store the characters inputs
                if (gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick2LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick2LVertical"));
                }
                else
                {
                    moveInput = new Vector3(0, 0, 0);
                    moveVelocity = new Vector3(0, 0, 0);
                    audio.Stop();
                }
                if (isCameraZoomedOut == false)
                {
                    if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -35f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -35f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 35f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 35f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -25f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -25f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 25f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 25f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                }
                else
                {

                    if (transform.position.x - mainCameraScript.averagePos.x <= -40f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -50f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -50f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 40f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 50f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 50f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -30f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -40f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -40f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 30f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 40f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 40f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }

                }
                if (canPlayerMove == true)
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
                playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick2RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick2RVertical");
                //Checking if the vector3 has got a value inputed
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    Vector3 tempRotationValue = transform.rotation.eulerAngles;
                    tempRotationValue.y = tempRotationValue.y + 17;
                    transform.rotation = Quaternion.Euler(tempRotationValue);
                    playerLookDirection.x = playerDirection.x;
                    playerLookDirection.y = playerDirection.z;
                }
                //Setting it where player can rotate whilst moving but not shoot
                else
                {
                    Vector3 playerDirectionAlt = Vector3.right * Input.GetAxisRaw("XboxJoystick2LHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick2LVertical");
                    if (playerDirectionAlt.sqrMagnitude > 0.0f)
                    {
                        transform.rotation = Quaternion.LookRotation(playerDirectionAlt, Vector3.up);
                        Vector3 tempRotationValue = transform.rotation.eulerAngles;
                        tempRotationValue.y = tempRotationValue.y + 17;
                        transform.rotation = Quaternion.Euler(tempRotationValue);
                        playerLookDirection.x = playerDirectionAlt.x;
                        playerLookDirection.y = playerDirectionAlt.z;
                    }
                }

                //Stops people from spam clicking to shoot faster
                timeToShoot -= Time.deltaTime;
                if (timeToShoot <= 0)
                {
                    //Shooting the bullet
                    if (playerDirection != new Vector3(0, 0, 0) && canPlayerShoot == true)
                    {
                        coopCharacterControllerTwo.isFiring = true;
                        isShooting = true;
                        timeToShoot = 0.5f;
                    }
                }
                //Not shootings the bullet
                if (playerDirection == new Vector3(0, 0, 0))
                {
                    coopCharacterControllerTwo.isFiring = false;
                    isShooting = false;
                }
                if (Input.GetButtonDown("Roll2") && currentlyDodging == false && dodgeCooldown <= 0f && gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
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
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().canBeDamaged = true;
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().ChangeToMatOne();
                    currentlyDodging = false;
                    canPlayerMove = true;
                    canPlayerShoot = true;
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



		            if (Input.GetButton("Fire2Left"))
		            {
		                if (splatTimer <= 0f)
		                {
		                    bool success = true;
		                    var paintObject = hit.transform.GetComponent<InkCanvas>();
		                    if (paintObject != null)
		                    {
		                        if (useMethodType == UseMethodType.RaycastHitInfo)
		                        {
		                            colourPicker.currentColourHighligted = "Red";
                                    GameObject tempPaintSplot = Instantiate(paintBlob, transform.position, Quaternion.identity);
		                            tempPaintSplot.GetComponent<paintSplatBlob>().SetPaintVariables(brush, hit, paintObject);
		                            tempPaintSplot.GetComponent<Renderer>().material.color = redColor;
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
                if (Input.GetButton("Fire2Left") && specialAttackCooldown <= 0 && specialAttackOn == false)
                {
                    // Debug.Log("USE Yellows SPECIAL ATTACK");
                    // specialAttackDuration = 5f;
                    // specialAttackOn = true;

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
        }
        else if (thisPlayersControllerIndex==3)
	    {
            //Checking whether an Xbox or Playstation controller is being used
            if (!usingXboxController)
            {
                var xLeft = Input.GetAxis("Joystick3LHorizontal");
                var yLeft = Input.GetAxis("Joystick3LVertical");
                var xRight = Input.GetAxis("Joystick3RHorizontal");
                var yRight = Input.GetAxis("Joystick3RVertical");
                Move(xLeft, yLeft, xRight, yRight);
                //Making a vector3 to store the characters inputs
                if (gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    moveInput = new Vector3(Input.GetAxisRaw("Joystick3LHorizontal"), 0f, Input.GetAxisRaw("Joystick3LVertical"));
                }
                else
                {
                    moveInput = new Vector3(0, 0, 0);
                    moveVelocity = new Vector3(0, 0, 0);
                    audio.Stop();
                }

                if (isCameraZoomedOut == false)
                {
                    if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -35f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -35f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 35f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 35f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -25f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -25f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 25f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 25f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                }
                else
                {

                    if (transform.position.x - mainCameraScript.averagePos.x <= -40f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -50f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -50f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 40f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 50f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 50f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -30f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -40f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -40f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 30f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 40f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 40f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }

                }

                if (canPlayerMove == true)
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
                playerDirection = Vector3.right * Input.GetAxisRaw("Joystick3RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick3RVertical");
                //Checking if the vector3 has got a value inputed
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    Vector3 tempRotationValue = transform.rotation.eulerAngles;
                    tempRotationValue.y = tempRotationValue.y + 17;
                    transform.rotation = Quaternion.Euler(tempRotationValue);
                    playerLookDirection.x = playerDirection.x;
                    playerLookDirection.y = playerDirection.z;
                }
                else
                {
                    Vector3 playerDirectionAlt = Vector3.right * Input.GetAxisRaw("Joystick3LHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick3LVertical");
                    if (playerDirectionAlt.sqrMagnitude > 0.0f)
                    {
                        transform.rotation = Quaternion.LookRotation(playerDirectionAlt, Vector3.up);
                        Vector3 tempRotationValue = transform.rotation.eulerAngles;
                        tempRotationValue.y = tempRotationValue.y + 17;
                        transform.rotation = Quaternion.Euler(tempRotationValue);
                        playerLookDirection.x = playerDirectionAlt.x;
                        playerLookDirection.y = playerDirectionAlt.z;
                    }
                }

                //Stops people from spam clicking to shoot faster
                timeToShoot -= Time.deltaTime;
                if (timeToShoot <= 0)
                {
                    //Shooting the bullet
                    if (playerDirection != new Vector3(0, 0, 0) && canPlayerShoot == true)
                    {
                        coopCharacterControllerTwo.isFiring = true;
                        isShooting = true;
                        timeToShoot = 0.5f;
                    }
                }
                //Not shootings the bullet
                if (playerDirection == new Vector3(0, 0, 0))
                {
                    coopCharacterControllerTwo.isFiring = false;
                    isShooting = false;
                }
                if (Input.GetKeyDown(KeyCode.Joystick3Button5) && currentlyDodging == false && dodgeCooldown <= 0f && gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
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
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().canBeDamaged = true;
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().ChangeToMatOne();
                    currentlyDodging = false;
                    canPlayerMove = true;
                    canPlayerShoot = true;
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



                    if (Input.GetKey(KeyCode.Joystick2Button6))
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
                                    tempPaintSplot.GetComponent<Renderer>().material.color = redColor;
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
                if (Input.GetKey(KeyCode.Joystick1Button4) && specialAttackCooldown <= 0 && specialAttackOn == false)
                {
                    //Debug.Log("USE red SPECIAL ATTACK");
                    //specialAttackDuration = 5f;
                    //specialAttackOn = true;

                }

                if (specialAttackOn == true && specialAttackDuration >= 0)
                {
                    Debug.Log("red Special Attack being used");
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

            if (usingXboxController)
            {
                var xLeft = Input.GetAxis("XboxJoystick3LHorizontal");
                var yLeft = Input.GetAxis("XboxJoystick3LVertical");
                var xRight = Input.GetAxis("XboxJoystick3RHorizontal");
                var yRight = Input.GetAxis("XboxJoystick3RVertical");
                Move(xLeft, yLeft, xRight, yRight);
                //Making a vector3 to store the characters inputs
                if (gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
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
                    if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -35f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -35f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 35f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 35f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -25f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -25f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 25f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 25f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                }
                else
                {

                    if (transform.position.x - mainCameraScript.averagePos.x <= -40f || transform.position.x - bluePlayer.gameObject.transform.position.x <= -50f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -50f)
                    {
                        if (moveInput.x <= 0)
                        {
                            moveInput.x = 0;
                        }
                    }
                    else if (transform.position.x - mainCameraScript.averagePos.x >= 40f || transform.position.x - bluePlayer.gameObject.transform.position.x >= 50f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 50f)
                    {
                        if (moveInput.x >= 0)
                        {
                            moveInput.x = 0;
                        }
                    }

                    if (transform.position.z - mainCameraScript.averagePos.z <= -30f || transform.position.z - bluePlayer.gameObject.transform.position.z <= -40f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -40f)
                    {
                        if (moveInput.z <= 0)
                        {
                            moveInput.z = 0;
                        }
                    }
                    else if (transform.position.z - mainCameraScript.averagePos.z >= 30f || transform.position.z - bluePlayer.gameObject.transform.position.z >= 40f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 40f)
                    {
                        if (moveInput.z >= 0)
                        {
                            moveInput.z = 0;
                        }
                    }

                }
                if (canPlayerMove == true)
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
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    Vector3 tempRotationValue = transform.rotation.eulerAngles;
                    tempRotationValue.y = tempRotationValue.y + 17;
                    transform.rotation = Quaternion.Euler(tempRotationValue);
                    playerLookDirection.x = playerDirection.x;
                    playerLookDirection.y = playerDirection.z;
                }
                //Setting it where player can rotate whilst moving but not shoot
                else
                {
                    Vector3 playerDirectionAlt = Vector3.right * Input.GetAxisRaw("XboxJoystick3LHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick3LVertical");
                    if (playerDirectionAlt.sqrMagnitude > 0.0f)
                    {
                        transform.rotation = Quaternion.LookRotation(playerDirectionAlt, Vector3.up);
                        Vector3 tempRotationValue = transform.rotation.eulerAngles;
                        tempRotationValue.y = tempRotationValue.y + 17;
                        transform.rotation = Quaternion.Euler(tempRotationValue);
                        playerLookDirection.x = playerDirectionAlt.x;
                        playerLookDirection.y = playerDirectionAlt.z;
                    }
                }

                //Stops people from spam clicking to shoot faster
                timeToShoot -= Time.deltaTime;
                if (timeToShoot <= 0)
                {
                    //Shooting the bullet
                    if (playerDirection != new Vector3(0, 0, 0) && canPlayerShoot == true)
                    {
                        coopCharacterControllerTwo.isFiring = true;
                        isShooting = true;
                        timeToShoot = 0.5f;
                    }
                }
                //Not shootings the bullet
                if (playerDirection == new Vector3(0, 0, 0))
                {
                    coopCharacterControllerTwo.isFiring = false;
                    isShooting = false;
                }
                if (Input.GetButtonDown("Roll3") && currentlyDodging == false && dodgeCooldown <= 0f && gameObject.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
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
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().canBeDamaged = true;
                    gameObject.GetComponent<CoopCharacterHealthControllerTwo>().ChangeToMatOne();
                    currentlyDodging = false;
                    canPlayerMove = true;
                    canPlayerShoot = true;
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



		            if (Input.GetButton("Fire2Left"))
		            {
		                if (splatTimer <= 0f)
		                {
		                    bool success = true;
		                    var paintObject = hit.transform.GetComponent<InkCanvas>();
		                    if (paintObject != null)
		                    {
		                        if (useMethodType == UseMethodType.RaycastHitInfo)
		                        {
		                            colourPicker.currentColourHighligted = "Red";
                                    GameObject tempPaintSplot = Instantiate(paintBlob, transform.position, Quaternion.identity);
		                            tempPaintSplot.GetComponent<paintSplatBlob>().SetPaintVariables(brush, hit, paintObject);
		                            tempPaintSplot.GetComponent<Renderer>().material.color = redColor;
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
                    // Debug.Log("USE Yellows SPECIAL ATTACK");
                    // specialAttackDuration = 5f;
                    // specialAttackOn = true;

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

	    if (isFalling ==true)
	    {
	        transform.position = Vector3.MoveTowards(transform.position,
	            new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z), 5f * Time.deltaTime);
        }
	    if (tagUnderPlayer == "FallingArea" && currentlyDodging == false)
	    {
	        isFalling = true;
	    }
	    if (transform.position.y <= -5f)
	    {
	        gameObject.GetComponent<CoopCharacterHealthControllerTwo>().Die();
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
			
            gameObject.GetComponent<CoopCharacterHealthControllerTwo>().canBeDamaged = false;
            gameObject.GetComponent<CoopCharacterHealthControllerTwo>().InvTimer=1f;
            //moveSpeed = 0;
            canPlayerShoot = false;
			canPlayerMove = false;
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
            anim.SetInteger("whatStateAmI", 1);
        }
	    else
	    {
		    anim.SetFloat("dotMovement", 0f);
		    anim.SetFloat("lookVelocity", -1f);
            anim.SetInteger("whatStateAmI", 0);
        }
    }
    private void SpawnColourBlindIndicator()
    {
        cbCurrentIndicator = Instantiate(cbIndicator,
            new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.Euler(90, 0, 0));
        cbCurrentIndicator.transform.SetParent(transform);
    }
}
