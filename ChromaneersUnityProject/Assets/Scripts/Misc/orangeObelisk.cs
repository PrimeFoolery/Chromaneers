using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orangeObelisk : MonoBehaviour {

    public float yellowHealthTimer = 0f;
    public float redHealthTimer = 0f;
    private bool heartSpawned = false;
    public GameObject heart;
    public bool opened = false;

    private Color color = new Color(1, 0.5f, 0,1);
    public float obeliskDissolveValue = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (redHealthTimer > -0.5f)
	    {
	        redHealthTimer -= Time.deltaTime;
	    }
	    if (yellowHealthTimer > -0.5f)
	    {
	        yellowHealthTimer -= Time.deltaTime;
	    }
	    if (redHealthTimer >= 0f && yellowHealthTimer >= 0f)
	    {
	        opened = true;
	    }

	    if (opened)
	    {
	        obeliskDissolveValue += 0.2f * Time.deltaTime;
	        gameObject.GetComponent<Renderer>().material.SetFloat("_SliceAmount", obeliskDissolveValue);
	        color.a -= 0.3f * Time.deltaTime;

	        gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", color);
	        if (heartSpawned == false)
	        {
	            Instantiate(heart, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z),
	                Quaternion.identity);
	            gameObject.GetComponent<BoxCollider>().enabled = false;
	            heartSpawned = true;
	        }

	        if (gameObject.GetComponent<Renderer>().material.GetFloat("_SliceAmount") > 0.9f)
	        {
	            Destroy(this.gameObject);

	        }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("YellowBullet") || other.collider.CompareTag("RainbowBullet"))
        {

            DamageYellow();
            Destroy(other.gameObject);
        }

        if (other.collider.CompareTag("RedBullet") || other.collider.CompareTag("RainbowBullet"))
        {
            DamageRed();
            Destroy(other.gameObject);
        }
    }
    public void DamageYellow()
    {
        yellowHealthTimer = 1f;
    }

    public void DamageRed()
    {
        redHealthTimer = 1f;
    }
}
