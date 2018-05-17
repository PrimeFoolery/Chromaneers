using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purpleObelisk : MonoBehaviour
{

    private float blueHealthTimer = 0f;
    private float redHealthTimer = 0f;
    private bool heartSpawned = false;
    public GameObject heart;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (redHealthTimer>-0.5f)
	    {
	        redHealthTimer -= Time.deltaTime;
	    }
	    if (blueHealthTimer > -0.5f)
	    {
	        blueHealthTimer -= Time.deltaTime;
	    }
        if (redHealthTimer >= 0f && blueHealthTimer >= 0f)
	    {
	        if (transform.localScale.z > -0.1f)
	        {
	            transform.localScale += new Vector3(0, 0, -0.2f);
	            gameObject.GetComponent<ParticleSystem>().Play();

	        }
	        else if (transform.localScale.z <= -0.1f)
	        {
	            if (heartSpawned == false)
	            {
	                Instantiate(heart, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
	                gameObject.GetComponent<BoxCollider>().enabled = false;
	                gameObject.GetComponent<ParticleSystem>().Play();
	                heartSpawned = true;
	            }
	            transform.localScale = new Vector3(3f, 3f, -0.1f);
	        }
	    }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("RedBullet") || other.collider.CompareTag("RainbowBullet"))
        {

            DamageRed();
            Destroy(other.gameObject);
        }

        if (other.collider.CompareTag("BlueBullet") || other.collider.CompareTag("RainbowBullet"))
        {
            DamageBlue();
            Destroy(other.gameObject);
        }
    }
    public void DamageRed()
    {
        redHealthTimer = 1f;
    }

    public void DamageBlue()
    {
        blueHealthTimer = 1f;
    }
}
