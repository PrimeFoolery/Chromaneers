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

    private EnemyManager listManager;
    public ColourPicker colourPicker;

    [Header("Script References")]
    public CharacterOneGunController coopCharacterControllerOne;

    public Color redColor = Color.red;
    public Color orangeColor = new Color(1, 0.75f, 0, 1);
    public Color yellowColor = Color.yellow;
    public Color greenColor = Color.green;
    public Color blueColor = Color.blue;
    public Color purpleColor = new Color(0.6f, 0, 1, 1);

    public string colourPlayerIsStandingOn;

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

	void Start () {
        //Getting the Rigidbody from the object attached to this script
        myRB = GetComponent<Rigidbody>();
        //Getting the mainCamera from the current scene
	    colourPicker = GameObject.FindGameObjectWithTag("ColourPicker").GetComponent<ColourPicker>();
	    listManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraScript = mainCamera.GetComponent<CameraScript>();
	    brush.Color = blueColor;
	}
	
	void Update () {
	    XDistBetweenPlayerAndAveragePlayerPos = Mathf.Abs(transform.position.x - mainCameraScript.averagePos.x);//CALCULATES DISTANCE ON X AXIS BETWEEN THIS PLAYER AND THE AVERAGE PLAYER POS THE CAMERA IS POINTED AT
        ZDistBetweenPlayerAndAveragePlayerPos = Mathf.Abs(transform.position.z - mainCameraScript.averagePos.z);//CALCULATES DISTANCE ON Z AXIS BETWEEN THIS PLAYER AND THE AVERAGE PLAYER POS THE CAMERA IS POINTED AT
        if (XDistBetweenPlayerAndAveragePlayerPos>=19.75f)
        {
            cameraBorderPushbackSpeed += 0.01f;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3 (mainCameraScript.averagePos.x, transform.position.y, transform.position.z), cameraBorderPushbackSpeed);//PUSHES THE PLAYER BACK IF THE GO TOO FAR
        }
        if (ZDistBetweenPlayerAndAveragePlayerPos >= 11.15f)
        {
            cameraBorderPushbackSpeed += 0.01f;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y, mainCameraScript.averagePos.z), cameraBorderPushbackSpeed);//PUSHES THE PLAYER BACK IF THE GO TOO FAR
        }
        if (XDistBetweenPlayerAndAveragePlayerPos < 19.75f&&ZDistBetweenPlayerAndAveragePlayerPos<11.15f)
        {
            cameraBorderPushbackSpeed = 0f;
        }

        //Checking whether an Xbox or Playstation controller is being used
        if (!usingXboxController) {
	        //Making a vector3 to store the characters inputs
	        moveInput = new Vector3(Input.GetAxisRaw("Joystick1LHorizontal"), 0f, Input.GetAxisRaw("Joystick1LVertical"));
            if (!isShooting) {
                //Multiply the moveInput by the moveVelocity to give it speed whilst walking
				if(moveInput!= new Vector3(0,0,0)){
					if(moveSpeed<=5f){
						moveSpeed = moveSpeed * movingAcceleration;
					}
					if(moveSpeed>=5f){
						moveSpeed = 5f;
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
                if (colourPlayerIsStandingOn == "red")
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
                if (Input.GetKey(KeyCode.Joystick1Button7)) {
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
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            //Debug.DrawRay(transform.position,Vector3.down, Color.yellow,20f);
            if (Physics.Raycast(ray, out hit, 20f))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider)
                {



                    if (Input.GetKey(KeyCode.Joystick1Button6))
                    {
                        bool success = true;
                        var paintObject = hit.transform.GetComponent<InkCanvas>();
                        if (paintObject != null)
                        {
                            if (useMethodType == UseMethodType.RaycastHitInfo)
                            {
                                brush.Scale = 0.068f;
                                GameObject paintProjectionObject = Instantiate(paintProjector, transform.position, Quaternion.identity);
                                paintProjectionObject.GetComponent<paintProjectorController>().PaintStart(hit, paintObject, brush);
                                listManager.projectorsList.Add(paintProjectionObject);
                                paintProjectionObject = null;
                                //success = erase ? paintObject.Erase(brush, hit) : paintObject.Paint(brush, hit);
                            }
                        }
                        if (brush.Color == redColor)
                        {
                            colourPlayerIsStandingOn = "red";
                        }
                        if (brush.Color == orangeColor)
                        {
                            colourPlayerIsStandingOn = "orange";
                        }
                        if (brush.Color == yellowColor)
                        {
                            colourPlayerIsStandingOn = "yellow";
                        }
                        if (brush.Color == greenColor)
                        {
                            colourPlayerIsStandingOn = "green";
                        }
                        if (brush.Color == blueColor)
                        {
                            colourPlayerIsStandingOn = "blue";
                        }
                        if (brush.Color == purpleColor)
                        {
                            colourPlayerIsStandingOn = "purple";
                        }
                        if (!success)
                        {
                            Debug.Log("Paint not painted correctly");
                        }
                    }
                }
            }
        }

		if (usingXboxController) {
			//Making a vector3 to store the characters inputs
			moveInput = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
		    if (!isShooting) {
		        //Multiply the moveInput by the moveVelocity to give it speed whilst walking
		        
				if(moveInput!= new Vector3(0,0,0)){
					if(moveSpeed<=5f){
						moveSpeed = moveSpeed * movingAcceleration;
					}
					if(moveSpeed>=5f){
						moveSpeed = 5f;
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
		        if (colourPlayerIsStandingOn == "red")
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
		        if (Input.GetButtonDown("Fire1Right")) {
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
		                bool success = true;
		                var paintObject = hit.transform.GetComponent<InkCanvas>();
		                if (paintObject != null)
		                {
		                    if (useMethodType == UseMethodType.RaycastHitInfo)
		                    {
		                        brush.Scale = 0.068f;
		                        GameObject paintProjectionObject = Instantiate(paintProjector, transform.position, Quaternion.identity);
		                        paintProjectionObject.GetComponent<paintProjectorController>().PaintStart(hit, paintObject, brush);
		                        listManager.projectorsList.Add(paintProjectionObject);
		                        paintProjectionObject = null;
		                        //success = erase ? paintObject.Erase(brush, hit) : paintObject.Paint(brush, hit);
		                    }
		                }
		                if (brush.Color == redColor)
		                {
		                    colourPlayerIsStandingOn = "red";
		                }
		                if (brush.Color == orangeColor)
		                {
		                    colourPlayerIsStandingOn = "orange";
		                }
		                if (brush.Color == yellowColor)
		                {
		                    colourPlayerIsStandingOn = "yellow";
		                }
		                if (brush.Color == greenColor)
		                {
		                    colourPlayerIsStandingOn = "green";
		                }
		                if (brush.Color == blueColor)
		                {
		                    colourPlayerIsStandingOn = "blue";
		                }
		                if (brush.Color == purpleColor)
		                {
		                    colourPlayerIsStandingOn = "purple";
		                }
                        if (!success)
		                {
		                    Debug.Log("Paint not painted correctly");
		                }
		            }
		        }
		    }
        }
	    
	    Debug.Log(colourPlayerIsStandingOn);
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
	    if (colourPlayerIsStandingOn == "red")
	    {
	        moveSpeed = -Mathf.Abs(moveSpeed);
	    }
	    if (colourPlayerIsStandingOn == "null")
	    {
	        moveSpeed = Mathf.Abs(moveSpeed);
	    }
    }

    void FixedUpdate () {
        //Set the Rigidbody to retreieve the moveVelocity;
        myRB.velocity = moveVelocity;

    }
}
