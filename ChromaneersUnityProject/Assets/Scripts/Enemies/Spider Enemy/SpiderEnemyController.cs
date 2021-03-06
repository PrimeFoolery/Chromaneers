﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderEnemyController : MonoBehaviour
{
	
    public int bodyColour;
    public Material bodyMaterial;
    private int legColour;
    private string legMaterial;
	[Header("Spider Variables")]
    public GameObject leg1;
    public GameObject leg2;
    public GameObject leg3;
    public GameObject leg4;
    public int howManyLegsAreAlive = 4;
    private bool bodyDropped = false;
    private int bodyHealth = 2;

	[Header("Spider Materials")]
    public Material RedJellyMaterial;
    public Material BlueJellyMaterial;
    public Material YellowJellyMaterial;
    public Material BlueLegMaterial;
    public Material RedLegMaterial;
    public Material YellowLegMaterial;
    public Material RedParticleMaterial;
    public Material BlueParticleMaterial;
    public Material YellowParticleMaterial;

    [Header ("Paint Splat")]
    public GameObject blueSplat;
    public GameObject redSplat;
    public GameObject yellowSplat;
    public string BodyColourHolder;
    public GameObject enemyEmpty;
    public GameObject TopEmpty;

    [Header ("DeathSplatter")]
    public GameObject deathSplatterRed;
    public GameObject deathSplatterBlue;
    public GameObject deathSplatterYellow;

    [Header("Player and Misc")]
    public GameObject player;
    private GameObject targetPlayer;
    NavMeshAgent agent;
    public bool isItCoop;
    private ColourSelectManager gameManager;
	public int enemyDamage;
    private PaintDetectionScript paintDetector;
    private float poisonTimer = 4f;
    private EnemyManager enemyManagerScript;
    private EnemySpawner spawner;
    public bool isAggroPlayer = false;
    public GameObject thisEnemiesSpawnPoint;

    //COOP PLAYER VARIABLES
    private GameObject RedPlayer;
    private GameObject BluePlayer;
    private GameObject YellowPlayer;
    private float retargetingDelay = 3f;
    private bool readyToRetarget = true;

    private GameObject mainCamera;

    public GameObject coin;

    private bool colourBlindModeActive = false;
    public GameObject cbRedIndicator;
    public GameObject cbYellowIndicator;
    public GameObject cbBlueIndicator;
    private GameObject cbBodyCurrentIndicator;
    private GameObject cbLeg1CurrentIndicator;
    private GameObject cbLeg2CurrentIndicator;
    private GameObject cbLeg3CurrentIndicator;
    private GameObject cbLeg4CurrentIndicator;

    // Use this for initialization
    void Start ()
    {
        paintDetector = gameObject.GetComponentInParent<PaintDetectionScript>();
	    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>();
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemySpawner>();
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
	        agent = gameObject.GetComponentInParent<NavMeshAgent>();
	        targetPlayer = player;
	    }
	    if (isItCoop)
	    {
	        agent = gameObject.GetComponentInParent<NavMeshAgent>();
            RedPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
	        BluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
	        YellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
	    }
        bodyColour = Random.Range(1, 4);
	    bodyMaterial = gameObject.GetComponent<Renderer>().material;
	    if (bodyColour == 1)
	    {
	        gameObject.GetComponent<ParticleSystemRenderer>().material = RedParticleMaterial;
            bodyMaterial = RedJellyMaterial;
            BodyColourHolder = "Red";
	        legColour = Random.Range(1, 3);
	        if (legColour == 1)
	        {
	            //Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "blue";
	            leg2.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "blue";
                leg3.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "blue";
                leg4.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "blue";
            } else if (legColour == 2)
	        {
	            //Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg2.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg3.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg4.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "yellow";
            }
	    }
	    if (bodyColour == 2)
	    {
	        gameObject.GetComponent<ParticleSystemRenderer>().material = BlueParticleMaterial;
            bodyMaterial = BlueJellyMaterial;
            BodyColourHolder = "Blue";
            legColour = Random.Range(1, 3);
	        if (legColour == 1)
	        {
	           // Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = RedLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "red";
                leg2.GetComponent<Renderer>().material = RedLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "red";
                leg3.GetComponent<Renderer>().material = RedLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "red";
                leg4.GetComponent<Renderer>().material = RedLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "red";
            }
	        else if (legColour == 2)
	        {
	            //Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg2.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg3.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg4.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "yellow";
            }
        }
	    if (bodyColour == 3)
	    {
	        gameObject.GetComponent<ParticleSystemRenderer>().material = YellowParticleMaterial;
            bodyMaterial = YellowJellyMaterial;
            BodyColourHolder = "Yellow";
            legColour = Random.Range(1, 3);
	        if (legColour == 1)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "blue";
                leg2.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "blue";
                leg3.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "blue";
                leg4.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "blue";
            }
	        else if (legColour == 2)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = RedLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "red";
                leg2.GetComponent<Renderer>().material = RedLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "red";
                leg3.GetComponent<Renderer>().material = RedLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "red";
                leg4.GetComponent<Renderer>().material = RedLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "red";
            }
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
        //Debug.Log("leg colour:  "+legColour+"  body colour:  "+bodyColour);
        gameObject.GetComponent<Renderer>().material = bodyMaterial;
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

	                ToggleAggro();

                }

	            if (isAggroPlayer == true)
	            {
	                agent.SetDestination(targetPlayer.transform.position);
	            }

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
	            Destroy(cbBodyCurrentIndicator);
                Destroy(cbLeg1CurrentIndicator);
                Destroy(cbLeg2CurrentIndicator);
                Destroy(cbLeg3CurrentIndicator);
                Destroy(cbLeg4CurrentIndicator);
	            colourBlindModeActive = false;
	        }
	    }
        if (isItCoop)
	    {
	        if (isAggroPlayer == false && (Vector3.Distance(transform.position, RedPlayer.transform.position) < 25f || Vector3.Distance(transform.position, BluePlayer.transform.position) < 25f || Vector3.Distance(transform.position, YellowPlayer.transform.position) < 25f))
	        {
	            ToggleAggro();
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

	    if (paintDetector.colourOfPaint=="yellow")
	    {
	        if (howManyLegsAreAlive == 4)
	        {
	            agent.speed = 5.5f;
	        }
	        if (howManyLegsAreAlive == 3)
	        {
	            agent.speed = 5f;
	        }
	        if (howManyLegsAreAlive == 2)
	        {
	            agent.speed = 4.5f;
	        }
	        if (howManyLegsAreAlive == 1)
	        {
	            agent.speed = 4f;
	        }
        }else if(paintDetector.colourOfPaint == "blue")

	    {
	        if (howManyLegsAreAlive == 4)
	        {
	            agent.speed = 1.5f;
	        }
	        if (howManyLegsAreAlive == 3)
	        {
	            agent.speed = 1f;
	        }
	        if (howManyLegsAreAlive == 2)
	        {
	            agent.speed = 0.5f;
	        }
	        if (howManyLegsAreAlive == 1)
	        {
	            agent.speed = 0f;
	        }
	    }
	    else
	    {
	        if (howManyLegsAreAlive == 4)
	        {
	            agent.speed = 2.5f;
	        }
	        if (howManyLegsAreAlive == 3)
	        {
	            agent.speed = 2f;
	        }
	        if (howManyLegsAreAlive == 2)
	        {
	            agent.speed = 1.5f;
	        }
	        if (howManyLegsAreAlive == 1)
	        {
	            agent.speed = 1f;
	        }
        }
        
	    if (paintDetector.colourOfPaint == "orange")
	    {
	        agent.speed = -Mathf.Abs(agent.speed);
	    }
	    else
	    {
	        agent.speed = Mathf.Abs(agent.speed);
	    }

        if (howManyLegsAreAlive == 0)
	    {
	        if (paintDetector.colourOfPaint=="yellow")
	        {
	            agent.speed = 9f;
            } else if (paintDetector.colourOfPaint == "blue")
	        {
	            agent.speed = 8f;
	        }
	        else
	        {
	            agent.speed = 7f; 
	        }
            
	        if (bodyDropped==false)
	        {
                GetComponentInParent<Transform>().position = new Vector3(GetComponentInParent<Transform>().position.x,0, GetComponentInParent<Transform>().position.z);
	            transform.position = new Vector3(transform.position.x, targetPlayer.transform.position.y-1f, transform.position.z);
	            bodyDropped = true;
	        }
	    }
        if(bodyHealth<=0)
	    {
            gameObject.GetComponent<ParticleSystem>().Play();
	        enemyManagerScript.enemyList.Remove(transform.parent.gameObject);
	        mainCamera.GetComponent<CameraScript>().SmallScreenShake();
	        Instantiate(coin, transform.position, Quaternion.identity);
	        Instantiate(coin, transform.position, Quaternion.identity);
	        Instantiate(coin, transform.position, Quaternion.identity);
            if (thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
	        {
	            thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList.Remove(gameObject.transform.parent.gameObject);
	        }
	        else if (thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
	        {
	            thisEnemiesSpawnPoint.GetComponent<newSpawner>().ThisSpawnpointsEnemyList.Remove(gameObject.transform.parent.gameObject);
	        }
            if(BodyColourHolder == "Red")
            {
                Instantiate(redSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
                Instantiate(deathSplatterRed, TopEmpty.gameObject.transform.position, TopEmpty.gameObject.transform.rotation);
            }
            if (BodyColourHolder == "Blue")
            {
                Instantiate(blueSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
                Instantiate(deathSplatterBlue, TopEmpty.gameObject.transform.position, TopEmpty.gameObject.transform.rotation);
            }
            if (BodyColourHolder == "Yellow")
            {
                Instantiate(yellowSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
                Instantiate(deathSplatterYellow, TopEmpty.gameObject.transform.position, TopEmpty.gameObject.transform.rotation);
            }
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
	    }

	    if (paintDetector.colourOfPaint=="red")
	    {
	        poisonTimer -= Time.deltaTime;
	        if (poisonTimer <= 0)
	        {
	           
	            if (leg1 !=null)
	            {
                    leg1.GetComponent<SpiderLegScript>().DamageLeg();
	            } else if (leg2!=null)
	            {
                    leg2.GetComponent<SpiderLegScript>().DamageLeg();
	            }
	            else if (leg3 != null)
	            {
	                leg3.GetComponent<SpiderLegScript>().DamageLeg();
	            }
	            else if (leg4 != null)
	            {
	                leg4.GetComponent<SpiderLegScript>().DamageLeg();
	            }else
	            {
                    gameObject.GetComponent<ParticleSystem>().Play();
	                bodyHealth -= 1;
	            }
	            if (isAggroPlayer == false)
	            {
	                ToggleAggro();
	            }
                poisonTimer = 4f;
	        }
        }
	    else
	    {
	        poisonTimer = 4f;
	    }
    }

    void OnCollisionEnter(Collision other)
    {
        if (bodyColour==1)
        {
            if (other.gameObject.tag == "RedBullet" || other.gameObject.CompareTag("RainbowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                bodyHealth -= 1;
                if (isAggroPlayer==false)
                {
                    ToggleAggro();
                }
                Destroy(other.gameObject);
            }
        }
        if (bodyColour == 2)
        {
            if (other.gameObject.tag == "BlueBullet" || other.gameObject.CompareTag("RainbowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                bodyHealth -= 1;
                if (isAggroPlayer == false)
                {
                    ToggleAggro();
                }
                Destroy(other.gameObject);
            }
        }
        if (bodyColour == 3)
        {
            if (other.gameObject.tag == "YellowBullet" || other.gameObject.CompareTag("RainbowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                bodyHealth -= 1;
                if (isAggroPlayer == false)
                {
                    ToggleAggro();
                }
                Destroy(other.gameObject);
            }
        }
	    
	    //Check if it collides with the blue enemy
	    if (other.gameObject.CompareTag("Player")) {
		    //When it collides with the enemy, apply the damage
		    other.gameObject.GetComponent<SingleplayerHealthController>().GetHit();
		    //Resseting the timer for the player to take damage
		    //other.gameObject.GetComponent<SingleplayerHealthController>().invincibility = 1f;
	    }
	    //Check if it collides with coop player one
	    if (other.gameObject.CompareTag("BluePlayer")) {
		    //When it collides with the enemy, apply the damage
		    other.gameObject.GetComponent<CoopCharacterHealthControllerOne>().GetHit();
		    //Resseting the timer for the player to take damage
		    //other.gameObject.GetComponent<CoopCharacterHealthControllerOne>().invincibility = 1f;
	    }
	    //Check if it collides with coop player two
	    if (other.gameObject.CompareTag("RedPlayer")) {
		    //When it collides with the enemy, apply the damage
		    other.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().GetHit();
		    //Resseting the timer for the player to take damage
		    //other.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().invincibility = 1f;
	    }
	    //Check if it collides with coop player three
	    if (other.gameObject.CompareTag("YellowPlayer")) {
		    //When it collides with the enemy, apply the damage
		    other.gameObject.GetComponent<CoopCharacterHealthControllerThree>().GetHit();
		    //Resseting the timer for the player to take damage
		   // other.gameObject.GetComponent<CoopCharacterHealthControllerThree>().invincibility = 1f;
	    }
    }
    void FindClosestPlayer()
    {
        readyToRetarget = false;
        //THIS CALCULATES THE DISTANCE BETWEEN THE ENEMY AND ALL OF THE PLAYERS AND THEN FINDS THE LOWEST AND SETS THE TARGETED PLAYER TO THAT
        float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, RedPlayer.transform.position);
        float distanceBetweenEnemyAndBluePlayer = Vector3.Distance(transform.position, BluePlayer.transform.position);
        float distanceBetweenEnemyAndYellowPlayer = Vector3.Distance(transform.position, YellowPlayer.transform.position);

        float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndBluePlayer), Mathf.Abs(distanceBetweenEnemyAndRedPlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));

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

    void ToggleAggro()
    {
        if (thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
        {
            thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
        }
        else if (thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
        {
            thisEnemiesSpawnPoint.GetComponent<newSpawner>().ToggleAggro();
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
    public void SpawnColourBlindIndicator()
    {
        if (bodyColour == 2)
        {
            cbBodyCurrentIndicator = Instantiate(cbBlueIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbBodyCurrentIndicator.transform.SetParent(transform);
        }
        else if (bodyColour == 1)
        {
            cbBodyCurrentIndicator= Instantiate(cbRedIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbBodyCurrentIndicator.transform.SetParent(transform);
        }
        else if (bodyColour == 3)
        {
            cbBodyCurrentIndicator = Instantiate(cbYellowIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbBodyCurrentIndicator.transform.SetParent(transform);
        }

        if (leg1.GetComponent<SpiderLegScript>().legColour=="blue")
        {
            cbLeg1CurrentIndicator = Instantiate(cbBlueIndicator, new Vector3(leg1.transform.position.x, leg1.transform.position.y + 1.5f, leg1.transform.position.z),
            Quaternion.Euler(90, 0, 0));
            cbLeg1CurrentIndicator.transform.SetParent(leg1.transform);
            cbLeg2CurrentIndicator = Instantiate(cbBlueIndicator, new Vector3(leg2.transform.position.x, leg2.transform.position.y + 1.5f, leg2.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg2CurrentIndicator.transform.SetParent(leg2.transform);
            cbLeg3CurrentIndicator = Instantiate(cbBlueIndicator, new Vector3(leg3.transform.position.x, leg3.transform.position.y + 1.5f, leg3.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg3CurrentIndicator.transform.SetParent(leg3.transform);
            cbLeg4CurrentIndicator = Instantiate(cbBlueIndicator, new Vector3(leg4.transform.position.x, leg4.transform.position.y + 1.5f, leg4.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg4CurrentIndicator.transform.SetParent(leg4.transform);
        }else if (leg1.GetComponent<SpiderLegScript>().legColour == "red")
        {
            cbLeg1CurrentIndicator = Instantiate(cbRedIndicator, new Vector3(leg1.transform.position.x, leg1.transform.position.y + 1.5f, leg1.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg1CurrentIndicator.transform.SetParent(leg1.transform);
            cbLeg2CurrentIndicator = Instantiate(cbRedIndicator, new Vector3(leg2.transform.position.x, leg2.transform.position.y + 1.5f, leg2.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg2CurrentIndicator.transform.SetParent(leg2.transform);
            cbLeg3CurrentIndicator = Instantiate(cbRedIndicator, new Vector3(leg3.transform.position.x, leg3.transform.position.y + 1.5f, leg3.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg3CurrentIndicator.transform.SetParent(leg3.transform);
            cbLeg4CurrentIndicator = Instantiate(cbRedIndicator, new Vector3(leg4.transform.position.x, leg4.transform.position.y + 1.5f, leg4.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg4CurrentIndicator.transform.SetParent(leg4.transform);
        }
        else if (leg1.GetComponent<SpiderLegScript>().legColour == "yellow")
        {
            cbLeg1CurrentIndicator = Instantiate(cbYellowIndicator, new Vector3(leg1.transform.position.x, leg1.transform.position.y + 1.5f, leg1.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg1CurrentIndicator.transform.SetParent(leg1.transform);
            cbLeg2CurrentIndicator = Instantiate(cbYellowIndicator, new Vector3(leg2.transform.position.x, leg2.transform.position.y + 1.5f, leg2.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg2CurrentIndicator.transform.SetParent(leg2.transform);
            cbLeg3CurrentIndicator = Instantiate(cbYellowIndicator, new Vector3(leg3.transform.position.x, leg3.transform.position.y + 1.5f, leg3.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg3CurrentIndicator.transform.SetParent(leg3.transform);
            cbLeg4CurrentIndicator = Instantiate(cbYellowIndicator, new Vector3(leg4.transform.position.x, leg4.transform.position.y + 1.5f, leg4.transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbLeg4CurrentIndicator.transform.SetParent(leg4.transform);
        }
    }
}
