using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartScript : MonoBehaviour
{

    private float YPosition;

	// Use this for initialization
	void Start ()
	{
	    YPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,7,0);
	    transform.position = new Vector3(transform.position.x, YPosition+Mathf.PingPong(Time.time, 1f), transform.position.z);
    }
	void OnCollisionEnter (Collision other){
		if(other.gameObject.CompareTag("BluePlayer")){
			other.gameObject.GetComponent<CoopCharacterHealthControllerOne> ().GetHeart ();
		}else
		if(other.gameObject.CompareTag("RedPlayer")){
			other.gameObject.GetComponent<CoopCharacterHealthControllerTwo> ().GetHeart ();
		}else
		if(other.gameObject.CompareTag("YellowPlayer")){
			other.gameObject.GetComponent<CoopCharacterHealthControllerThree> ().GetHeart ();
		}
		Destroy (gameObject);
	}
}
