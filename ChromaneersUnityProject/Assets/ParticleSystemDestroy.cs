using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour {
    public float delay = 0.5f;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        delay -= Time.deltaTime;
        if(delay <= 0)
        {
            Destroy(gameObject);
        }
	}
}
