using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePropScript : MonoBehaviour
{
    public GameObject dustExplosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BlueBullet")|| other.gameObject.CompareTag("RedBullet")|| other.gameObject.CompareTag("YellowBullet"))
        {
            Instantiate(dustExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("BluePlayer"))
        {
            if (other.gameObject.GetComponent<CoopCharacterControllerOne>().currentlyDodging == true)
            {
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        if (other.gameObject.CompareTag("RedPlayer"))
        {
            if (other.gameObject.GetComponent<CoopCharacterControllerTwo>().currentlyDodging == true)
            {
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        if (other.gameObject.CompareTag("YellowPlayer"))
        {
            if (other.gameObject.GetComponent<CoopCharacterControllerThree>().currentlyDodging == true)
            {
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
