using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniteSpawner : MonoBehaviour {

    private bool HasSpawnerBeenTriggered = false;

    public List<GameObject> thisTriggersSpawners = new List<GameObject>();
    public List<InfiniteSpawnPoint.enemyTypes> SpawnPoint1Enemies = new List<InfiniteSpawnPoint.enemyTypes>();

    private int amountOfPlayersInTrigger = 0;

    private float spawnerTimer = 5f;

    public int randomSpawnPoint;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfPlayersInTrigger>0)
        {
            if (spawnerTimer>5f)
            {
                randomSpawnPoint = Random.Range(0, thisTriggersSpawners.Count-1);
                thisTriggersSpawners[randomSpawnPoint].GetComponent<InfiniteSpawnPoint>().SpawnEnemies(SpawnPoint1Enemies);
                spawnerTimer = 0f;
            }
            spawnerTimer += Time.deltaTime;
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
