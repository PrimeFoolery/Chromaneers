using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{

    public float YPosition;

	// Use this for initialization
	void Start ()
	{
	    YPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
	    transform.Rotate(0, 7, 0);
        transform.position = new Vector3(transform.position.x, YPosition + Mathf.PingPong(Time.time, 1f),transform.position.z);
    }
}
