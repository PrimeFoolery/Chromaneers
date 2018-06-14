using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishAI : MonoBehaviour {

    public enum FishRoute
    {
        smallCircle,
        largeSquare,
        verticalLine,
        horizontalLine,
        diagUpRightLine,
        diagDownRightLine,
        diagDownLeftLine,
        diagUpLeftLine
    }

    public FishRoute thisFishesRoute;

    public enum circleRoute
    {
        move1,
        move2,
        move3,
        move4,
        move5,
        move6,
        move7,
        move8
    }

    private circleRoute circleRouteState = circleRoute.move1;

    public enum squareRoute
    {
        move1,
        pause1,
        move2,
        pause2,
        move3,
        pause3,
        move4,
        pause4
    }

    private squareRoute squareRouteState = squareRoute.move2;

    public enum LineRoute
    {
        move1,
        pause1,
        move2,
        pause2
    }

    private LineRoute lineRouteState = LineRoute.move2;

    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;
    private Vector3 pos4;
    private Vector3 pos5;
    private Vector3 pos6;
    private Vector3 pos7;
    private Vector3 pos8;

    public float RouteScaleMultiplier =1f;

    public float waitTime = 2f;
    private float waitTimeVar;

    public float fishSpeed = 1f;

    public enum FishColours
    {
        darkBlue,
        lightBlue,
        yellow,
        orange,
        green,
        red
    }

    public FishColours colourOfThisFish = FishColours.red;

    public Material RedFishMat;
    public Material DarkBlueFishMat;
    public Material LightBlueFishMat;
    public Material YellowFishMat;
    public Material OrangeFishMat;
    public Material GreenFishMat;


    // Use this for initialization
    void Start () {
        if (thisFishesRoute == FishRoute.smallCircle)
        {
            pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pos2 = new Vector3(transform.position.x+(0.25f*RouteScaleMultiplier), transform.position.y, transform.position.z+(0.75f * RouteScaleMultiplier));
            pos3 = new Vector3(transform.position.x + (1f * RouteScaleMultiplier), transform.position.y, transform.position.z + (1f * RouteScaleMultiplier));
            pos4 = new Vector3(transform.position.x + (1.75f * RouteScaleMultiplier), transform.position.y, transform.position.z + (0.75f * RouteScaleMultiplier));
            pos5 = new Vector3(transform.position.x + (2f * RouteScaleMultiplier), transform.position.y, transform.position.z + (0f * RouteScaleMultiplier));
            pos6 = new Vector3(transform.position.x + (1.75f * RouteScaleMultiplier), transform.position.y, transform.position.z + (-0.75f * RouteScaleMultiplier));
            pos7 = new Vector3(transform.position.x + (1f * RouteScaleMultiplier), transform.position.y, transform.position.z + (-1f * RouteScaleMultiplier));
            pos8 = new Vector3(transform.position.x + (0.25f * RouteScaleMultiplier), transform.position.y, transform.position.z + (-0.75f * RouteScaleMultiplier));
        }
        else if (thisFishesRoute == FishRoute.largeSquare)
        {
            pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pos2 = new Vector3(transform.position.x+(1*RouteScaleMultiplier), transform.position.y, transform.position.z+ (1 * RouteScaleMultiplier));
            pos3 = new Vector3(transform.position.x+ (2 * RouteScaleMultiplier), transform.position.y, transform.position.z);
            pos4 = new Vector3(transform.position.x+(1 * RouteScaleMultiplier), transform.position.y, transform.position.z+(-1 * RouteScaleMultiplier));
        }
        else if (thisFishesRoute == FishRoute.verticalLine)
        {
            pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pos2 = new Vector3(transform.position.x, transform.position.y, transform.position.z+2);
        }else if (thisFishesRoute == FishRoute.horizontalLine)
        {
            pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pos2 = new Vector3(transform.position.x+2, transform.position.y, transform.position.z);
        }
        else if (thisFishesRoute == FishRoute.diagDownLeftLine)
        {
            pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pos2 = new Vector3(transform.position.x -2, transform.position.y, transform.position.z-2);
        }
        else if (thisFishesRoute == FishRoute.diagDownRightLine)
        {
            pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pos2 = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z-2);
        }
        else if (thisFishesRoute == FishRoute.diagUpLeftLine)
        {
            pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pos2 = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z+2);
        }
        else if (thisFishesRoute == FishRoute.diagUpRightLine)
        {
            pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pos2 = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z+2);
        }

        waitTimeVar = waitTime;

        if (colourOfThisFish == FishColours.red)
        {
            gameObject.GetComponent<Renderer>().material = RedFishMat;
        }else
        if (colourOfThisFish == FishColours.darkBlue)
        {
            gameObject.GetComponent<Renderer>().material = DarkBlueFishMat;
        }
        else
        if (colourOfThisFish == FishColours.lightBlue)
        {
            gameObject.GetComponent<Renderer>().material = LightBlueFishMat;
        }
        else
        if (colourOfThisFish == FishColours.yellow)
        {
            gameObject.GetComponent<Renderer>().material = YellowFishMat;
        }
        else
        if (colourOfThisFish == FishColours.orange)
        {
            gameObject.GetComponent<Renderer>().material = OrangeFishMat;
        }
        else
        if (colourOfThisFish == FishColours.green)
        {
            gameObject.GetComponent<Renderer>().material = GreenFishMat;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (thisFishesRoute == FishRoute.smallCircle)
	    {
	        if (circleRouteState == circleRoute.move1)
	        {
                transform.LookAt(pos1);
	            transform.position = Vector3.MoveTowards(transform.position, pos1, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos1)<= 0.2f)
	            {
	                circleRouteState = circleRoute.move2;
	            }
	        }else
	        if (circleRouteState == circleRoute.move2)
	        {
	            transform.LookAt(pos2);
	            transform.position = Vector3.MoveTowards(transform.position, pos2, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos2) <= 0.2f)
	            {
	                circleRouteState = circleRoute.move3;
	            }
            }
	        else
	        if (circleRouteState == circleRoute.move3)
	        {
	            transform.LookAt(pos3);
	            transform.position = Vector3.MoveTowards(transform.position, pos3, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos3) <= 0.2f)
	            {
	                circleRouteState = circleRoute.move4;
	            }
            }
	        else
	        if (circleRouteState == circleRoute.move4)
	        {
	            transform.LookAt(pos4);
	            transform.position = Vector3.MoveTowards(transform.position, pos4, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos4) <= 0.2f)
	            {
	                circleRouteState = circleRoute.move5;
	            }
            }
	        else
	        if (circleRouteState == circleRoute.move5)
	        {
	            transform.LookAt(pos5);
	            transform.position = Vector3.MoveTowards(transform.position, pos5, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos5) <= 0.2f)
	            {
	                circleRouteState = circleRoute.move6;
	            }
            }
	        else
	        if (circleRouteState == circleRoute.move6)
	        {
	            transform.LookAt(pos6);
	            transform.position = Vector3.MoveTowards(transform.position, pos6, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos6) <= 0.2f)
	            {
	                circleRouteState = circleRoute.move7;
	            }
            }
	        else
	        if (circleRouteState == circleRoute.move7)
	        {
	            transform.LookAt(pos7);
	            transform.position = Vector3.MoveTowards(transform.position, pos7, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos7) <= 0.2f)
	            {
	                circleRouteState = circleRoute.move8;
	            }
            }
	        else
	        if (circleRouteState == circleRoute.move8)
	        {
	            transform.LookAt(pos8);
	            transform.position = Vector3.MoveTowards(transform.position, pos8, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos8) <= 0.2f)
	            {
	                circleRouteState = circleRoute.move1;
	            }
            }

        }else if
	        (thisFishesRoute == FishRoute.largeSquare)
	    {
	        if (squareRouteState == squareRoute.move1)
	        {
	            transform.LookAt(pos1);
	            transform.position = Vector3.MoveTowards(transform.position, pos1, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos1) <= 0.2f)
	            {
	                waitTimeVar = waitTime;
	                squareRouteState = squareRoute.pause1;
	            }
            }
            else
            if (squareRouteState == squareRoute.pause1)
            {
                waitTimeVar -= Time.deltaTime;
                if (waitTimeVar <0)
                {
                    squareRouteState = squareRoute.move2;
                }
            }
            else
	        if (squareRouteState == squareRoute.move2)
	        {
	            transform.LookAt(pos2);
	            transform.position = Vector3.MoveTowards(transform.position, pos2, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos2) <= 0.2f)
	            {
	                waitTimeVar = waitTime;
                    squareRouteState = squareRoute.pause2;
	            }
            }
	        else
	        if (squareRouteState == squareRoute.pause2)
	        {
	            waitTimeVar -= Time.deltaTime;
	            if (waitTimeVar < 0)
	            {
	                squareRouteState = squareRoute.move3;
	            }
            }
	        else
	        if (squareRouteState == squareRoute.move3)
	        {
	            transform.LookAt(pos3);
	            transform.position = Vector3.MoveTowards(transform.position, pos3, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos3) <= 0.2f)
	            {
	                waitTimeVar = waitTime;
                    squareRouteState = squareRoute.pause3;
	            }
            }
	        else
	        if (squareRouteState == squareRoute.pause3)
	        {
	            waitTimeVar -= Time.deltaTime;
	            if (waitTimeVar < 0)
	            {
	                squareRouteState = squareRoute.move4;
	            }
            }
	        else
	        if (squareRouteState == squareRoute.move4)
	        {
	            transform.LookAt(pos4);
	            transform.position = Vector3.MoveTowards(transform.position, pos4, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos4) <= 0.2f)
	            {
	                waitTimeVar = waitTime;
                    squareRouteState = squareRoute.pause4;
	            }
            }
	        else
	        if (squareRouteState == squareRoute.pause4)
	        {
	            waitTimeVar -= Time.deltaTime;
	            if (waitTimeVar < 0)
	            {
	                squareRouteState = squareRoute.move1;
	            }
            }
        }else if
	        (thisFishesRoute == FishRoute.verticalLine||thisFishesRoute == FishRoute.horizontalLine || thisFishesRoute == FishRoute.diagDownLeftLine || thisFishesRoute == FishRoute.diagDownRightLine || thisFishesRoute == FishRoute.diagUpLeftLine || thisFishesRoute == FishRoute.diagUpRightLine)
	    {

	        if (lineRouteState==LineRoute.move1)
	        {
                
	            transform.LookAt(pos1);
	            transform.position = Vector3.MoveTowards(transform.position, pos1, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos1) <= 0.2f)
	            {
	                waitTimeVar = waitTime;
	                lineRouteState = LineRoute.pause1;
	            }
            }
            else
	        if (lineRouteState == LineRoute.pause1)
	        {
	            
                waitTimeVar -= Time.deltaTime;
	            if (waitTimeVar < 0)
	            {
	                lineRouteState = LineRoute.move2;
                }
            }
	        else
	        if (lineRouteState == LineRoute.move2)
	        {
	            
                transform.LookAt(pos2);
	            transform.position = Vector3.MoveTowards(transform.position, pos2, fishSpeed * Time.deltaTime);
	            if (Vector3.Distance(transform.position, pos2) <= 0.2f)
	            {
                    
	                waitTimeVar = waitTime;
	                lineRouteState = LineRoute.pause2;
                }
            }
	        else
	        if (lineRouteState == LineRoute.pause2)
	        {
	           
                waitTimeVar -= Time.deltaTime;
	            if (waitTimeVar < 0)
	            {
	                lineRouteState = LineRoute.move1;
                }
            }


        }
	}
}
