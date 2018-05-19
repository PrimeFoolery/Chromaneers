using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour {

    public enum BossPhases
    {
        vials,
        pens,
        panic,
        death
    }

    public BossPhases currentBossPhase;

    private bool openDoors = false;
    public float rotateSpeed = 1000f;

    public int enemyHealth;

    public string colourOfWeakSpot;

    public GameObject targetPlayer;
    public bool isAggroPlayer = false;
    NavMeshAgent agent;

    public Renderer SphereRenderer;
    public float enemySpeed = 3f;
    public bool colourOverride = false;
    public int randomColour;

    private GameObject RedPlayer;
    private GameObject BluePlayer;
    private GameObject YellowPlayer;
    private float retargetingDelay = 3f;
    private bool readyToRetarget = true;
    
    public List<MotherSpawnPoint> spawnPointList = new List<MotherSpawnPoint>();
    private float spawningTimer = 6f;

    public bool isItCoop;
    private ColourSelectManager gameManager;
    private EnemySpawner spawner;
    public GameObject thisEnemiesSpawnPoint;
    public int enemyDamage;

    public GameObject backVial;
    public GameObject leftDoorHolder;
    public GameObject rightDoorHolder;

    public string colourOfEnemy;

    public Material blueEnemyMat;
    public Material redEnemyMat;
    public Material yellowEnemyMat;
    public Material orangeEnemyMat;
    public Material purpleEnemyMat;
    public Material greenEnemyMat;

    public GameObject WeakSpot;

    private GameObject mainCamera;

    public GameObject coin;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>();
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
        if (isItCoop)
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            RedPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
            BluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
            YellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
            FindClosestPlayer();
        }

        randomColour = Random.Range(1, 4);
        if (randomColour == 1)
        {
            colourOfWeakSpot = "blue";
            WeakSpot.GetComponent<Renderer>().material = blueEnemyMat;
            backVial.GetComponent<VialController>().ResetToOrange();
        }else if (randomColour == 2)
        {
            colourOfWeakSpot = "red";
            WeakSpot.GetComponent<Renderer>().material = redEnemyMat;
            backVial.GetComponent<VialController>().ResetToGreen();
        }else if (randomColour == 3)
        {
            colourOfWeakSpot = "yellow";
            WeakSpot.GetComponent<Renderer>().material = yellowEnemyMat;
            backVial.GetComponent<VialController>().ResetToPurple();
        }


    }
	
	// Update is called once per frame
	void Update () {
	    if (gameManager.isItSingleplayer == false)
	    {
	        isItCoop = true;
	    }
	    if (isItCoop)
	    {
	        if (isAggroPlayer == false && (Vector3.Distance(transform.position, RedPlayer.transform.position) < 25f || Vector3.Distance(transform.position, BluePlayer.transform.position) < 25f || Vector3.Distance(transform.position, YellowPlayer.transform.position) < 25f))
	        {
	            isAggroPlayer = true;
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
	    transform.LookAt(new Vector3(targetPlayer.transform.position.x, transform.position.y, targetPlayer.transform.position.z));
	    if (enemyHealth <= 0)
	    {


	    }
	    agent.speed = enemySpeed;

	    if (currentBossPhase==BossPhases.vials)
	    {
	        if (backVial.GetComponent<VialController>().VialCorrectlyFilled == true)
	        {
	            openDoors = true;
            }
	        else
	        {
	            openDoors = false;
	        }

	        if (openDoors == true)
            {
                if (leftDoorHolder.transform.localRotation.y >-0.6f)
                {
                    leftDoorHolder.transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
                }
                if (rightDoorHolder.transform.localRotation.y <0.6f)
                {
                    rightDoorHolder.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
                }
	            
	        }
	        else
	        {
	            if (leftDoorHolder.transform.localRotation.y <0)
	            {
	                leftDoorHolder.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
                }
	            if (rightDoorHolder.transform.localRotation.y > 0)
	            {
	                rightDoorHolder.transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
	            }
            }
	    }
    }
    void FindClosestPlayer()
    {

        /*if (colourOfEnemy == "blue")
        {
            readyToRetarget = false;
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
                else if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = BluePlayer;
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
        else if (colourOfEnemy == "red")
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
            if (BluePlayer.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
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
        }*/
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
            if (theCol.gameObject.CompareTag("BlueBullet") || theCol.gameObject.CompareTag("RainbowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                enemyHealth -= 1;
                Destroy(theCol.gameObject);
            }
        }
        if (colourOfEnemy == "red")
        {
            if (theCol.gameObject.CompareTag("RedBullet") || theCol.gameObject.CompareTag("RainbowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                enemyHealth -= 1;
                Destroy(theCol.gameObject);
            }
        }
        if (colourOfEnemy == "yellow")
        {
            if (theCol.gameObject.CompareTag("YellowBullet") || theCol.gameObject.CompareTag("RainbowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                enemyHealth -= 1;
                Destroy(theCol.gameObject);
            }
        }
    }
    public void AggroToggle()
    {
        /*
        if (thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
        {
            thisEnemiesSpawnPoint.GetComponent<newSpawner>().ToggleAggro();
        }
        else if (thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
        {
            thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
        }
        */

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
