using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueBulletController : MonoBehaviour {

    [Header ("Bullet variables")]
    public float speed;
	public float deceleration;
    public float bulletLifeTime;
    public float reboundBulletLifeTime;
    public int bulletDamage;

    private float directionTimer = 0f;
    private Vector3 savedDirection;

    [Header("Misc")]
	public GameObject paint;
	public float paintLifeTime;
	private GameObject bullet;
	public ColourSelectManager colourSelectManager;

    enum bulletState
    {
        normalBullet,
        reboundBullet
    }
    private bulletState stateOfBullet;

	//Private variables
	Vector3 previousBulletPosition;

	void Start () {
        //Tells the previous bullet position to be set to the current transform position
		previousBulletPosition = transform.position;
	}
	
	void Update () {

	    directionTimer -= Time.deltaTime;
	    if (directionTimer <= 0f)
	    {
	        savedDirection = transform.position;
	        directionTimer = 1f;
	    }

        //Setting previous button position to the new updated position
        previousBulletPosition = transform.position;

        if (stateOfBullet == bulletState.normalBullet) {
            //Moving the bullet
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            speed = speed * deceleration;

            //Bullets lifeTime
            bulletLifeTime -= Time.deltaTime;
            //Destroying the bullet after its time has reached 0
            if (bulletLifeTime <= 0) {
                Destroy(gameObject);
            }
        }

        if (stateOfBullet == bulletState.reboundBullet) {
            //Rebound the bullet
            transform.Translate(((Vector3.back * 0.8f * speed) + (Vector3.up * 0.2f * speed)) * Time.deltaTime);

            speed = speed * deceleration;

            //Bullets lifeTime
            reboundBulletLifeTime -= Time.deltaTime;
            //Destroying the bullet after its time has reached 0
            if (reboundBulletLifeTime <= 0) {
                Destroy(gameObject);
            }
        }

		//Paint LifeTime
		paintLifeTime -= Time.deltaTime;
		//Destroying the paint after its time has reached 0
		if (bulletLifeTime <= 0) {
			//Destroy (paint);
		}
	}

    void OnCollisionEnter (Collision theCol) {
        //Check if it collides with the blue enemy
        if (stateOfBullet == bulletState.normalBullet)
        {
            if (theCol.gameObject.CompareTag("BlueEnemy"))
            {
                //When it collides with the enemy, apply the damage
                theCol.gameObject.GetComponent<BlueEnemyHealth>().EnemyDamaged(bulletDamage);
                //and destroy the bullet
                Destroy(gameObject);
            }

            if (theCol.gameObject.CompareTag("PurpleEnemy"))
            {
                //Pushes the enemy back a slight amount [WiP]
                theCol.gameObject.GetComponent<PurpleEnemyHealth>().EnemyDamaged(bulletDamage);
                //Destroy bullet
                Destroy(gameObject);
            }
            if (theCol.gameObject.CompareTag("GreenEnemy"))
            {
                //Pushes the enemy back a slight amount [WiP]
                theCol.gameObject.GetComponent<GreenEnemyHealth>().EnemyDamaged(bulletDamage);
                //Destroy bullet
                Destroy(gameObject);
            }

            if (theCol.gameObject.CompareTag("RedEnemy") || theCol.gameObject.CompareTag("YellowEnemy") || theCol.gameObject.CompareTag("OrangeEnemy") || theCol.gameObject.CompareTag("BlueEnemy") 
				|| theCol.gameObject.CompareTag("GreenEnemy") || theCol.gameObject.CompareTag("PurpleEnemy"))
            {
                theCol.gameObject.GetComponent<StandardEnemyBehaviour>().BulletKnockback(savedDirection);
            }
			if(theCol.gameObject.GetComponent<FastEnemy>()!=null){
				if(theCol.gameObject.GetComponent<FastEnemy>().colourOfEnemy == "red" || theCol.gameObject.GetComponent<FastEnemy>().colourOfEnemy == "yellow") {
					theCol.gameObject.GetComponent<FastEnemy> ().BulletKnockback (savedDirection);
				}
			}
			if(theCol.gameObject.GetComponent<SpiderEnemyController>()!=null){
				if(theCol.gameObject.GetComponent<SpiderEnemyController>().bodyColour==1 || theCol.gameObject.GetComponent<SpiderEnemyController>().bodyColour==3){
					//Change bullet state to rebound
					stateOfBullet = bulletState.reboundBullet;
					//Randomly rotate the gameObject into the sky
					transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
					//Scaling the bullet down
					transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
					//Change trail width
					this.GetComponent<TrailRenderer>().startWidth = 0.3f;
				}
			}
			if(theCol.gameObject.GetComponent<SpiderLegScript>()!=null){
				if(theCol.gameObject.GetComponent<SpiderLegScript>().legColour=="red" || theCol.gameObject.GetComponent<SpiderLegScript>().legColour=="yellow"){
					//Change bullet state to rebound
					stateOfBullet = bulletState.reboundBullet;
					//Randomly rotate the gameObject into the sky
					transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
					//Scaling the bullet down
					transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
					//Change trail width
					this.GetComponent<TrailRenderer>().startWidth = 0.3f;
				}
			}
			if(theCol.gameObject.GetComponent<SnakeEnemyScript>()!=null){
				if(theCol.gameObject.GetComponent<SnakeEnemyScript>().colourOfSnake == "red" || theCol.gameObject.GetComponent<SnakeEnemyScript>().colourOfSnake == "yellow"){
					//Change bullet state to rebound
					stateOfBullet = bulletState.reboundBullet;
					//Randomly rotate the gameObject into the sky
					transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
					//Scaling the bullet down
					transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
					//Change trail width
					this.GetComponent<TrailRenderer>().startWidth = 0.3f;
				}
			}
            if (theCol.gameObject.CompareTag("RedPlayer"))
            {
                theCol.gameObject.GetComponent<CoopCharacterControllerTwo>().Knockback(savedDirection);
            }
            if (theCol.gameObject.CompareTag("YellowPlayer"))
            {
                theCol.gameObject.GetComponent<CoopCharacterControllerThree>().Knockback(savedDirection);
            }

        }


        

        //Check if it collides with the red enemy
        if (theCol.gameObject.CompareTag("RedEnemy") || theCol.gameObject.CompareTag("YellowEnemy") || theCol.gameObject.CompareTag("OrangeEnemy")|| theCol.gameObject.CompareTag("Wall") 
			|| theCol.gameObject.CompareTag("RedPlayer") || theCol.gameObject.CompareTag("YellowPlayer") || theCol.gameObject.CompareTag("RedBullet") || theCol.gameObject.CompareTag("YellowBullet")) {
            //Change bullet state to rebound
            stateOfBullet = bulletState.reboundBullet;
            //Randomly rotate the gameObject into the sky
            transform.Rotate(new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(5f, 15f), UnityEngine.Random.Range(-15f, 15f)));
            //Scaling the bullet down
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            //Change trail width
            this.GetComponent<TrailRenderer>().startWidth = 0.3f;
        }
		/*
		if (theCol.gameObject.CompareTag("RedBullet")) {
			//Setting bullet to shoot
			GameObject bulletToShoot = colourSelectManager.GetBulletPurpleToShoot();
			//Instantiate
			bullet = (GameObject)Instantiate(bulletToShoot, transform.position, Quaternion.identity);
			//Other bullet
			Destroy(theCol.gameObject);
			//This bullet
			Destroy(gameObject);
		}
		*/
    }
}
