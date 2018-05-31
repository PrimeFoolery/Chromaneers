using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{

    private bool hasWallSpawned = false;
    private int amountOfPlayersInEndArena = 0;

    public GameObject wall;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (amountOfPlayersInEndArena==3 && hasWallSpawned==false)
	    {
	        Instantiate(wall, transform.position, transform.rotation);
	        hasWallSpawned = true;
	    }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BluePlayer")||other.CompareTag("RedPlayer")||other.CompareTag("YellowPlayer"))
        {
            amountOfPlayersInEndArena += 1;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BluePlayer") || other.CompareTag("RedPlayer") || other.CompareTag("YellowPlayer"))
        {
            //amountOfPlayersInEndArena -= 1;
        }
    }
}
