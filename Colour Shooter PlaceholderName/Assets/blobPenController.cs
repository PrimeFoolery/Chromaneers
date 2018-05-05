using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobPenController : MonoBehaviour
{
    public enum ColoursOfPen
    {
        blue,
        yellow,
        red
    }

    public ColoursOfPen colourOfThisPen = ColoursOfPen.blue;

    private int amountOfCorrectEnemies = 0;
    private bool doorOpened = false;

    public GameObject thisPensInfiniteSpawnTrigger;
    public GameObject doorToOpen;

    public List<GameObject> CorrectEnemiesInPen = new List<GameObject>();

    public GameObject pensSpawnPoint;
    private List<InfiniteSpawnPoint.enemyTypes> enemyToSpawn = new List<InfiniteSpawnPoint.enemyTypes>();

    public GameObject penLeftWall;
    public GameObject penRightWall;
    public GameObject penTopWall;
    public GameObject penBottomWall;
    public GameObject penFloor;

    public GameObject obelisk1;
    public GameObject obelisk2;
    public GameObject obelisk3;
    public GameObject obelisk4;

    public Material bluePenWall;
    public Material redPenWall;
    public Material yellowPenWall;
    public Material bluePenFloor;
    public Material redPenFloor;
    public Material yellowPenFloor;

    public Texture2D penObeliskEmissionMap0;
    public Texture2D penObeliskEmissionMap1;
    public Texture2D penObeliskEmissionMap2;
    public Texture2D penObeliskEmissionMap3;
    public Texture2D penObeliskEmissionMap4;
    public Texture2D penObeliskEmissionMap5;




    // Use this for initialization
    void Start () {
	    if (colourOfThisPen == ColoursOfPen.blue)
	    {
	        penLeftWall.GetComponent<Renderer>().material = bluePenWall;
	        penTopWall.GetComponent<Renderer>().material = bluePenWall;
	        penRightWall.GetComponent<Renderer>().material = bluePenWall;
	        penBottomWall.GetComponent<Renderer>().material = bluePenWall;
	        penFloor.GetComponent<Renderer>().material = bluePenFloor;
	        obelisk1.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
	        obelisk2.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
	        obelisk3.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
	        obelisk4.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
            enemyToSpawn.Add(InfiniteSpawnPoint.enemyTypes.StandardBlue);
            pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().SpawnEnemies(enemyToSpawn);
            pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
        }
	    else if (colourOfThisPen == ColoursOfPen.red)
	    {
	        penLeftWall.GetComponent<Renderer>().material = redPenWall;
	        penTopWall.GetComponent<Renderer>().material = redPenWall;
	        penRightWall.GetComponent<Renderer>().material = redPenWall;
	        penBottomWall.GetComponent<Renderer>().material = redPenWall;
	        penFloor.GetComponent<Renderer>().material = redPenFloor;
	        obelisk1.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
	        obelisk2.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
	        obelisk3.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
	        obelisk4.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
	        enemyToSpawn.Add(InfiniteSpawnPoint.enemyTypes.StandardRed);
	        pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().SpawnEnemies(enemyToSpawn);
	        pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
        }
	    else if (colourOfThisPen == ColoursOfPen.yellow)
	    {
	        penLeftWall.GetComponent<Renderer>().material = yellowPenWall;
	        penTopWall.GetComponent<Renderer>().material = yellowPenWall;
	        penRightWall.GetComponent<Renderer>().material = yellowPenWall;
	        penBottomWall.GetComponent<Renderer>().material = yellowPenWall;
	        penFloor.GetComponent<Renderer>().material = yellowPenFloor;
	        obelisk1.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
	        obelisk2.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
	        obelisk3.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
	        obelisk4.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
	        enemyToSpawn.Add(InfiniteSpawnPoint.enemyTypes.StandardYellow);
	        pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().SpawnEnemies(enemyToSpawn);
	        pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
        }
    }
	
	// Update is called once per frame
	void Update () {
	    foreach (GameObject enemy in CorrectEnemiesInPen)
	    {
	        if (enemy.transform.position.x<penLeftWall.transform.position.x)
	        {
	            enemy.transform.position = new Vector3(penLeftWall.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
	        }else if (enemy.transform.position.x > penRightWall.transform.position.x)
	        {
	            enemy.transform.position = new Vector3(penRightWall.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            }

	        if (enemy.transform.position.z < penBottomWall.transform.position.z)
	        {
	            enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, penBottomWall.transform.position.z);
            }
	        else if(enemy.transform.position.z > penTopWall.transform.position.z)
	        {
	            enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, penTopWall.transform.position.z);
            }
	    }

	    if (amountOfCorrectEnemies==0)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap0);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap0);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap0);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap0);
        }
        else if (amountOfCorrectEnemies == 1)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap1);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap1);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap1);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap1);
        }
	    else if (amountOfCorrectEnemies == 2)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap2);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap2);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap2);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap2);
	    }
	    else if (amountOfCorrectEnemies == 3)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap3);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap3);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap3);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap3);
	    }
	    else if (amountOfCorrectEnemies == 4)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap4);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap4);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap4);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap4);
	    }
	    else if (amountOfCorrectEnemies == 5)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap5);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap5);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap5);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap5);
	    }
        if (amountOfCorrectEnemies>=5&&doorOpened==false)
	    {
            doorToOpen.GetComponent<doorController>().OpenSesame();
	        doorOpened = true;
	    }
	}

    void OnTriggerEnter(Collider other)
    {
        if (colourOfThisPen==ColoursOfPen.blue)
        {
            if (other.gameObject.CompareTag("BlueEnemy"))
            {
                if (other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
                {
                    other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>()
                        .ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                else if (other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
                {
                    other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<newSpawner>()
                        .ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                CorrectEnemiesInPen.Add(other.gameObject);
                other.gameObject.layer = 10;
                amountOfCorrectEnemies += 1;

            }
        }else
        if (colourOfThisPen == ColoursOfPen.red)
        {
            if (other.gameObject.CompareTag("RedEnemy"))
            {
                if (other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
                {
                    other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>()
                        .ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                else if (other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
                {
                    other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<newSpawner>()
                        .ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                CorrectEnemiesInPen.Add(other.gameObject);
                other.gameObject.layer = 10;
                amountOfCorrectEnemies += 1;

            }
        }
        else if (colourOfThisPen == ColoursOfPen.yellow)
        {
            if (other.gameObject.CompareTag("YellowEnemy"))
            {
                if (other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
                {
                    other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>()
                        .ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                else if (other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
                {
                    other.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint.GetComponent<newSpawner>()
                        .ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                CorrectEnemiesInPen.Add(other.gameObject);
                other.gameObject.layer = 10;
                amountOfCorrectEnemies += 1;

            }
        }
    }
}
