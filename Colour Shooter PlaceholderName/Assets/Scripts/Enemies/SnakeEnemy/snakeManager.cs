using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class snakeManager : MonoBehaviour
{

    public int amountOfSnakeSegments = 6;
    private EnemyManager enemyManagerScript;
    public List<SnakeEnemyScript> snakeSegments = new List<SnakeEnemyScript>();

    private GameObject thisEnemiesSpawnPoint;

    // Use this for initialization
    void Start()
    {
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        thisEnemiesSpawnPoint = snakeSegments[0].thisEnemiesSpawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfSnakeSegments <= 0)
        {
            thisEnemiesSpawnPoint.GetComponent<newSpawner>().ThisSpawnpointsEnemyList.Remove(gameObject);
            //enemyManagerScript.enemyList.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    public void SegmentKilled()
    {
        amountOfSnakeSegments -= 1;
        if (amountOfSnakeSegments == 5)
        {
            foreach (SnakeEnemyScript snakeSegmentScript in snakeSegments)
            {
                snakeSegmentScript.headNormalSpeed = 3.33f;
                snakeSegmentScript.headYellowSpeed = 5.33f;
                snakeSegmentScript.headBlueSpeed = 1.33f;
                snakeSegmentScript.bodyNormalSpeed = 5.33f;
                snakeSegmentScript.bodyYellowSpeed = 7.33f;
                snakeSegmentScript.bodyBlueSpeed = 3.33f;
            }
        }
        else if (amountOfSnakeSegments == 4)
        {
            foreach (SnakeEnemyScript snakeSegmentScript in snakeSegments)
            {
                snakeSegmentScript.headNormalSpeed = 3.66f;
                snakeSegmentScript.headYellowSpeed = 5.66f;
                snakeSegmentScript.headBlueSpeed = 1.66f;
                snakeSegmentScript.bodyNormalSpeed = 5.66f;
                snakeSegmentScript.bodyYellowSpeed = 7.66f;
                snakeSegmentScript.bodyBlueSpeed = 3.66f;
            }
        }
        else if (amountOfSnakeSegments == 3)
        {
            foreach (SnakeEnemyScript snakeSegmentScript in snakeSegments)
            {
                snakeSegmentScript.headNormalSpeed = 4f;
                snakeSegmentScript.headYellowSpeed = 6f;
                snakeSegmentScript.headBlueSpeed = 2f;
                snakeSegmentScript.bodyNormalSpeed = 6f;
                snakeSegmentScript.bodyYellowSpeed = 8f;
                snakeSegmentScript.bodyBlueSpeed = 4f;
            }
        }
        else if (amountOfSnakeSegments == 2)
        {
            foreach (SnakeEnemyScript snakeSegmentScript in snakeSegments)
            {
                snakeSegmentScript.headNormalSpeed = 4.33f;
                snakeSegmentScript.headYellowSpeed = 6.33f;
                snakeSegmentScript.headBlueSpeed = 2.33f;
                snakeSegmentScript.bodyNormalSpeed = 6.33f;
                snakeSegmentScript.bodyYellowSpeed = 8.33f;
                snakeSegmentScript.bodyBlueSpeed = 4.33f;
            }
        }
        else if (amountOfSnakeSegments == 1)
        {
            foreach (SnakeEnemyScript snakeSegmentScript in snakeSegments)
            {
                snakeSegmentScript.headNormalSpeed = 4.66f;
                snakeSegmentScript.headYellowSpeed = 6.66f;
                snakeSegmentScript.headBlueSpeed = 2.66f;
                snakeSegmentScript.bodyNormalSpeed = 6.66f;
                snakeSegmentScript.bodyYellowSpeed = 8.66f;
                snakeSegmentScript.bodyBlueSpeed = 4.66f;
            }
        }
    }
}
