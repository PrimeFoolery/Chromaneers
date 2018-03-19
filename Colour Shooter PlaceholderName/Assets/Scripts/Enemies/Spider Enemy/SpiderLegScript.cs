using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegScript : MonoBehaviour
{

    public string legColour;
    private int legHealth = 2;
    private bool setUpLeg = false;
    private SpiderEnemyController spiderBodyScript;
	public int enemyDamage;

	// Use this for initialization
	void Start ()
	{
	    spiderBodyScript = GetComponentInParent<SpiderEnemyController>();

	}
	
	// Update is called once per frame
	void Update () {
	    if (setUpLeg==false)
	    {
	        if (legColour=="red")
	        {
	            gameObject.GetComponent<ParticleSystemRenderer>().material = spiderBodyScript.RedParticleMaterial;
	        }
	        if (legColour == "blue")
	        {
	            gameObject.GetComponent<ParticleSystemRenderer>().material = spiderBodyScript.BlueParticleMaterial;
            }
	        if (legColour == "yellow")
	        {
	            gameObject.GetComponent<ParticleSystemRenderer>().material = spiderBodyScript.YellowParticleMaterial;
            }
        }
	    if (legHealth<=0)
	    {
	        spiderBodyScript.howManyLegsAreAlive -= 1;
            Destroy(gameObject);
	    }
	}

    void OnCollisionEnter(Collision other)
    {
        if (legColour=="blue")
        {
            if (other.gameObject.tag == "BlueBullet")
            {
                //Debug.Log("hit with blue bullet");
                legHealth -= 1;
                gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject);
            }
        }
        if (legColour == "red")
        {
            if (other.gameObject.tag == "RedBullet")
            {
                //Debug.Log("hit with red bullet");
                legHealth -= 1;
                gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject);
            }
        }
        if (legColour == "yellow")
        {
            if (other.gameObject.tag == "YellowBullet")
            {
                //Debug.Log("hit with yellow bullet");
                legHealth -= 1;
                gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject);
            }
        }
		//Check if it collides with the blue enemy
		if (other.gameObject.CompareTag("Player")) {
			//When it collides with the enemy, apply the damage
			other.gameObject.GetComponent<SingleplayerHealthController>().EnemyDamaged(enemyDamage);
			//Resseting the timer for the player to take damage
			//other.gameObject.GetComponent<SingleplayerHealthController>().invincibility = 1f;
		}
		//Check if it collides with coop player one
		if (other.gameObject.CompareTag("BluePlayer")) {
			//When it collides with the enemy, apply the damage
			other.gameObject.GetComponent<CoopCharacterHealthControllerOne>().EnemyDamaged(enemyDamage);
			//Resseting the timer for the player to take damage
			//other.gameObject.GetComponent<CoopCharacterHealthControllerOne>().invincibility = 1f;
		}
		//Check if it collides with coop player two
		if (other.gameObject.CompareTag("RedPlayer")) {
			//When it collides with the enemy, apply the damage
			other.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().EnemyDamaged(enemyDamage);
			//Resseting the timer for the player to take damage
			//other.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().invincibility = 1f;
		}
		//Check if it collides with coop player three
		if (other.gameObject.CompareTag("YellowPlayer")) {
			//When it collides with the enemy, apply the damage
			other.gameObject.GetComponent<CoopCharacterHealthControllerThree>().EnemyDamaged(enemyDamage);
			//Resseting the timer for the player to take damage
			//other.gameObject.GetComponent<CoopCharacterHealthControllerThree>().invincibility = 1f;
		}
    }

    public void DamageLeg()
    {
        legHealth -= 1;
        gameObject.GetComponent<ParticleSystem>().Play();
    }
}
