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
    public GameObject ObjectToTrigger;

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
    public Texture2D penObeliskEmissionMap6;
    public Texture2D penObeliskEmissionMap7;
    public Texture2D penObeliskEmissionMap8;
    public Texture2D penObeliskEmissionMap9;
    public Texture2D penObeliskEmissionMap10;

    public GameObject blueFireworkLauncher;
    public GameObject redFireworkLauncher;
    public GameObject yellowFireworkLauncher;

    private bool enemySpawned = false;

    public Material whiteEnemyMat;
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
	        //enemyToSpawn.Add(InfiniteSpawnPoint.enemyTypes.StandardRed);
	        //pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().SpawnEnemies(enemyToSpawn);
	        //pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
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
	        //enemyToSpawn.Add(InfiniteSpawnPoint.enemyTypes.StandardYellow);
	       // pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().SpawnEnemies(enemyToSpawn);
	        //pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (enemySpawned==false)
	    {
	        if (colourOfThisPen == ColoursOfPen.blue)
	        {
	            enemyToSpawn.Add(InfiniteSpawnPoint.enemyTypes.StandardBlue);
	            pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().SpawnEnemies(enemyToSpawn);
	            pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
            }
            else if (colourOfThisPen == ColoursOfPen.red)
	        {
	            enemyToSpawn.Add(InfiniteSpawnPoint.enemyTypes.StandardRed);
	            pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().SpawnEnemies(enemyToSpawn);
	            pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
	        }
	        else if (colourOfThisPen == ColoursOfPen.yellow)
	        {
	            enemyToSpawn.Add(InfiniteSpawnPoint.enemyTypes.StandardYellow);
	            pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().SpawnEnemies(enemyToSpawn);
	            pensSpawnPoint.GetComponent<InfiniteSpawnPoint>().ToggleAggro();
	        }

	        enemySpawned = true;
	    }
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
	    else if (amountOfCorrectEnemies == 6)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap6);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap6);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap6);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap6);
	    }
	    else if (amountOfCorrectEnemies == 7)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap7);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap7);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap7);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap7);
	    }
	    else if (amountOfCorrectEnemies == 8)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap8);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap8);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap8);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap8);
	    }
	    else if (amountOfCorrectEnemies == 9)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap9);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap9);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap9);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap9);
	    }
	    else if (amountOfCorrectEnemies == 10)
	    {
	        obelisk1.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap10);
	        obelisk2.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap10);
	        obelisk3.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap10);
	        obelisk4.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap10);
	    }
        if (amountOfCorrectEnemies>=10&&doorOpened==false)
	    {
	        if (ObjectToTrigger.GetComponent<doorController>()!=null)
	        {
                ObjectToTrigger.GetComponent<doorController>().OpenSesame();
	        }

	        if (ObjectToTrigger.GetComponent<cableController>()!=null)
	        {
	            if (colourOfThisPen== ColoursOfPen.blue)
	            {
	                Instantiate(blueFireworkLauncher, transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
	                ObjectToTrigger.GetComponent<cableController>().Trigger(Color.blue);
                }else if (colourOfThisPen == ColoursOfPen.red)
	            {
	                Instantiate(redFireworkLauncher, transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
                    ObjectToTrigger.GetComponent<cableController>().Trigger(Color.red);
	            }else if (colourOfThisPen == ColoursOfPen.yellow)
	            {
	                Instantiate(yellowFireworkLauncher, transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
                    ObjectToTrigger.GetComponent<cableController>().Trigger(Color.yellow);
	            }

            }
	        doorOpened = true;
	    }
	}

    void OnTriggerStay(Collider other)
    {
        if (colourOfThisPen==ColoursOfPen.blue)
        {
            if (other.gameObject.CompareTag("BlueEnemy") && other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material != whiteEnemyMat)
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

                other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material = whiteEnemyMat;
                CorrectEnemiesInPen.Add(other.gameObject);
                other.gameObject.layer = 10;
                amountOfCorrectEnemies += 1;

            }else if (other.gameObject.CompareTag("PurpleEnemy") && other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material != whiteEnemyMat)
            {
                if(other.gameObject.GetComponent<PurpleEnemyHealth>().blueHealth>0&& other.gameObject.GetComponent<PurpleEnemyHealth>().redHealth<=0)
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
                    other.gameObject.GetComponent<PurpleEnemyHealth>().isWhite = true;
                    other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material = whiteEnemyMat;
                    CorrectEnemiesInPen.Add(other.gameObject);
                    other.gameObject.layer = 10;
                    amountOfCorrectEnemies += 1;
                }
            }
            else if (other.gameObject.CompareTag("GreenEnemy") && other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material != whiteEnemyMat)
            {
                if (other.gameObject.GetComponent<GreenEnemyHealth>().blueHealth > 0 && other.gameObject.GetComponent<GreenEnemyHealth>().yellowHealth <= 0)
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
                    other.gameObject.GetComponent<GreenEnemyHealth>().isWhite = true;
                    other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material = whiteEnemyMat;
                    CorrectEnemiesInPen.Add(other.gameObject);
                    other.gameObject.layer = 10;
                    amountOfCorrectEnemies += 1;
                }
            } else if (other.gameObject.CompareTag("SpiderEnemy") && other.gameObject.GetComponentInChildren<Renderer>().material != whiteEnemyMat)
            {
                if (other.gameObject.GetComponentInChildren<SpiderEnemyController>().bodyColour==2&& other.gameObject.GetComponentInChildren<SpiderEnemyController>().howManyLegsAreAlive == 0)
                {
                    if (other.GetComponentInChildren<SpiderEnemyController>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>()!=null)
                    {
                        other.GetComponent<SpiderEnemyController>().thisEnemiesSpawnPoint
                            .GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList
                            .Remove(other.gameObject.transform.parent.gameObject);
                    }
                    if (other.GetComponentInChildren<SpiderEnemyController>().thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
                    {
                        other.GetComponent<SpiderEnemyController>().thisEnemiesSpawnPoint
                            .GetComponent<newSpawner>().ThisSpawnpointsEnemyList
                            .Remove(other.gameObject.transform.parent.gameObject);
                    }

                    other.gameObject.GetComponentInChildren<Renderer>().material = whiteEnemyMat;
                    CorrectEnemiesInPen.Add(other.gameObject);
                    other.gameObject.layer = 10;
                    amountOfCorrectEnemies += 1;    
                }
            }
            else if (other.gameObject.CompareTag("FastEnemy") && other.gameObject.GetComponent<FastEnemy>().SphereRenderer.material != whiteEnemyMat && other.gameObject.GetComponent<FastEnemy>().colourOfEnemy == "blue")
            {
                
                if (other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
                {
                   other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint
                    .GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                else if (other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
                {
                   other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint.GetComponent<newSpawner>()
                    .ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                other.gameObject.GetComponent<FastEnemy>().SphereRenderer.material = whiteEnemyMat;
                CorrectEnemiesInPen.Add(other.gameObject);
                other.gameObject.layer = 10;
                amountOfCorrectEnemies += 1;
            }
        }
        else
        if (colourOfThisPen == ColoursOfPen.red)
        {
            if (other.gameObject.CompareTag("RedEnemy") && other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material != whiteEnemyMat)
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
                other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material = whiteEnemyMat;
                CorrectEnemiesInPen.Add(other.gameObject);
                other.gameObject.layer = 10;
                amountOfCorrectEnemies += 1;

            }
            else if (other.gameObject.CompareTag("PurpleEnemy") && other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material != whiteEnemyMat)
            {
                if (other.gameObject.GetComponent<PurpleEnemyHealth>().redHealth > 0 && other.gameObject.GetComponent<PurpleEnemyHealth>().blueHealth <= 0)
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
                    other.gameObject.GetComponent<PurpleEnemyHealth>().isWhite = true;
                    other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material = whiteEnemyMat;
                    CorrectEnemiesInPen.Add(other.gameObject);
                    other.gameObject.layer = 10;
                    amountOfCorrectEnemies += 1;
                }
            }
            else if (other.gameObject.CompareTag("OrangeEnemy") && other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material != whiteEnemyMat)
            {
                if (other.gameObject.GetComponent<OrangeEnemyHealth>().redHealth > 0 && other.gameObject.GetComponent<OrangeEnemyHealth>().yellowHealth <= 0)
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
                    other.gameObject.GetComponent<OrangeEnemyHealth>().isWhite = true;
                    other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material = whiteEnemyMat;
                    CorrectEnemiesInPen.Add(other.gameObject);
                    other.gameObject.layer = 10;
                    amountOfCorrectEnemies += 1;
                }
            }
            else if (other.gameObject.CompareTag("SpiderEnemy") && other.gameObject.GetComponentInChildren<Renderer>().material != whiteEnemyMat)
            {
                if (other.gameObject.GetComponentInChildren<SpiderEnemyController>().bodyColour == 1 && other.gameObject.GetComponentInChildren<SpiderEnemyController>().howManyLegsAreAlive == 0)
                {
                    if (other.GetComponentInChildren<SpiderEnemyController>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
                    {
                        other.GetComponent<SpiderEnemyController>().thisEnemiesSpawnPoint
                            .GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList
                            .Remove(other.gameObject.transform.parent.gameObject);
                    }
                    if (other.GetComponentInChildren<SpiderEnemyController>().thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
                    {
                        other.GetComponent<SpiderEnemyController>().thisEnemiesSpawnPoint
                            .GetComponent<newSpawner>().ThisSpawnpointsEnemyList
                            .Remove(other.gameObject.transform.parent.gameObject);
                    }

                    other.gameObject.GetComponentInChildren<Renderer>().material = whiteEnemyMat;
                    CorrectEnemiesInPen.Add(other.gameObject);
                    other.gameObject.layer = 10;
                    amountOfCorrectEnemies += 1;
                }
            }
            else if (other.gameObject.CompareTag("FastEnemy") && other.gameObject.GetComponent<FastEnemy>().SphereRenderer.material != whiteEnemyMat && other.gameObject.GetComponent<FastEnemy>().colourOfEnemy == "red")
            {

                if (other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
                {
                    other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint
                        .GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                else if (other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
                {
                    other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint.GetComponent<newSpawner>()
                        .ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                other.gameObject.GetComponent<FastEnemy>().SphereRenderer.material = whiteEnemyMat;
                CorrectEnemiesInPen.Add(other.gameObject);
                other.gameObject.layer = 10;
                amountOfCorrectEnemies += 1;
            }
        }
        else if (colourOfThisPen == ColoursOfPen.yellow)
        {
            if (other.gameObject.CompareTag("YellowEnemy") && other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material != whiteEnemyMat)
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
                other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material = whiteEnemyMat;
                CorrectEnemiesInPen.Add(other.gameObject);
                other.gameObject.layer = 10;
                amountOfCorrectEnemies += 1;

            }
            else if (other.gameObject.CompareTag("GreenEnemy") && other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material != whiteEnemyMat)
            {
                if (other.gameObject.GetComponent<GreenEnemyHealth>().yellowHealth > 0 && other.gameObject.GetComponent<GreenEnemyHealth>().blueHealth <= 0)
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

                    other.gameObject.GetComponent<GreenEnemyHealth>().isWhite = true;
                    other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material = whiteEnemyMat;
                    CorrectEnemiesInPen.Add(other.gameObject);
                    other.gameObject.layer = 10;
                    amountOfCorrectEnemies += 1;
                }
            }
            else if (other.gameObject.CompareTag("OrangeEnemy") && other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material != whiteEnemyMat)
            {
                if (other.gameObject.GetComponent<OrangeEnemyHealth>().yellowHealth > 0 && other.gameObject.GetComponent<OrangeEnemyHealth>().redHealth <= 0)
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
                    other.gameObject.GetComponent<OrangeEnemyHealth>().isWhite = true;
                    other.gameObject.GetComponent<StandardEnemyBehaviour>().SphereRenderer.material = whiteEnemyMat;
                    CorrectEnemiesInPen.Add(other.gameObject);
                    other.gameObject.layer = 10;
                    amountOfCorrectEnemies += 1;
                }
            }
            else if (other.gameObject.CompareTag("SpiderEnemy") && other.gameObject.GetComponentInChildren<Renderer>().material != whiteEnemyMat)
            {
                if (other.gameObject.GetComponentInChildren<SpiderEnemyController>().bodyColour == 3 && other.gameObject.GetComponentInChildren<SpiderEnemyController>().howManyLegsAreAlive == 0)
                {
                    if (other.GetComponentInChildren<SpiderEnemyController>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
                    {
                        other.GetComponent<SpiderEnemyController>().thisEnemiesSpawnPoint
                            .GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList
                            .Remove(other.gameObject.transform.parent.gameObject);
                    }
                    if (other.GetComponentInChildren<SpiderEnemyController>().thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
                    {
                        other.GetComponent<SpiderEnemyController>().thisEnemiesSpawnPoint
                            .GetComponent<newSpawner>().ThisSpawnpointsEnemyList
                            .Remove(other.gameObject.transform.parent.gameObject);
                    }

                    other.gameObject.GetComponentInChildren<Renderer>().material = whiteEnemyMat;
                    CorrectEnemiesInPen.Add(other.gameObject);
                    other.gameObject.layer = 10;
                    amountOfCorrectEnemies += 1;
                }
            }
            else if (other.gameObject.CompareTag("FastEnemy") && other.gameObject.GetComponent<FastEnemy>().SphereRenderer.material != whiteEnemyMat && other.gameObject.GetComponent<FastEnemy>().colourOfEnemy == "yellow")
            {

                if (other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
                {
                    other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint
                        .GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                else if (other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
                {
                    other.GetComponent<FastEnemy>().thisEnemiesSpawnPoint.GetComponent<newSpawner>()
                        .ThisSpawnpointsEnemyList.Remove(other.gameObject);
                }
                other.gameObject.GetComponent<FastEnemy>().SphereRenderer.material = whiteEnemyMat;
                CorrectEnemiesInPen.Add(other.gameObject);
                other.gameObject.layer = 10;
                amountOfCorrectEnemies += 1;
            }
        }
    }
}
