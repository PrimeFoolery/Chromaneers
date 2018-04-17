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
    public Transform spawnPointDTransform;
    public Transform spawnPointETransform;
    public Transform spawnPointFTransform;
    public Transform spawnPointGTransform;
    public Transform spawnPointHTransform;
    public Transform spawnPointITransform;
    public Transform spawnPointJTransform;

    private bool hasSpawnASpawned = false;
    private bool hasSpawnBSpawned = false;
    private bool hasSpawnCSpawned = false;
    private bool hasSpawnDSpawned = false;
    private bool hasSpawnESpawned = false;
    private bool hasSpawnFSpawned = false;
    private bool hasSpawnGSpawned = false;
    private bool hasSpawnHSpawned = false;
    private bool hasSpawnISpawned = false;
    private bool hasSpawnJSpawned = false;

    public List<GameObject> pointAEnemyList = new List<GameObject>();
    public List<GameObject> pointBEnemyList = new List<GameObject>();
    public List<GameObject> pointCEnemyList = new List<GameObject>();
    public List<GameObject> pointDEnemyList = new List<GameObject>();
    public List<GameObject> pointEEnemyList = new List<GameObject>();
    public List<GameObject> pointFEnemyList = new List<GameObject>();
    public List<GameObject> pointGEnemyList = new List<GameObject>();
    public List<GameObject> pointHEnemyList = new List<GameObject>();
    public List<GameObject> pointIEnemyList = new List<GameObject>();
    public List<GameObject> pointJEnemyList = new List<GameObject>();


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

	public GameObject tempEnemy;
    private StandardEnemyBehaviour tempStandardEnemyBehaviour;
    private SpiderEnemyController tempSpiderEnemyController;
    private SnakeEnemyScript tempSnakeEnemyScript;
    private FastEnemy tempFastEnemyScript;
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
        //DIFFERENT SPAWN CHOICES

        //SpawnStandardBlueEnemy("[SPAWNPOINTLETTER]");
        //SpawnStandardRedEnemy("[SPAWNPOINTLETTER]");
        //SpawnStandardYellowEnemy("[SPAWNPOINTLETTER]");

        //SpawnStandardGreenEnemy("[SPAWNPOINTLETTER]");
        //SpawnStandardOrangeEnemy("[SPAWNPOINTLETTER]");
        //SpawnStandardPurpleEnemy("[SPAWNPOINTLETTER]");

        //SpawnSpiderEnemy("[SPAWNPOINTLETTER]");

        //SpawnSnakeEnemy("[SPAWNPOINTLETTER]" , "random/blue/red/yellow/MAXRANDOM");

        //SpawnFastEnemy("[SPAWNPOINTLETTER]", "random/blue/red/yellow");

        //PS SPAWNPOINTLETTER NEEDS TO BE A CAPITAL LETTER FROM A-J AS SO FAR ONLY 10 SPAWN POINTS ARE SUPPORTED

        
	    
        

        
	}

    public void SpawnGroupA()
    {
        if (hasSpawnASpawned==false)
        {
            //SPAWN POINT A ENEMIES
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "random");
            SpawnFastEnemy("A", "red");
            SpawnFastEnemy("A", "red");
            SpawnFastEnemy("A", "red");
            hasSpawnASpawned = true;
        }
    }
    public void SpawnGroupB()
    {
        if (hasSpawnBSpawned == false)
        {
            //SPAWN POINT B ENEMIES

            SpawnSpiderEnemy("B");
            SpawnStandardRedEnemy("B");
            SpawnStandardBlueEnemy("B");
            SpawnStandardYellowEnemy("B");
            hasSpawnBSpawned = true;
        }
    }
    public void SpawnGroupC()
    {
        if (hasSpawnCSpawned == false)
        {
            //SPAWN POINT C ENEMIES

            //SpawnSnakeEnemy("C", "blue");
            SpawnFastEnemy("C", "red");
            SpawnFastEnemy("C", "red");
            SpawnFastEnemy("C", "red");
            SpawnFastEnemy("C", "blue");
            SpawnFastEnemy("C", "blue");
			SpawnFastEnemy("C", "blue");
			SpawnFastEnemy("C", "yellow");
			SpawnFastEnemy("C", "yellow");
			SpawnFastEnemy("C", "yellow");
            hasSpawnCSpawned = true;
        }
    }
    public void SpawnGroupD()
    {
        if (hasSpawnDSpawned == false)
        {
            //SPAWN POINT D ENEMIES

            SpawnStandardGreenEnemy("D");
			SpawnStandardPurpleEnemy("D");
			SpawnStandardOrangeEnemy("D");
            SpawnStandardBlueEnemy("D");
            SpawnStandardRedEnemy("D");
			SpawnStandardYellowEnemy("D");
			SpawnStandardBlueEnemy("D");
			SpawnStandardRedEnemy("D");
			SpawnStandardYellowEnemy("D");
			SpawnStandardBlueEnemy("D");
			SpawnStandardRedEnemy("D");
			SpawnStandardYellowEnemy("D");
            hasSpawnDSpawned = true;
        }
    }
    public void SpawnGroupE()
    {
        if (hasSpawnESpawned == false)
        {
            //SPAWN POINT E ENEMIES
			SpawnSpiderEnemy("E");
			SpawnSpiderEnemy("E");
			SpawnSpiderEnemy("E");
			SpawnSpiderEnemy("E");
			SpawnFastEnemy("E", "random");
			SpawnFastEnemy("E", "random");
			SpawnFastEnemy("E", "random");
			SpawnFastEnemy("E", "random");
			SpawnFastEnemy("E", "random");
			SpawnFastEnemy("E", "random");
			/*
            SpawnStandardGreenEnemy("E");
            SpawnStandardOrangeEnemy("E");
            SpawnStandardPurpleEnemy("E");
            */
            hasSpawnESpawned = true;
        }
    }
    public void SpawnGroupF()
    {
        if (hasSpawnFSpawned == false)
        {
            //SPAWN POINT F ENEMIES
			SpawnStandardPurpleEnemy("F");
			SpawnStandardOrangeEnemy("F");
			SpawnStandardBlueEnemy("F");
			SpawnStandardRedEnemy("F");
			SpawnStandardYellowEnemy("F");
			SpawnStandardBlueEnemy("F");
			SpawnStandardRedEnemy("F");
			SpawnStandardYellowEnemy("F");
			/*
            SpawnSnakeEnemy("F", "MAXRANDOM");
            */
            hasSpawnFSpawned = true;
        }
    }
    public void SpawnGroupG()
    {
        if (hasSpawnGSpawned == false)
        {
            //SPAWN POINT G ENEMIES
			SpawnSnakeEnemy("G", "MAXRANDOM");
			SpawnSnakeEnemy("G", "MAXRANDOM");
			SpawnSnakeEnemy("G", "MAXRANDOM");
			/*
            SpawnStandardBlueEnemy("G");
            SpawnStandardRedEnemy("G");
            SpawnStandardYellowEnemy("G");
            SpawnFastEnemy("G", "random");
            SpawnFastEnemy("G", "random");
            SpawnFastEnemy("G", "random");
            SpawnFastEnemy("G", "random");
            SpawnFastEnemy("G", "random");
            SpawnFastEnemy("G", "random");
            */
            hasSpawnGSpawned = true;
        }
    }
    public void SpawnGroupH()
    {
        if (hasSpawnHSpawned == false)
        {
            //SPAWN POINT H ENEMIES
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "random");
                SpawnFastEnemy("D", "red");
                SpawnFastEnemy("D", "red");
                SpawnFastEnemy("D", "red");
                

            SpawnSnakeEnemy("H", "MAXRANDOM");
            SpawnSnakeEnemy("H", "MAXRANDOM");

            hasSpawnHSpawned = true;
        }
    }
    public void SpawnGroupI()
    {
        if (hasSpawnISpawned == false)
        {
            //SPAWN POINT I ENEMIES

            SpawnSpiderEnemy("I");
            SpawnSpiderEnemy("I");
            SpawnSpiderEnemy("I");
            hasSpawnISpawned = true;
        }
    }
    public void SpawnGroupJ()
    {
        if (hasSpawnJSpawned == false)
        {
            //SPAWN POINT J ENEMIES
            
            SpawnStandardGreenEnemy("J");
            SpawnFastEnemy("J", "yellow");
            hasSpawnJSpawned = true;
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

                if (enemy.GetComponent<FastEnemy>()!=null)
                {
                    enemy.GetComponent<FastEnemy>().isAggroPlayer = true;
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
                if (enemy.GetComponent<FastEnemy>() != null)
                {
                    enemy.GetComponent<FastEnemy>().isAggroPlayer = true;
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
                if (enemy.GetComponent<FastEnemy>() != null)
                {
                    enemy.GetComponent<FastEnemy>().isAggroPlayer = true;
                }

            }
        }
        if (enemyGroup == "D")
        {
            foreach (GameObject enemy in pointDEnemyList)
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

            }
        }
        if (enemyGroup == "E")
        {
            foreach (GameObject enemy in pointEEnemyList)
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

            }
        }
        if (enemyGroup == "F")
        {
            foreach (GameObject enemy in pointFEnemyList)
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

            }
        }
        if (enemyGroup == "G")
        {
            foreach (GameObject enemy in pointGEnemyList)
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

            }
        }
        if (enemyGroup == "H")
        {
            foreach (GameObject enemy in pointHEnemyList)
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

            }
        }
        if (enemyGroup == "I")
        {
            foreach (GameObject enemy in pointIEnemyList)
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

            }
        }
        if (enemyGroup == "J")
        {
            foreach (GameObject enemy in pointJEnemyList)
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

            }
        }
    }

    void SpawnStandardBlueEnemy(string spawnPoint)
    {
        if (spawnPoint=="A")
        {
            tempEnemy = Instantiate(BlueEnemy, spawnPointATransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointAEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "A";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }else
        if (spawnPoint == "B")
        {
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
            tempStandardEnemyBehaviour = null;
        }else
        if (spawnPoint == "C")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "D")
        {
            tempEnemy = Instantiate(BlueEnemy, spawnPointDTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointDEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "D";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "E")
        {
            tempEnemy = Instantiate(BlueEnemy, spawnPointETransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointEEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "E";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "F")
        {
            tempEnemy = Instantiate(BlueEnemy, spawnPointFTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointFEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "F";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "G")
        {
            tempEnemy = Instantiate(BlueEnemy, spawnPointGTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointGEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "G";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "H")
        {
            tempEnemy = Instantiate(BlueEnemy, spawnPointHTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointHEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "H";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "I")
        {
            tempEnemy = Instantiate(BlueEnemy, spawnPointITransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointIEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "I";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "J")
        {
            tempEnemy = Instantiate(BlueEnemy, spawnPointJTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointJEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "J";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
    }
    void SpawnStandardRedEnemy(string spawnPoint)
    {
        if (spawnPoint == "A")
        {
            tempEnemy = Instantiate(RedEnemy, spawnPointATransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointAEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "A";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "B")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "C")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "D")
        {
            tempEnemy = Instantiate(RedEnemy, spawnPointDTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointDEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "D";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "E")
        {
            tempEnemy = Instantiate(RedEnemy, spawnPointETransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointEEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "E";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "F")
        {
            tempEnemy = Instantiate(RedEnemy, spawnPointFTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointFEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "F";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "G")
        {
            tempEnemy = Instantiate(RedEnemy, spawnPointGTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointGEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "G";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "H")
        {
            tempEnemy = Instantiate(RedEnemy, spawnPointHTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointHEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "H";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "I")
        {
            tempEnemy = Instantiate(RedEnemy, spawnPointITransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointIEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "I";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "J")
        {
            tempEnemy = Instantiate(RedEnemy, spawnPointJTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointJEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "J";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
    }
    void SpawnStandardYellowEnemy(string spawnPoint)
    {
        if (spawnPoint == "A")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "B")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "C")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "D")
        {
            tempEnemy = Instantiate(YellowEnemy, spawnPointDTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointDEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "D";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "E")
        {
            tempEnemy = Instantiate(YellowEnemy, spawnPointETransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointEEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "E";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "F")
        {
            tempEnemy = Instantiate(YellowEnemy, spawnPointFTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointFEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "F";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "G")
        {
            tempEnemy = Instantiate(YellowEnemy, spawnPointGTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointGEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "G";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "H")
        {
            tempEnemy = Instantiate(YellowEnemy, spawnPointHTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointHEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "H";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "I")
        {
            tempEnemy = Instantiate(YellowEnemy, spawnPointITransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointIEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "I";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "J")
        {
            tempEnemy = Instantiate(YellowEnemy, spawnPointJTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointJEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "J";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
    }
    void SpawnStandardPurpleEnemy(string spawnPoint)
    {
        if (spawnPoint == "A")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "B")
        {
            tempEnemy = Instantiate(PurpleEnemy, spawnPointBTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointBEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "B";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "C")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "D")
        {
            tempEnemy = Instantiate(PurpleEnemy, spawnPointDTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointDEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "D";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "E")
        {
            tempEnemy = Instantiate(PurpleEnemy, spawnPointETransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointEEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "E";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "F")
        {
            tempEnemy = Instantiate(PurpleEnemy, spawnPointFTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointFEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "F";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "G")
        {
            tempEnemy = Instantiate(PurpleEnemy, spawnPointGTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointGEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "G";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "H")
        {
            tempEnemy = Instantiate(PurpleEnemy, spawnPointHTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointHEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "H";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "I")
        {
            tempEnemy = Instantiate(PurpleEnemy, spawnPointITransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointIEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "I";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "J")
        {
            tempEnemy = Instantiate(PurpleEnemy, spawnPointJTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointJEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "J";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
    }
    void SpawnStandardGreenEnemy(string spawnPoint)
    {
        if (spawnPoint == "A")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "B")
        {
            tempEnemy = Instantiate(GreenEnemy, spawnPointBTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointBEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "B";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "C")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "D")
        {
            tempEnemy = Instantiate(GreenEnemy, spawnPointDTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointDEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "D";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "E")
        {
            tempEnemy = Instantiate(GreenEnemy, spawnPointETransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointEEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "E";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "F")
        {
            tempEnemy = Instantiate(GreenEnemy, spawnPointFTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointFEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "F";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "G")
        {
            tempEnemy = Instantiate(GreenEnemy, spawnPointGTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointGEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "G";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "H")
        {
            tempEnemy = Instantiate(GreenEnemy, spawnPointHTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointHEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "H";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "I")
        {
            tempEnemy = Instantiate(GreenEnemy, spawnPointITransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointIEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "I";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "J")
        {
            tempEnemy = Instantiate(GreenEnemy, spawnPointJTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointJEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "J";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
    }
    void SpawnStandardOrangeEnemy(string spawnPoint)
    {
        if (spawnPoint == "A")
        {
            tempEnemy = Instantiate(OrangeEnemy, spawnPointATransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointAEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "A";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "B")
        {
            tempEnemy = Instantiate(OrangeEnemy, spawnPointBTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointBEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "B";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "C")
        {
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
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "J")
        {
            tempEnemy = Instantiate(OrangeEnemy, spawnPointJTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointJEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "J";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "D")
        {
            tempEnemy = Instantiate(OrangeEnemy, spawnPointDTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointDEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "D";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "E")
        {
            tempEnemy = Instantiate(OrangeEnemy, spawnPointETransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointEEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "E";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "F")
        {
            tempEnemy = Instantiate(OrangeEnemy, spawnPointFTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointFEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "F";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "G")
        {
            tempEnemy = Instantiate(OrangeEnemy, spawnPointGTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointGEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "G";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "H")
        {
            tempEnemy = Instantiate(OrangeEnemy, spawnPointHTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointHEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "H";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
        else
        if (spawnPoint == "I")
        {
            tempEnemy = Instantiate(OrangeEnemy, spawnPointITransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointIEnemyList.Add(tempEnemy);
            tempStandardEnemyBehaviour = tempEnemy.GetComponent<StandardEnemyBehaviour>();
            if (tempStandardEnemyBehaviour != null)
            {
                tempStandardEnemyBehaviour.thisEnemiesSpawnPoint = "I";
            }
            tempEnemy = null;
            tempStandardEnemyBehaviour = null;
        }
    }

    void SpawnSpiderEnemy(string spawnPoint)
    {
        if(spawnPoint=="A")
        {
            tempEnemy = Instantiate(SpiderEnemy, spawnPointATransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointAEnemyList.Add(tempEnemy);
            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
            if (tempSpiderEnemyController != null)
            {
                tempSpiderEnemyController.thisEnemiesSpawnPoint = "A";
            }
            tempEnemy = null;
            tempSpiderEnemyController = null;
        } else if (spawnPoint=="B")
        {
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
        }
        else if (spawnPoint == "C")
        {
            tempEnemy = Instantiate(SpiderEnemy, spawnPointCTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointCEnemyList.Add(tempEnemy);
            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
            if (tempSpiderEnemyController != null)
            {
                tempSpiderEnemyController.thisEnemiesSpawnPoint = "C";
            }
            tempEnemy = null;
            tempSpiderEnemyController = null;
        }
        else if (spawnPoint == "D")
        {
            tempEnemy = Instantiate(SpiderEnemy, spawnPointDTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointDEnemyList.Add(tempEnemy);
            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
            if (tempSpiderEnemyController != null)
            {
                tempSpiderEnemyController.thisEnemiesSpawnPoint = "D";
            }
            tempEnemy = null;
            tempSpiderEnemyController = null;
        }
        else if (spawnPoint == "E")
        {
            tempEnemy = Instantiate(SpiderEnemy, spawnPointETransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointEEnemyList.Add(tempEnemy);
            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
            if (tempSpiderEnemyController != null)
            {
                tempSpiderEnemyController.thisEnemiesSpawnPoint = "E";
            }
            tempEnemy = null;
            tempSpiderEnemyController = null;
        }
        else if (spawnPoint == "F")
        {
            tempEnemy = Instantiate(SpiderEnemy, spawnPointFTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointFEnemyList.Add(tempEnemy);
            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
            if (tempSpiderEnemyController != null)
            {
                tempSpiderEnemyController.thisEnemiesSpawnPoint = "F";
            }
            tempEnemy = null;
            tempSpiderEnemyController = null;
        }
        else if (spawnPoint == "G")
        {
            tempEnemy = Instantiate(SpiderEnemy, spawnPointGTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointGEnemyList.Add(tempEnemy);
            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
            if (tempSpiderEnemyController != null)
            {
                tempSpiderEnemyController.thisEnemiesSpawnPoint = "G";
            }
            tempEnemy = null;
            tempSpiderEnemyController = null;
        }
        else if (spawnPoint == "H")
        {
            tempEnemy = Instantiate(SpiderEnemy, spawnPointHTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointHEnemyList.Add(tempEnemy);
            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
            if (tempSpiderEnemyController != null)
            {
                tempSpiderEnemyController.thisEnemiesSpawnPoint = "H";
            }
            tempEnemy = null;
            tempSpiderEnemyController = null;
        }
        else if (spawnPoint == "I")
        {
            tempEnemy = Instantiate(SpiderEnemy, spawnPointITransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointIEnemyList.Add(tempEnemy);
            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
            if (tempSpiderEnemyController != null)
            {
                tempSpiderEnemyController.thisEnemiesSpawnPoint = "I";
            }
            tempEnemy = null;
            tempSpiderEnemyController = null;
        }
        else if (spawnPoint == "J")
        {
            tempEnemy = Instantiate(SpiderEnemy, spawnPointJTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointJEnemyList.Add(tempEnemy);
            tempSpiderEnemyController = tempEnemy.GetComponentInChildren<SpiderEnemyController>();
            if (tempSpiderEnemyController != null)
            {
                tempSpiderEnemyController.thisEnemiesSpawnPoint = "J";
            }
            tempEnemy = null;
            tempSpiderEnemyController = null;
        }
    }
    void SpawnSnakeEnemy(string spawnPoint, string colourOfSnake)
    {
        
            if (spawnPoint == "A")
            {
                tempEnemy = Instantiate(SnakeEnemy, spawnPointATransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointAEnemyList.Add(tempEnemy);
                if (colourOfSnake=="random")
                {
                    tempSnakeEnemyScript.RandomAllSameColour();
                } else if (colourOfSnake == "blue")
                {
                    tempSnakeEnemyScript.ChangeToBlue();
                }
                else if (colourOfSnake == "red")
                {
                tempSnakeEnemyScript.ChangeToRed();
            }
                else if (colourOfSnake == "yellow")
                {
                tempSnakeEnemyScript.ChangeToYellow();
            }
                else if (colourOfSnake == "MAXRANDOM")
                {
                    tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                    tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
                if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "A";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }
            else if (spawnPoint == "B")
            {
                tempEnemy = Instantiate(SnakeEnemy, spawnPointBTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointBEnemyList.Add(tempEnemy);
                tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
            if (colourOfSnake == "random")
            {
                tempSnakeEnemyScript.RandomAllSameColour();
            }
            else if (colourOfSnake == "blue")
            {
                tempSnakeEnemyScript.ChangeToBlue();
            }
            else if (colourOfSnake == "red")
            {
                tempSnakeEnemyScript.ChangeToRed();
            }
            else if (colourOfSnake == "yellow")
            {
                tempSnakeEnemyScript.ChangeToYellow();
            }
            else if (colourOfSnake == "MAXRANDOM")
            {
                tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
            if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "B";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }
            else if (spawnPoint == "C")
            {
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
            if (colourOfSnake == "random")
            {
                tempSnakeEnemyScript.RandomAllSameColour();
            }
            else if (colourOfSnake == "blue")
            {
                tempSnakeEnemyScript.ChangeToBlue();
            }
            else if (colourOfSnake == "red")
            {
                tempSnakeEnemyScript.ChangeToRed();
            }
            else if (colourOfSnake == "yellow")
            {
                tempSnakeEnemyScript.ChangeToYellow();
            }
            else if (colourOfSnake == "MAXRANDOM")
            {
                tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
            if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "C";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }
            else if (spawnPoint == "D")
            {
                tempEnemy = Instantiate(SnakeEnemy, spawnPointDTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointDEnemyList.Add(tempEnemy);
                tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
            if (colourOfSnake == "random")
            {
                tempSnakeEnemyScript.RandomAllSameColour();
            }
            else if (colourOfSnake == "blue")
            {
                tempSnakeEnemyScript.ChangeToBlue();
            }
            else if (colourOfSnake == "red")
            {
                tempSnakeEnemyScript.ChangeToRed();
            }
            else if (colourOfSnake == "yellow")
            {
                tempSnakeEnemyScript.ChangeToYellow();
            }
            else if (colourOfSnake == "MAXRANDOM")
            {
                tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
            if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "D";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }
            else if (spawnPoint == "E")
            {
                tempEnemy = Instantiate(SnakeEnemy, spawnPointETransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointEEnemyList.Add(tempEnemy);
                tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
            if (colourOfSnake == "random")
            {
                tempSnakeEnemyScript.RandomAllSameColour();
            }
            else if (colourOfSnake == "blue")
            {
                tempSnakeEnemyScript.ChangeToBlue();
            }
            else if (colourOfSnake == "red")
            {
                tempSnakeEnemyScript.ChangeToRed();
            }
            else if (colourOfSnake == "yellow")
            {
                tempSnakeEnemyScript.ChangeToYellow();
            }
            else if (colourOfSnake == "MAXRANDOM")
            {
                tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
            if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "E";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }
            else if (spawnPoint == "F")
            {
                tempEnemy = Instantiate(SnakeEnemy, spawnPointFTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointFEnemyList.Add(tempEnemy);
                tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
            if (colourOfSnake == "random")
            {
                tempSnakeEnemyScript.RandomAllSameColour();
            }
            else if (colourOfSnake == "blue")
            {
                tempSnakeEnemyScript.ChangeToBlue();
            }
            else if (colourOfSnake == "red")
            {
                tempSnakeEnemyScript.ChangeToRed();
            }
            else if (colourOfSnake == "yellow")
            {
                tempSnakeEnemyScript.ChangeToYellow();
            }
            else if (colourOfSnake == "MAXRANDOM")
            {
                tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
            if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "F";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }
            else if (spawnPoint == "G")
            {
                tempEnemy = Instantiate(SnakeEnemy, spawnPointGTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointGEnemyList.Add(tempEnemy);
                tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
            if (colourOfSnake == "random")
            {
                tempSnakeEnemyScript.RandomAllSameColour();
            }
            else if (colourOfSnake == "blue")
            {
                tempSnakeEnemyScript.ChangeToBlue();
            }
            else if (colourOfSnake == "red")
            {
                tempSnakeEnemyScript.ChangeToRed();
            }
            else if (colourOfSnake == "yellow")
            {
                tempSnakeEnemyScript.ChangeToYellow();
            }
            else if (colourOfSnake == "MAXRANDOM")
            {
                tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
            if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "G";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }
            else if (spawnPoint == "H")
            {
                tempEnemy = Instantiate(SnakeEnemy, spawnPointHTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointHEnemyList.Add(tempEnemy);
                tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
            if (colourOfSnake == "random")
            {
                tempSnakeEnemyScript.RandomAllSameColour();
            }
            else if (colourOfSnake == "blue")
            {
                tempSnakeEnemyScript.ChangeToBlue();
            }
            else if (colourOfSnake == "red")
            {
                tempSnakeEnemyScript.ChangeToRed();
            }
            else if (colourOfSnake == "yellow")
            {
                tempSnakeEnemyScript.ChangeToYellow();
            }
            else if (colourOfSnake == "MAXRANDOM")
            {
                tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
            if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "H";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }
            else if (spawnPoint == "I")
            {
                tempEnemy = Instantiate(SnakeEnemy, spawnPointITransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointIEnemyList.Add(tempEnemy);
                tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
            if (colourOfSnake == "random")
            {
                tempSnakeEnemyScript.RandomAllSameColour();
            }
            else if (colourOfSnake == "blue")
            {
                tempSnakeEnemyScript.ChangeToBlue();
            }
            else if (colourOfSnake == "red")
            {
                tempSnakeEnemyScript.ChangeToRed();
            }
            else if (colourOfSnake == "yellow")
            {
                tempSnakeEnemyScript.ChangeToYellow();
            }
            else if (colourOfSnake == "MAXRANDOM")
            {
                tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
            if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "I";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }
            else if (spawnPoint == "J")
            {
                tempEnemy = Instantiate(SnakeEnemy, spawnPointJTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
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
                pointJEnemyList.Add(tempEnemy);
                tempSnakeEnemyScript = tempEnemy.GetComponentInChildren<SnakeEnemyScript>();
            if (colourOfSnake == "random")
            {
                tempSnakeEnemyScript.RandomAllSameColour();
            }
            else if (colourOfSnake == "blue")
            {
                tempSnakeEnemyScript.ChangeToBlue();
            }
            else if (colourOfSnake == "red")
            {
                tempSnakeEnemyScript.ChangeToRed();
            }
            else if (colourOfSnake == "yellow")
            {
                tempSnakeEnemyScript.ChangeToYellow();
            }
            else if (colourOfSnake == "MAXRANDOM")
            {
                tempEnemy.transform.GetChild(0).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(1).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(2).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(3).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(4).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
                tempEnemy.transform.GetChild(5).gameObject.GetComponent<SnakeEnemyScript>().MaxRandom();
            }
            if (tempSnakeEnemyScript != null)
                {
                    tempSnakeEnemyScript.thisEnemiesSpawnPoint = "J";
                }
                tempEnemy = null;
                tempSnakeEnemyScript = null;
            }

        
        

    }
    void SpawnFastEnemy(string spawnPoint, string colourOfFastEnemy)
    {
            if (spawnPoint == "A")
            {
                tempEnemy = Instantiate(FastEnemy, spawnPointATransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                if (colourOfFastEnemy == "random")
                {

                }
                else if (colourOfFastEnemy == "blue")
                {
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 1;
                }
                else if (colourOfFastEnemy == "red")
                {
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 2;
                }
                else if (colourOfFastEnemy == "yellow")
                {
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 3;
                }
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                pointAEnemyList.Add(tempEnemy);
                if (tempFastEnemyScript != null)
                {
                    tempFastEnemyScript.thisEnemiesSpawnPoint = "A";
                }
                tempEnemy = null;
                tempFastEnemyScript = null;
            }
            else
        if (spawnPoint == "B")
            {
                tempEnemy = Instantiate(FastEnemy, spawnPointBTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                if (colourOfFastEnemy == "random")
                {

                }
                else if (colourOfFastEnemy == "blue")
                {
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 1;
                }
                else if (colourOfFastEnemy == "red")
                {
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 2;
                }
                else if (colourOfFastEnemy == "yellow")
                {
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 3;
                }
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                pointBEnemyList.Add(tempEnemy);
                if (tempFastEnemyScript != null)
                {
                    tempFastEnemyScript.thisEnemiesSpawnPoint = "B";
                }
                tempEnemy = null;
                tempFastEnemyScript = null;
            }
            else
        if (spawnPoint == "C")
            {
                tempEnemy = Instantiate(FastEnemy, spawnPointCTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
                tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
                if (colourOfFastEnemy == "random")
                {

                }
                else if (colourOfFastEnemy == "blue")
                {
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 1;
                }
                else if (colourOfFastEnemy == "red")
                {
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 2;
                }
                else if (colourOfFastEnemy == "yellow")
                {
                    tempFastEnemyScript.colourOverride = true;
                    tempFastEnemyScript.randomColour = 3;
                }
                enemyManagerScript.AddExtraBoolToProjectorsScript();
                enemyManagerScript.enemyList.Add(tempEnemy);
                pointCEnemyList.Add(tempEnemy);
                if (tempFastEnemyScript != null)
                {
                    tempFastEnemyScript.thisEnemiesSpawnPoint = "C";
                }
                tempEnemy = null;
                tempFastEnemyScript = null;
            }
        else
        if (spawnPoint == "D")
        {
            tempEnemy = Instantiate(FastEnemy, spawnPointDTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
            if (colourOfFastEnemy == "random")
            {

            }
            else if (colourOfFastEnemy == "blue")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 1;
            }
            else if (colourOfFastEnemy == "red")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 2;
            }
            else if (colourOfFastEnemy == "yellow")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 3;
            }
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointDEnemyList.Add(tempEnemy);
            if (tempFastEnemyScript != null)
            {
                tempFastEnemyScript.thisEnemiesSpawnPoint = "D";
            }
            tempEnemy = null;
            tempFastEnemyScript = null;
        }
        else
        if (spawnPoint == "E")
        {
            tempEnemy = Instantiate(FastEnemy, spawnPointETransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
            if (colourOfFastEnemy == "random")
            {

            }
            else if (colourOfFastEnemy == "blue")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 1;
            }
            else if (colourOfFastEnemy == "red")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 2;
            }
            else if (colourOfFastEnemy == "yellow")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 3;
            }
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointEEnemyList.Add(tempEnemy);
            if (tempFastEnemyScript != null)
            {
                tempFastEnemyScript.thisEnemiesSpawnPoint = "E";
            }
            tempEnemy = null;
            tempFastEnemyScript = null;
        }
        else
        if (spawnPoint == "F")
        {
            tempEnemy = Instantiate(FastEnemy, spawnPointFTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
            if (colourOfFastEnemy == "random")
            {

            }
            else if (colourOfFastEnemy == "blue")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 1;
            }
            else if (colourOfFastEnemy == "red")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 2;
            }
            else if (colourOfFastEnemy == "yellow")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 3;
            }
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointFEnemyList.Add(tempEnemy);
            if (tempFastEnemyScript != null)
            {
                tempFastEnemyScript.thisEnemiesSpawnPoint = "F";
            }
            tempEnemy = null;
            tempFastEnemyScript = null;
        }
        else
        if (spawnPoint == "G")
        {
            tempEnemy = Instantiate(FastEnemy, spawnPointGTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
            if (colourOfFastEnemy == "random")
            {

            }
            else if (colourOfFastEnemy == "blue")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 1;
            }
            else if (colourOfFastEnemy == "red")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 2;
            }
            else if (colourOfFastEnemy == "yellow")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 3;
            }
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointGEnemyList.Add(tempEnemy);
            if (tempFastEnemyScript != null)
            {
                tempFastEnemyScript.thisEnemiesSpawnPoint = "G";
            }
            tempEnemy = null;
            tempFastEnemyScript = null;
        }
        else
        if (spawnPoint == "H")
        {
            tempEnemy = Instantiate(FastEnemy, spawnPointHTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
            if (colourOfFastEnemy == "random")
            {

            }
            else if (colourOfFastEnemy == "blue")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 1;
            }
            else if (colourOfFastEnemy == "red")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 2;
            }
            else if (colourOfFastEnemy == "yellow")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 3;
            }
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointHEnemyList.Add(tempEnemy);
            if (tempFastEnemyScript != null)
            {
                tempFastEnemyScript.thisEnemiesSpawnPoint = "H";
            }
            tempEnemy = null;
            tempFastEnemyScript = null;
        }
        else
        if (spawnPoint == "I")
        {
            tempEnemy = Instantiate(FastEnemy, spawnPointITransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
            if (colourOfFastEnemy == "random")
            {

            }
            else if (colourOfFastEnemy == "blue")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 1;
            }
            else if (colourOfFastEnemy == "red")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 2;
            }
            else if (colourOfFastEnemy == "yellow")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 3;
            }
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointIEnemyList.Add(tempEnemy);
            if (tempFastEnemyScript != null)
            {
                tempFastEnemyScript.thisEnemiesSpawnPoint = "I";
            }
            tempEnemy = null;
            tempFastEnemyScript = null;
        }
        else
        if (spawnPoint == "J")
        {
            tempEnemy = Instantiate(FastEnemy, spawnPointJTransform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f)), Quaternion.identity);
            tempFastEnemyScript = tempEnemy.GetComponent<FastEnemy>();
            if (colourOfFastEnemy == "random")
            {

            }
            else if (colourOfFastEnemy == "blue")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 1;
            }
            else if (colourOfFastEnemy == "red")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 2;
            }
            else if (colourOfFastEnemy == "yellow")
            {
                tempFastEnemyScript.colourOverride = true;
                tempFastEnemyScript.randomColour = 3;
            }
            enemyManagerScript.AddExtraBoolToProjectorsScript();
            enemyManagerScript.enemyList.Add(tempEnemy);
            pointJEnemyList.Add(tempEnemy);
            if (tempFastEnemyScript != null)
            {
                tempFastEnemyScript.thisEnemiesSpawnPoint = "J";
            }
            tempEnemy = null;
            tempFastEnemyScript = null;
        }
    }
}
    


