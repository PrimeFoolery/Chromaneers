using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegScript : MonoBehaviour
{

    public string legColour;
    private int legHealth = 5;
    private SpiderEnemyController spiderBodyScript;

	// Use this for initialization
	void Start ()
	{
	    spiderBodyScript = GetComponentInParent<SpiderEnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
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
                Destroy(other.gameObject);
            }
        }
        if (legColour == "red")
        {
            if (other.gameObject.tag == "RedBullet")
            {
                //Debug.Log("hit with red bullet");
                legHealth -= 1;
                Destroy(other.gameObject);
            }
        }
        if (legColour == "yellow")
        {
            if (other.gameObject.tag == "YellowBullet")
            {
                //Debug.Log("hit with yellow bullet");
                legHealth -= 1;
                Destroy(other.gameObject);
            }
        }
        
    }
}
