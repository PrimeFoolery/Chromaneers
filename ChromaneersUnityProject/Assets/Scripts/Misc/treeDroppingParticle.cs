using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeDroppingParticle : MonoBehaviour
{

	private float deathTimer = 1f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		deathTimer -= Time.deltaTime;
		if (deathTimer<0)
		{
			Destroy(this.gameObject);
		}
	}
}