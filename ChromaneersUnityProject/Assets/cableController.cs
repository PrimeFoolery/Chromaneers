using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cableController : MonoBehaviour
{

    public bool hasCableBeenTriggered = false;
    private float cableDuration = 1f;

    public GameObject cablePivot;
    public GameObject cableFill;
    public GameObject greyCableFill;
    private float movement;
    public float speed = 1f;
    public string cableColour;

    public GameObject ObjectToTrigger;
    private bool nextObjectTriggered = false;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	    movement += (Time.deltaTime*speed);
        greyCableFill.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(movement, 0));
	    cableFill.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(-movement, 0));
        if (hasCableBeenTriggered==true)
	    {
	        cableDuration -= Time.deltaTime;
	        if (cablePivot.transform.localScale.x < 1)
	        {
	            cablePivot.transform.localScale += new Vector3(0.05f, 0, 0);
            }
	        
	    }

	    if (cableDuration<0&&nextObjectTriggered==false)
	    {
	        if (ObjectToTrigger.GetComponent<doorController>()!=null)
	        {
                ObjectToTrigger.GetComponent<doorController>().OpenSesame();
	        }else if (ObjectToTrigger.GetComponent<cableController>()!=null)
	        {
	            if (cableColour=="blue")
	            {
	                ObjectToTrigger.GetComponent<cableController>().Trigger(Color.blue);
                }
	            else if(cableColour == "red")
	            {
	                ObjectToTrigger.GetComponent<cableController>().Trigger(Color.red);
	            }else if (cableColour == "yellow")
	            {
	                ObjectToTrigger.GetComponent<cableController>().Trigger(Color.yellow);
	            }

            }

	        nextObjectTriggered = true;
	    }
	}

    public void Trigger(Color inputColour)
    {
        hasCableBeenTriggered = true;
        if (inputColour == Color.blue)
        {
            cableFill.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.4f,0.4f,1));
            cableFill.GetComponent<Renderer>().material.color = Color.blue;
            cableColour = "blue";
        }
        else if (inputColour == Color.red)
        {
            cableFill.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1,0.5f,0.5f));
            cableFill.GetComponent<Renderer>().material.color = Color.red;
            cableColour = "red";
        }
        else if (inputColour == Color.yellow)
        {
            cableFill.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1,1,0.25f));
            cableFill.GetComponent<Renderer>().material.color = Color.yellow;
            cableColour = "yellow";
        }
        
    }
}
