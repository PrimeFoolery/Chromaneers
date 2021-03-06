﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SnakeEnemyScript : MonoBehaviour
{

    public GameObject Target_Or_SegmentAhead;
    public GameObject SegmentBehind;
    public Renderer SphereRenderer;
    public GameObject enemyEmpty;

    private ColourSelectManager gameManager;
    private EnemyManager enemyManagerScript;
    private EnemySpawner spawner;
    private bool isitCoop;
    private GameObject targetPlayer;
    private bool targetLock = false;
    private NavMeshAgent agent;
    private float timer = 0.2f;
    private int segmentHealth = 3;
    public bool isAggroPlayer = false;
    public GameObject thisEnemiesSpawnPoint;
    public string colourOfSnake;
    public int randomColour;
    public float headNormalSpeed = 3;
    public float headYellowSpeed = 5;
    public float headBlueSpeed = 1;
    public float bodyNormalSpeed = 3;
    public float bodyYellowSpeed = 5;
    public float bodyBlueSpeed = 1;

    public Material blueMaterial;
    public GameObject blueSplat;
    public Material blueParticle;
    public Material redMaterial;
    public GameObject redSplat;
    public Material redParticle;
    public Material yellowMaterial;
    public GameObject yellowSplat;
    public Material yellowParticle;

    public GameObject thisSegmentsSnakeHead;
    public GameObject thisSegmentsSnakeTail;
    public GameObject thisSegmentsSnakeBody;

    public Renderer sphereIndicatorRenderer;
    public Renderer triangleIndicatorRenderer;
    public Renderer squareIndicatorRenderer;

    public GameObject singlePlayerChar;

    public GameObject redPlayerChar;
    public GameObject bluePlayerChar;
    public GameObject yellowPlayerChar;

    public string colourOfPaintBelow = "null";
    public bool colourOverride = false;
    private float poisonTimer = 4f;

    private GameObject mainCamera;

    public GameObject coin;

    private bool colourBlindModeActive = false;
    public GameObject cbRedIndicator;
    public GameObject cbYellowIndicator;
    public GameObject cbBlueIndicator;
    private GameObject cbCurrentIndicator;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>();
        spawner = gameManager.gameObject.GetComponent<EnemySpawner>();
        enemyManagerScript = gameManager.gameObject.GetComponent<EnemyManager>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");



        if (gameManager.isItSingleplayer == true)
        {
            isitCoop = false;

        }
        else if (gameManager.isItSingleplayer == false)
        {
            isitCoop = true;
        }
        if (gameObject.name == "SnakeHead")
        {
            if (isitCoop == false)
            {
                singlePlayerChar = GameObject.FindGameObjectWithTag("Player");
            }
            else if (isitCoop == true)
            {
                redPlayerChar = GameObject.FindGameObjectWithTag("RedPlayer");
                bluePlayerChar = GameObject.FindGameObjectWithTag("BluePlayer");
                yellowPlayerChar = GameObject.FindGameObjectWithTag("YellowPlayer");
            }
            CalculateClosestPlayer();
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
    }

    // Update is called once per frame
    void Update()
    {
        if (name == "SnakeHead")
        {
            //Debug.Log(agent.destination);
        }
        
        if (isAggroPlayer == true&& Target_Or_SegmentAhead!=null)
        {
            agent.SetDestination(Target_Or_SegmentAhead.transform.position);
            //agent.isStopped = false;
        }

        if (colourOfPaintBelow != "yellow" && colourOfPaintBelow != "blue")
        {
            if (name != "SnakeHead" && Vector3.Distance(transform.position, Target_Or_SegmentAhead.transform.position) > 5f)
            {
                agent.speed = bodyYellowSpeed;
            }
            else if (name != "SnakeHead" && Vector3.Distance(transform.position, Target_Or_SegmentAhead.transform.position) < 5f)
            {
                agent.speed = headYellowSpeed;
            }
        }


        if (name == "SnakeHead")
        {
            GetComponentInParent<Transform>().position = transform.position;
            if (isitCoop == false)
            {
                if (Vector3.Distance(transform.position, singlePlayerChar.transform.position) < 25f && isAggroPlayer == false)
                {
                    ToggleAggro();
                }


            }
            else if (isitCoop == true)
            {
                if (isAggroPlayer == false && (Vector3.Distance(transform.position, redPlayerChar.transform.position) < 25f || Vector3.Distance(transform.position, bluePlayerChar.transform.position) < 25f || Vector3.Distance(transform.position, yellowPlayerChar.transform.position) < 25f))
                {
                    ToggleAggro();
                }
            }
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                CalculateClosestPlayer();
                timer = 0.2f;
                //agent.SetDestination(Target_Or_SegmentAhead.position);
            }
            //
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
        if (segmentHealth <= 0)
        {
            if (colourOfSnake == "blue")
            {
                Instantiate(blueSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
            }
            else if (colourOfSnake == "red")
            {
                Instantiate(redSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
            }
            else if (colourOfSnake == "yellow")
            {
                Instantiate(yellowSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
            }
            Instantiate(coin, transform.position, Quaternion.identity);
            if (name != "SnakeHead")
            {
                Target_Or_SegmentAhead.GetComponent<SnakeEnemyScript>().SegmentBehind = null;
                if (Target_Or_SegmentAhead.name != "SnakeHead")
                {
                    Target_Or_SegmentAhead.name = "SnakeTail";
                    Target_Or_SegmentAhead.gameObject.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody
                        .GetComponent<Renderer>().enabled = false;
                    Target_Or_SegmentAhead.gameObject.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail
                        .GetComponent<Renderer>().enabled = true;
                }
            }

            if (name != "SnakeTail" && SegmentBehind != null)
            {
                SegmentBehind.GetComponent<SnakeEnemyScript>().Target_Or_SegmentAhead = null;
                SegmentBehind.name = "SnakeHead";
                SegmentBehind.gameObject.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>()
                    .enabled = false;
                SegmentBehind.gameObject.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>()
                    .enabled = true;
                if (SegmentBehind.gameObject.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().enabled == true)
                {
                    SegmentBehind.gameObject.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail
                        .GetComponent<Renderer>().enabled = false;
                }
                SegmentBehind.GetComponent<SnakeEnemyScript>().agent.speed = headNormalSpeed;
                if (isitCoop == true)
                {
                    SegmentBehind.GetComponent<SnakeEnemyScript>().redPlayerChar = GameObject.FindGameObjectWithTag("RedPlayer");
                    SegmentBehind.GetComponent<SnakeEnemyScript>().bluePlayerChar = GameObject.FindGameObjectWithTag("BluePlayer");
                    SegmentBehind.GetComponent<SnakeEnemyScript>().yellowPlayerChar = GameObject.FindGameObjectWithTag("YellowPlayer");
                }
                if (isitCoop == false)
                {
                    SegmentBehind.GetComponent<SnakeEnemyScript>().singlePlayerChar = GameObject.FindGameObjectWithTag("Player");
                }

                SegmentBehind.GetComponent<SnakeEnemyScript>().CalculateClosestPlayer();
            }

            mainCamera.GetComponent<CameraScript>().SmallScreenShake();
            GetComponentInParent<snakeManager>().SegmentKilled();
            GetComponentInParent<snakeManager>().snakeSegments.Remove(gameObject.GetComponent<SnakeEnemyScript>());
            enemyManagerScript.enemyList.Remove(gameObject);
            Destroy(this.gameObject);
        }

        colourOfPaintBelow = gameObject.GetComponent<PaintDetectionScript>().colourOfPaint;

        if (colourOfPaintBelow == "yellow")
        {
            if (name == "SnakeHead")
            {
                agent.speed = headYellowSpeed;
            }
            else
            {
                agent.speed = bodyYellowSpeed;
            }
        }
        else

        if (colourOfPaintBelow == "blue")
        {
            if (name == "SnakeHead")
            {
                agent.speed = headBlueSpeed;
            }
            else
            {
                agent.speed = bodyBlueSpeed;
            }
        }
        else
        {
            if (name == "SnakeHead")
            {
                agent.speed = headNormalSpeed;
            }
        }

        if (colourOfPaintBelow == "red")
        {
            poisonTimer -= Time.deltaTime;
            if (poisonTimer <= 0)
            {
                segmentHealth -= 1;
                gameObject.GetComponent<ParticleSystem>().Play();
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

    public void CalculateClosestPlayer()
    {
        if (isitCoop == true)
        {

            float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, redPlayerChar.transform.position);
            float distanceBetweenEnemyAndBluePlayer =
                Vector3.Distance(transform.position, bluePlayerChar.transform.position);
            float distanceBetweenEnemyAndYellowPlayer =
                Vector3.Distance(transform.position, yellowPlayerChar.transform.position);

            float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndBluePlayer),
                Mathf.Abs(distanceBetweenEnemyAndRedPlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));

            if (closestDistance == distanceBetweenEnemyAndRedPlayer)
            {
                if (redPlayerChar.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                {
                    targetPlayer = redPlayerChar;
                }
                else if (Mathf.Min(distanceBetweenEnemyAndBluePlayer, distanceBetweenEnemyAndYellowPlayer) == distanceBetweenEnemyAndBluePlayer)
                {
                    if (bluePlayerChar.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                    {
                        targetPlayer = bluePlayerChar;
                    }
                    else if (yellowPlayerChar.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                    {
                        targetPlayer = yellowPlayerChar;
                    }
                    else
                    {
                        agent.SetDestination(RandomNavmeshLocation(10f));
                    }
                }
                else if (yellowPlayerChar.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = yellowPlayerChar;
                }
                else
                {
                    agent.SetDestination(RandomNavmeshLocation(10f));
                }

            }
            else if (closestDistance == distanceBetweenEnemyAndBluePlayer)
            {
                if (bluePlayerChar.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = bluePlayerChar;
                }
                else if (Mathf.Min(distanceBetweenEnemyAndRedPlayer, distanceBetweenEnemyAndYellowPlayer) == distanceBetweenEnemyAndRedPlayer)
                {
                    if (redPlayerChar.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                    {
                        targetPlayer = redPlayerChar;
                    }
                    else if (yellowPlayerChar.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                    {
                        targetPlayer = yellowPlayerChar;
                    }
                    else
                    {
                        agent.SetDestination(RandomNavmeshLocation(10f));
                    }
                }
                else if (yellowPlayerChar.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = yellowPlayerChar;
                }
                else
                {
                    agent.SetDestination(RandomNavmeshLocation(10f));
                }
            }
            else if (closestDistance == distanceBetweenEnemyAndYellowPlayer)
            {
                if (yellowPlayerChar.GetComponent<CoopCharacterHealthControllerThree>().PlayerState == "Alive")
                {
                    targetPlayer = yellowPlayerChar;
                }
                else if (Mathf.Min(distanceBetweenEnemyAndRedPlayer, distanceBetweenEnemyAndBluePlayer) == distanceBetweenEnemyAndRedPlayer)
                {
                    if (redPlayerChar.GetComponent<CoopCharacterHealthControllerTwo>().PlayerState == "Alive")
                    {
                        targetPlayer = redPlayerChar;
                    }
                    else if (bluePlayerChar.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                    {
                        targetPlayer = bluePlayerChar;
                    }
                    else
                    {
                        agent.SetDestination(RandomNavmeshLocation(10f));
                    }
                }
                else if (bluePlayerChar.GetComponent<CoopCharacterHealthControllerOne>().PlayerState == "Alive")
                {
                    targetPlayer = bluePlayerChar;
                }
                else
                {
                    agent.SetDestination(RandomNavmeshLocation(10f));
                }
            }
        }
        else
        if (isitCoop == false)
        {
            targetPlayer = singlePlayerChar;
        }
        Target_Or_SegmentAhead = targetPlayer;
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

        if (colourOfSnake == "blue")
        {
            if (theCol.gameObject.CompareTag("BlueBullet") || theCol.gameObject.CompareTag("RainbowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                segmentHealth -= 1;
                if (isAggroPlayer == false)
                {
                    ToggleAggro();
                }
                Destroy(theCol.gameObject);
            }
        }
        if (colourOfSnake == "red")
        {
            if (theCol.gameObject.CompareTag("RedBullet") || theCol.gameObject.CompareTag("RainbowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                segmentHealth -= 1;
                if (isAggroPlayer == false)
                {
                    ToggleAggro();
                }
                Destroy(theCol.gameObject);
            }
        }
        if (colourOfSnake == "yellow")
        {
            if (theCol.gameObject.CompareTag("YellowBullet") || theCol.gameObject.CompareTag("RainbowBullet"))
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                segmentHealth -= 1;
                if (isAggroPlayer == false)
                {
                    ToggleAggro();
                }
                Destroy(theCol.gameObject);
            }
        }
    }

    public void ChangeToBlue()
    {
        if (name == "SnakeHead")
        {
            colourOfSnake = "blue";
            SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";

            thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;

            gameObject.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;

            sphereIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;
        }
    }

    public void ChangeToRed()
    {
        if (name == "SnakeHead")
        {
            colourOfSnake = "red";
            SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";

            thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;

            gameObject.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;

            triangleIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;
        }
    }
    public void ChangeToYellow()
    {
        if (name == "SnakeHead")
        {
            colourOfSnake = "yellow";
            SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";

            thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;

            gameObject.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;

            squareIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;
        }
    }

    public void RandomAllSameColour()
    {
        if (name == "SnakeHead")
        {
            randomColour = Random.Range(1, 4);
            if (randomColour == 1)
            {
                colourOfSnake = "blue";
                SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "blue";

                thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;

                gameObject.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;

                sphereIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().sphereIndicatorRenderer.enabled = true;

            }
            else if (randomColour == 2)
            {
                colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";

                thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;

                gameObject.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;

                triangleIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().triangleIndicatorRenderer.enabled = true;

            }
            else if (randomColour == 3)
            {
                colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";

                thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;

                gameObject.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;

                squareIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().squareIndicatorRenderer.enabled = true;

            }
        }
    }

    public void MaxRandom()
    {
        randomColour = Random.Range(1, 4);
        if (randomColour == 1)
        {
            colourOfSnake = "blue";
            if (name == "SnakeHead")
            {
                thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
            }
            else if (name == "SnakeTail")
            {
                thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
                thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
            }
            else
            {
                thisSegmentsSnakeHead.GetComponent<Renderer>().material = blueMaterial;
                thisSegmentsSnakeTail.GetComponent<Renderer>().material = blueMaterial;
                thisSegmentsSnakeBody.GetComponent<Renderer>().material = blueMaterial;
            }

            sphereIndicatorRenderer.enabled = true;

            gameObject.GetComponent<ParticleSystemRenderer>().material = blueParticle;

        }
        else if (randomColour == 2)
        {
            colourOfSnake = "red";

            if (name == "SnakeHead")
            {
                thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
            }
            else if (name == "SnakeTail")
            {
                thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
                thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
            }
            else
            {
                thisSegmentsSnakeHead.GetComponent<Renderer>().material = redMaterial;
                thisSegmentsSnakeTail.GetComponent<Renderer>().material = redMaterial;
                thisSegmentsSnakeBody.GetComponent<Renderer>().material = redMaterial;
            }

            triangleIndicatorRenderer.enabled = true;

            gameObject.GetComponent<ParticleSystemRenderer>().material = redParticle;

        }
        else if (randomColour == 3)
        {
            colourOfSnake = "yellow";

            if (name == "SnakeHead")
            {
                thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
            }
            else if (name == "SnakeTail")
            {
                thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
                thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
            }
            else
            {
                thisSegmentsSnakeHead.GetComponent<Renderer>().material = yellowMaterial;
                thisSegmentsSnakeTail.GetComponent<Renderer>().material = yellowMaterial;
                thisSegmentsSnakeBody.GetComponent<Renderer>().material = yellowMaterial;
            }

            squareIndicatorRenderer.enabled = true;

            gameObject.GetComponent<ParticleSystemRenderer>().material = yellowParticle;

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
        if (colourOfSnake == "blue")
        {
            cbCurrentIndicator = Instantiate(cbBlueIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
        else if (colourOfSnake == "red")
        {
            cbCurrentIndicator = Instantiate(cbRedIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
        else if (colourOfSnake == "yellow")
        {
            cbCurrentIndicator = Instantiate(cbYellowIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
    }
}
