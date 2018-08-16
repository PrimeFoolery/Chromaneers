using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRateResetter : MonoBehaviour {

    private int amountOfPlayersInTrigger = 0;
    public GameObject spawnRateToReset;
    private bool hasSpawnRateReset = false;
    private float originalSpawnRate;


    // Use this for initialization
    void Start ()
    {
        originalSpawnRate = spawnRateToReset.GetComponent<infiniteSpawner>().spawnRate;
    }
	
	// Update is called once per frame
	void Update () {
	    if (hasSpawnRateReset == false)
	    {
	        if (amountOfPlayersInTrigger>0)
	        {
	            spawnRateToReset.GetComponent<infiniteSpawner>().spawnRate = originalSpawnRate;
	            hasSpawnRateReset = true;
	        }
	    }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true || other.CompareTag("RedPlayer") == true ||
            other.CompareTag("BluePlayer") == true || other.CompareTag("YellowPlayer"))
        {
            amountOfPlayersInTrigger += 1;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == true || other.CompareTag("RedPlayer") == true ||
            other.CompareTag("BluePlayer") == true || other.CompareTag("YellowPlayer"))
        {
            amountOfPlayersInTrigger -= 1;
        }
    }
}
