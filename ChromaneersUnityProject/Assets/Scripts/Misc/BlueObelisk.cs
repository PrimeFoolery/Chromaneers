using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueObelisk : MonoBehaviour {

	public int obeliskHealth = 3;
	private bool heartSpawned = false;
	public GameObject heart;

    private Color color = new Color(0,0.5f,1,1);
    public float obeliskDissolveValue = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(obeliskHealth<=0)
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
		    
            /*if(transform.localScale.z>-0.1f){
                transform.localScale += new Vector3 (0, 0, -0.2f);
                gameObject.GetComponent<ParticleSystem>().Play();

            }
            else if(transform.localScale.z<=-0.1f){
                if(heartSpawned==false){
                    Instantiate (heart, new Vector3(transform.position.x,transform.position.y+1f,transform.position.z), Quaternion.identity);
                    gameObject.GetComponent<BoxCollider> ().enabled = false;
                    //gameObject.GetComponent<ParticleSystem>().Play();
                    heartSpawned = true;
                }
                transform.localScale = new Vector3 (3f, 3f, -0.1f);
            }*/
        }
	}
	void OnCollisionEnter (Collision other){
		if(other.collider.CompareTag("BlueBullet")||other.collider.CompareTag("RainbowBullet")){
			
			DamageBlue ();
			Destroy (other.gameObject);
		}
	}
	public void DamageBlue(){
		obeliskHealth -= 1;
	}
}
