using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour {

	public float Timer;
    private bool haveAllEnemiesSpawned = false;
    public Transform spawnPointATransform;
    public Transform spawnPointBTransform;
    public Transform spawnPointCTransform;

    public List<GameObject> pointAEnemyList = new List<GameObject>();
    public List<GameObject> pointBEnemyList = new List<GameObject>();
    public List<GameObject> pointCEnemyList = new List<GameObject>();

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

	public GameObject tempEnemy;
    private StandardEnemyBehaviour tempStandardEnemyBehaviour;
    private SpiderEnemyController tempSpiderEnemyController;
    private SnakeEnemyScript tempSnakeEnemyScript;
	private EnemyManager enemyManagerScript;

	private int RanNumb;
	private int ranShieldEnemy;
	private int ranSecondaryEnemy;

	// Use this for initialization
	void Start () {
		enemyManagerScript = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<EnemyManager> ();
	}
	
	// Update is called once per frame
	void Update () {
        
	    if (haveAllEnemiesSpawned==false)
	    {
	        if (SceneManager.GetActiveScene().name == "DemoWorld")
	        {
                //SPAWN POINT A ENEMIES
	            tempEnemy = Instantiate(RedEnemy, spawnPointATransform.position+new Vector3(Random.Range(-3f,3f),0,Random.Range(-3f,3f)), Quaternion.identity);
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
	            pointAEnemyList.Add(tempEnemy);
                tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
	            if (tempStandardEnemyBehaviour!=null)
	            {
	                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "A";
	            }
	            tempEnemy = null;
	            tempSpiderEnemyController = null;
	            tempStandardEnemyBehaviour = null;
                
                //


	            tempEnemy = Instantiate(YellowEnemy, spawnPointATransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
	            enemyManagerScript.AddExtraBoolToProjectorsScript();
	            enemyManagerScript.enemyList.Add(tempEnemy);
	            pointAEnemyList.Add(tempEnemy);
	            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
	            if (tempStandardEnemyBehaviour != null)
	            {
	                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "A";
	            }
	            tempEnemy = null;
	            tempSpiderEnemyController = null;
	            tempStandardEnemyBehaviour = null;
                //


                tempEnemy = Instantiate(GreenEnemy, spawnPointATransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
	            enemyManagerScript.AddExtraBoolToProjectorsScript();
	            enemyManagerScript.enemyList.Add(tempEnemy);
	            pointAEnemyList.Add(tempEnemy);
	            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
	            if (tempStandardEnemyBehaviour != null)
	            {
	                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "A";
	            }
	            tempEnemy = null;
	            tempSpiderEnemyController = null;
	            tempStandardEnemyBehaviour = null;
                //


                tempEnemy = Instantiate(PurpleEnemy, spawnPointATransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
	            enemyManagerScript.AddExtraBoolToProjectorsScript();
	            enemyManagerScript.enemyList.Add(tempEnemy);
	            pointAEnemyList.Add(tempEnemy);
	            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
	            if (tempStandardEnemyBehaviour != null)
	            {
	                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "A";
	            }
	            tempEnemy = null;
	            tempSpiderEnemyController = null;
	            tempStandardEnemyBehaviour = null;


                //SPAWN POINT B ENEMIES
                tempEnemy = Instantiate(SpiderEnemy, spawnPointBTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
	            enemyManagerScript.AddExtraBoolToProjectorsScript();
	            enemyManagerScript.enemyList.Add(tempEnemy);
	            pointBEnemyList.Add(tempEnemy);
	            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
	            if (tempSpiderEnemyController != null)
	            {
	                tempSpiderEnemyController.thisEnemiesSpawnPoint = "B";
	            }
	            tempEnemy = null;
	            tempSpiderEnemyController = null;
	            tempStandardEnemyBehaviour = null;
                //


                tempEnemy = Instantiate(RedEnemy, spawnPointBTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
	            enemyManagerScript.AddExtraBoolToProjectorsScript();
	            enemyManagerScript.enemyList.Add(tempEnemy);
	            pointBEnemyList.Add(tempEnemy);
	            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
	            if (tempStandardEnemyBehaviour != null)
	            {
	                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "B";
	            }
	            tempEnemy = null;
	            tempSpiderEnemyController = null;
	            tempStandardEnemyBehaviour = null;
                //


                tempEnemy = Instantiate(BlueEnemy, spawnPointBTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
	            enemyManagerScript.AddExtraBoolToProjectorsScript();
	            enemyManagerScript.enemyList.Add(tempEnemy);
	            pointBEnemyList.Add(tempEnemy);
	            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
	            if (tempStandardEnemyBehaviour != null)
	            {
	                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "B";
	            }
	            tempEnemy = null;
	            tempSpiderEnemyController = null;
	            tempStandardEnemyBehaviour = null;
                //


	            tempEnemy = Instantiate(YellowEnemy, spawnPointBTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
	            enemyManagerScript.AddExtraBoolToProjectorsScript();
	            enemyManagerScript.enemyList.Add(tempEnemy);
	            pointBEnemyList.Add(tempEnemy);
	            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
	            if (tempStandardEnemyBehaviour != null)
	            {
	                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "B";
	            }
	            tempEnemy = null;
	            tempSpiderEnemyController = null;
	            tempStandardEnemyBehaviour = null;
                //SPAWN POINT C ENEMIES

                tempEnemy = Instantiate(SnakeEnemy, spawnPointCTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointCEnemyList.Add(tempEnemy);
                tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
                if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "C";
                }
                tempEnemy = null;
                tempSpiderEnemyController = null;
                tempStandardEnemyBehaviour = null;
	            tempSnakeEnemyScript = null;
                /*
                //
                tempEnemy = Instantiate(YellowEnemy, spawnPointCTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                pointCEnemyList.Add(tempEnemy);
                tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                if (tempStandardEnemyBehaviour != null)
                {
                    tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "C";
                }
                tempEnemy = null;
                tempSpiderEnemyController = null;
                tempStandardEnemyBehaviour = null;
                //

                tempEnemy = Instantiate(RedEnemy, spawnPointCTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                pointCEnemyList.Add(tempEnemy);
                tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                if (tempStandardEnemyBehaviour != null)
                {
                    tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "C";
                }
                tempEnemy = null;
                tempSpiderEnemyController = null;
                tempStandardEnemyBehaviour = null;
                //

                tempEnemy = Instantiate(BlueEnemy, spawnPointCTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                pointCEnemyList.Add(tempEnemy);
                tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                if (tempStandardEnemyBehaviour != null)
                {
                    tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "C";
                }
                tempEnemy = null;
                tempSpiderEnemyController = null;
                tempStandardEnemyBehaviour = null;
                //

                tempEnemy = Instantiate(OrangeEnemy, spawnPointCTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                pointCEnemyList.Add(tempEnemy);
                tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                if (tempStandardEnemyBehaviour != null)
                {
                    tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "C";
                }
                tempEnemy = null;
                tempSpiderEnemyController = null;
                tempStandardEnemyBehaviour = null;
                //

                tempEnemy = Instantiate(GreenEnemy, spawnPointCTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                pointCEnemyList.Add(tempEnemy);
                tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                if (tempStandardEnemyBehaviour != null)
                {
                    tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "C";
                }
                tempEnemy = null;
                tempSpiderEnemyController = null;
                tempStandardEnemyBehaviour = null;
                //

                tempEnemy = Instantiate(PurpleEnemy, spawnPointCTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                pointCEnemyList.Add(tempEnemy);
                tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
                if (tempStandardEnemyBehaviour != null)
                {
                    tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "C";
                }
                tempEnemy = null;
                tempSpiderEnemyController = null;
                tempStandardEnemyBehaviour = null;
                */
                //ALL ENEMIES SPAWNED
                haveAllEnemiesSpawned = true;
	        }
        }
	    
        

        
	}

    public void AggroGroupOfEnemies(string enemyGroup)
    {
        if (enemyGroup=="A")
        {
            foreach (GameObject enemy in pointAEnemyList)
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

            }
        }
        if (enemyGroup == "B")
        {
            foreach (GameObject enemy in pointBEnemyList)
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

            }
        }
        if (enemyGroup == "C")
        {
            foreach (GameObject enemy in pointCEnemyList)
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

            }
        }
    }
}
