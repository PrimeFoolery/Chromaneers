using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reviveCircleRotation : MonoBehaviour
{

    public float reviveCircleRotationSpeedNormal = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,reviveCircleRotationSpeedNormal*Time.deltaTime);
	}
}
