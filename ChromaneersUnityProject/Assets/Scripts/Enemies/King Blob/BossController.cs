﻿using System.Collections;
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
    public float bodyRotationSpeed = 10f;
    public float rotateSpeed = 1000f;

    public GameObject Obelisk1;
    public GameObject Obelisk2;
    public GameObject Obelisk3;

    public int enemyHealth;
    public int triggersInStage2 = 0;

    public List<string> PreviousColoursList = new List<string>();
    public string colourOfWeakSpot;

    public GameObject targetPlayer;
    public bool isAggroPlayer = false;
    NavMeshAgent agent;

    public Renderer SphereRenderer;
    public float enemySpeed = 2.5f;
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

    public Material blueParticle;
    public Material redParticle;
    public Material yellowParticle;
    public Material purpleParticle;
    public Material orangeParticle;
    public Material greenParticle;
    public Material greyParticle;

    public Material blueEnemyMat;
    public Material redEnemyMat;
    public Material yellowEnemyMat;
    public Material orangeEnemyMat;
    public Material purpleEnemyMat;
    public Material greenEnemyMat;
    public Material greyEnemyMat;

    private bool colourBlindModeActive = false;
    public GameObject cbRedIndicator;
    public GameObject cbYellowIndicator;
    public GameObject cbBlueIndicator;
    public GameObject cbPurpleIndicator;
    public GameObject cbGreenIndicator;
    public GameObject cbOrangeIndicator;
    private GameObject cbCurrentWeakSpotIndicator;
    private GameObject cbCurrentVialIndicator;
    private GameObject cbCurrentBodyIndicator;

    public GameObject rainbow;

	private bool waitForUpdate = false;

    private GameObject tempRainbowBlue;
    private GameObject tempRainbowRed;
    private GameObject tempRainbowYellow;
    private bool haveRainbowsSpawned = false;

    public float rainbowGrowSpeed = 8f;
    private float rainbowTimer = 1.4f;

    private float panicTimer = 0.2f;

    private float deathTime = 3f;

    public GameObject WeakSpot;

    public coinController.RainbowState CurrentRainbowState = coinController.RainbowState.preStart;

    private GameObject mainCamera;

    public GameObject coin;
    private GameObject tempPing;
    public GameObject pingPrefab;

    public GameObject doorToTrigger;

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
        agent.speed = enemySpeed;
        

        gameObject.GetComponent<ParticleSystemRenderer>().material = greyParticle;

        if (gameManager.colourBlindMode == true)
        {
            
            colourBlindModeActive = true;
        }
        else
        {
            colourBlindModeActive = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.F1))
	    {
	        if (colourBlindModeActive == false)
	        {
	            SpawnColourBlindIndicator();
	            colourBlindModeActive = true;
	        }
	        else
	        {
	            if (currentBossPhase == BossPhases.vials)
	            {
                    Destroy(cbCurrentWeakSpotIndicator);
                    Destroy(cbCurrentVialIndicator);
	            }

	            if (currentBossPhase == BossPhases.pens)
	            {
	                if (cbCurrentBodyIndicator!=null)
	                {
                        Destroy(cbCurrentBodyIndicator);
	                }
	            }
	            colourBlindModeActive = false;
	        }
	    }
        if (waitForUpdate==false){
			randomColour = Random.Range(1, 4);
			if (randomColour == 1)
			{
				colourOfWeakSpot = "blue";
				WeakSpot.GetComponent<Renderer>().material = blueEnemyMat;
				backVial.GetComponent<VialController>().ResetToOrange();
				PreviousColoursList.Add(colourOfWeakSpot);
			    if (colourBlindModeActive==true)
			    {
			        SpawnColourBlindIndicator();
			    }
			}else if (randomColour == 2)
			{
				colourOfWeakSpot = "red";
				WeakSpot.GetComponent<Renderer>().material = redEnemyMat;
				backVial.GetComponent<VialController>().ResetToGreen();
				PreviousColoursList.Add(colourOfWeakSpot);
			    if (colourBlindModeActive == true)
			    {
			        SpawnColourBlindIndicator();
			    }
            }
			else if (randomColour == 3)
			{
				colourOfWeakSpot = "yellow";
				WeakSpot.GetComponent<Renderer>().material = yellowEnemyMat;
				backVial.GetComponent<VialController>().ResetToPurple();
				PreviousColoursList.Add(colourOfWeakSpot);
			    if (colourBlindModeActive == true)
			    {
			        SpawnColourBlindIndicator();
			    }
            }
			waitForUpdate = true;
		}
	    if (gameManager.isItSingleplayer == false)
	    {
	        isItCoop = true;
	    }
	    if (isItCoop)
	    {
	        if (isAggroPlayer == false && (Vector3.Distance(transform.position, RedPlayer.transform.position) < 25f || Vector3.Distance(transform.position, BluePlayer.transform.position) < 25f || Vector3.Distance(transform.position, YellowPlayer.transform.position) < 25f))
	        {
	            //isAggroPlayer = true;
	        }
	        if (isAggroPlayer == true)
	        {
	            
	            agent.SetDestination(targetPlayer.transform.position);
	           
	        }
	        if (retargetingDelay == 3f&&currentBossPhase!=BossPhases.death)
	        {
	            PingForPlayer();
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
        //transform.LookAt(new Vector3(targetPlayer.transform.position.x, transform.position.y, targetPlayer.transform.position.z));
	    if (targetPlayer!=null)
	    {
	        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
	        //Debug.Log(direction);
	        Quaternion toRotation = Quaternion.FromToRotation(transform.position, direction);
	        //Debug.Log(toRotation);
	        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, toRotation.eulerAngles.y, transform.rotation.eulerAngles.z), bodyRotationSpeed * Time.time * Time.deltaTime);
        }
	    

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

	        if (enemyHealth<=0)
	        {
                RandomiseAndResetVials(PreviousColoursList);
	            enemyHealth = 3;
	        }
	    }else if (currentBossPhase == BossPhases.pens)
	    {

	        if (backVial!=null)
	        {
                Destroy(rightDoorHolder);
                Destroy(leftDoorHolder);
                Destroy(WeakSpot);
	            Destroy(backVial.transform.parent.gameObject);
            }

	        if (enemyHealth<=0)
	        {
	            if (triggersInStage2 == 2)
	            {
	                CurrentRainbowState = coinController.RainbowState.spawn;
	                currentBossPhase = BossPhases.panic;
	            }
                if (triggersInStage2<2)
	            {
	                triggersInStage2 += 1;
	                ResetToGrey();
                }

	            
                
	        }
	    }else if (currentBossPhase == BossPhases.panic)
	    {
            if (CurrentRainbowState == coinController.RainbowState.spawn)
            {
                enemyHealth = 60;
                tempRainbowBlue = Instantiate(rainbow, new Vector3(BluePlayer.transform.position.x, BluePlayer.transform.position.y + 50f, BluePlayer.transform.position.z), Quaternion.identity, BluePlayer.transform);
                
                tempRainbowRed = Instantiate(rainbow, new Vector3(RedPlayer.transform.position.x, RedPlayer.transform.position.y + 50f, RedPlayer.transform.position.z), Quaternion.identity, RedPlayer.transform);
                
                tempRainbowYellow = Instantiate(rainbow, new Vector3(YellowPlayer.transform.position.x, YellowPlayer.transform.position.y + 50f, YellowPlayer.transform.position.z), Quaternion.identity, YellowPlayer.transform);
                

                CurrentRainbowState = coinController.RainbowState.grow;
            }

            if (CurrentRainbowState == coinController.RainbowState.grow)
            {
                tempRainbowBlue.transform.localScale += new Vector3(0, rainbowGrowSpeed, 0);
                tempRainbowRed.transform.localScale += new Vector3(0, rainbowGrowSpeed, 0);
                tempRainbowYellow.transform.localScale += new Vector3(0, rainbowGrowSpeed, 0);
                if (tempRainbowBlue.transform.localScale.y > 35.5f)
                {
                    CurrentRainbowState = coinController.RainbowState.savour;
                }
            }

            if (CurrentRainbowState == coinController.RainbowState.savour)
            {

                rainbowTimer -= Time.deltaTime;
                if (rainbowTimer <= 0)
                {
                    BluePlayer.GetComponentInChildren<CharacterOneGunController>().stateOfWeapon =
                            CharacterOneGunController.currentWeapon.RainbowWeapon;
                    
                    RedPlayer.GetComponentInChildren<CharacterTwoGunController>().stateOfWeapon =
                            CharacterOneGunController.currentWeapon.RainbowWeapon;
                    
                    YellowPlayer.GetComponentInChildren<CharacterThreeGunController>().stateOfWeapon =
                            CharacterOneGunController.currentWeapon.RainbowWeapon;
                    
                    CurrentRainbowState = coinController.RainbowState.stop;
                }
            }

            if (CurrentRainbowState == coinController.RainbowState.stop)
            {
                Destroy(tempRainbowBlue.gameObject);
                Destroy(tempRainbowRed.gameObject);
                Destroy(tempRainbowYellow.gameObject);
                rainbowTimer = 1.4f;
                CurrentRainbowState = coinController.RainbowState.preStart;
            }

	        if (enemyHealth<60&&enemyHealth>50)
	        {
	            agent.speed = 3.5f;
	        }else
	        if (enemyHealth < 50 && enemyHealth > 42)
	        {
	            agent.speed = 4f;
	        }else
	        if (enemyHealth < 42 && enemyHealth > 34)
	        {
	            agent.speed = 4.5f;
	        }else
	        if (enemyHealth < 34 && enemyHealth > 26)
	        {
	            agent.speed = 5f;
	        }else
	        if (enemyHealth < 26 && enemyHealth > 18)
	        {
	            agent.speed = 5.5f;
	        }
	        else
	        if (enemyHealth < 18 && enemyHealth > 10)
	        {
	            agent.speed = 6f;
	        }
	        else
	        if (enemyHealth < 10 && enemyHealth > 0)
	        {
	            agent.speed = 6.5f;
	        }

            panicTimer -= Time.deltaTime;
            
	        if (panicTimer< 0)
	        {
                RandomiseColor();
	            panicTimer = 0.2f;
	        }
	        if (enemyHealth<=0)
	        {
	            if (tempPing!=null)
	            {
	                Destroy(tempPing);
                }
                
	            currentBossPhase = BossPhases.death;
	        }
        }else if (currentBossPhase == BossPhases.death)
	    {
	        if (agent.speed!=0)
	        {
	            agent.speed = 0;
	        }

	        deathTime -= Time.deltaTime;
	        Instantiate(coin, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), Quaternion.identity);

	        if (deathTime<=0)
	        {
                doorToTrigger.GetComponent<doorController>().OpenSesame();
                Destroy(this.gameObject);
	        }
	    }
    }
    void FindClosestPlayer()
    {

        /*if (colourOfEnem
         y == "blue")
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

    public void RandomiseAndResetVials(List<string> previousColours)
    {
        Destroy(cbCurrentWeakSpotIndicator);
        Destroy(cbCurrentVialIndicator);
        if (previousColours.Contains("blue")==true && previousColours.Contains("red")==false && previousColours.Contains("yellow") == false)
        {
            int randomColourNumber = Random.Range(1, 3);
            if (randomColourNumber==1)
            {
                colourOfWeakSpot = "red";
                WeakSpot.GetComponent<Renderer>().material = redEnemyMat;
                backVial.GetComponent<VialController>().ResetToGreen();
                PreviousColoursList.Add(colourOfWeakSpot);
                if (colourBlindModeActive==true)
                {
                    SpawnColourBlindIndicator();
                }
                
            }
            else 
            {
                colourOfWeakSpot = "yellow";
                WeakSpot.GetComponent<Renderer>().material = yellowEnemyMat;
                backVial.GetComponent<VialController>().ResetToPurple();
                PreviousColoursList.Add(colourOfWeakSpot);
                if (colourBlindModeActive == true)
                {
                    SpawnColourBlindIndicator();
                }
            }
        }else
        if (previousColours.Contains("red") == true && previousColours.Contains("blue") == false && previousColours.Contains("yellow") == false)
        {
            int randomColourNumber = Random.Range(1, 3);
            if (randomColourNumber == 1)
            {
                colourOfWeakSpot = "blue";
                WeakSpot.GetComponent<Renderer>().material = blueEnemyMat;
                backVial.GetComponent<VialController>().ResetToOrange();
                PreviousColoursList.Add(colourOfWeakSpot);
                if (colourBlindModeActive == true)
                {
                    SpawnColourBlindIndicator();
                }
            }
            else
            {
                colourOfWeakSpot = "yellow";
                WeakSpot.GetComponent<Renderer>().material = yellowEnemyMat;
                backVial.GetComponent<VialController>().ResetToPurple();
                PreviousColoursList.Add(colourOfWeakSpot);
                if (colourBlindModeActive == true)
                {
                    SpawnColourBlindIndicator();
                }
            }
        }
        else
        if (previousColours.Contains("yellow") == true && previousColours.Contains("red") == false && previousColours.Contains("blue") == false)
        {
            int randomColourNumber = Random.Range(1, 3);
            if (randomColourNumber == 1)
            {
                colourOfWeakSpot = "blue";
                WeakSpot.GetComponent<Renderer>().material = blueEnemyMat;
                backVial.GetComponent<VialController>().ResetToOrange();
                PreviousColoursList.Add(colourOfWeakSpot);
                if (colourBlindModeActive == true)
                {
                    SpawnColourBlindIndicator();
                }
            }
            else
            {
                colourOfWeakSpot = "red";
                WeakSpot.GetComponent<Renderer>().material = redEnemyMat;
                backVial.GetComponent<VialController>().ResetToGreen();
                PreviousColoursList.Add(colourOfWeakSpot);
                if (colourBlindModeActive == true)
                {
                    SpawnColourBlindIndicator();
                }
            }
        }
        else
        if (previousColours.Contains("blue") == true && previousColours.Contains("red") == true && previousColours.Contains("yellow") == false)
        {
            colourOfWeakSpot = "yellow";
            WeakSpot.GetComponent<Renderer>().material = yellowEnemyMat;
            backVial.GetComponent<VialController>().ResetToPurple();
            PreviousColoursList.Add(colourOfWeakSpot);
            if (colourBlindModeActive == true)
            {
                SpawnColourBlindIndicator();
            }
        }
        else
        if (previousColours.Contains("blue") == true && previousColours.Contains("red") == false && previousColours.Contains("yellow") == true)
        {
            colourOfWeakSpot = "red";
            WeakSpot.GetComponent<Renderer>().material = redEnemyMat;
            backVial.GetComponent<VialController>().ResetToGreen();
            PreviousColoursList.Add(colourOfWeakSpot);
            if (colourBlindModeActive == true)
            {
                SpawnColourBlindIndicator();
            }
        }
        else
        if (previousColours.Contains("blue") == false && previousColours.Contains("red") == true && previousColours.Contains("yellow") == true)
        {
            colourOfWeakSpot = "blue";
            WeakSpot.GetComponent<Renderer>().material = blueEnemyMat;
            backVial.GetComponent<VialController>().ResetToOrange();
            PreviousColoursList.Add(colourOfWeakSpot);
            if (colourBlindModeActive == true)
            {
                SpawnColourBlindIndicator();
            }
        }
        else if (previousColours.Contains("blue") == true && previousColours.Contains("red") == true && previousColours.Contains("yellow") == true)
        {
            Obelisk1.GetComponentInChildren<BossBattleObelisk>().StartPhase2();
            Obelisk2.GetComponentInChildren<BossBattleObelisk>().StartPhase2();
            Obelisk3.GetComponentInChildren<BossBattleObelisk>().StartPhase2();
            currentBossPhase = BossPhases.pens;
        }
    }

    public void ChangeToBlue()
    {
        SphereRenderer.material = blueEnemyMat;
        gameObject.GetComponent<ParticleSystemRenderer>().material = blueParticle;
        enemyHealth = 10;
        colourOfEnemy = "blue";
        if (colourBlindModeActive == true)
        {
            SpawnColourBlindIndicator();
        }
    }
    public void ChangeToRed()
    {
        SphereRenderer.material = redEnemyMat;
        gameObject.GetComponent<ParticleSystemRenderer>().material =redParticle;
        enemyHealth = 10;
        colourOfEnemy = "red";
        if (colourBlindModeActive == true)
        {
            SpawnColourBlindIndicator();
        }
    }
    public void ChangeToYellow()
    {
        gameObject.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
        SphereRenderer.material = yellowEnemyMat;
        enemyHealth = 10;
        colourOfEnemy = "yellow";
        if (colourBlindModeActive == true)
        {
            SpawnColourBlindIndicator();
        }
    }

    public void ResetToGrey()
    {
        gameObject.GetComponent<ParticleSystemRenderer>().material = greyParticle;
        SphereRenderer.material = greyEnemyMat;
        enemyHealth = 10;
        colourOfEnemy = "grey";
        if (colourBlindModeActive == true)
        {
            Destroy(cbCurrentBodyIndicator);
        }
    }

    public void RandomiseColor()
    {
        int randomNumberForColour = Random.Range(1, 8);
        if (randomNumberForColour == 1)
        {
            gameObject.GetComponent<ParticleSystemRenderer>().material = greyParticle;
            SphereRenderer.material = greyEnemyMat;
        }else
        if (randomNumberForColour == 2)
        {
            gameObject.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SphereRenderer.material = blueEnemyMat;
        }
        else
        if (randomNumberForColour == 3)
        {
            gameObject.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SphereRenderer.material = redEnemyMat;
        }
        else
        if (randomNumberForColour == 4)
        {
            gameObject.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SphereRenderer.material = yellowEnemyMat;
        }
        else
        if (randomNumberForColour == 5)
        {
            gameObject.GetComponent<ParticleSystemRenderer>().material = purpleParticle;
            SphereRenderer.material = purpleEnemyMat;
        }
        else
        if (randomNumberForColour == 1)
        {
            gameObject.GetComponent<ParticleSystemRenderer>().material = orangeParticle;
            SphereRenderer.material = orangeEnemyMat;
        }
        else
        if (randomNumberForColour == 1)
        {
            gameObject.GetComponent<ParticleSystemRenderer>().material = greenParticle;
            SphereRenderer.material = greenEnemyMat;
        }
    }

    public void SpawnCandy()
    {
        Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
    }
    public void PingForPlayer()
    {
        //Debug.Log("Spawn ping");
        tempPing = Instantiate(pingPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        tempPing.GetComponent<BossAggroPingScript>().boss = gameObject;
        readyToRetarget = false;
    }
    public void SpawnColourBlindIndicator()
    {
        if (currentBossPhase == BossPhases.vials)
        {
            if (colourOfWeakSpot == "blue")
            {
                cbCurrentWeakSpotIndicator = Instantiate(cbBlueIndicator,
                    new Vector3(WeakSpot.transform.position.x, WeakSpot.transform.position.y + 6.5f, WeakSpot.transform.position.z),
                    Quaternion.Euler(90, 0, 0));
                cbCurrentWeakSpotIndicator.transform.SetParent(WeakSpot.transform);
                cbCurrentVialIndicator = Instantiate(cbOrangeIndicator,
                    new Vector3(backVial.transform.position.x, backVial.transform.position.y + 6.5f, backVial.transform.position.z),
                    Quaternion.Euler(90, 0, 0));
                cbCurrentVialIndicator.transform.SetParent(backVial.transform);
            }
            else if (colourOfWeakSpot == "red")
            {
                cbCurrentWeakSpotIndicator = Instantiate(cbRedIndicator,
                    new Vector3(WeakSpot.transform.position.x, WeakSpot.transform.position.y + 6.5f, WeakSpot.transform.position.z),
                    Quaternion.Euler(90, 0, 0));
                cbCurrentWeakSpotIndicator.transform.SetParent(WeakSpot.transform);
                cbCurrentVialIndicator = Instantiate(cbGreenIndicator,
                    new Vector3(backVial.transform.position.x, backVial.transform.position.y + 6.5f, backVial.transform.position.z),
                    Quaternion.Euler(90, 0, 0));
                cbCurrentVialIndicator.transform.SetParent(backVial.transform);
            }
            else if (colourOfWeakSpot == "yellow")
            {
                cbCurrentWeakSpotIndicator = Instantiate(cbYellowIndicator,
                    new Vector3(WeakSpot.transform.position.x, WeakSpot.transform.position.y + 6.5f, WeakSpot.transform.position.z),
                    Quaternion.Euler(90, 0, 0));
                cbCurrentWeakSpotIndicator.transform.SetParent(WeakSpot.transform);
                cbCurrentVialIndicator = Instantiate(cbPurpleIndicator,
                    new Vector3(backVial.transform.position.x, backVial.transform.position.y + 6.5f, backVial.transform.position.z),
                    Quaternion.Euler(90, 0, 0));
                cbCurrentVialIndicator.transform.SetParent(backVial.transform);
            }
        }else if(currentBossPhase==BossPhases.pens)

        {
            if (colourOfEnemy=="blue")
            {
                cbCurrentBodyIndicator = Instantiate(cbBlueIndicator,
                    new Vector3(SphereRenderer.transform.position.x, SphereRenderer.transform.position.y + 10.5f, SphereRenderer.transform.position.z),
                    Quaternion.Euler(90, 0, 0));
                cbCurrentBodyIndicator.transform.SetParent(transform);
            }
            else if (colourOfEnemy == "red")
            {
                cbCurrentBodyIndicator = Instantiate(cbRedIndicator,
                    new Vector3(SphereRenderer.transform.position.x, SphereRenderer.transform.position.y + 10.5f, SphereRenderer.transform.position.z),
                    Quaternion.Euler(90, 0, 0));
                cbCurrentBodyIndicator.transform.SetParent(transform);
            }
            else if (colourOfEnemy =="yellow")
            {
                cbCurrentBodyIndicator = Instantiate(cbYellowIndicator,
                    new Vector3(SphereRenderer.transform.position.x, SphereRenderer.transform.position.y + 10.5f, SphereRenderer.transform.position.z),
                    Quaternion.Euler(90, 0, 0));
                cbCurrentBodyIndicator.transform.SetParent(transform);
            }
        }
        
    }
}
