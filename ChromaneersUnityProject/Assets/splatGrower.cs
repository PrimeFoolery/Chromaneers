using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splatGrower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.localScale.x<0.5f)
	    {
	        transform.localScale += new Vector3(2f,2f,-2f)*Time.deltaTime;

	    }
	}
}
