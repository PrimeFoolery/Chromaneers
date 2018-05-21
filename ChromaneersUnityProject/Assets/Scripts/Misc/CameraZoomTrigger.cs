using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{

    private GameObject Camera;

    private GameObject BluePlayer;
    private GameObject RedPlayer;
    private GameObject YellowPlayer;

    // Use this for initialization
    void Start () {
		Camera = GameObject.FindGameObjectWithTag("MainCamera");
        BluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
        RedPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
        YellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        BluePlayer.GetComponent<CoopCharacterControllerOne>().isCameraZoomedOut = true;
        RedPlayer.GetComponent<CoopCharacterControllerTwo>().isCameraZoomedOut = true;
        YellowPlayer.GetComponent<CoopCharacterControllerThree>().isCameraZoomedOut = true;
        Camera.GetComponent<CameraScript>().ToggleToZoomedOut();
    }
}
