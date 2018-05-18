using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTracker : MonoBehaviour
{

    public int howManyAreasComplete = 0;

    private bool isBluePlayerAlive = true;
    private bool isRedPlayerAlive = true;
    private bool isYellowPlayerAlive = true;

    private CoopCharacterHealthControllerOne blueHealth;
    private CoopCharacterHealthControllerTwo redHealth;
    private CoopCharacterHealthControllerThree yellowHealth;

    public enum VignetteState
    {
        widen,
        idle, 
        shrink,
    }

    private VignetteState currentVignetteState = VignetteState.widen;

    private GameObject vignetteImage;

    private static bool created = false;

    public float vignetteScaleSpeed = 1;

    private Vector3 velocity;

    private float startTime;

    void Awake()
    {
        if (created == false)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
	// Use this for initialization
	void Start ()
	{
	    startTime = Time.time;
	    blueHealth = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<CoopCharacterHealthControllerOne>();
	    redHealth = GameObject.FindGameObjectWithTag("RedPlayer").GetComponent<CoopCharacterHealthControllerTwo>();
	    yellowHealth = GameObject.FindGameObjectWithTag("YellowPlayer").GetComponent<CoopCharacterHealthControllerThree>();
        vignetteImage = GameObject.FindGameObjectWithTag("Vignette");
    }
	
	// Update is called once per frame
	void Update () {
	    if (blueHealth==null)
	    {
	        blueHealth = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<CoopCharacterHealthControllerOne>();
        }

	    if (redHealth == null)
	    {
	        redHealth = GameObject.FindGameObjectWithTag("RedPlayer").GetComponent<CoopCharacterHealthControllerTwo>();
        }

	    if (yellowHealth == null)
	    {
	        yellowHealth = GameObject.FindGameObjectWithTag("YellowPlayer").GetComponent<CoopCharacterHealthControllerThree>();
	    }

	    if (vignetteImage == null)
	    {
	        vignetteImage = GameObject.FindGameObjectWithTag("Vignette");
        }

	    if (blueHealth.PlayerState=="Alive")
	    {
	        isBluePlayerAlive = true;
	    }
	    else
	    {
	        isBluePlayerAlive = false;
	    }

	    if (redHealth.PlayerState == "Alive")
	    {
	        isRedPlayerAlive = true;
	    }
	    else
	    {
	        isRedPlayerAlive = false;
	    }

	    if (yellowHealth.PlayerState == "Alive")
	    {
	        isYellowPlayerAlive = true;
	    }
	    else
	    {
	        isYellowPlayerAlive = false;
	    }

	    if (isBluePlayerAlive == false && isRedPlayerAlive == false && isYellowPlayerAlive == false)
	    {
	        if (currentVignetteState == VignetteState.idle)
	        {
	            currentVignetteState = VignetteState.shrink;
	        }
	    }
        if(currentVignetteState == VignetteState.widen)
	    {
            //vignetteImage.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(vignetteImage.GetComponent<RectTransform>().localScale, new Vector3(76800f, 43200f, 1f), ref velocity, vignetteScaleSpeed * Time.deltaTime );
            vignetteImage.GetComponent<RectTransform>().localScale = Vector3.Lerp(vignetteImage.GetComponent<RectTransform>().localScale, new Vector3(76800f, 43200f, 1f), vignetteScaleSpeed*(Time.time-startTime) * Time.deltaTime);
	    }
    }
}
