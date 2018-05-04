using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobPenController : MonoBehaviour
{

    private int amountOfCorrectEnemies = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BlueEnemy"))
        {

        }
    }
}
