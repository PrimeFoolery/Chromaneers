using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public enum GameState {SinglePlayer, Multiplayer };
    GameState currentGameState = GameState.Multiplayer; 

    private float cameraMoveSpeed = 0.3f;//SPEED THAT CAMERA MOVES TOWARDS TARGET POSITION
    private Vector3 targetCameraPosition;// THE POSITION OF THE CAMERA
    private Camera cameraComponent;

    //SINGLEPLAYER VARIABLES
    private GameObject SPPlayer; //PLAYER IN SINGLEPLAYER
    


    //MULTIPLAYER VARIABLES
    private GameObject CoopRedPlayer; //PLAYERS IN CO-OP
    private GameObject CoopBluePlayer;
    private GameObject CoopYellowPlayer;

    private Vector3 averagePos;
    private float sizeNeeded;
    private float minimumSize = 7f;
    private float screenEdgeBuffer = 4f;
    private float aspectRatio;
    private float zoomSpeed = 4f;
    private float dampTime = 0.2f;

	// Use this for initialization
	void Start () {
        if (currentGameState==GameState.SinglePlayer)//IF THE GAME IS IN SINGLEPLAYER
        {
            SPPlayer = GameObject.FindGameObjectWithTag("Player"); //FINDS AND ASSIGNS THE SINGLE PLAYER TO THE CAMERA
        }
        if (currentGameState==GameState.Multiplayer)//IF THE GAME IS IN MULTIPLAYER
        {
            CoopRedPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
            CoopBluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
            CoopYellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
        }
        cameraComponent = gameObject.GetComponent<Camera>();
        aspectRatio = cameraComponent.aspect;
		Debug.Log (aspectRatio);
	}
	
	// Update is called once per frame
	void Update () {
        if (currentGameState==GameState.SinglePlayer)//IF THE GAME IS IN SINGLEPLAYER
        {
            targetCameraPosition = new Vector3(SPPlayer.transform.position.x, SPPlayer.transform.position.y+15f, SPPlayer.transform.position.z-7.5f);//The target Position above player is calculated
            transform.position = Vector3.MoveTowards(transform.position, targetCameraPosition, cameraMoveSpeed);//The Camera always moves smoothly towards
            transform.LookAt(SPPlayer.transform); //THE CAMERA LOOKS TOWARDS PLAYER CONSTANTLY
        }
        if (currentGameState==GameState.Multiplayer)//IF THE GAME IS IN MULTIPLAYER
        {
            CalculateAveragePosInCoOp();//CALCULATE THE AVERAGE POSITION BETWEEN ALL 3 PLAYERS
            CalculateSizeNeeded();//CALCULATE THE LEVEL OF ZOOM THAT THE CAMERA NEEDS TO BE TO FIT ALL PLAYERS ON SCREEN
            targetCameraPosition = new Vector3(averagePos.x, averagePos.y + 15f, averagePos.z - 7.5f);//WHERE THE CAMERA SHOULD BE MOVING
            transform.position = Vector3.MoveTowards(transform.position, targetCameraPosition, cameraMoveSpeed);//MOVING THE CAMERA TOWARDS THE TARGET POS FOR IT
            transform.LookAt(new Vector3(averagePos.x, 0, averagePos.z));//HAVING THE CAMERA LOOK AT THE TARGET POS
            cameraComponent.orthographicSize = Mathf.SmoothDamp(cameraComponent.orthographicSize, sizeNeeded,ref zoomSpeed, dampTime);//CHANGING THE SIZE TO THE NEEDED ONE TO FIT ALL PLAYERS ON SCREEN
        }
	}
    private void CalculateAveragePosInCoOp()
    {
        averagePos = (CoopBluePlayer.transform.position + CoopRedPlayer.transform.position + CoopYellowPlayer.transform.position)/3; //THE AVERAGE POS OF ALL 3 PLAYERS
    }
    private void CalculateSizeNeeded()
    {
        sizeNeeded = 0f;
        Vector3 distanceBetweenRedPlayerAndAverage = averagePos - CoopRedPlayer.transform.position;
        Vector3 distanceBetweenBluePlayerAndAverage = averagePos - CoopBluePlayer.transform.position;
        Vector3 distanceBetweenYellowPlayerAndAverage = averagePos - CoopYellowPlayer.transform.position;

		sizeNeeded = Mathf.Max(sizeNeeded,Mathf.Abs(distanceBetweenBluePlayerAndAverage.z), (Mathf.Abs(distanceBetweenBluePlayerAndAverage.x)/aspectRatio));
		sizeNeeded = Mathf.Max(sizeNeeded, Mathf.Abs(distanceBetweenRedPlayerAndAverage.z), (Mathf.Abs(distanceBetweenRedPlayerAndAverage.x) / aspectRatio));
		sizeNeeded = Mathf.Max(sizeNeeded, Mathf.Abs(distanceBetweenYellowPlayerAndAverage.z), (Mathf.Abs(distanceBetweenYellowPlayerAndAverage.x) / aspectRatio));
        sizeNeeded = Mathf.Max(sizeNeeded, minimumSize);//FINDS THE MAXIMUM SIZE NEEDED BETWEEN ALL CHOICES
        sizeNeeded += screenEdgeBuffer;//ADDS THE BUFFER TO SIDE OF SCREEN
    }
}
