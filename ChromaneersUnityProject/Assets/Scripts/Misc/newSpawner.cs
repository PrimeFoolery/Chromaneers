using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class newSpawner : MonoBehaviour
{

    private bool haveAllEnemiesSpawned = false;

    public enum enemyTypes
    {
        StandardBlue,
        StandardRed,
        StandardYellow,

        StandardPurple,
        StandardOrange,
        StandardGreen,

        Spider,

        SnakePartialRandom,
        SnakeMAXRANDOM,
        SnakeBlue,
        SnakeYellow,
        SnakeRed,

        FastEnemyRandom,
        FastEnemyBlue,
        FastEnemyRed,
        FastEnemyYellow,

        MotherRandom,
        MotherBlue,
        MotherRed,
        MotherYellow,

        BossEnemy
    }

    public GameObject RedEnemy;
    public GameObject YellowEnemy;
    public GameObject BlueEnemy;
    public GameObject SpiderEnemy;
    public GameObject RedShieldEnemy;
    public GameObject BlueShieldEnemy;
    public GameObject YellowShieldEnemy;
    public GameObject OrangeEnemy;
    public GameObject PurpleEnemy;
    public GameObject GreenEnemy;
    public GameObject SnakeEnemy;
    public GameObject FastEnemy;
    public GameObject MotherEnemy;
    public GameObject BossEnemy;

    public GameObject tempEnemy;
    private StandardEnemyBehaviour tempStandardEnemyBehaviour;
    private SpiderEnemyController tempSpiderEnemyController;
    private SnakeEnemyScript tempSnakeEnemyScript;
    private FastEnemy tempFastEnemyScript;
    private MotherController tempMotherEnemyScript;
    private BossController tempBossEnemyScript;
    public EnemyManager enemyManagerScript;

    public List<GameObject> ThisSpawnpointsEnemyList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        enemyManagerScript = UnityEngine.GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEnemies(List<enemyTypes> EnemiesToSpawn)
    {
        if (haveAllEnemiesSpawned==false)
        {
            foreach (enemyTypes enemy in EnemiesToSpawn)
            {
                if (enemy == enemyTypes.StandardBlue)
                {
                    tempEnemy = Instantiate(BlueEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                    if (tempStandardEnemyBehaviour != null)
                    {
                        tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = this.gameObject;
                    }
                    tempEnemy = null;
                    tempStandardEnemyBehaviour = null;
                }
                else if (enemy == enemyTypes.StandardRed)
                {
                    tempEnemy = Instantiate(RedEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                    if (tempStandardEnemyBehaviour != null)
                    {
                        tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = this.gameObject;
                    }
                    tempEnemy = null;
                    tempStandardEnemyBehaviour = null;
                }
                else if (enemy == enemyTypes.StandardYellow)
                {
                    tempEnemy = Instantiate(YellowEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                    if (tempStandardEnemyBehaviour != null)
                    {
                        tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = this.gameObject;
                    }
                    tempEnemy = null;
                    tempStandardEnemyBehaviour = null;
                }
                else if (enemy == enemyTypes.StandardPurple)
                {
                    tempEnemy = Instantiate(PurpleEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                    if (tempStandardEnemyBehaviour != null)
                    {
                        tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = this.gameObject;
                    }
                    tempEnemy = null;
                    tempStandardEnemyBehaviour = null;
                }
                else if (enemy == enemyTypes.StandardOrange)
                {
                    tempEnemy = Instantiate(OrangeEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                    if (tempStandardEnemyBehaviour != null)
                    {
                        tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = this.gameObject;
                    }
                    tempEnemy = null;
                    tempStandardEnemyBehaviour = null;
                }
                else if (enemy == enemyTypes.StandardGreen)
                {
                    tempEnemy = Instantiate(GreenEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                    if (tempStandardEnemyBehaviour != null)
                    {
                        tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = this.gameObject;
                    }
                    tempEnemy = null;
                    tempStandardEnemyBehaviour = null;
                }
                else if (enemy == enemyTypes.Spider)
                {
                    tempEnemy = Instantiate(SpiderEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
                    if (tempSpiderEnemyController != null)
                    {
                        tempSpiderEnemyController.thisEnemiesSpawnPoint = this.gameObject;
                    }
                    tempEnemy = null;
                    tempSpiderEnemyController = null;
                }
                else if (enemy == enemyTypes.SnakeRed)
                {
                    tempEnemy = Instantiate(SnakeEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(0).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(1).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(2).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(3).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(4).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(5).gameObject);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempSnakeEnemyScript.ChangeToRed();

                    if (tempSnakeEnemyScript != null)
                    {
                        tempSnakeEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }
                    tempEnemy = null;
                    tempSnakeEnemyScript = null;
                }
                else if (enemy == enemyTypes.SnakeBlue)
                {
                    tempEnemy = Instantiate(SnakeEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(0).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(1).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(2).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(3).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(4).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(5).gameObject);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempSnakeEnemyScript.ChangeToBlue();
                    if (tempSnakeEnemyScript != null)
                    {
                        tempSnakeEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }
                    tempEnemy = null;
                    tempSnakeEnemyScript = null;
                }
                else if (enemy == enemyTypes.SnakeYellow)
                {
                    tempEnemy = Instantiate(SnakeEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(0).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(1).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(2).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(3).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(4).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(5).gameObject);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempSnakeEnemyScript.ChangeToYellow();
                    tempEnemy = null;
                    tempSnakeEnemyScript = null;
                }
                else if (enemy == enemyTypes.SnakeMAXRANDOM)
                {
                    tempEnemy = Instantiate(SnakeEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(0).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(1).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(2).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(3).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(4).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(5).gameObject);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    if (tempSnakeEnemyScript != null)
                    {
                        tempSnakeEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }
                    tempEnemy = null;
                    tempSnakeEnemyScript = null;
                }
                else if (enemy == enemyTypes.SnakePartialRandom)
                {
                    tempEnemy = Instantiate(SnakeEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(0).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(1).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(2).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(3).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(4).gameObject);
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy.transform.GetChild(5).gameObject);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    tempSnakeEnemyScript.RandomAllSameColour();
                    if (tempSnakeEnemyScript != null)
                    {
                        tempSnakeEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }
                    tempEnemy = null;
                    tempSnakeEnemyScript = null;
                }
                else if (enemy == enemyTypes.FastEnemyBlue)
                {
                    tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 1;
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    if (tempFastEnemyScript != null)
                    {
                        tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }
                    tempEnemy = null;
                    tempFastEnemyScript = null;
                }
                else if (enemy == enemyTypes.FastEnemyRandom)
                {
                    tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    if (tempFastEnemyScript != null)
                    {
                        tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }
                    tempEnemy = null;
                    tempFastEnemyScript = null;
                }
                else if (enemy == enemyTypes.FastEnemyRed)
                {
                    tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 2;
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    if (tempFastEnemyScript != null)
                    {
                        tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }
                    tempEnemy = null;
                    tempFastEnemyScript = null;
                }
                else if (enemy == enemyTypes.FastEnemyYellow)
                {
                    tempEnemy = Instantiate(FastEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 3;
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    if (tempFastEnemyScript != null)
                    {
                        tempFastEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }
                    tempEnemy = null;
                    tempFastEnemyScript = null;
                }
                else if (enemy == enemyTypes.MotherBlue)
                {
                    tempEnemy = Instantiate(MotherEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempMotherEnemyScript = tempEnemy.GetComponent<MotherController>();
                    tempMotherEnemyScript.colourOverride = true;
                    tempMotherEnemyScript.randomColour = 1;
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    if (tempMotherEnemyScript != null)
                    {
                        tempMotherEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }

                    tempEnemy = null;
                    tempMotherEnemyScript = null;
                }
                else if (enemy == enemyTypes.MotherRed)
                {
                    tempEnemy = Instantiate(MotherEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempMotherEnemyScript = tempEnemy.GetComponent<MotherController>();
                    tempMotherEnemyScript.colourOverride = true;
                    tempMotherEnemyScript.randomColour = 2;
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    if (tempMotherEnemyScript != null)
                    {
                        tempMotherEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }

                    tempEnemy = null;
                    tempMotherEnemyScript = null;
                }
                else if (enemy == enemyTypes.MotherYellow)
                {
                    tempEnemy = Instantiate(MotherEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempMotherEnemyScript = tempEnemy.GetComponent<MotherController>();
                    tempMotherEnemyScript.colourOverride = true;
                    tempMotherEnemyScript.randomColour = 3;
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    if (tempMotherEnemyScript != null)
                    {
                        tempMotherEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }

                    tempEnemy = null;
                    tempMotherEnemyScript = null;
                }
                else if (enemy == enemyTypes.MotherRandom)
                {
                    tempEnemy = Instantiate(MotherEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempMotherEnemyScript = tempEnemy.GetComponent<MotherController>();
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    if (tempMotherEnemyScript != null)
                    {
                        tempMotherEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }

                    tempEnemy = null;
                    tempMotherEnemyScript = null;
                }
                else if (enemy == enemyTypes.BossEnemy)
                {
                    tempEnemy = Instantiate(BossEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                    tempBossEnemyScript = tempEnemy.GetComponent<BossController>();
                    enemyManagerScript.AddExtraBoolToProjectorsScript();
                    enemyManagerScript.enemyList.Add(tempEnemy);
                    ThisSpawnpointsEnemyList.Add(tempEnemy);
                    if (tempBossEnemyScript != null)
                    {
                        tempBossEnemyScript.thisEnemiesSpawnPoint = gameObject;
                    }

                    tempEnemy = null;
                    tempBossEnemyScript = null;
                }
            }

            //haveAllEnemiesSpawned = true;
        } else if (haveAllEnemiesSpawned==true)
        {
            //haveAllEnemiesSpawned = false;
        }
        
    }

    public void ToggleAggro()
    {
        foreach (GameObject enemy in ThisSpawnpointsEnemyList)
        {
            if (enemy.GetComponent<StandardEnemyBehaviour>() != null)
            {
                enemy.GetComponent<StandardEnemyBehaviour>().isAggroPlayer = true;
            }

            if (enemy.GetComponentInChildren<SpiderEnemyController>() != null)
            {
                enemy.GetComponentInChildren<SpiderEnemyController>().isAggroPlayer = true;
            }
            if (enemy.GetComponentsInChildren<SnakeEnemyScript>() != null)
            {
                foreach (SnakeEnemyScript enemySnake in enemy.GetComponentsInChildren<SnakeEnemyScript>())
                {
                    enemySnake.isAggroPlayer = true;
                }
            }

            if (enemy.GetComponent<FastEnemy>() != null)
            {
                enemy.GetComponent<FastEnemy>().isAggroPlayer = true;
            }

            if (enemy.GetComponent<MotherController>() != null)
            {
                enemy.GetComponent<MotherController>().isAggroPlayer = true;
            }

            if(enemy.GetComponent<BossController>()!=null)
            {
                enemy.GetComponent<BossController>().isAggroPlayer = true;
            }
        }
    }

    public void PurgeEnemies()
    {
        foreach (GameObject enemy in ThisSpawnpointsEnemyList.ToList())
        {
            if (enemy.GetComponent<StandardEnemyBehaviour>() != null)
            {
                enemyManagerScript.enemyList.Remove(enemy);
                ThisSpawnpointsEnemyList.Remove(enemy);
                Destroy(enemy);
            }

            if (enemy.GetComponentInChildren<SpiderEnemyController>() != null)
            {
                enemyManagerScript.enemyList.Remove(enemy);
                ThisSpawnpointsEnemyList.Remove(enemy);
                Destroy(enemy);
            }
            if (enemy.GetComponentsInChildren<SnakeEnemyScript>() != null)
            {
                foreach (SnakeEnemyScript enemySnake in enemy.GetComponentsInChildren<SnakeEnemyScript>())
                {
                    enemyManagerScript.enemyList.Remove(enemySnake.gameObject);
                    ThisSpawnpointsEnemyList.Remove(enemySnake.gameObject);
                    Destroy(enemySnake.gameObject);
                }
            }

            if (enemy.GetComponent<FastEnemy>() != null)
            {
                enemyManagerScript.enemyList.Remove(enemy);
                ThisSpawnpointsEnemyList.Remove(enemy);
                Destroy(enemy);
            }

            if (enemy.GetComponent<MotherController>() != null)
            {
                enemyManagerScript.enemyList.Remove(enemy);
                foreach (MotherSpawnPoint spawnPoint in enemy.GetComponent<MotherController>().spawnPointList)
                {
                    spawnPoint.PurgeEnemies();
                }
                ThisSpawnpointsEnemyList.Remove(enemy);
                Destroy(enemy);
            }

        }
    }
}
