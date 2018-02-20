using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandardEnemyBehaviour : MonoBehaviour {

    public GameObject player;
    private GameObject targetPlayer;
    NavMeshAgent agent;
    public bool isItCoop;
    private ColourSelectManager gameManager;

    //COOP PLAYER VARIABLES
    private GameObject RedPlayer;
    private GameObject BluePlayer;
    private GameObject YellowPlayer;






    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>();
        if (gameManager.isItSingleplayer==true)
        {
            isItCoop = false;
        }
        if (gameManager.isItSingleplayer == false)
        {
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
        if (!isItCoop) {
            if (player==null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            else if (player!=null)
            {
                agent.SetDestination(targetPlayer.transform.position);
            }
			
		}
		if (isItCoop) {
            FindClosestPlayer();
			agent.SetDestination (targetPlayer.transform.position);

		}
	}
    void FindClosestPlayer()
    {   //THIS CALCULATES THE DISTANCE BETWEEN THE ENEMY AND ALL OF THE PLAYERS AND THEN FINDS THE LOWEST AND SETS THE TARGETED PLAYER TO THAT
        float distanceBetweenEnemyAndRedPlayer = Vector3.Distance(transform.position, RedPlayer.transform.position);
        float distanceBetweenEnemyAndBluePlayer = Vector3.Distance(transform.position, BluePlayer.transform.position);
        float distanceBetweenEnemyAndYellowPlayer = Vector3.Distance(transform.position, YellowPlayer.transform.position);
        float closestDistance = Mathf.Min(Mathf.Abs(distanceBetweenEnemyAndBluePlayer), Mathf.Abs(distanceBetweenEnemyAndRedPlayer), Mathf.Abs(distanceBetweenEnemyAndYellowPlayer));
        if (closestDistance==distanceBetweenEnemyAndRedPlayer)
        {
            targetPlayer = RedPlayer;
        } else if (closestDistance==distanceBetweenEnemyAndBluePlayer)
        {
            targetPlayer = BluePlayer;
        } else if (closestDistance==distanceBetweenEnemyAndYellowPlayer)
        {
            targetPlayer = YellowPlayer;
        }
    }
}
