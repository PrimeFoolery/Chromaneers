using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialLiquidScript : MonoBehaviour
{

    private Material VialLiquid;
    private float pingPongFloat = -0.5f;
    public bool goingUp = true;
    public float waveSpeed = 2f;
    public float waveHeight = 10f;


	// Use this for initialization
	void Start ()
	{
	    VialLiquid = gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (goingUp==true&&pingPongFloat<waveHeight)
	    {
	        pingPongFloat = Mathf.SmoothStep(pingPongFloat, waveHeight+0.5f,waveSpeed*Time.deltaTime);
	    }

	    if (goingUp==true&&pingPongFloat>=waveHeight)
	    {
	        goingUp = false;
	    }

	    if (goingUp==false&&pingPongFloat>-waveHeight)
	    {
	        pingPongFloat = Mathf.SmoothStep(pingPongFloat, -waveHeight - 0.5f, waveSpeed/4 * Time.deltaTime);
        }
        if(goingUp==false&&pingPongFloat<=-waveHeight)
        {
            goingUp = true;
        }
        //Debug.Log(pingPongFloat);
		VialLiquid.SetFloat("_WaveSpeed",pingPongFloat);

	}
}
