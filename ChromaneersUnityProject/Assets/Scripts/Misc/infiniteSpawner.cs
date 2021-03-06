﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniteSpawner : MonoBehaviour {

    private bool HasSpawnerBeenTriggered = false;

    public List<GameObject> thisTriggersSpawners = new List<GameObject>();
    public List<InfiniteSpawnPoint.enemyTypes> EveryEnemySpawnEnemies = new List<InfiniteSpawnPoint.enemyTypes>();
    public List<InfiniteSpawnPoint.enemyTypes> RandomExtraEnemiesPool = new List<InfiniteSpawnPoint.enemyTypes>();
    
    private List<InfiniteSpawnPoint.enemyTypes> ExtraEnemiesToSpawn = new List<InfiniteSpawnPoint.enemyTypes>(); 

    public int howManyExtraEnemies = 1;

    private int amountOfPlayersInTrigger = 0;

    private float spawnerTimer = 15f;

    private float startSpawnRate;
    public float spawnRate = 5f;
    public float spawnRateIncrease = 4f;
    public int randomSpawnPoint;
    // Use this for initialization
    void Start()
    {
        startSpawnRate = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfPlayersInTrigger>0&&thisTriggersSpawners.Count>0)
        {
            if (spawnerTimer>spawnRate)
            {
                if (howManyExtraEnemies == 1)
                {
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                }
                else if (howManyExtraEnemies == 2)
                {
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                }
                else if (howManyExtraEnemies == 3)
                {
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                }else if (howManyExtraEnemies == 4)
                {
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                }else if (howManyExtraEnemies == 5)
                {
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                    ExtraEnemiesToSpawn.Add(RandomExtraEnemiesPool[Random.Range(0, RandomExtraEnemiesPool.Count)]);
                }
                randomSpawnPoint = Random.Range(0, thisTriggersSpawners.Count-1);
                if (EveryEnemySpawnEnemies.Count>0)
                {
                    thisTriggersSpawners[randomSpawnPoint].GetComponent<InfiniteSpawnPoint>().SpawnEnemies(EveryEnemySpawnEnemies);
                }
                if (ExtraEnemiesToSpawn.Count>0)
                {
                    thisTriggersSpawners[randomSpawnPoint].GetComponent<InfiniteSpawnPoint>().SpawnEnemies(ExtraEnemiesToSpawn);
                }
                ExtraEnemiesToSpawn.Clear();
                foreach (GameObject spawnPoint in thisTriggersSpawners)
                {
                    spawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
                }

                spawnRate += spawnRateIncrease;
                spawnerTimer = 0f;
            }
            spawnerTimer += Time.deltaTime;
        }

        if (amountOfPlayersInTrigger==0)
        {
            foreach (GameObject spawnPoint in thisTriggersSpawners)
            {
                spawnPoint.GetComponent<InfiniteSpawnPoint>().PurgeEnemies();
            }

            spawnRate = startSpawnRate;
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
