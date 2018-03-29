using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public enum GameState { SinglePlayer, Multiplayer };
    public GameState currentGameState = GameState.Multiplayer;
    private ColourSelectManager gameManager;

    private float cameraMoveSpeed = 0.15f;//SPEED THAT CAMERA MOVES TOWARDS TARGET POSITION
    private Vector3 targetCameraPosition;// THE POSITION OF THE CAMERA
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
    private float minimumSize = 7f;
    private float maximumSize = 11f;
    private float screenEdgeBuffer = 4f;
    private float aspectRatio;
    private float zoomSpeed = 4f;
    private float dampTime = 0.2f;

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
            targetCameraPosition = new Vector3(averagePos.x, averagePos.y + 15f, averagePos.z - 7.5f);//The target Position above player is calculated
            //transform.position = Vector3.MoveTowards(transform.position, targetCameraPosition, cameraMoveSpeed);//The Camera always moves smoothly towards
            //transform.position = targetCameraPosition;

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
            targetCameraPosition = new Vector3(averagePos.x, averagePos.y + 15f, averagePos.z - 7.5f);//WHERE THE CAMERA SHOULD BE MOVING
            Vector3 velocity = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, targetCameraPosition, ref velocity, cameraMoveSpeed);
            //transform.position = Vector3.MoveTowards(transform.position, targetCameraPosition, cameraMoveSpeed);//MOVING THE CAMERA TOWARDS THE TARGET POS FOR IT
            transform.LookAt(new Vector3(averagePos.x, 0, averagePos.z));//HAVING THE CAMERA LOOK AT THE TARGET POS
            cameraComponent.orthographicSize = Mathf.SmoothDamp(cameraComponent.orthographicSize, sizeNeeded, ref zoomSpeed, dampTime);//CHANGING THE SIZE TO THE NEEDED ONE TO FIT ALL PLAYERS ON SCREEN
        }
        if (shakeDuration > 0)
        {
            transform.localPosition = targetCameraPosition + Random.insideUnitSphere * shakeAmount;
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
    }
    private void CalculateAveragePosInCoOp()
    {
        averagePos = (CoopBluePlayer.transform.position + CoopRedPlayer.transform.position + CoopYellowPlayer.transform.position) / 3; //THE AVERAGE POS OF ALL 3 PLAYERS
    }
    private void CalculateAveragePosInSinglePlayer()
    {
       // Debug.Log(normMousePos);
        Vector2 centreOfScreen = new Vector2(Screen.width/2,Screen.height/2);
        Debug.Log(centreOfScreen);
        Vector2 differenceBetweenCentreAndMouse = new Vector2(Input.mousePosition.x - centreOfScreen.x , Input.mousePosition.y - centreOfScreen.y);

        //averagePos = SPPlayer.transform.position;
        averagePos = (new Vector3((SPPlayer.transform.position.x + (differenceBetweenCentreAndMouse.x/150)), SPPlayer.transform.position.y, (SPPlayer.transform.position.z + (differenceBetweenCentreAndMouse.y/150))));
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
        shakeDuration = 0.03f;
        shakeAmount = 0.1f;
    }
    public void BigScreenShake()
    {
        shakeDuration = 0.1f;
        shakeAmount = 0.75f;
    }
}
