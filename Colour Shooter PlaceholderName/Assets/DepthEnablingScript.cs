using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DepthEnablingScript : MonoBehaviour
{


    private Camera cam;
	// Use this for initialization
	void Start ()
	{
	    cam = gameObject.GetComponent<Camera>();
	    cam.depthTextureMode = DepthTextureMode.Depth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
