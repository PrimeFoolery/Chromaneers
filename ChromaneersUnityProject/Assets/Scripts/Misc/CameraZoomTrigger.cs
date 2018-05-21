using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{

    private GameObject Camera;

	// Use this for initialization
	void Start () {
		Camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Camera.GetComponent<CameraScript>().ToggleToZoomedOut();
    }
}
