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

	public GameObject tempEnemy;
    private StandardEnemyBehaviour tempStandardEnemyBehaviour;
    private SpiderEnemyController tempSpiderEnemyController;
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
                tempStandardEnemyBehaviour = null;
                //

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
                tempStandardEnemyBehaviour = null;
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
                //ALL ENEMIES SPAWNED
                haveAllEnemiesSpawned = true;
	        }
        }
	    
        

        /*//OLD SPAWNING SYSTEM
		Timer += Time.deltaTime;

		if (Timer >= 2) {
			
			RanNumb = Random.Range (0, 6);
			if (RanNumb == 0) {
				tempEnemy = Instantiate (RedEnemy);
				enemyManagerScript.AddExtraBoolToProjectorsScript ();
				enemyManagerScript.enemyList.Add (tempEnemy);
				Timer = 0;
			}
			if (RanNumb == 1) {
				tempEnemy = Instantiate (YellowEnemy);
				enemyManagerScript.AddExtraBoolToProjectorsScript ();
				enemyManagerScript.enemyList.Add (tempEnemy);
				Timer = 0;
			}
			if (RanNumb == 2) {
				tempEnemy = Instantiate (BlueEnemy);
				enemyManagerScript.AddExtraBoolToProjectorsScript ();
				enemyManagerScript.enemyList.Add (tempEnemy);
				Timer = 0;
			}
		    if (RanNumb == 3) {
				tempEnemy = Instantiate(SpiderEnemy);
				enemyManagerScript.AddExtraBoolToProjectorsScript ();
				enemyManagerScript.enemyList.Add (tempEnemy);
		        Timer = 0;
		    }
			if(RanNumb == 4||RanNumb == 5){
				ranSecondaryEnemy = Random.Range (0, 3);
				if(ranSecondaryEnemy==0){
					tempEnemy = Instantiate (OrangeEnemy);
					enemyManagerScript.AddExtraBoolToProjectorsScript ();
					enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
				if(ranSecondaryEnemy==1){
					tempEnemy = Instantiate (GreenEnemy);
					enemyManagerScript.AddExtraBoolToProjectorsScript ();
					enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
				if(ranSecondaryEnemy==2){
					tempEnemy = Instantiate (PurpleEnemy);
					enemyManagerScript.AddExtraBoolToProjectorsScript ();
					enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
			}
			if(RanNumb == 6){
				ranShieldEnemy = Random.Range (0,3);
				if(ranShieldEnemy==0){
					tempEnemy = Instantiate (RedShieldEnemy);
					enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
				if(ranShieldEnemy==1){
					tempEnemy = Instantiate (BlueShieldEnemy);
					enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
				if(ranShieldEnemy==2){
					tempEnemy = Instantiate (YellowShieldEnemy);
					enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
			}

        }*/
	}
}
