using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartScript : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
