using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;

public class CameraScript : MonoBehaviour
{

    public enum GameState { SinglePlayer, Multiplayer };
    public GameState currentGameState = GameState.Multiplayer;
    private ColourSelectManager gameManager;

    private enum CameraState
    {
        TopDown,
        SixtyDegrees,
        FiftyDegrees,
        SixtyDegreesSlanted,
        FiftyDegreesSlanted
    };

    private CameraState currentCameraState = CameraState.SixtyDegreesSlanted;

    private float cameraMoveSpeed =3f;//SPEED THAT CAMERA MOVES TOWARDS TARGET POSITION
    private Vector3 targetCameraPosition;// THE POSITION OF THE CAMERA
    private Vector3 targetCameraRotation;
    private Camera cameraComponent;

    //SINGLEPLAYER VARIABLES
    private GameObject SPPlayer; //PLAYER IN SINGLEPLAYER
    private Vector3 rawMousePos;
    private Vector3 normMousePos;


    //MULTIPLAYER VARIABLES
    private GameObject CoopRedPlayer; //PLAYERS IN CO-OP
    private GameObject CoopBluePlayer;
    private GameObject CoopYellowPlayer;

    public Vector3 averagePos;
    private float sizeNeeded;
    private float minimumSize = 10f;
    private float maximumSize = 11f;
    private float screenEdgeBuffer = 4f;
    private float aspectRatio;
    private float zoomSpeed = 4f;
    private float dampTime = 2f;
    private float rotationTime = 0.35f;

