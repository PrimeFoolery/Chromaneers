using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

public class CoopCharacterControllerOne : MonoBehaviour {

    [Header("Player Variables")]
    public float moveSpeed;
    public float shootingSpeed;
    public float timeToShoot;
    [Space(10)]
    public bool usingXboxController;
    public bool isShooting;
    public GameObject paintProjector;

    private string tagUnderPlayer;
    private Vector3 savedPosition;

    private bool currentlyDodging = false;
    private Vector3 dodgeDirection;
    private float dodgeDuration = 0.15f;
    public float RollSpeed;
    private float dodgeCooldown = 0f;

    private EnemyManager listManager;
    public ColourPicker colourPicker;
    public bool canPlayerShoot = true;

    [Header("Script References")]
    public CharacterOneGunController coopCharacterControllerOne;

    private CoopCharacterControllerTwo redPlayer;
    private CoopCharacterControllerThree yellowPlayer;

    public Color redColor = Color.red;
    public Color orangeColor = new Color(1, 0.75f, 0, 1);
    public Color yellowColor = Color.yellow;
    public Color greenColor = Color.green;
    public Color blueColor = Color.blue;
    public Color purpleColor = new Color(0.6f, 0, 1, 1);

    public string colourPlayerIsStandingOn;

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
        //Getting the mainCamera from the current scene
	    yellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer").GetComponent<CoopCharacterControllerThree>(); 
	    redPlayer = GameObject.FindGameObjectWithTag("RedPlayer").GetComponent<CoopCharacterControllerTwo>();
        colourPicker = GameObject.FindGameObjectWithTag("ColourPicker").GetComponent<ColourPicker>();
	    listManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraScript = mainCamera.GetComponent<CameraScript>();
	    brush.Color = blueColor;
	}
	
	void Update () {
        

        //Checking whether an Xbox or Playstation controller is being used
        if (!usingXboxController) {
            
            //Making a vector3 to store the characters inputs
            moveInput = new Vector3(Input.GetAxisRaw("Joystick1LHorizontal"), 0f, Input.GetAxisRaw("Joystick1LVertical"));
            if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - redPlayer.gameObject.transform.position.x <= -35f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -35f)
            {
                if (moveInput.x <= 0)
                {
                    moveInput.x = 0;
                }
            }
            else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - redPlayer.gameObject.transform.position.x >= 35f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 35f)
            {
                if (moveInput.x >= 0)
                {
                    moveInput.x = 0;
                }
            }

            if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - redPlayer.gameObject.transform.position.z <= -25f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -25f)
            {
                if (moveInput.z <= 0)
                {
                    moveInput.z = 0;
                }
            }
            else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - redPlayer.gameObject.transform.position.z >= 25f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 25f)
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
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Joystick1RHorizontal") + Vector3.forward * Input.GetAxisRaw("Joystick1RVertical");
	        //Checking if the vector3 has got a value inputed
	        if (playerDirection.sqrMagnitude > 0.0f) {
	            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
	        }

            //Stops people from spam clicking to shoot faster
            timeToShoot -= Time.deltaTime;
            if (timeToShoot <= 0) {
                //Shooting the bullet
                if (Input.GetKey(KeyCode.Joystick1Button7)&&canPlayerShoot==true) {
                    coopCharacterControllerOne.isFiring = true;
                    isShooting = true;
                    timeToShoot = 0.5f;
                }
            }
	        //Not shootings the bullet
	        if (Input.GetKeyUp(KeyCode.Joystick1Button7)) {
	            coopCharacterControllerOne.isFiring = false;
	            isShooting = false;
	        }

            if (Input.GetKeyDown(KeyCode.Joystick1Button1)&&currentlyDodging==false&&dodgeCooldown<=0f)
            {
                dodgeDirection = moveInput;
                Roll(dodgeDirection);
            }else if (currentlyDodging==true&&dodgeDuration>=0f)
            {
                Roll(dodgeDirection);
                dodgeDuration -= Time.deltaTime;
            }
            if(dodgeDuration<0)
            {
                currentlyDodging = false;
                dodgeDuration = 0.15f;
                dodgeCooldown = 1f;
            }

            if (currentlyDodging==false)
            {
                dodgeCooldown -= Time.deltaTime;
            }
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            //Debug.DrawRay(transform.position,Vector3.down, Color.yellow,20f);
            if (Physics.Raycast(ray, out hit, 20f))
            {
                
				float groundSizeX = hit.collider.gameObject.GetComponent<Renderer>().bounds.size.x;
				float neededBrushSize = 4.8228f * (Mathf.Pow(groundSizeX, -0.982f));
				brush.Scale = neededBrushSize;
                if (hit.collider)
                {



                    if (Input.GetKey(KeyCode.Joystick1Button6))
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
                                    tempPaintSplot.GetComponent<Renderer>().material.color = blueColor;
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
            moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
		    if (transform.position.x - mainCameraScript.averagePos.x <= -25f || transform.position.x - redPlayer.gameObject.transform.position.x <= -35f || transform.position.x - yellowPlayer.gameObject.transform.position.x <= -35f)
		    {
		        if (moveInput.x <= 0)
		        {
		            moveInput.x = 0;
		        }
		    }
		    else if (transform.position.x - mainCameraScript.averagePos.x >= 25f || transform.position.x - redPlayer.gameObject.transform.position.x >= 35f || transform.position.x - yellowPlayer.gameObject.transform.position.x >= 35f)
		    {
		        if (moveInput.x >= 0)
		        {
		            moveInput.x = 0;
		        }
		    }

		    if (transform.position.z - mainCameraScript.averagePos.z <= -15f || transform.position.z - redPlayer.gameObject.transform.position.z <= -25f || transform.position.z - yellowPlayer.gameObject.transform.position.z <= -25f)
		    {
		        if (moveInput.z <= 0)
		        {
		            moveInput.z = 0;
		        }
		    }
		    else if (transform.position.z - mainCameraScript.averagePos.z >= 15f || transform.position.z - redPlayer.gameObject.transform.position.z >= 25f || transform.position.z - yellowPlayer.gameObject.transform.position.z >= 25f)
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
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("XboxJoystick1RHorizontal") + Vector3.forward * Input.GetAxisRaw("XboxJoystick1RVertical");
			//Checking if the vector3 has got a value inputed
			if (playerDirection.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
			}

            //Stops people from spam clicking to shoot faster
		    timeToShoot -= Time.deltaTime;
		    if (timeToShoot <= 0) {
		        //Shooting the bullet
		        if (Input.GetButtonDown("Fire1Right")&&canPlayerShoot==true) {
                    //Debug.Log("this is happening");
		            coopCharacterControllerOne.isFiring = true;
		            isShooting = true;
		            timeToShoot = 0.5f;
                }
		    }
		    //Not shootings the bullet
			if (Input.GetButtonUp("Fire1Right")) {
				coopCharacterControllerOne.isFiring = false;
			    isShooting = false;
			}
		    if (Input.GetButtonDown("Roll1") && currentlyDodging == false && dodgeCooldown <= 0f)
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
		        currentlyDodging = false;
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
                    



		            if (Input.GetButton("Fire1Left"))
		            {
		                if (splatTimer <= 0f)
		                {
		                    bool success = true;
		                    var paintObject = hit.transform.GetComponent<InkCanvas>();
		                    if (paintObject != null)
		                    {
		                        if (useMethodType == UseMethodType.RaycastHitInfo)
		                        {
		                            colourPicker.currentColourHighligted = "Blue";
		                            GameObject tempPaintSplot = Instantiate(paintBlob, transform.position, Quaternion.identity);
		                            tempPaintSplot.GetComponent<paintSplatBlob>().SetPaintVariables(brush, hit, paintObject);
		                            tempPaintSplot.GetComponent<Renderer>().material.color = blueColor;
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
	            gameObject.GetComponent<CoopCharacterHealthControllerOne>().GetHit();
	            poisonTimer = 3f;
	        }
	    }

	    RaycastHit floorHit;
	    Ray floorRay = new Ray(transform.position, Vector3.down);
	    if(Physics.Raycast(floorRay, out floorHit, 20f))
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
	    if (tagUnderPlayer!="Floor")
	    {
	        transform.position = Vector3.MoveTowards(transform.position,
	            new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z), 0.2f);
	    }

	    if (transform.position.y<=-5f)
	    {
	        gameObject.GetComponent<CoopCharacterHealthControllerOne>().Die();
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
        if (currentDirection!= new  Vector3(0,0,0))
        {
            moveSpeed = 0;
            canPlayerShoot = false;
            currentlyDodging = true;
            transform.Translate(currentDirection);
            moveVelocity = currentDirection * RollSpeed;
        }
    }
}
