using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegScript : MonoBehaviour
{

    public string legColour;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (legColour=="blue")
        {
            if (other.gameObject.name == "blueBullet")
            {
                Debug.Log("hit with blue bullet");
            }
        }
        if (legColour == "red")
        {
            if (other.gameObject.name == "redBullet")
            {
                Debug.Log("hit with red bullet");
            }
        }
        if (legColour == "yellow")
        {
            if (other.gameObject.name == "yellowBullet")
            {
                Debug.Log("hit with yellow bullet");
            }
        }
        
    }
}
