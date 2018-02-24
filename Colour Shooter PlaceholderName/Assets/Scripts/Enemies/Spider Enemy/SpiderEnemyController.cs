using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyController : MonoBehaviour
{

    private int bodyColour;
    private Material bodyMaterial;
    private int legColour;
    private string legMaterial;
    public GameObject leg1;
    public GameObject leg2;
    public GameObject leg3;
    public GameObject leg4;
    public int howManyLegsAreAlive = 4;

    public Material RedJellyMaterial;
    public Material BlueJellyMaterial;
    public Material YellowJellyMaterial;
    public Material BlueLegMaterial;
    public Material RedLegMaterial;
    public Material YellowLegMaterial;

    // Use this for initialization
    void Start ()
	{
	    bodyColour = Random.Range(1, 4);
	    bodyMaterial = gameObject.GetComponent<Renderer>().material;
	    if (bodyColour == 1)
	    {
	        bodyMaterial = RedJellyMaterial;
	        legColour = Random.Range(1, 3);
	        if (legColour == 1)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "blue";
	            leg2.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "blue";
                leg3.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "blue";
                leg4.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "blue";
            } else if (legColour == 2)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg2.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg3.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg4.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "yellow";
            }
	    }
	    if (bodyColour == 2)
	    {
	        bodyMaterial = BlueJellyMaterial;
	        legColour = Random.Range(1, 3);
	        if (legColour == 1)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = RedLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "red";
                leg2.GetComponent<Renderer>().material = RedLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "red";
                leg3.GetComponent<Renderer>().material = RedLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "red";
                leg4.GetComponent<Renderer>().material = RedLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "red";
            }
	        else if (legColour == 2)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg2.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg3.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "yellow";
                leg4.GetComponent<Renderer>().material = YellowLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "yellow";
            }
        }
	    if (bodyColour == 3)
	    {
	        bodyMaterial = YellowJellyMaterial;
	        legColour = Random.Range(1, 3);
	        if (legColour == 1)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "blue";
                leg2.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "blue";
                leg3.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "blue";
                leg4.GetComponent<Renderer>().material = BlueLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "blue";
            }
	        else if (legColour == 2)
	        {
	            Debug.Log("leg colour:  " + legColour + "  body colour:  " + bodyColour);
                leg1.GetComponent<Renderer>().material = RedLegMaterial;
	            leg1.GetComponent<SpiderLegScript>().legColour = "red";
                leg2.GetComponent<Renderer>().material = RedLegMaterial;
	            leg2.GetComponent<SpiderLegScript>().legColour = "red";
                leg3.GetComponent<Renderer>().material = RedLegMaterial;
	            leg3.GetComponent<SpiderLegScript>().legColour = "red";
                leg4.GetComponent<Renderer>().material = RedLegMaterial;
	            leg4.GetComponent<SpiderLegScript>().legColour = "red";
            }
        }
        Debug.Log("leg colour:  "+legColour+"  body colour:  "+bodyColour);
        gameObject.GetComponent<Renderer>().material = bodyMaterial;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
