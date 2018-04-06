using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class snakeManager : MonoBehaviour {

    public int amountOfSnakeSegments = 6;
    private EnemyManager enemyManagerScript;

    // Use this for initialization
    void Start ()
    {
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (amountOfSnakeSegments<=0)
	    {
	        //enemyManagerScript.enemyList.Remove(gameObject);
            Destroy(gameObject);
	    }
	}
}
