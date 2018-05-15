using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class rotating3DModels : MonoBehaviour {

    public float yRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    transform.Rotate(Vector3.forward * yRotation * Time.deltaTime);
	}
}