    //CAMERA SHAKE VARIABLES
    public float shakeDuration = 0f;
    private float shakeAmount = 0.1f;
    private float decreaseFactor = 1f;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>();
        if (gameManager.isItSingleplayer == true)
        {
            currentGameState = GameState.SinglePlayer;
        }
        if (gameManager.isItSingleplayer == false)
        {
            currentGameState = GameState.Multiplayer;
        }
        if (currentGameState == GameState.SinglePlayer)//IF THE GAME IS IN SINGLEPLAYER
        {
            SPPlayer = GameObject.FindGameObjectWithTag("Player"); //FINDS AND ASSIGNS THE SINGLE PLAYER TO THE CAMERA
        }
        if (currentGameState == GameState.Multiplayer)//IF THE GAME IS IN MULTIPLAYER
        {
            CoopRedPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
            CoopBluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
            CoopYellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
        }
        cameraComponent = gameObject.GetComponent<Camera>();
        aspectRatio = cameraComponent.aspect;
        //Debug.Log(aspectRatio);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(sizeNeeded);
        if (gameManager.isItSingleplayer == true)
        {
            currentGameState = GameState.SinglePlayer;
        }
        if (gameManager.isItSingleplayer == false)
        {
            currentGameState = GameState.Multiplayer;
        }
        if (currentGameState == GameState.SinglePlayer)//IF THE GAME IS IN SINGLEPLAYER
        {
            //Debug.Log(targetCameraPosition);
            rawMousePos = Input.mousePosition;
            rawMousePos.z = 0f;
            normMousePos = Camera.main.ScreenToWorldPoint(rawMousePos);
            CalculateAveragePosInSinglePlayer();
            Vector3 velocity = Vector3.zero;
            if (currentCameraState==CameraState.SixtyDegrees)
            {
                targetCameraPosition = new Vector3(averagePos.x, averagePos.y + 45f, averagePos.z - 27.5f);
                targetCameraRotation = new Vector3(60,0,0);
            }
            else if (currentCameraState == CameraState.SixtyDegreesSlanted)
            {
                targetCameraPosition = new Vector3(averagePos.x-5f, averagePos.y + 45f, averagePos.z - 27.5f);
                targetCameraRotation = new Vector3(60, 15, 0);
            }
            else if (currentCameraState == CameraState.FiftyDegrees)
            {
                targetCameraPosition = new Vector3(averagePos.x, averagePos.y + 45f, averagePos.z - 40f);
                targetCameraRotation = new Vector3(50, 0, 0);
            }
            else if (currentCameraState == CameraState.FiftyDegreesSlanted)
            {
                targetCameraPosition = new Vector3(averagePos.x-6f, averagePos.y + 45f, averagePos.z - 37.5f);
                targetCameraRotation = new Vector3(50, 10, 0);
            }
            else if (currentCameraState == CameraState.TopDown)
            {
                targetCameraPosition = new Vector3(averagePos.x, averagePos.y + 45f, averagePos.z - 10f);
                targetCameraRotation = new Vector3(80, 0, 0);
            }
            //The target Position above player is calculated
            //transform.position = Vector3.MoveTowards(transform.position, targetCameraPosition, cameraMoveSpeed);//The Camera always moves smoothly towards
            //transform.position = targetCameraPosition;
            Vector3 tempCurrentRotation = transform.rotation.eulerAngles;
            Vector3 tempRotation = Vector3.SmoothDamp(tempCurrentRotation, targetCameraRotation, ref velocity, rotationTime);
            transform.rotation = Quaternion.Euler(tempRotation);
            transform.position = Vector3.SmoothDamp(transform.position, targetCameraPosition, ref velocity, cameraMoveSpeed);
            //averagePos.y = 0;


            //transform.LookAt(averagePos); //THE CAMERA LOOKS TOWARDS PLAYER CONSTANTLY
            //transform.rotation.y = 0;
            cameraComponent.orthographicSize = Mathf.SmoothDamp(cameraComponent.orthographicSize, 11, ref zoomSpeed, dampTime);
        }
        if (currentGameState == GameState.Multiplayer)//IF THE GAME IS IN MULTIPLAYER
        {
            CalculateAveragePosInCoOp();//CALCULATE THE AVERAGE POSITION BETWEEN ALL 3 PLAYERS
            CalculateSizeNeeded();//CALCULATE THE LEVEL OF ZOOM THAT THE CAMERA NEEDS TO BE TO FIT ALL PLAYERS ON SCREEN
            if (currentCameraState == CameraState.SixtyDegrees)
            {
                targetCameraPosition = new Vector3(averagePos.x, averagePos.y + 45f, averagePos.z - 27.5f);
                targetCameraRotation = new Vector3(60, 0, 0);
            }
            else if (currentCameraState == CameraState.SixtyDegreesSlanted)
            {
                targetCameraPosition = new Vector3(averagePos.x - 5f, averagePos.y + 45f, averagePos.z - 27.5f);
                targetCameraRotation = new Vector3(60, 15, 0);
            }
            else if (currentCameraState == CameraState.FiftyDegrees)
            {
                targetCameraPosition = new Vector3(averagePos.x, averagePos.y + 45f, averagePos.z - 40f);
                targetCameraRotation = new Vector3(50, 0, 0);
            }
            else if (currentCameraState == CameraState.FiftyDegreesSlanted)
            {
                targetCameraPosition = new Vector3(averagePos.x - 6f, averagePos.y + 45f, averagePos.z - 37.5f);
                targetCameraRotation = new Vector3(50, 10, 0);
            }
            else if (currentCameraState == CameraState.TopDown)
            {
                targetCameraPosition = new Vector3(averagePos.x, averagePos.y + 45f, averagePos.z - 10f);
                targetCameraRotation = new Vector3(80, 0, 0);
            }
            //Debug.Log(targetCameraRotation);
            //Debug.Log(currentCameraState);
            Vector3 velocity = Vector3.zero;
            Vector3 tempCurrentRotation = transform.rotation.eulerAngles;
            Vector3 tempRotation = Vector3.SmoothDamp(tempCurrentRotation, targetCameraRotation, ref velocity, rotationTime);
            transform.rotation = Quaternion.Euler(tempRotation);
            transform.position = Vector3.SmoothDamp(transform.position, targetCameraPosition, ref velocity, cameraMoveSpeed*Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, targetCameraPosition, cameraMoveSpeed);//MOVING THE CAMERA TOWARDS THE TARGET POS FOR IT
            //transform.LookAt(new Vector3(averagePos.x, 0, averagePos.z));//HAVING THE CAMERA LOOK AT THE TARGET POS
            cameraComponent.orthographicSize = Mathf.SmoothDamp(cameraComponent.orthographicSize, sizeNeeded, ref zoomSpeed, dampTime);//CHANGING THE SIZE TO THE NEEDED ONE TO FIT ALL PLAYERS ON SCREEN
        }
        if (shakeDuration > 0 )
        {
            transform.localPosition = transform.localPosition + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            //transform.localPosition = targetCameraPosition;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //SmallScreenShake();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //BigScreenShake();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //currentCameraState = CameraState.SixtyDegrees;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //currentCameraState = CameraState.SixtyDegreesSlanted;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //currentCameraState = CameraState.FiftyDegrees;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
           // currentCameraState = CameraState.FiftyDegreesSlanted;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            //currentCameraState = CameraState.TopDown;
        }
    }
    private void CalculateAveragePosInCoOp()
    {
        //averagePos = (CoopBluePlayer.transform.position + CoopRedPlayer.transform.position + CoopYellowPlayer.transform.position) / 3; //THE AVERAGE POS OF ALL 3 PLAYERS

        float diffBetweenBlueAndRed = Vector3.Distance(CoopBluePlayer.transform.position, CoopRedPlayer.transform.position);
        float diffBetweenBlueAndYellow = Vector3.Distance(CoopBluePlayer.transform.position, CoopYellowPlayer.transform.position);
        float diffBetweenYellowAndRed = Vector3.Distance(CoopYellowPlayer.transform.position, CoopRedPlayer.transform.position);

        if (Mathf.Max(diffBetweenBlueAndRed, diffBetweenBlueAndYellow, diffBetweenYellowAndRed) == diffBetweenBlueAndRed)
        {
            averagePos = (CoopBluePlayer.transform.position + CoopRedPlayer.transform.position) / 2;
        }
        else if (Mathf.Max(diffBetweenBlueAndRed, diffBetweenBlueAndYellow, diffBetweenYellowAndRed) == diffBetweenBlueAndYellow)
        {
            averagePos = (CoopBluePlayer.transform.position + CoopYellowPlayer.transform.position) / 2;
        }
        else if (Mathf.Max(diffBetweenBlueAndRed, diffBetweenBlueAndYellow, diffBetweenYellowAndRed) == diffBetweenYellowAndRed)
        {
            averagePos = (CoopRedPlayer.transform.position + CoopYellowPlayer.transform.position) / 2;
        }



    }
    private void CalculateAveragePosInSinglePlayer()
    {
       // Debug.Log(normMousePos);
        Vector2 centreOfScreen = new Vector2(Screen.width/2,Screen.height/2);
        //Debug.Log(centreOfScreen);
        Vector2 differenceBetweenCentreAndMouse = new Vector2(Input.mousePosition.x - centreOfScreen.x , Input.mousePosition.y - centreOfScreen.y);

        //averagePos = SPPlayer.transform.position;
        averagePos = (new Vector3((SPPlayer.transform.position.x + Mathf.Max(Mathf.Min((differenceBetweenCentreAndMouse.x/150),5f),-5f)), SPPlayer.transform.position.y, (SPPlayer.transform.position.z + Mathf.Max(Mathf.Min((differenceBetweenCentreAndMouse.y/150),5f),-5f))));
        
        //averagePos = (new Vector3(SPPlayer.transform.position.x , SPPlayer.transform.position.y, SPPlayer.transform.position.z ));
    }
    private void CalculateSizeNeeded()
    {
        sizeNeeded = 0f;
        Vector3 distanceBetweenRedPlayerAndAverage = averagePos - CoopRedPlayer.transform.position;
        Vector3 distanceBetweenBluePlayerAndAverage = averagePos - CoopBluePlayer.transform.position;
        Vector3 distanceBetweenYellowPlayerAndAverage = averagePos - CoopYellowPlayer.transform.position;

        sizeNeeded = Mathf.Max(sizeNeeded, Mathf.Abs(distanceBetweenBluePlayerAndAverage.z), (Mathf.Abs(distanceBetweenBluePlayerAndAverage.x) / aspectRatio));
        sizeNeeded = Mathf.Max(sizeNeeded, Mathf.Abs(distanceBetweenRedPlayerAndAverage.z), (Mathf.Abs(distanceBetweenRedPlayerAndAverage.x) / aspectRatio));
        sizeNeeded = Mathf.Max(sizeNeeded, Mathf.Abs(distanceBetweenYellowPlayerAndAverage.z), (Mathf.Abs(distanceBetweenYellowPlayerAndAverage.x) / aspectRatio));
        sizeNeeded = Mathf.Max(sizeNeeded, minimumSize);//FINDS THE MAXIMUM SIZE NEEDED BETWEEN ALL CHOICES
        sizeNeeded = Mathf.Min(sizeNeeded, maximumSize);//MAKES SURE THE CAMERA SIZE ISNT ABOVE THE MAXIMUM SIZE
        sizeNeeded += screenEdgeBuffer;//ADDS THE BUFFER TO SIDE OF SCREEN
    }
    public void SwapBetweenCoOpAndSingle()
    {//CALL THIS FUNCTION TO SWAP BETWEEN CO OP AND SINGLEPLAYER, IT NEED TO BE CALLED ONLY ONCE AND PLAYER PREFABS NEED TO BE INSTANTIATED BEFORE THE CALL

        if (currentGameState == GameState.SinglePlayer)
        {
            CoopRedPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
            CoopBluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
            CoopYellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
            currentGameState = GameState.Multiplayer;
        }
        else if (currentGameState == GameState.Multiplayer)
        {
            SPPlayer = GameObject.FindGameObjectWithTag("Player");
            currentGameState = GameState.SinglePlayer;
        }
    }
    public void SmallScreenShake()
    {
        shakeDuration = 0.01f;
        shakeAmount = 0.1f;
    }
    public void BigScreenShake()
    {
        shakeDuration = 0.5f;
        shakeAmount = 0.75f;
    }

    public void ToggleToZoomedOut()
    {
        maximumSize = 16f;
        minimumSize = 15f;
    }

    public void ToggleToZoomedIn()
    {
        maximumSize = 11f;
        minimumSize = 10f;
    }
}
