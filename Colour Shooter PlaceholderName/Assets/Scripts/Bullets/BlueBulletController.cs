using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueBulletController : MonoBehaviour {

    [Header ("Bullet variables")]
    public float speed;
	public float deceleration;
    public float bulletLifeTime;
    public int bulletDamage;

	[Header("Misc")]
	public GameObject paint;
	public float paintLifeTime;
    [Space(10)]
    public float enemySlowSpeed;
    public float enemyNormalSpeed;
    public float slowTimer;
    public GameObject redEnemy;
    public enum EnemySlowed { notSlowed, canBeSlowed, slowed};
    public EnemySlowed enemySlowed;

	//Private variables
	Vector3 previousBulletPosition;

	void Start () {
        //Tells the previous bullet position to be set to the current transform position
		previousBulletPosition = transform.position;
	}
	
	void Update () {
		print("Bullet move speed = " + (speed - deceleration));
        //Setting previous button position to the new updated position
		previousBulletPosition = transform.position;

        //Moving the bullet
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
		speed = speed * deceleration;

        //Bullets lifeTime
        bulletLifeTime -= Time.deltaTime;
        //Destroying the bullet after its time has reached 0
        if (bulletLifeTime <= 0) {
            Destroy(gameObject);
        }

		//Paint LifeTime
		paintLifeTime -= Time.deltaTime;
		//Destroying the paint after its time has reached 0
		if (bulletLifeTime <= 0) {
			//Destroy (paint);
		}
        
	    if (redEnemy.GetComponent<NavMeshAgent>().speed == enemySlowSpeed) {
            print("I am slowed");
	        slowTimer -= Time.deltaTime;
	        if (slowTimer <= 0) {
                print("I am normal again");
	            redEnemy.GetComponent<NavMeshAgent>().speed = enemyNormalSpeed;
            }
        }
	}

    void OnCollisionEnter (Collision theCol) {
        //Check if it collides with the blue enemy
        if (theCol.gameObject.CompareTag("BlueEnemy")) {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<BlueEnemyHealth>().EnemyDamaged(bulletDamage);
            //and destroy the bullet
            Destroy(gameObject);
        }
	    //Check if it collides with the red enemy
        if (theCol.gameObject.CompareTag("RedEnemy")) {
            //Stunning Enemy from moving[WiP]
            //theCol.gameObject.GetComponent<NavMeshAgent>().speed = enemySlowSpeed;
	        //Destroy bullet
	        Destroy(gameObject);
        }
	    //Check if it collides with the yellow enemy
	    if (theCol.gameObject.CompareTag("YellowEnemy")) {
		    //Pushes the enemy back a slight amount [WiP]

		    //Destroy bullet
		    Destroy(gameObject);
	    }
		if (theCol.gameObject.CompareTag("PurpleEnemy")) {
			//Pushes the enemy back a slight amount [WiP]
			theCol.gameObject.GetComponent<PurpleEnemyHealth>().EnemyDamaged(bulletDamage);
			//Destroy bullet
			Destroy(gameObject);
		}
		if (theCol.gameObject.CompareTag("GreenEnemy")) {
			//Pushes the enemy back a slight amount [WiP]
			theCol.gameObject.GetComponent<GreenEnemyHealth>().EnemyDamaged(bulletDamage);
			//Destroy bullet
			Destroy(gameObject);
		}

        //Check if its the Wall
        if (theCol.gameObject.CompareTag("Wall")) {
			//Testing Raycast hitting
			RaycastHit[] hit = Physics.RaycastAll(new Ray(previousBulletPosition, Vector3.forward), (transform.position - previousBulletPosition).magnitude);

			for (int i = 0; i < hit.Length; i++) {
				//Print to check what object has collided
				Debug.Log (hit [i].collider.gameObject.name);

				//If it touches the wall, destroy
				//Destroy(gameObject);

				//Insantiating the paint when the bullet is destroyed
				Instantiate(paint, hit [i].point, hit [i].collider.gameObject.transform.rotation);
			}

			Debug.DrawLine (transform.position, previousBulletPosition);
		}
    }
}
