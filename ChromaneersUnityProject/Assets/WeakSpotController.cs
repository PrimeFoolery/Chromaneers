using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpotController : MonoBehaviour
{

    public GameObject bossMain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (bossMain.gameObject.GetComponent<BossController>().colourOfWeakSpot == "blue")
        {
            if (other.gameObject.tag == "BlueBullet")
            {
                bossMain.GetComponent<BossController>().enemyHealth -= 1;
                Destroy(other.gameObject);
            }else
            if (other.gameObject.tag == "RedBullet")
            {
                Destroy(other.gameObject);
            }
            else
            if (other.gameObject.tag == "YellowBullet")
            {
                Destroy(other.gameObject);
            }
        }else
        if (bossMain.gameObject.GetComponent<BossController>().colourOfWeakSpot == "red")
        {
            if (other.gameObject.tag == "RedBullet")
            {
                bossMain.GetComponent<BossController>().enemyHealth -= 1;
                Destroy(other.gameObject);
            }
            else
            if (other.gameObject.tag == "BlueBullet")
            {
                Destroy(other.gameObject);
            }
            else
            if (other.gameObject.tag == "YellowBullet")
            {
                Destroy(other.gameObject);
            }
        }
        else
        if (bossMain.gameObject.GetComponent<BossController>().colourOfWeakSpot == "yellow")
        {
            if (other.gameObject.tag == "YellowBullet")
            {
                bossMain.GetComponent<BossController>().enemyHealth -= 1;
                Destroy(other.gameObject);
            }
            else
            if (other.gameObject.tag == "BlueBullet")
            {
                Destroy(other.gameObject);
            }
            else
            if (other.gameObject.tag == "RedBullet")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
