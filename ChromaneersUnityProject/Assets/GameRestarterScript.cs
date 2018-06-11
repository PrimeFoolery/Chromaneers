using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarterScript : MonoBehaviour {

	// Use this for initialization

    public bool shouldGameRestart = true;
    private float gameRestartTimer = 90f;

    private GameObject bluePlayer;
    private GameObject redPlayer;
    private GameObject yellowPlayer;

	void Start () {
		bluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
	    redPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
	    yellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
    }
	
	// Update is called once per frame
	void Update ()
	{
	    gameRestartTimer -= Time.deltaTime;
        //Debug.Log(gameRestartTimer);
	    if (Input.anyKey|| new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"))!=new Vector3(0,0,0)||
	        new Vector3(Input.GetAxisRaw("XboxJoystick2LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick2LVertical")) != new Vector3(0, 0, 0) ||
	        new Vector3(Input.GetAxisRaw("XboxJoystick3LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick3LVertical")) != new Vector3(0, 0, 0)||
	        new Vector3(Input.GetAxisRaw("Joystick3LHorizontal"), 0f, Input.GetAxisRaw("Joystick3LVertical")) != new Vector3(0, 0, 0) || 
	        new Vector3(Input.GetAxisRaw("Joystick1LHorizontal"), 0f, Input.GetAxisRaw("Joystick1LVertical")) != new Vector3(0, 0, 0) || 
	        new Vector3(Input.GetAxisRaw("Joystick2LHorizontal"), 0f, Input.GetAxisRaw("Joystick2LVertical")) != new Vector3(0, 0, 0) || 
            bluePlayer.GetComponent<CoopCharacterControllerOne>().isShooting == true ||
	        redPlayer.GetComponent<CoopCharacterControllerTwo>().isShooting == true ||
	        yellowPlayer.GetComponent<CoopCharacterControllerThree>().isShooting == true)
	    {
	        gameRestartTimer = 90f;
	    }

	    if (gameRestartTimer < 0)
	    {
            SceneManager.LoadScene("MenuTest");
	    }
	}
}
