using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObeliskTriggerCube : MonoBehaviour
{

    public GameObject obelisk;

    public int health1 = 1;
    public int health2 = 1;

    private float health1ResetTimer = 3f;
    private float health2ResetTimer = 3f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (health1==0)
	    {
	        health1ResetTimer -= Time.deltaTime;
	    }

	    if (health1ResetTimer<=0)
	    {
	        health1 = 1;
	        health1ResetTimer = 3f;
	    }
	    if (health2 == 0)
	    {
	        health2ResetTimer -= Time.deltaTime;
	    }

	    if (health2ResetTimer <= 0)
	    {
	        health2 = 1;
	        health2ResetTimer = 3f;
	    }

	    if (health1==0 && health2==0)
	    {
            obelisk.GetComponent<BossBattleObelisk>().Grow();
	    }
    }

    void OnCollisionEnter(Collision other)
    {
        if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.blue)
        {
            if (other.gameObject.tag=="RedBullet")
            {
                if (health1 >0)
                {
                    health1 -= 1;
                }
                health1ResetTimer = 3f;
            }else
            if (other.gameObject.tag == "YellowBullet")
            {
                if (health2 > 0)
                {
                    health2 -= 1;
                }
                health2ResetTimer = 3f;
            }else if (other.gameObject.tag =="BlueBullet")
            {
                Destroy(other.gameObject);
            }
        }
        if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.red)
        {
            if (other.gameObject.tag == "BlueBullet")
            {
                if (health1 >0)
                {
                    health1 -= 1;
                }
                health1ResetTimer = 3f;
            }else
            if (other.gameObject.tag == "YellowBullet")
            {
                if (health2 > 0)
                {
                    health2 -= 1;
                }
                health2ResetTimer = 3f;
            }
            else if (other.gameObject.tag == "RedBullet")
            {
                Destroy(other.gameObject);
            }
        }
        if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.yellow)
        {
            if (other.gameObject.tag == "BlueBullet")
            {
                if (health1 > 0)
                {
                    health1 -= 1;
                }
                health1ResetTimer = 3f;
            }else
            if (other.gameObject.tag == "RedBullet")
            {
                if (health2 > 0)
                {
                    health2 -= 1;
                }
                health2ResetTimer = 3f;
            }
            else if (other.gameObject.tag == "YellowBullet")
            {
                Destroy(other.gameObject);
            }
        }
    }
    
}
