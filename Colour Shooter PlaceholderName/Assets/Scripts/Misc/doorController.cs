using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{

    private Vector3 targetMovement;

    public bool doorOpen = false;

    [Range(0.0f, 10.0f)] public float doorSpeed = 5f;

	// Use this for initialization
	void Start () {
		targetMovement =new Vector3(transform.localPosition.x,transform.localPosition.y, transform.localPosition.z+20);
	}
	
	// Update is called once per frame
	void Update () {
	    if (doorOpen == true && Vector3.Distance(transform.position, targetMovement)>0.5f)
	    {
	        transform.position = Vector3.MoveTowards(transform.position, targetMovement, doorSpeed);
	    }
	}

    public void OpenSesame()
    {
        doorOpen = true;
    }
}
