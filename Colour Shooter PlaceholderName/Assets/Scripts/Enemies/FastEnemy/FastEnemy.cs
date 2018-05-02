using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemy : MonoBehaviour {

    [Header("Singleplayer Variables")]
    public GameObject player;
    public GameObject targetPlayer;
    public bool isAggroPlayer = false;
    NavMeshAgent agent;
    private float poisonTimer = 4f;

    private int thisEnemiesHealth = 1;

    public Renderer SphereRenderer;
    public bool colourOverride = false;
    public string colourOfEnemy;
    public int randomColour;
    public Material blueMaterial;
    public GameObject blueSplat;
    public Material blueParticle;
    public Material redMaterial;
    public GameObject redSplat;
    public Material redParticle;
    public Material yellowMaterial;
    public GameObject yellowSplat;
    public Material yellowParticle;
    public GameObject enemyEmpty;

    //COOP PLAYER VARIABLES
    [Header("Coop Variables")]
    public GameObject RedPlayer;
    public GameObject BluePlayer;
    public GameObject YellowPlayer;
    private float retargetingDelay = 3f;
    private bool readyToRetarget = true;
    bool redPaintLock = false;

    [Header("Misc")]
    public bool isItCoop;
    private ColourSelectManager gameManager;
    private EnemySpawner spawner;
    private EnemyManager enemyManagerScript;
    public GameObject thisEnemiesSpawnPoint;
    public int enemyDamage;

    private GameObject mainCamera;


    // Use this for initialization
    void Start () {
        
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>();
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemySpawner>();
        enemyManagerScript = gameManager.gameObject.GetComponent<EnemyManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (gameManager.isItSingleplayer == true)
        {
            isItCoop = false;
        }
        if (gameManager.isItSingleplayer == false)
        {
            isItCoop = true;
        }
        if (!isItCoop)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            agent = gameObject.GetComponent<NavMeshAgent>();
            targetPlayer = player;
        }
        if (isItCoop)
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            RedPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
            BluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
            YellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
            FindClosestPlayer();
        }

        if (colourOverride==false)
        {
            randomColour = Random.Range(1, 4);
        }
        
        if (randomColour==1)
        {
            colourOfEnemy = "blue";
            SphereRenderer.material = blueMaterial;
            gameObject.GetComponent<ParticleSystemRenderer>().material = blueParticle;

        } else if (randomColour == 2)
        {
            colourOfEnemy = "red";
            SphereRenderer.material = redMaterial;
            gameObject.GetComponent<ParticleSystemRenderer>().material = redParticle;
        } else if (randomColour == 3)
        {
            colourOfEnemy = "yellow";
            SphereRenderer.material = yellowMaterial;
            gameObject.GetComponent<ParticleSystemRenderer>().material =yellowParticle;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	    if (gameManager.isItSingleplayer == true)
	    {
	        isItCoop = false;
	    }
	    if (gameManager.isItSingleplayer == false)
	    {
	        isItCoop = true;
	    }
	    if (!isItCoop)
	    {
	        if (player == null)
	        {
	            player = GameObject.FindGameObjectWithTag("Player");
	        }
	        else if (player != null)
	        {
	            if (Vector3.Distance(transform.position, player.transform.position) < 25f && isAggroPlayer == false)
	            {
	                thisEnemiesSpawnPoint.GetComponent<newSpawner>().ToggleAggro();
	            }

	            if (isAggroPlayer == true)
	            {
	                agent.SetDestination(targetPlayer.transform.position);
	            }

	        }

	    }
	    if (isItCoop)
	    {
	        if (isAggroPlayer == false && (Vector3.Distance(transform.position, RedPlayer.transform.position) < 25f || Vector3.Distance(transform.position, BluePlayer.transform.position) < 25f || Vector3.Distance(transform.position, YellowPlayer.transform.position) < 25f))
	        {
	            thisEnemiesSpawnPoint.GetComponent<newSpawner>().ToggleAggro();
            }
	        if (isAggroPlayer == true)
	        {
	            if (retargetingDelay == 3f)
	            {
	                FindClosestPlayer();
	            }
	            agent.SetDestination(targetPlayer.transform.position);
	        }


	    }
	    if (readyToRetarget == false)//DELAYS THE RETARGETING TO STOP PLAYER TARGET SWAPPING
	    {
	        retargetingDelay -= Time.deltaTime;
	    }
	    if (retargetingDelay <= 0f)
	    {
	        readyToRetarget = true;
	        retargetingDelay = 3f;
	    }
	    transform.LookAt(targetPlayer.transform);
	    if (gameObject.GetComponent<PaintDetectionScript>().colourOfPaint == "yellow")
	    {
	        agent.speed = 5;
	    }
	    else
	    if (gameObject.GetComponent<PaintDetectionScript>().colourOfPaint == "blue")
	    {
	        agent.speed = 1.5f;
	    }
	    else
	    {
	        agent.speed = 4;
	    }
	    if (gameObject.GetComponent<PaintDetectionScript>().colourOfPaint == "red")
	    {
	        poisonTimer -= Time.deltaTime;
	        if (poisonTimer <= 0)
	        {
	            thisEnemiesHealth -= 1;
                gameObject.GetComponent<ParticleSystem>().Play();
	            poisonTimer = 4f;
	        }
	    }
	    else
	    {
	        poisonTimer = 4f;
	    }

	    if (thisEnemiesHealth<=0)
	    {
	        if (colourOfEnemy=="blue")
	        {
	            Instantiate(blueSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
            }
            else if (colourOfEnemy=="red")
	        {
	            Instantiate(redSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
            }
            else if (colourOfEnemy=="yellow")
	        {
	            Instantiate(yellowSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
            }

	        thisEnemiesSpawnPoint.GetComponent<newSpawner>().ThisSpawnpointsEnemyList.Remove(gameObject);
	        mainCamera.GetComponent<CameraScript>().SmallScreenShake();
            enemyManagerScript.enemyList.Remove(gameObject);
            Destroy(this.gameObject);
	    }
    }
    void FindClosestPlayer()
    {


        readyToRetarget = false;
        //THIS CALCULATES THE DISTANCE BETWEEN THE ENEMY AND ALL OF THE PLAYERS AND THEN FINDS THE LOWEST AND SETS THE TARGETED PLAYER TO THAT
        float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, RedPlayer.transform.position);
        float distanceBetweenEnemyAndBluePlayer =
            Vector3.Distance(transform.position, BluePlayer.transform.position);
        float distanceBetweenEnemyAndYellowPlayer =
            Vector3.Distance(transform.position, YellowPlayer.transform.position);

        float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndBluePlayer),
            Mathf.Abs(distanceBetweenEnemyAndRedPlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));

        if (closestDistance == distanceBetweenEnemyAndRedPlayer)
        {
            if (RedPlayer.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
            {
                targetPlayer = RedPlayer;
            }
            else if (Mathf.Min(distanceBetweenEnemyAndBluePlayer, distanceBetweenEnemyAndYellowPlayer) == distanceBetweenEnemyAndBluePlayer)
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
        else if (closestDistance == distanceBetweenEnemyAndYellowPlayer)
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
        }

    }
    void OnCollisionEnter(Collision theCol)
    {
        //Check if it collides with the player
        if (theCol.gameObject.CompareTag("Player"))
        {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<SingleplayerHealthController>().GetHit();
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<SingleplayerHealthController>().invincibility = 1f;
        }
        //Check if it collides with coop player one
        if (theCol.gameObject.CompareTag("BluePlayer"))
        {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<CoopCharacterHealthControllerOne>().GetHit();
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<CoopCharacterHealthControllerOne>().invincibility = 1f;
        }
        //Check if it collides with coop player two
        if (theCol.gameObject.CompareTag("RedPlayer"))
        {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().GetHit();
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().invincibility = 1f;
        }
        //Check if it collides with coop player three
        if (theCol.gameObject.CompareTag("YellowPlayer"))
        {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<CoopCharacterHealthControllerThree>().GetHit();
            //Resseting the timer for the player to take damage
            //theCol.gameObject.GetComponent<CoopCharacterHealthControllerThree>().invincibility = 1f;
        }

        if (colourOfEnemy == "blue")
        {
            if (theCol.gameObject.CompareTag("BlueBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                thisEnemiesHealth -= 1;
                Destroy(theCol.gameObject);
            }
        }
        if (colourOfEnemy == "red")
        {
            if (theCol.gameObject.CompareTag("RedBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                thisEnemiesHealth -= 1;
                Destroy(theCol.gameObject);
            }
        }
        if (colourOfEnemy == "yellow")
        {
            if (theCol.gameObject.CompareTag("YellowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                thisEnemiesHealth -= 1;
                Destroy(theCol.gameObject);
            }
        }
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
	public void BulletKnockback(Vector3 bulletPosition)
	{
		transform.position = Vector3.MoveTowards(transform.position, bulletPosition, -0.5f);
	}
}
