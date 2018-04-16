using System.Collections;
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

    //COOP PLAYER VARIABLES
    [Header("Coop Variables")]
    private GameObject RedPlayer;
    private GameObject BluePlayer;
    private GameObject YellowPlayer;
    private float retargetingDelay = 5f;
    private bool readyToRetarget = true;
    bool redPaintLock = false;

    [Header("Misc")]
    public bool isItCoop;
    private ColourSelectManager gameManager;
    private EnemySpawner spawner;
    public string thisEnemiesSpawnPoint;
    public int enemyDamage;

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
                if (Vector3.Distance(transform.position,player.transform.position)<7f&&isAggroPlayer==false)
                {
                    spawner.AggroGroupOfEnemies(thisEnemiesSpawnPoint);
                }

                if (isAggroPlayer == true)
                {
                    agent.SetDestination(targetPlayer.transform.position);
                }
                
            }
			
		}
		if (isItCoop) {
		    if (isAggroPlayer==false&&(Vector3.Distance(transform.position,RedPlayer.transform.position)<7f||Vector3.Distance(transform.position,BluePlayer.transform.position)<7f|| Vector3.Distance(transform.position, YellowPlayer.transform.position) < 7f))
		    {
		        spawner.AggroGroupOfEnemies(thisEnemiesSpawnPoint);
            }
		    if (isAggroPlayer == true)
		    {
		        if (retargetingDelay == 5f)
		        {
		            FindClosestPlayer();
		        }
		        agent.SetDestination(targetPlayer.transform.position);
            }
            

		}
        if (readyToRetarget==false)//DELAYS THE RETARGETING TO STOP PLAYER TARGET SWAPPING
        {
            retargetingDelay -= Time.deltaTime;
        }
        if (retargetingDelay<=0f)
        {
            readyToRetarget = true;
            retargetingDelay = 5f;
        }
        transform.LookAt(targetPlayer.transform);
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
                    purpleHealth.EnemyDamaged(1);
                }
                else if (redHealth != null)
                {
                    redHealth.EnemyDamaged(1);
                }
                else if (orangeHealth != null)
                {
                    orangeHealth.EnemyDamaged(1);
                }
                else if (yellowHealth != null)
                {
                    yellowHealth.EnemyDamaged(1);
                }
                else if (greenHealth != null)
                {
                    greenHealth.EnemyDamaged(1);
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
        

            readyToRetarget = false;
            //THIS CALCULATES THE DISTANCE BETWEEN THE ENEMY AND ALL OF THE PLAYERS AND THEN FINDS THE LOWEST AND SETS THE TARGETED PLAYER TO THAT
            float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, RedPlayer.transform.position);
            float distanceBetweenEnemyAndBluePlayer =
                Vector3.Distance(transform.position, BluePlayer.transform.position);
            float distanceBetweenEnemyAndYellowPlayer =
                Vector3.Distance(transform.position, YellowPlayer.transform.position);

            float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndBluePlayer),
                Mathf.Abs(distanceBetweenEnemyAndRedPlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));
            Debug.Log(closestDistance);

            if (closestDistance == distanceBetweenEnemyAndRedPlayer)
            {
                targetPlayer = RedPlayer;
            }
            else if (closestDistance == distanceBetweenEnemyAndBluePlayer)
            {
                targetPlayer = BluePlayer;
            }
            else if (closestDistance == distanceBetweenEnemyAndYellowPlayer)
            {
                targetPlayer = YellowPlayer;
            }
        
    }

    void OnCollisionEnter(Collision theCol) {
        //Check if it collides with the player
        if (theCol.gameObject.CompareTag("Player")) {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<SingleplayerHealthController>().EnemyDamaged(enemyDamage);
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<SingleplayerHealthController>().invincibility = 1f;
        }
        //Check if it collides with coop player one
        if (theCol.gameObject.CompareTag("BluePlayer")) {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<CoopCharacterHealthControllerOne>().EnemyDamaged(enemyDamage);
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<CoopCharacterHealthControllerOne>().invincibility = 1f;
        }
        //Check if it collides with coop player two
        if (theCol.gameObject.CompareTag("RedPlayer")) {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().EnemyDamaged(enemyDamage);
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().invincibility = 1f;
        }
        //Check if it collides with coop player three
        if (theCol.gameObject.CompareTag("YellowPlayer")) {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<CoopCharacterHealthControllerThree>().EnemyDamaged(enemyDamage);
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<CoopCharacterHealthControllerThree>().invincibility = 1f;
        }
    }
}
