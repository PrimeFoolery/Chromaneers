﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderEnemyController : MonoBehaviour
{

    private int bodyColour;
    private Material bodyMaterial;
    private int legColour;
    private string legMaterial;
    public GameObject leg1;
    public GameObject leg2;
    public GameObject leg3;
    public GameObject leg4;
    public int howManyLegsAreAlive = 4;
    private bool bodyDropped = false;
    private int bodyHealth = 5;

    public Material RedJellyMaterial;
    public Material BlueJellyMaterial;
    public Material YellowJellyMaterial;
    public Material BlueLegMaterial;
    public Material RedLegMaterial;
    public Material YellowLegMaterial;
    public Material RedParticleMaterial;
    public Material BlueParticleMaterial;
    public Material YellowParticleMaterial;

    public GameObject player;
    private GameObject targetPlayer;
    NavMeshAgent agent;
    public bool isItCoop;
    private ColourSelectManager gameManager;

    //COOP PLAYER VARIABLES
    private GameObject RedPlayer;
    private GameObject BluePlayer;
    private GameObject YellowPlayer;
    private float retargetingDelay = 5f;
    private bool readyToRetarget = true;

    // Use this for initialization
    void Start ()
	{
	    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>();

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
            } else if (legColour == 2)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
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
	        legColour = Random.Range(1, 3);
	        if (legColour == 1)
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
	        else if (legColour == 2)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
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
        Debug.Log("leg colour:  "+legColour+"  body colour:  "+bodyColour);
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
	            agent.SetDestination(targetPlayer.transform.position);
	        }

	    }
	    if (isItCoop)
	    {
	        if (retargetingDelay == 5f)
	        {
	            FindClosestPlayer();
	        }
	        agent.SetDestination(targetPlayer.transform.position);

	    }
	    if (readyToRetarget == false)//DELAYS THE RETARGETING TO STOP PLAYER TARGET SWAPPING
	    {
	        retargetingDelay -= Time.deltaTime;
	    }
	    if (retargetingDelay <= 0f)
	    {
	        readyToRetarget = true;
	        retargetingDelay = 5f;
	    }
        if (howManyLegsAreAlive==4)
        {
            agent.speed = 4f;
        }
	    if (howManyLegsAreAlive == 3)
	    {
	        agent.speed = 3.5f;
        }
	    if (howManyLegsAreAlive == 2)
	    {
	        agent.speed = 3f;
        }
	    if (howManyLegsAreAlive == 1)
	    {
	        agent.speed = 2.5f;
        }

	    if (howManyLegsAreAlive == 0)
	    {
	        agent.speed = 1f;
	        if (bodyDropped==false)
	        {
                GetComponentInParent<Transform>().position = new Vector3(GetComponentInParent<Transform>().position.x,0, GetComponentInParent<Transform>().position.z);
	            transform.position = new Vector3(transform.position.x, targetPlayer.transform.position.y, transform.position.z);
	            bodyDropped = true;
	        }
	    }
        if(bodyHealth<=0)
	    {
            gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
	    }
    }

    void OnCollisionEnter(Collision other)
    {
        if (bodyColour==1)
        {
            if (other.gameObject.tag == "RedBullet")
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                bodyHealth -= 1;
                Destroy(other.gameObject);
            }
        }
        if (bodyColour == 2)
        {
            if (other.gameObject.tag == "BlueBullet")
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                bodyHealth -= 1;
                Destroy(other.gameObject);
            }
        }
        if (bodyColour == 3)
        {
            if (other.gameObject.tag == "YellowBullet")
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                bodyHealth -= 1;
                Destroy(other.gameObject);
            }
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
}