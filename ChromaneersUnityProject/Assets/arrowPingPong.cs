using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowPingPong : MonoBehaviour
{

    public enum PositionState
    {
        topMiddle,
        topMiddleRight,
        topMiddleLeft,
        topRight,
        topLeft
    }

    private PositionState currentPosition = PositionState.topMiddleRight;

    private enum PingPongState
    {
        toFirstPos,
        toSecondPos
    }

    private PingPongState currentPingPongState;

    private enum FadeState
    {
        fadeIn,
        idleInvisible,
        fadeOut,
        idleVisible
    }

    private FadeState currentFadeState = FadeState.idleInvisible;

    private Color lerpedColor = new Color(1,1,1,0);
    private float fadeTime = 2f;


    public Vector3 TempVector3;

    private RectTransform tempRectTransform;

    private float pingPongSpeed = 50f;

    private Vector3 startPos;

	// Use this for initialization
	void Start ()
	{
	    startPos = GetComponent<RectTransform>().anchoredPosition;
	}
	
	// Update is called once per frame
	void Update () {
	    
	    

        if (currentFadeState == FadeState.fadeIn)
	    {
	        lerpedColor = Color.Lerp(lerpedColor, new Color(1, 1, 1, 1), fadeTime * Time.deltaTime);
	        gameObject.GetComponent<Image>().color = lerpedColor;
	        if (lerpedColor.a >= 0.95f)
	        {
                lerpedColor = new Color(1,1,1,1);
	            gameObject.GetComponent<Image>().color = lerpedColor;
	            currentFadeState = FadeState.idleVisible;
	        }
	    }else if (currentFadeState == FadeState.fadeOut)
	    {
	        lerpedColor = Color.Lerp(lerpedColor, new Color(1, 1, 1, 0), fadeTime * Time.deltaTime);
	        gameObject.GetComponent<Image>().color = lerpedColor;
	        if (lerpedColor.a <= 0.05f)
	        {
	            lerpedColor = new Color(1, 1, 1, 0);
	            gameObject.GetComponent<Image>().color = lerpedColor;
                currentFadeState = FadeState.idleInvisible;
	        }
        }



	    if (currentPosition == PositionState.topMiddleRight)
	    {
	        if (currentPingPongState == PingPongState.toFirstPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x + 10, startPos.y + 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y + 20)
	            {
	                currentPingPongState = PingPongState.toSecondPos;
	            }
	        }
	        else
	        if (currentPingPongState == PingPongState.toSecondPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x - 10, startPos.y - 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y - 20)
	            {
	                currentPingPongState = PingPongState.toFirstPos;
	            }
	        }
        }else if
	        (currentPosition == PositionState.topMiddle)
	    {
	        if (currentPingPongState == PingPongState.toFirstPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x, startPos.y + 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y + 20)
	            {
	                currentPingPongState = PingPongState.toSecondPos;
	            }
	        }
	        else
	        if (currentPingPongState == PingPongState.toSecondPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x , startPos.y - 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y - 20)
	            {
	                currentPingPongState = PingPongState.toFirstPos;
	            }
	        }
        }
	    else if
	        (currentPosition == PositionState.topMiddleLeft)
	    {
	        if (currentPingPongState == PingPongState.toFirstPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x-10, startPos.y + 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y + 20)
	            {
	                currentPingPongState = PingPongState.toSecondPos;
	            }
	        }
	        else
	        if (currentPingPongState == PingPongState.toSecondPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x+10, startPos.y - 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y - 20)
	            {
	                currentPingPongState = PingPongState.toFirstPos;
	            }
	        }
	    }
	    else if
	        (currentPosition == PositionState.topRight)
	    {
	        if (currentPingPongState == PingPongState.toFirstPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x+20, startPos.y + 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y + 20)
	            {
	                currentPingPongState = PingPongState.toSecondPos;
	            }
	        }
	        else
	        if (currentPingPongState == PingPongState.toSecondPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x-20, startPos.y - 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y - 20)
	            {
	                currentPingPongState = PingPongState.toFirstPos;
	            }
	        }
	    }
	    else if
	        (currentPosition == PositionState.topLeft)
	    {
	        if (currentPingPongState == PingPongState.toFirstPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x-20, startPos.y + 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y + 20)
	            {
	                currentPingPongState = PingPongState.toSecondPos;
	            }
	        }
	        else
	        if (currentPingPongState == PingPongState.toSecondPos)
	        {
	            GetComponent<RectTransform>().anchoredPosition =
	                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, new Vector3(startPos.x+20, startPos.y - 20, startPos.z), pingPongSpeed * Time.deltaTime);
	            if (GetComponent<RectTransform>().anchoredPosition.y == startPos.y - 20)
	            {
	                currentPingPongState = PingPongState.toFirstPos;
	            }
	        }
	    }
    }

    public void ArrowFadeIn()
    {
        if (currentFadeState == FadeState.idleInvisible)
        {
            currentFadeState = FadeState.fadeIn;
        }
    }

    public void ArrowFadeOut()
    {
        if (currentFadeState == FadeState.idleVisible)
        {
            Debug.Log("fadeoutHappening");
            currentFadeState = FadeState.fadeOut;
        }
    }

    public void ChangePosition(PositionState inputPosition)
    {
        if (inputPosition == PositionState.topLeft)
        {
           GetComponent<RectTransform>().anchoredPosition = new Vector2(-752, 381.85f);
            currentPosition = PositionState.topLeft;

            TempVector3 = new Vector3(57.73f, 0, 33.1f);
            GetComponent<RectTransform>().SetPositionAndRotation(GetComponent<RectTransform>().transform.position, Quaternion.Euler(TempVector3));
        }else if (inputPosition == PositionState.topMiddleLeft)
        {
            currentPosition = PositionState.topMiddleLeft;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-437, 381.85f);

            TempVector3 = new Vector3(57.73f, 0, 18.2f);
            GetComponent<RectTransform>().SetPositionAndRotation(GetComponent<RectTransform>().transform.position, Quaternion.Euler(TempVector3));
        }
        else if (inputPosition == PositionState.topMiddle)
        {
            currentPosition = PositionState.topMiddle;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 381.85f);

            TempVector3 = new Vector3(57.73f, 0, -12.5f);
            GetComponent<RectTransform>().SetPositionAndRotation(GetComponent<RectTransform>().transform.position, Quaternion.Euler(TempVector3));
        }
        else if (inputPosition == PositionState.topMiddleRight)
        {
            currentPosition = PositionState.topMiddleRight;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(437, 381.85f);

            TempVector3 = new Vector3(57.73f, 0, -33.71f);
            GetComponent<RectTransform>().SetPositionAndRotation(GetComponent<RectTransform>().transform.position, Quaternion.Euler(TempVector3));
        }
        else if (inputPosition == PositionState.topRight)
        {
            currentPosition = PositionState.topRight;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(752, 381.85f);

            TempVector3 = new Vector3(57.73f, 0, -52.1f);
            GetComponent<RectTransform>().SetPositionAndRotation(GetComponent<RectTransform>().transform.position, Quaternion.Euler(TempVector3));
        }
        startPos = GetComponent<RectTransform>().anchoredPosition;
    }
}
