using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SnakeEnemyScript : MonoBehaviour
{

    public Transform Target_Or_SegmentAhead;
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
    private float timer = 3f;
    private int segmentHealth = 3;
    public bool isAggroPlayer = false;
    public GameObject thisEnemiesSpawnPoint;
    public string colourOfSnake;
    public int randomColour;
    public float headNormalSpeed = 3;
    public float headYellowSpeed = 5;
    public float headBlueSpeed = 1;
    public float bodyNormalSpeed = 5;
    public float bodyYellowSpeed = 7;
    public float bodyBlueSpeed = 3;

    public Material blueMaterial;
    public GameObject blueSplat;
    public Material blueParticle;
    public Material redMaterial;
    public GameObject redSplat;
    public Material redParticle;
    public Material yellowMaterial;
    public GameObject yellowSplat;
    public Material yellowParticle;

    public GameObject singlePlayerChar;

    public GameObject redPlayerChar;
    public GameObject bluePlayerChar;
    public GameObject yellowPlayerChar;

    public string colourOfPaintBelow = "null";
    public bool colourOverride = false;
    private float poisonTimer = 4f;


    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>();
        spawner = gameManager.gameObject.GetComponent<EnemySpawner>();
        enemyManagerScript = gameManager.gameObject.GetComponent<EnemyManager>();
        agent = gameObject.GetComponent<NavMeshAgent>();



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
    }

    // Update is called once per frame
    void Update()
    {
        if (isAggroPlayer == true)
        {
            agent.SetDestination(Target_Or_SegmentAhead.position);
        }

        if (colourOfPaintBelow != "yellow" && colourOfPaintBelow != "blue")
        {
            if (name != "SnakeHead" && Vector3.Distance(transform.position, Target_Or_SegmentAhead.position) > 3f)
            {
                agent.speed = bodyYellowSpeed;
            }
            else if (name != "SnakeHead" && Vector3.Distance(transform.position, Target_Or_SegmentAhead.position) < 3f)
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
                timer = 3f;
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

            if (name != "SnakeHead")
            {
                Target_Or_SegmentAhead.GetComponent<SnakeEnemyScript>().SegmentBehind = null;
                if (Target_Or_SegmentAhead.name != "SnakeHead")
                {
                    Target_Or_SegmentAhead.name = "SnakeTail";
                }
            }

            if (name != "SnakeTail" && SegmentBehind != null)
            {
                SegmentBehind.GetComponent<SnakeEnemyScript>().Target_Or_SegmentAhead = null;
                SegmentBehind.name = "SnakeHead";
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
        Target_Or_SegmentAhead = targetPlayer.transform;
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
            if (theCol.gameObject.CompareTag("BlueBullet"))
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
            if (theCol.gameObject.CompareTag("RedBullet"))
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
            if (theCol.gameObject.CompareTag("YellowBullet"))
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

            SphereRenderer.material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;

            gameObject.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
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

            SphereRenderer.material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;

            gameObject.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;

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

            SphereRenderer.material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;

            gameObject.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
            SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;

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

                SphereRenderer.material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = blueMaterial;

                gameObject.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = blueParticle;

            }
            else if (randomColour == 2)
            {
                colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "red";

                SphereRenderer.material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = redMaterial;

                gameObject.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = redParticle;

            }
            else if (randomColour == 3)
            {
                colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().colourOfSnake = "yellow";

                SphereRenderer.material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SphereRenderer.material = yellowMaterial;

                gameObject.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;
                SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<SnakeEnemyScript>().SegmentBehind.GetComponent<ParticleSystemRenderer>().material = yellowParticle;

            }
        }
    }

    public void MaxRandom()
    {
        randomColour = Random.Range(1, 4);
        if (randomColour == 1)
        {
            colourOfSnake = "blue";

            SphereRenderer.material = blueMaterial;

            gameObject.GetComponent<ParticleSystemRenderer>().material = blueParticle;

        }
        else if (randomColour == 2)
        {
            colourOfSnake = "red";

            SphereRenderer.material = redMaterial;

            gameObject.GetComponent<ParticleSystemRenderer>().material = redParticle;

        }
        else if (randomColour == 3)
        {
            colourOfSnake = "yellow";

            SphereRenderer.material = yellowMaterial;

            gameObject.GetComponent<ParticleSystemRenderer>().material = yellowParticle;

        }
    }

    void ToggleAggro()
    {
        thisEnemiesSpawnPoint.GetComponent<newSpawner>().ToggleAggro();
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
}
