using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBulletController : MonoBehaviour {

    [Header("Bullet variables")]
    public float speed;
    public float bulletLifeTime;
    public int bulletDamage;

    void Update()
    {
        //Moving the bullet
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //Bullets lifeTime
        bulletLifeTime -= Time.deltaTime;
        //Destroying the bullet after its time has reached 0
        if (bulletLifeTime <= 0) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision theCol)
    {
        //Check the enemy
        if (theCol.gameObject.tag == "BlueEnemy") {
            //When it collides with the enemy, apply the damage
            theCol.gameObject.GetComponent<BlueEnemyHealth>().EnemyDamaged(bulletDamage);
            //and destroy the bullet
            Destroy(gameObject);
        }
    }
}
