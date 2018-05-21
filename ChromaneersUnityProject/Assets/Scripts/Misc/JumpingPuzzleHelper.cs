using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPuzzleHelper : MonoBehaviour
{

    private CoopCharacterHealthControllerOne blueHealth;
    private CoopCharacterHealthControllerTwo redHealth;
    private CoopCharacterHealthControllerThree yellowHealth;

	// Use this for initialization
	void Start ()
	{
	    blueHealth = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<CoopCharacterHealthControllerOne>();
	    redHealth = GameObject.FindGameObjectWithTag("RedPlayer").GetComponent<CoopCharacterHealthControllerTwo>();
	    yellowHealth = GameObject.FindGameObjectWithTag("YellowPlayer").GetComponent<CoopCharacterHealthControllerThree>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "BluePlayer")
        {
            if (blueHealth.PlayerState == "Dead")
            {
                blueHealth.reviveTimer -= Time.deltaTime * 3;
            }
        }
        if (other.tag == "RedPlayer")
        {
            if (redHealth.PlayerState == "Dead")
            {
                redHealth.reviveTimer -= Time.deltaTime * 3;
            }
        }
        if (other.tag == "YellowPlayer")
        {
            if (yellowHealth.PlayerState == "Dead")
            {
                yellowHealth.reviveTimer -= Time.deltaTime * 3;
            }
        }

    }
}
