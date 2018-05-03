using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartScript : MonoBehaviour
{
    private int heartHealth = 3;
    private float YPosition;
    public GameObject dustExplosion;

	// Use this for initialization
	void Start ()
	{
	    YPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,7,0);
	    transform.position = new Vector3(transform.position.x, YPosition+Mathf.PingPong(Time.time, 1f), transform.position.z);

	    if (heartHealth<=0)
	    {
	        Instantiate(dustExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
	    }
    }
	void OnCollisionEnter (Collision other){
		if(other.gameObject.CompareTag("BluePlayer")){
			other.gameObject.GetComponent<CoopCharacterHealthControllerOne> ().GetHeart ();
		    Destroy(gameObject);
        }
        else
		if(other.gameObject.CompareTag("RedPlayer")){
			other.gameObject.GetComponent<CoopCharacterHealthControllerTwo> ().GetHeart ();
		    Destroy(gameObject);
        }
        else
		if(other.gameObject.CompareTag("YellowPlayer")){
			other.gameObject.GetComponent<CoopCharacterHealthControllerThree> ().GetHeart ();
            Destroy (gameObject);
		} else if (other.gameObject.CompareTag("BlueBullet")||other.gameObject.CompareTag("RedBullet")||other.gameObject.CompareTag("YellowBullet")||other.gameObject.CompareTag("RainbowBullet"))
		{
		    heartHealth -= 1;
            Destroy(other.gameObject);
		}
		
	}
}
