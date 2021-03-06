﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSpawnerTrigger : MonoBehaviour
{

    private bool HasSpawnerBeenTriggered = false;

    public List<GameObject> thisTriggersSpawners = new List<GameObject>();
    public List<newSpawner.enemyTypes> SpawnPoint1Enemies = new List<newSpawner.enemyTypes>();
    public List<newSpawner.enemyTypes> SpawnPoint2Enemies = new List<newSpawner.enemyTypes>();
    public List<newSpawner.enemyTypes> SpawnPoint3Enemies = new List<newSpawner.enemyTypes>();
    public List<newSpawner.enemyTypes> SpawnPoint4Enemies = new List<newSpawner.enemyTypes>();
    public List<newSpawner.enemyTypes> SpawnPoint5Enemies = new List<newSpawner.enemyTypes>();

    private int amountOfPlayersInTrigger = 0;

    private GameObject Arrow;
    public arrowPingPong.PositionState positionForArrow;
    public float arrowTimer = 90f;
    private bool hasArrowBeenTriggered = false;

    // Use this for initialization
    void Start () {
		Arrow = GameObject.FindGameObjectWithTag("Arrow");
	}
	
	// Update is called once per frame
	void Update () {


	    if (HasSpawnerBeenTriggered==false)
	    {
	        if (amountOfPlayersInTrigger>0)
	        {
	            for (int spawnPointNumber = 0; spawnPointNumber < thisTriggersSpawners.Count; spawnPointNumber++)
	            {
	                if (spawnPointNumber == 0)
	                {
	                    thisTriggersSpawners[spawnPointNumber].GetComponent<newSpawner>().SpawnEnemies(SpawnPoint1Enemies);
	                }
	                else if (spawnPointNumber == 1)
	                {
	                    thisTriggersSpawners[spawnPointNumber].GetComponent<newSpawner>().SpawnEnemies(SpawnPoint2Enemies);
	                }
	                else if (spawnPointNumber == 2)
	                {
	                    thisTriggersSpawners[spawnPointNumber].GetComponent<newSpawner>().SpawnEnemies(SpawnPoint3Enemies);
	                }
	                else if (spawnPointNumber == 3)
	                {
	                    thisTriggersSpawners[spawnPointNumber].GetComponent<newSpawner>().SpawnEnemies(SpawnPoint4Enemies);
	                }
	                else if (spawnPointNumber == 4)
	                {
	                    thisTriggersSpawners[spawnPointNumber].GetComponent<newSpawner>().SpawnEnemies(SpawnPoint5Enemies);
	                }
	            }

	            HasSpawnerBeenTriggered = true;
	        }
	    }

	    if (amountOfPlayersInTrigger>0)
	    {
	        arrowTimer -= Time.deltaTime;
	    }

	    if (arrowTimer<0&&hasArrowBeenTriggered==false)
	    {
            Arrow.GetComponent<arrowPingPong>().ChangePosition(positionForArrow);
            Arrow.GetComponent<arrowPingPong>().ArrowFadeIn();
	        hasArrowBeenTriggered = true;
	    }
	    if (amountOfPlayersInTrigger==0)
	    {
	        arrowTimer = 90f;
            //Debug.Log("thisis happening");
            
	        for (int spawnPointNumber = 0;spawnPointNumber<thisTriggersSpawners.Count; spawnPointNumber++)
	        {
                thisTriggersSpawners[spawnPointNumber].GetComponent<newSpawner>().PurgeEnemies();
	        }
	        HasSpawnerBeenTriggered = false;
	        hasArrowBeenTriggered = false;
	        //Arrow.GetComponent<arrowPingPong>().ArrowFadeOut();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true || other.CompareTag("RedPlayer") == true ||
            other.CompareTag("BluePlayer") == true || other.CompareTag("YellowPlayer"))
        {
            /*
            if (HasSpawnerBeenTriggered==false)
            {
                

                HasSpawnerBeenTriggered = true;
            }*/
            amountOfPlayersInTrigger += 1;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == true || other.CompareTag("RedPlayer") == true ||
            other.CompareTag("BluePlayer") == true || other.CompareTag("YellowPlayer"))
        {
            amountOfPlayersInTrigger -= 1;
            if (amountOfPlayersInTrigger<=0)
            {
                Arrow.GetComponent<arrowPingPong>().ArrowFadeOut();
            }
        }
    }
}
