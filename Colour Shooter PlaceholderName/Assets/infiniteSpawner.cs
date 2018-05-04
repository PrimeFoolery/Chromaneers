using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniteSpawner : MonoBehaviour {

    private bool HasSpawnerBeenTriggered = false;

    public List<GameObject> thisTriggersSpawners = new List<GameObject>();
    public List<newSpawner.enemyTypes> SpawnPoint1Enemies = new List<newSpawner.enemyTypes>();

    private int amountOfPlayersInTrigger = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (HasSpawnerBeenTriggered == false)
        {
            if (amountOfPlayersInTrigger > 0)
            {
                for (int spawnPointNumber = 0; spawnPointNumber < thisTriggersSpawners.Count; spawnPointNumber++)
                {
                    if (spawnPointNumber == 0)
                    {
                        thisTriggersSpawners[spawnPointNumber].GetComponent<newSpawner>().SpawnEnemies(SpawnPoint1Enemies);
                    }
                }

                HasSpawnerBeenTriggered = true;
            }
        }

        if (amountOfPlayersInTrigger == 0)
        {
            for (int spawnPointNumber = 0; spawnPointNumber < thisTriggersSpawners.Count; spawnPointNumber++)
            {
                thisTriggersSpawners[spawnPointNumber].GetComponent<newSpawner>().PurgeEnemies();
            }
            HasSpawnerBeenTriggered = false;
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
        }
    }
}
