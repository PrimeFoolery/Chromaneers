using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowObelisk : MonoBehaviour {

	private int obeliskHealth = 3;
	private bool heartSpawned = false;
	public GameObject heart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(obeliskHealth<=0){
			if(transform.localScale.z>-0.1f){
				transform.localScale += new Vector3 (0, 0, -0.2f);
			    gameObject.GetComponent<ParticleSystem>().Play();

            }
            else if(transform.localScale.z<=-0.1f){
				if(heartSpawned==false){
					Instantiate (heart, new Vector3(transform.position.x,transform.position.y+1f,transform.position.z), Quaternion.identity);
					gameObject.GetComponent<BoxCollider> ().enabled = false;
				    gameObject.GetComponent<ParticleSystem>().Play();
                    heartSpawned = true;
				}
				transform.localScale = new Vector3 (3f, 3f, -0.1f);
			}
		}
	}
	void OnCollisionEnter (Collision other){
		if(other.collider.CompareTag("YellowBullet") || other.collider.CompareTag("RainbowBullet"))
        {

			DamageYellow ();
			Destroy (other.gameObject);
		}
	}
	public void DamageYellow(){
		obeliskHealth -= 1;
	}
}
