using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossWall : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "BluePlayer")
        {
            other.gameObject.GetComponent<CoopCharacterHealthControllerOne>().GetHit();
        }
        if (other.gameObject.tag == "RedPlayer")
        {
            other.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().GetHit();
        }
        if (other.gameObject.tag == "YellowPlayer")
        {
            other.gameObject.GetComponent<CoopCharacterHealthControllerThree>().GetHit();
        }
    }
}
