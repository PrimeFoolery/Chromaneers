using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeEnemyHealth : MonoBehaviour {

	//Private variables
	public int redHealth=3;
	public int yellowHealth=3;
	private float recoveryTimer = 2f;
	public GameObject sphere;

    public GameObject coin;

    public Material redJellyMaterial;
	public Material yellowJellyMaterial;
	public Material orangeJellyMaterial;

    private GameObject mainCamera;
    private EnemyManager enemyManagerScript;

    private GameObject thisEnemiesSpawnPoint;

    [Header("Splat")]
    public GameObject OrangeSplat;
    public GameObject enemyEmpty;

    public bool isWhite = false;

    void Start () {
        //Setting the current health to be the health variable
        //so that when we start the game, the enemy has full HP
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        thisEnemiesSpawnPoint = gameObject.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint;
    }

	void Update () {
		//If the enemy reaches 0 HP, destroy the enemy
		if(redHealth<=0&&yellowHealth>0 && isWhite == false)
        {
			GetComponent<Renderer> ().material = yellowJellyMaterial;
			sphere.GetComponent<Renderer> ().material = yellowJellyMaterial;
		}
		if(redHealth<0){
			redHealth=0;
		}
		if(yellowHealth<=0&&redHealth>0 && isWhite == false)
        {
			GetComponent<Renderer> ().material = redJellyMaterial;
			sphere.GetComponent<Renderer> ().material = redJellyMaterial;
		}
		if(yellowHealth<0){
			yellowHealth=0;
		}
		if(yellowHealth<=0&&redHealth<=0){
		    enemyManagerScript.enemyList.Remove(gameObject);
		    mainCamera.GetComponent<CameraScript>().SmallScreenShake();
		    if (thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
		    {
		        thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList.Remove(gameObject);
		    }
		    else if (thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
		    {
		        thisEnemiesSpawnPoint.GetComponent<newSpawner>().ThisSpawnpointsEnemyList.Remove(gameObject);
		    }
            Instantiate(OrangeSplat, enemyEmpty.gameObject.transform.position, enemyEmpty.gameObject.transform.rotation);
		    Instantiate(coin, transform.position, Quaternion.identity);
            Destroy (this.gameObject);
		}
		recoveryTimer -= Time.deltaTime;
		if(recoveryTimer<=0 && isWhite == false)
        {
			redHealth = 3;
			yellowHealth = 3;
			GetComponent<Renderer> ().material = orangeJellyMaterial;
			sphere.GetComponent<Renderer> ().material = orangeJellyMaterial;
		}
	}

	//Used to call this void in the bullet scripts
	//since currentHealth is a private variable
	public void EnemyDamaged (int damage) {
		//currentHealth -= damage;
		GetComponent<ParticleSystem> ().Play ();
	}
    public void PoisonDamaged()
    {
        GetComponent<ParticleSystem>().Play();
        if (redHealth > 0)
        {
            redHealth -= 1;
            recoveryTimer = 2f;
        }
        else
        {
            yellowHealth -= 1;
            recoveryTimer = 2f;
        }
    }
    public void OnCollisionEnter (Collision other){
		if(other.gameObject.CompareTag("RedBullet")){
			redHealth -= 1;
		    if (gameObject.GetComponent<StandardEnemyBehaviour>().isAggroPlayer == false)
		    {
		        gameObject.GetComponent<StandardEnemyBehaviour>().AggroToggle();
		    }
            recoveryTimer = 2f;
		}
		if(other.gameObject.CompareTag("YellowBullet")){
			yellowHealth -= 1;
		    if (gameObject.GetComponent<StandardEnemyBehaviour>().isAggroPlayer == false)
		    {
		        gameObject.GetComponent<StandardEnemyBehaviour>().AggroToggle();
		    }
            recoveryTimer = 2f;
		}
	}
}
