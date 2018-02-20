using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public enum GameState {SinglePlayer, Multiplayer };
    GameState currentGameState = GameState.Multiplayer;

    private float cameraMoveSpeed = 0.3f;//SPEED THAT CAMERA MOVES TOWARDS TARGET POSITION
    private Vector3 targetCameraPosition;// THE POSITION OF THE CAMERA

    //SINGLEPLAYER VARIABLES
    private GameObject SPPlayer; //PLAYER IN SINGLEPLAYER
    


    //MULTIPLAYER VARIABLES
    private GameObject CoopRedPlayer; //PLAYERS IN CO-OP
    private GameObject CoopBluePlayer;
    private GameObject CoopYellowPlayer;


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

        }
	}
}
