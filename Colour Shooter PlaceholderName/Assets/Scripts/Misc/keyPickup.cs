using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPickup : MonoBehaviour
{

    private keyController keyTracker;

    public GameObject dustExplosion;

	// Use this for initialization
	void Start ()
	{
	    keyTracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<keyController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BluePlayer")||other.CompareTag("RedPlayer")||other.CompareTag("YellowPlayer"))
        {
            keyTracker.amountOfKeys += 1;
            Instantiate(dustExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
