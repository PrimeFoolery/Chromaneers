using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeShaker : MonoBehaviour
{
    private Vector3 originalPos;
    private float shakeTimer = 0f;
    private float speed = 1.0f; 
    private float amount = 1.0f;
    private float randomX;
    private float randomY;

	public ParticleSystem[] leavesParticle;

    public enum TreeOrBush
    {
        tree,
        bush
    }

    public TreeOrBush treeOrBush = TreeOrBush.tree;

    // Use this for initialization
    void Start ()
    {
        originalPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	    if (shakeTimer>0)
	    {
	        randomX = Random.Range(-1f, 1f);
	        randomY = Random.Range(-1f, 1f);
	        transform.position = new Vector3(transform.position.x + ((randomX * speed) * amount * Time.deltaTime), transform.position.y, transform.position.z+((randomY * speed) * amount * Time.deltaTime));
	        shakeTimer -= Time.deltaTime;
	        if (treeOrBush == TreeOrBush.tree)
	        {
	            leavesParticle[0].Play();
	            leavesParticle[1].Play();
	            leavesParticle[2].Play();
	            leavesParticle[3].Play();
	            leavesParticle[4].Play();
            }else if (treeOrBush == TreeOrBush.bush)
	        {
	            gameObject.GetComponent<ParticleSystem>().Play();
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
	        }
			
	    }
	    else
	    {
	        transform.position = originalPos;
	    }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag ==("BlueBullet") || other.gameObject.tag == ("RedBullet")|| other.gameObject.tag == ("YellowBullet")||other.gameObject.tag == ("RainbowBullet"))
        {
            if (shakeTimer<=0)
            {
                shakeTimer = 0.09f;
            }
            
        }
    }
}
