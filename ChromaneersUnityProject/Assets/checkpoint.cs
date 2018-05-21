using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{

    public deathTracker DeathTrackerScript;
    private bool checkPointTriggered = false;

	// Use this for initialization
	void Start () {
	    DeathTrackerScript = GameObject.FindGameObjectWithTag("DeathTracker").GetComponent<deathTracker>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BluePlayer")||other.CompareTag("RedPlayer")||other.CompareTag("YellowPlayer"))
        {
            if (checkPointTriggered==false)
            {
                DeathTrackerScript.howManyAreasComplete += 1;
                checkPointTriggered = true;
            }
            
        }
    }
}
