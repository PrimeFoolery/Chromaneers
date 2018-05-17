using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePropScript : MonoBehaviour
{
    public enum DestructiblePropType
    {
        dust,
        clay,
        wood,
        glass
    }

    public DestructiblePropType thisPropsMaterialType;

    public GameObject dustExplosion;
    public GameObject clayExplosion;
    public GameObject woodExplosion;
    public GameObject glassExplosion;

    public GameObject Candy;
    public GameObject Heart;
    public GameObject newSpawnPoint;
    private GameObject justSpawnedSpawnPoint;

    public string spawnEnemy;

    public List<newSpawner.enemyTypes> blueEnemy = new List<newSpawner.enemyTypes>();
    public List<newSpawner.enemyTypes> redEnemy = new List<newSpawner.enemyTypes>();
    public List<newSpawner.enemyTypes> yellowEnemy = new List<newSpawner.enemyTypes>();

    public List<newSpawner.enemyTypes> fastEnemy = new List<newSpawner.enemyTypes>();

    // Use this for initialization
    void Start () {
        //FIND NEAREST SPAWNPOINT 
	}
	
	// Update is called once per frame
	void Update () {
	    if (spawnEnemy == "blue")
	    {
            Debug.Log("spawning blie");
	        float waitTime = 0.5f;
	        waitTime -= Time.deltaTime;
	        if (waitTime < 0)
	        {
	            justSpawnedSpawnPoint.GetComponent<newSpawner>().SpawnEnemies(blueEnemy);
	        }
            Destroy(this.gameObject);
        }
        else if (spawnEnemy == "red")
	    {
	        Debug.Log("spawningred");
            float waitTime = 0.5f;
	        waitTime -= Time.deltaTime;
	        if (waitTime < 0)
	        {
	            justSpawnedSpawnPoint.GetComponent<newSpawner>().SpawnEnemies(redEnemy);
	        }
	        Destroy(this.gameObject);
        }
        else if (spawnEnemy == "yellow")
	    {
	        Debug.Log("spawning yelloq");
            float waitTime = 0.5f;
	        waitTime -= Time.deltaTime;
	        if (waitTime < 0)
	        {
	            justSpawnedSpawnPoint.GetComponent<newSpawner>().SpawnEnemies(yellowEnemy);
	        }
	        Destroy(this.gameObject);
        }
	    else if (spawnEnemy == "fast")
	    {
	        Debug.Log("spawning fast");
	        float waitTime = 0.5f;
	        waitTime -= Time.deltaTime;
	        if (waitTime < 0)
	        {
	            justSpawnedSpawnPoint.GetComponent<newSpawner>().SpawnEnemies(fastEnemy);
	        }
	        Destroy(this.gameObject);
	    }
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("somethingColliding");
        if (other.gameObject.CompareTag("BlueBullet")|| other.gameObject.CompareTag("RedBullet")|| other.gameObject.CompareTag("YellowBullet")||other.gameObject.CompareTag("RainbowBullet"))
        {
            //Debug.Log("bulletColliding");
            if (thisPropsMaterialType==DestructiblePropType.dust)
            {
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
            }else if (thisPropsMaterialType==DestructiblePropType.clay)
            {
                Instantiate(clayExplosion, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), Quaternion.identity);
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
            }
            else if (thisPropsMaterialType == DestructiblePropType.glass)
            {
                Instantiate(glassExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
            }
            else if (thisPropsMaterialType == DestructiblePropType.wood)
            {
                Instantiate(woodExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
            }

            DestroyDestructible();
        }

        if (other.gameObject.CompareTag("BluePlayer"))
        {
            if (other.gameObject.GetComponent<CoopCharacterControllerOne>().currentlyDodging == true)
            {
                if (thisPropsMaterialType == DestructiblePropType.dust)
                {
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.clay)
                {
                    Instantiate(clayExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.glass)
                {
                    Instantiate(glassExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.wood)
                {
                    Instantiate(woodExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                DestroyDestructible();
            }
        }
        if (other.gameObject.CompareTag("RedPlayer"))
        {
            if (other.gameObject.GetComponent<CoopCharacterControllerTwo>().currentlyDodging == true)
            {
                if (thisPropsMaterialType == DestructiblePropType.dust)
                {
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.clay)
                {
                    Instantiate(clayExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.glass)
                {
                    Instantiate(glassExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.wood)
                {
                    Instantiate(woodExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                DestroyDestructible();
            }
        }
        if (other.gameObject.CompareTag("YellowPlayer"))
        {
            if (other.gameObject.GetComponent<CoopCharacterControllerThree>().currentlyDodging == true)
            {
                if (thisPropsMaterialType == DestructiblePropType.dust)
                {
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.clay)
                {
                    Instantiate(clayExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.glass)
                {
                    Instantiate(glassExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.wood)
                {
                    Instantiate(woodExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                DestroyDestructible();
            }
        }
    }

    void DestroyDestructible()
    {
        int randomNumber = Random.Range(0, 100);
        if (randomNumber>0&&randomNumber<=40)
        {
            //Spawn candy
            Instantiate(Candy, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (randomNumber>40 && randomNumber <= 45 )
        {
            //Spawn Heart
            Instantiate(Heart, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
