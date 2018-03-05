using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBulletController : MonoBehaviour {

    [Header("Bullet variables")]
    public float speed;
    public float deceleration;
    public float bulletLifeTime;
    public int bulletDamage;

    [Header("Misc")]
    public GameObject paint;
    public float paintLifeTime;

    //Private variables
    Vector3 previousBulletPosition;

    void Start () {
        //Tells the previous bullet position to be set to the current transform position
        previousBulletPosition = transform.position;
    }

    void Update () {
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
    }

    void OnCollisionEnter (Collision theCol) {
        //Check if its the Enemy
        if (theCol.gameObject.tag == "OrangeEnemy") {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<OrangeEnemyHealth>().EnemyDamaged(bulletDamage);
            //and destroy the bullet
            Destroy(gameObject);
        }

        //Check if its the Wall
        if (theCol.gameObject.tag == "Wall") {
            //Testing Raycast hitting
            RaycastHit[] hit = Physics.RaycastAll(new Ray(previousBulletPosition, Vector3.forward), (transform.position - previousBulletPosition).magnitude);

            for (int i = 0; i < hit.Length; i++) {
                //Print to check what object has collided
                Debug.Log(hit[i].collider.gameObject.name);

                //If it touches the wall, destroy
                //Destroy(gameObject);

                //Insantiating the paint when the bullet is destroyed
                Instantiate(paint, hit[i].point, hit[i].collider.gameObject.transform.rotation);
            }

            Debug.DrawLine(transform.position, previousBulletPosition);
        }
    }
}
