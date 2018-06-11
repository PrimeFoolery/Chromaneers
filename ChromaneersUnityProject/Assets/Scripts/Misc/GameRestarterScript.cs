using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarterScript : MonoBehaviour {

	// Use this for initialization

    public bool shouldGameRestart = true;
    private float gameRestartTimer = 90f;

    private bool forcedRestartDown = false;
    private float forcedRestartTimer = 4f;

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

	    if (Input.GetKey(KeyCode.Joystick1Button9)|| Input.GetKey(KeyCode.Joystick2Button9)|| Input.GetKey(KeyCode.Joystick3Button9)|| Input.GetButton("Pause1") || Input.GetButton("Pause2") || Input.GetButton("Pause3"))
	    {
	        forcedRestartDown = true;
	    }
	    else
	    {
	        forcedRestartDown = false;
	    }

	    if (forcedRestartDown == true)
	    {
	        forcedRestartTimer -= Time.deltaTime;
	        if (forcedRestartTimer<0)
	        {
                SceneManager.LoadScene("MenuTest");
	        }

	    }
	    else
	    {
	        forcedRestartTimer = 4f;
	    }
	}
}
