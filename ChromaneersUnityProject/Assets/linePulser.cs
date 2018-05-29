using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linePulser : MonoBehaviour
{
    public string colourOfLine = "blue";
    private Color whiteColor =  new Color(1,1,1,1);
    private Color redColor = Color.red;
    private Color blueColor = Color.blue;
    private Color yellowColor = Color.yellow;
    public int lengthOfLineRenderer = 20;

    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start ()
	{
	    lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer .material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.startColor = c1;
	    //lineRenderer.endColor = c2;
	    lineRenderer.positionCount = lengthOfLineRenderer;

        
	}
	
	// Update is called once per frame
	void Update ()
	{
	    AnimationCurve widthCurve = new AnimationCurve();
	    Gradient colorGradient = new Gradient();
        int i = 0;
	    while (i< lengthOfLineRenderer)
	    {
	        Vector3 pos = new Vector3(lineRenderer.GetPosition(0).x + i * 0.5f, (Mathf.Sin(i+Time.time))/100, lineRenderer.GetPosition(0).y);
            lineRenderer.SetPosition(i, pos);
	        float width = ((((Mathf.Sin(i+ Time.time)+1) / 2)+0.5f)/2);
	        widthCurve.AddKey((i / 20f), width);
	        if (colourOfLine == "blue")
	        {
	            colorGradient.SetKeys(
	                new GradientColorKey[] { new GradientColorKey(blueColor, 0), new GradientColorKey(whiteColor, 1) },
	                new GradientAlphaKey[] { new GradientAlphaKey(1, i / 20f) }
	            );
            }else if
	            (colourOfLine == "yellow")
	        {
	            colorGradient.SetKeys(
	                new GradientColorKey[] { new GradientColorKey(yellowColor, 0), new GradientColorKey(whiteColor, 1) },
	                new GradientAlphaKey[] { new GradientAlphaKey(1, i / 20f) }
	            );
            }
	        else if
	            (colourOfLine == "red")
	        {
	            colorGradient.SetKeys(
	                new GradientColorKey[] { new GradientColorKey(redColor, 0), new GradientColorKey(whiteColor, 1) },
	                new GradientAlphaKey[] { new GradientAlphaKey(1, i / 20f) }
	            );
	        }

            lineRenderer.colorGradient = colorGradient;
            i++;
	    }

	    
	    lineRenderer.widthCurve = widthCurve;
	}
}
