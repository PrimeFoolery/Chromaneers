using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpawnPoint : MonoBehaviour
{

    public bool isSpawnerAboveGround;

    private string tagUnderSpawner;

    public GameObject FastEnemy;
    private FastEnemy tempFastEnemyScript;
    public List<GameObject> ThisSpawnpointsEnemyList = new List<GameObject>();
    private EnemyManager enemyManagerScript;

    private int randomNumber;
    public GameObject tempEnemy;

    // Use this for initialization
    void Start () {
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
    }
	
	// Update is called once per frame
    void Update()
    {
        RaycastHit floorHit;
        Ray floorRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(floorRay, out floorHit, 20f))
        {
            if (floorHit.collider)
            {
                tagUnderSpawner = floorHit.collider.gameObject.tag;

            }
            else
            {
                tagUnderSpawner = "null";
            }
        }
        else
        {
            tagUnderSpawner = "null";
        }

        if (tagUnderSpawner!="Floor"&&tagUnderSpawner!="FastEnemy" && tagUnderSpawner != "RedEnemy" && tagUnderSpawner != "YellowEnemy" && tagUnderSpawner != "BlueEnemy" && tagUnderSpawner != "SnakeEnemy")
        {
            isSpawnerAboveGround = false;
        }

        if (tagUnderSpawner=="Floor" || tagUnderSpawner == "FastEnemy" || tagUnderSpawner == "RedEnemy" || tagUnderSpawner == "YellowEnemy" || tagUnderSpawner == "BlueEnemy" || tagUnderSpawner == "SnakeEnemy")
        {
            isSpawnerAboveGround = true;
        }
    }

    public void SpawnMeatShield(string ColourOfMother)
    {
        if (ColourOfMother == "blue")
        {
            randomNumber = Random.Range(1, 3);
            if (randomNumber == 1 )
            {
                //tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                tempEnemy = Instantiate(FastEnemy, transform.position, Quaternion.identity);
                tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 2;
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                ThisSpawnpointsEnemyList.Add(tempEnemy);

                if (tempFastEnemyScript != null)
                {
                    tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    tempFastEnemyScript.isAggroPlayer = true;
                }
                tempEnemy = null;
                tempFastEnemyScript = null;
            }
            else if (randomNumber == 2)
            {
                tempEnemy = Instantiate(FastEnemy, transform.position, Quaternion.identity);
                //tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 3;
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                ThisSpawnpointsEnemyList.Add(tempEnemy);
                if (tempFastEnemyScript != null)
                {
                    tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    tempFastEnemyScript.isAggroPlayer = true;
                }
                tempEnemy = null;
                tempFastEnemyScript = null;
            }
        }else if (ColourOfMother == "red")
        {
            randomNumber = Random.Range(1, 3);
            if (randomNumber == 1)
            {
                tempEnemy = Instantiate(FastEnemy, transform.position, Quaternion.identity);
                //tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 1;
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                ThisSpawnpointsEnemyList.Add(tempEnemy);
                if (tempFastEnemyScript != null)
                {
                    tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    tempFastEnemyScript.isAggroPlayer = true;
                }
                tempEnemy = null;
                tempFastEnemyScript = null;
            }
            else if (randomNumber == 2)
            {
                tempEnemy = Instantiate(FastEnemy, transform.position, Quaternion.identity);
                //tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 3;
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                ThisSpawnpointsEnemyList.Add(tempEnemy);
                if (tempFastEnemyScript != null)
                {
                    tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    tempFastEnemyScript.isAggroPlayer = true;
                }
                tempEnemy = null;
                tempFastEnemyScript = null;
            }
        }
        else if (ColourOfMother == "yellow")
        {
            randomNumber = Random.Range(1, 3);
            if (randomNumber == 1)
            {
                tempEnemy = Instantiate(FastEnemy, transform.position, Quaternion.identity);
                //tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 1;
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                ThisSpawnpointsEnemyList.Add(tempEnemy);
                if (tempFastEnemyScript != null)
                {
                    tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    tempFastEnemyScript.isAggroPlayer = true;
                }
                tempEnemy = null;
                tempFastEnemyScript = null;
            }
            else if (randomNumber == 2)
            {
                tempEnemy = Instantiate(FastEnemy, transform.position, Quaternion.identity);
                //tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 2;
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                ThisSpawnpointsEnemyList.Add(tempEnemy);
                if (tempFastEnemyScript != null)
                {
                    tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    tempFastEnemyScript.isAggroPlayer = true;
                }
                tempEnemy = null;
                tempFastEnemyScript = null;
            }
        }
    }
}
