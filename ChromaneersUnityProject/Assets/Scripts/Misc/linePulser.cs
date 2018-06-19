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

    public GameObject targetGameObject;
    private Vector3 directionOfTargetGameObject;
    public float scalingLengthBetweenPositions = 0;

    public enum StateOfLine
    {
        growing,
        waiting,
        shrinking
    }

    private float lineTimer = 2f;

    public StateOfLine currentLineState = StateOfLine.growing;

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
	    if (targetGameObject!=null)
	    {
	        directionOfTargetGameObject = (targetGameObject.transform.position - transform.position).normalized;
        }
	    
	    float distanceBetweenTargetAndLastPos = Vector3.Distance(
	        transform.position + lineRenderer.GetPosition(lengthOfLineRenderer - 1),
	        targetGameObject.transform.position);

	    Vector3 directionFromLastPosToTarget = (targetGameObject.transform.position -
	                                            (transform.position +
	                                             lineRenderer.GetPosition(lengthOfLineRenderer - 1))).normalized;
	    if (currentLineState == StateOfLine.growing ||currentLineState == StateOfLine.waiting)
	    {
	        if (distanceBetweenTargetAndLastPos < 1f)
	        {
	            //Debug.Log("shrinkingline");
	            currentLineState = StateOfLine.waiting;
	        }
	        if (distanceBetweenTargetAndLastPos > 0.1f)
	        {

	            scalingLengthBetweenPositions += 0.5f * Time.deltaTime;
	        }
        }
	    if (currentLineState == StateOfLine.shrinking)
	    {
	        scalingLengthBetweenPositions -= 0.6f * Time.deltaTime;
	        if (scalingLengthBetweenPositions <= 0)
	        {
	            Destroy(this.gameObject);
	        }
	    }
        while (i< lengthOfLineRenderer)
	    {
	        
	        

	        
            Vector3 pos = new Vector3(lineRenderer.GetPosition(0).x + (i * scalingLengthBetweenPositions * directionOfTargetGameObject.x), lineRenderer.GetPosition(0).y + (i* scalingLengthBetweenPositions * directionOfTargetGameObject.y), lineRenderer.GetPosition(0).z + (i* scalingLengthBetweenPositions * directionOfTargetGameObject.z));
            lineRenderer.SetPosition(i, pos);
	        float width = ((((Mathf.Sin(i+ -Time.time*Time.deltaTime*2)+1) / 2)+0.5f)/2);
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

	        

            lineRenderer.widthCurve = widthCurve;
            //Debug.Log("End of line position: "+ transform.position + lineRenderer.GetPosition(lengthOfLineRenderer - 1));
            // Debug.Log("target Position: "+targetGameObject.transform.position);
            //Debug.Log("Distance Difference:" + Vector3.Distance(transform.position + lineRenderer.GetPosition(lengthOfLineRenderer - 1), targetGameObject.transform.position));
            lineRenderer.colorGradient = colorGradient;
            i++;
	    }

	    if (currentLineState == StateOfLine.waiting)
	    {
	        lineTimer -= Time.deltaTime;
	        if (lineTimer<=0)
	        {
	            currentLineState = StateOfLine.shrinking;
	        }
	    }

	    
	    lineRenderer.widthCurve = widthCurve;
	}
}
