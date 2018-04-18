using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBulletController : MonoBehaviour {

    [Header("Bullet variables")]
    public float speed;
    public float deceleration;
    public float bulletLifeTime;
    public float reboundBulletLifeTime;
    public int bulletDamage;

    private float directionTimer =0f;
    private Vector3 savedDirection;

    [Header("Misc")]
    public GameObject paint;
    public float paintLifeTime;

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

    void Update ()
    {

        directionTimer -= Time.deltaTime;
        if (directionTimer<=0f)
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
        if (stateOfBullet == bulletState.normalBullet)
        {
            //Check if its the Enemy
            if (theCol.gameObject.CompareTag("YellowEnemy"))
            {
                //When it collides with the enemy, apply the damage
                theCol.gameObject.GetComponent<YellowEnemyHealth>().EnemyDamaged(bulletDamage);
                //and destroy the bullet
                Destroy(gameObject);
            }
            if (theCol.gameObject.CompareTag("OrangeEnemy"))
            {
                //When it collides with the enemy, apply the damage
                theCol.gameObject.GetComponent<OrangeEnemyHealth>().EnemyDamaged(bulletDamage);
                //and destroy the bullet
                Destroy(gameObject);
            }
            if (theCol.gameObject.CompareTag("GreenEnemy"))
            {
                //When it collides with the enemy, apply the damage
                theCol.gameObject.GetComponent<GreenEnemyHealth>().EnemyDamaged(bulletDamage);
                //and destroy the bullet
                Destroy(gameObject);
            }
            if (theCol.gameObject.CompareTag("RedEnemy") || theCol.gameObject.CompareTag("YellowEnemy") || theCol.gameObject.CompareTag("OrangeEnemy") || theCol.gameObject.CompareTag("BlueEnemy") || theCol.gameObject.CompareTag("GreenEnemy") || theCol.gameObject.CompareTag("PurpleEnemy"))
            {
                theCol.gameObject.GetComponent<StandardEnemyBehaviour>().BulletKnockback(savedDirection);
            }

            if (theCol.gameObject.CompareTag("BluePlayer"))
            {
                theCol.gameObject.GetComponent<CoopCharacterControllerOne>().Knockback(savedDirection);
            }
            if (theCol.gameObject.CompareTag("RedPlayer"))
            {
                theCol.gameObject.GetComponent<CoopCharacterControllerTwo>().Knockback(savedDirection);
            }
        }

        

        //Check if it collides with the blue enemy
        if (theCol.gameObject.CompareTag("BlueEnemy") || theCol.gameObject.CompareTag("RedEnemy") || theCol.gameObject.CompareTag("PurpleEnemy") || theCol.gameObject.CompareTag("Wall") || theCol.gameObject.CompareTag("RedPlayer") || theCol.gameObject.CompareTag("BluePlayer")) {
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
}
