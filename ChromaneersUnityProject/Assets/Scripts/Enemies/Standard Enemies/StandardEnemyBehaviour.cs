﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandardEnemyBehaviour : MonoBehaviour {

    [Header("Singleplayer Variables")]
    public GameObject player;
    public GameObject targetPlayer;
    public bool isAggroPlayer = false;
    NavMeshAgent agent;
    private BlueEnemyHealth blueHealth;
    private YellowEnemyHealth yellowHealth;
    private OrangeEnemyHealth orangeHealth;
    private PurpleEnemyHealth purpleHealth;
    private GreenEnemyHealth greenHealth;
    private RedEnemyHealth redHealth;
    private float poisonTimer = 4f;

    public Renderer SphereRenderer;

    //COOP PLAYER VARIABLES
    [Header("Coop Variables")]
    private GameObject RedPlayer;
    private GameObject BluePlayer;
    private GameObject YellowPlayer;
    private float retargetingDelay = 3f;
    private bool readyToRetarget = true;
    bool redPaintLock = false;

    [Header("Enemy Animators")]
    public Animator anim;

    private bool colourBlindModeActive = false;
    public GameObject cbRedIndicator;
    public GameObject cbYellowIndicator;
    public GameObject cbBlueIndicator;
    public GameObject cbPurpleIndicator;
    public GameObject cbGreenIndicator;
    public GameObject cbOrangeIndicator;
    private GameObject cbCurrentIndicator;

    [Header("Misc")]
    public bool isItCoop;
    private ColourSelectManager gameManager;
    private EnemySpawner spawner;
    public GameObject thisEnemiesSpawnPoint;
    public int enemyDamage;

    public string colourOfEnemy;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>();
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemySpawner>();
        if (gameManager.isItSingleplayer==true) {
            isItCoop = false;
        }
        if (gameManager.isItSingleplayer == false) {
            isItCoop = true;
        }

        if (gameManager.colourBlindMode == true)
        {
            SpawnColourBlindIndicator();
            colourBlindModeActive = true;
        }
        else
        {
            colourBlindModeActive = false;
        }
        if (!isItCoop) {
			player = GameObject.FindGameObjectWithTag ("Player");
            agent = gameObject.GetComponent<NavMeshAgent>();
            targetPlayer = player;
		}
		if (isItCoop) {
			agent = gameObject.GetComponent<NavMeshAgent> ();
            RedPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
            BluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
            YellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
            FindClosestPlayer();
        }

        
        blueHealth = gameObject.GetComponent<BlueEnemyHealth>();
        purpleHealth = gameObject.GetComponent<PurpleEnemyHealth>();
        redHealth = gameObject.GetComponent<RedEnemyHealth>();
        orangeHealth = gameObject.GetComponent<OrangeEnemyHealth>();
        yellowHealth = gameObject.GetComponent<YellowEnemyHealth>();
        greenHealth = gameObject.GetComponent<GreenEnemyHealth>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameManager.isItSingleplayer == true) {
            isItCoop = false;
        }
        if (gameManager.isItSingleplayer == false) {
            isItCoop = true;
        }
               
        if (!isItCoop) {
            if (player==null) {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            else if (player!=null) {
                if (Vector3.Distance(transform.position,player.transform.position)<25f&&isAggroPlayer==false)
                {
                    AggroToggle();
                }

                if (isAggroPlayer == true)
                {
                    agent.SetDestination(targetPlayer.transform.position);
                }
                
            }
			
		}
		if (isItCoop) {
		    if (isAggroPlayer==false&&(Vector3.Distance(transform.position,RedPlayer.transform.position)<25f||Vector3.Distance(transform.position,BluePlayer.transform.position)<25f|| Vector3.Distance(transform.position, YellowPlayer.transform.position) < 25f))
		    {
		        AggroToggle();
            }
		    if (isAggroPlayer == true)
		    {
                anim.SetBool("isIdle", false);
                if (retargetingDelay == 3f)
		        {
		            FindClosestPlayer();
		        }
		        agent.SetDestination(targetPlayer.transform.position);
                
            }
            

		}

	    if (Input.GetKeyUp(KeyCode.F1))
	    {
	        if (colourBlindModeActive == false) 
	        {
                SpawnColourBlindIndicator();
	            colourBlindModeActive = true;
            }
	        else
	        {
                Destroy(cbCurrentIndicator);
	            colourBlindModeActive = false;
	        }
	    }
        if (readyToRetarget==false)//DELAYS THE RETARGETING TO STOP PLAYER TARGET SWAPPING
        {
            retargetingDelay -= Time.deltaTime;
        }
        if (retargetingDelay<=0f)
        {
            readyToRetarget = true;
            retargetingDelay = 3f;
        }
        transform.LookAt(new Vector3(targetPlayer.transform.position.x, transform.position.y, targetPlayer.transform.position.z));
	    if (gameObject.GetComponent<PaintDetectionScript>().colourOfPaint=="yellow")
	    {
	        agent.speed = 4;
	    } else
	    if (gameObject.GetComponent<PaintDetectionScript>().colourOfPaint == "blue")
	    {
	        agent.speed = 1;
	    }
	    else
	    {
	        agent.speed = 2;
	    }

	    //CONFUSION PAINT
	    if (gameObject.GetComponent<PaintDetectionScript>().colourOfPaint == "orange")
	    {
	        float XRandom = 0;
	        float ZRandom = 0;
            if (redPaintLock == false)
	        {
	            XRandom = Random.Range(-10000f, 10000f);
	            ZRandom = Random.Range(-10000f, 10000f);
	            redPaintLock = true;
	        }
            Debug.Log("ON RED");
	        GameObject tempGameObject = new GameObject("tempObject");
            tempGameObject.transform.position = new Vector3(XRandom, 0, ZRandom);
	        targetPlayer = tempGameObject;

	    }
	    else
	    {
	        redPaintLock = true;
	        if (isItCoop==false)
	        {
	            targetPlayer = player;
	        }
	        else
	        {
	            FindClosestPlayer();
            }
	    }
	    if (gameObject.GetComponent<PaintDetectionScript>().colourOfPaint == "red")
        {
            poisonTimer -= Time.deltaTime;
            if (poisonTimer <= 0)
            {
                if (blueHealth != null)
                {
                    blueHealth.EnemyDamaged(1);
                }
                else if (purpleHealth != null)
                {
                    purpleHealth.PoisonDamaged();
                }
                else if (redHealth != null)
                {
                    redHealth.EnemyDamaged(1);
                }
                else if (orangeHealth != null)
                {
                    orangeHealth.PoisonDamaged();
                }
                else if (yellowHealth != null)
                {
                    yellowHealth.EnemyDamaged(1);
                }
                else if (greenHealth != null)
                {
                    greenHealth.PoisonDamaged();
                }
                poisonTimer = 4f;
            }
        }
	    else
	    {
	        poisonTimer = 4f;
	    }
    }

    void FindClosestPlayer() {

        if (colourOfEnemy=="blue")
        {
            readyToRetarget = false;
            float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, RedPlayer.transform.position);
            float distanceBetweenEnemyAndYellowPlayer = Vector3.Distance(transform.position, YellowPlayer.transform.position);
            float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndRedPlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));
            if (closestDistance==distanceBetweenEnemyAndRedPlayer)
            {
                if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState=="Alive")
                {
                    targetPlayer = RedPlayer;
                }else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState=="Alive")
                {
                    targetPlayer = YellowPlayer;
                }else if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState=="Alive")
                {
                    targetPlayer = BluePlayer;
                }
                else
                {
                    targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f),Quaternion.identity);
                }
            }else if (closestDistance == distanceBetweenEnemyAndYellowPlayer)
            {
                if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = YellowPlayer;
                }
                else if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    targetPlayer = RedPlayer;
                }
                else if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = BluePlayer;
                }
                else
                {
                    targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                }
            }
        }
        else if (colourOfEnemy=="red")
        {
            readyToRetarget = false;
            float distanceBetweenEnemyAndBluePlayer = Vector3.Distance(transform.position, BluePlayer.transform.position);
            float distanceBetweenEnemyAndYellowPlayer = Vector3.Distance(transform.position, YellowPlayer.transform.position);
            float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndBluePlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));
            if (closestDistance == distanceBetweenEnemyAndBluePlayer)
            {
                if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = BluePlayer;
                }
                else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = YellowPlayer;
                }
                else if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    targetPlayer = RedPlayer;
                }
                else
                {
                    targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                }
            }
            else if (closestDistance == distanceBetweenEnemyAndYellowPlayer)
            {
                if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = YellowPlayer;
                }
                else if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = BluePlayer;
                }
                else if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    targetPlayer = RedPlayer;
                }
                else
                {
                    targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                }
            }
        }
        else if (colourOfEnemy == "yellow")
        {
            readyToRetarget = false;
            float distanceBetweenEnemyAndBluePlayer = Vector3.Distance(transform.position, BluePlayer.transform.position);
            float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, YellowPlayer.transform.position);
            float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndBluePlayer), Mathf.Abs(distanceBetweenEnemyAndRedPlayer));
            if (closestDistance == distanceBetweenEnemyAndBluePlayer)
            {
                if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = BluePlayer;
                }
                else if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    targetPlayer = RedPlayer;
                }
                else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = YellowPlayer;
                }
                else
                {
                    targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                }
            }
            else if (closestDistance == distanceBetweenEnemyAndRedPlayer)
            {
                if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    targetPlayer = RedPlayer;
                }
                else if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = BluePlayer;
                }
                else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = YellowPlayer;
                }
                else
                {
                    targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                }
            }
        }
        else if (colourOfEnemy == "orange")
        {
            readyToRetarget = false;
            if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState=="Alive")
            {
                targetPlayer = BluePlayer;
            }
            else
            {
                float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, RedPlayer.transform.position);
                float distanceBetweenEnemyAndYellowPlayer = Vector3.Distance(transform.position, YellowPlayer.transform.position);
                float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndRedPlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));
                if (closestDistance == distanceBetweenEnemyAndRedPlayer)
                {
                    if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                    {
                        targetPlayer = RedPlayer;
                    }
                    else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                    {
                        targetPlayer = YellowPlayer;
                    }
                    else
                    {
                        targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                    }
                }
                else if (closestDistance == distanceBetweenEnemyAndYellowPlayer)
                {
                    if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                    {
                        targetPlayer = YellowPlayer;
                    }
                    else if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                    {
                        targetPlayer = RedPlayer;
                    }
                    else
                    {
                        targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                    }
                }
            }
        }
        else if (colourOfEnemy == "green")
        {
            readyToRetarget = false;
            if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
            {
                targetPlayer = RedPlayer;
            }
            else
            {
                float distanceBetweenEnemyAndBluePlayer = Vector3.Distance(transform.position, BluePlayer.transform.position);
                float distanceBetweenEnemyAndYellowPlayer = Vector3.Distance(transform.position, YellowPlayer.transform.position);
                float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndBluePlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));
                if (closestDistance == distanceBetweenEnemyAndBluePlayer)
                {
                    if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                    {
                        targetPlayer = BluePlayer;
                    }
                    else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                    {
                        targetPlayer = YellowPlayer;
                    }
                    else
                    {
                        targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                    }
                }
                else if (closestDistance == distanceBetweenEnemyAndYellowPlayer)
                {
                    if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                    {
                        targetPlayer = YellowPlayer;
                    }
                    else if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                    {
                        targetPlayer = BluePlayer;
                    }
                    else
                    {
                        targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                    }
                }
            }
        }
        else if (colourOfEnemy == "purple")
        {
            readyToRetarget = false;
            if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
            {
                targetPlayer = YellowPlayer;
            }
            else
            {
                float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, RedPlayer.transform.position);
                float distanceBetweenEnemyAndBluePlayer = Vector3.Distance(transform.position, BluePlayer.transform.position);
                float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndRedPlayer), Mathf.Abs(distanceBetweenEnemyAndBluePlayer));
                if (closestDistance == distanceBetweenEnemyAndRedPlayer)
                {
                    if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                    {
                        targetPlayer = RedPlayer;
                    }
                    else if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                    {
                        targetPlayer = BluePlayer;
                    }
                    else
                    {
                        targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                    }
                }
                else if (closestDistance == distanceBetweenEnemyAndBluePlayer)
                {
                    if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                    {
                        targetPlayer = BluePlayer;
                    }
                    else if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                    {
                        targetPlayer = RedPlayer;
                    }
                    else
                    {
                        targetPlayer = Instantiate(new GameObject(), RandomNavmeshLocation(10f), Quaternion.identity);
                    }
                }
            }
        }/*
        readyToRetarget = false;
            //THIS CALCULATES THE DISTANCE BETWEEN THE ENEMY AND ALL OF THE PLAYERS AND THEN FINDS THE LOWEST AND SETS THE TARGETED PLAYER TO THAT
            float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, RedPlayer.transform.position);
            float distanceBetweenEnemyAndBluePlayer =
                Vector3.Distance(transform.position, BluePlayer.transform.position);
            float distanceBetweenEnemyAndYellowPlayer =
                Vector3.Distance(transform.position, YellowPlayer.transform.position);

            float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndBluePlayer),
                Mathf.Abs(distanceBetweenEnemyAndRedPlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));
            //Debug.Log(closestDistance);

            if (closestDistance == distanceBetweenEnemyAndRedPlayer)
            {
                if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                targetPlayer = RedPlayer;
                } else if (Mathf.Min(distanceBetweenEnemyAndBluePlayer,distanceBetweenEnemyAndYellowPlayer)==distanceBetweenEnemyAndBluePlayer)
                {
                    if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                    {
                        targetPlayer = BluePlayer;
                    }
                    else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                    {
                        targetPlayer = YellowPlayer;
                    }
                    else
                    {
                    agent.SetDestination(RandomNavmeshLocation(10f));
                }
                } else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = YellowPlayer;
                }
                else
                {
                agent.SetDestination(RandomNavmeshLocation(10f));
            }
               
            }
            else if (closestDistance == distanceBetweenEnemyAndBluePlayer)
            {
                if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = BluePlayer;
                }
                else if (Mathf.Min(distanceBetweenEnemyAndRedPlayer, distanceBetweenEnemyAndYellowPlayer) == distanceBetweenEnemyAndRedPlayer)
                {
                    if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                    {
                        targetPlayer = RedPlayer;
                    }
                    else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                    {
                        targetPlayer = YellowPlayer;
                    }
                    else
                    {
                    agent.SetDestination(RandomNavmeshLocation(10f));
                }
                }
                else if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = YellowPlayer;
                }
                else
                {
                agent.SetDestination(RandomNavmeshLocation(10f));
            }
            }
            else if (closestDistance == distanceBetweenEnemyAndYellowPlayer )
            {
                if (YellowPlayer.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = YellowPlayer;
                }
                else if (Mathf.Min(distanceBetweenEnemyAndRedPlayer, distanceBetweenEnemyAndBluePlayer) == distanceBetweenEnemyAndRedPlayer)
                {
                    if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                    {
                        targetPlayer = RedPlayer;
                    }
                    else if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                    {
                        targetPlayer = BluePlayer;
                    }
                    else
                    {
                        agent.SetDestination(RandomNavmeshLocation(10f));
                    }
                }
                else if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = BluePlayer;
                }
                else
                {
                    agent.SetDestination(RandomNavmeshLocation(10f));
            }
            }*/
        
    }

    void OnCollisionEnter(Collision theCol) {
        //Check if it collides with the player
        if (theCol.gameObject.CompareTag("Player")) {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<SingleplayerHealthController>().GetHit();
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<SingleplayerHealthController>().invincibility = 1f;
        }
        //Check if it collides with coop player one
        if (theCol.gameObject.CompareTag("BluePlayer")) {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<CoopCharacterHealthControllerOne>().GetHit();
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<CoopCharacterHealthControllerOne>().invincibility = 1f;
        }
        //Check if it collides with coop player two
        if (theCol.gameObject.CompareTag("RedPlayer")) {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().GetHit();
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().invincibility = 1f;
        }
        //Check if it collides with coop player three
        if (theCol.gameObject.CompareTag("YellowPlayer")) {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<CoopCharacterHealthControllerThree>().GetHit();
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<CoopCharacterHealthControllerThree>().invincibility = 1f;
        }
    }

    public void AggroToggle()
    {
        if (thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
        {
            thisEnemiesSpawnPoint.GetComponent<newSpawner>().ToggleAggro();
        }else if (thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
        {
            thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
        }
        
    }

    public void BulletKnockback(Vector3 bulletPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, bulletPosition, -0.5f);
    }
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void SpawnColourBlindIndicator()
    {
        if (colourOfEnemy == "blue")
        {
            cbCurrentIndicator = Instantiate(cbBlueIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
        else
        if (colourOfEnemy == "red")
        {
            cbCurrentIndicator = Instantiate(cbRedIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
        else
        if (colourOfEnemy == "yellow")
        {
            cbCurrentIndicator = Instantiate(cbYellowIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
        else
        if (colourOfEnemy == "purple")
        {
            cbCurrentIndicator = Instantiate(cbPurpleIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
        else
        if (colourOfEnemy == "orange")
        {
            cbCurrentIndicator = Instantiate(cbOrangeIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
        else
        if (colourOfEnemy == "green")
        {
            cbCurrentIndicator = Instantiate(cbGreenIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
    }
}
