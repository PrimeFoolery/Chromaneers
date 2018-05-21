using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireworksLauncher : MonoBehaviour
{

    private float fireworksLifetime = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    fireworksLifetime -= Time.deltaTime;
	    if (fireworksLifetime<5)
	    {
            gameObject.GetComponent<ParticleSystem>().Stop();
	    }

	    if (fireworksLifetime<0)
	    {
            Destroy(gameObject);
	    }
	}
}
