using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private scoreTracker scoreTracker;

    private float startTime;

    void Awake()
    {
    }
	// Use this for initialization
	void Start ()
	{
	    scoreTracker = GameObject.FindGameObjectWithTag("Settings").GetComponent<scoreTracker>();
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
	        if (vignetteImage.GetComponent<RectTransform>().localScale.x> 74000f)
	        {
	            currentVignetteState = VignetteState.idle;
	        }
	    }

	    if (currentVignetteState == VignetteState.shrink)
	    {
	        vignetteImage.GetComponent<RectTransform>().localScale = Vector3.Lerp(vignetteImage.GetComponent<RectTransform>().localScale, new Vector3(1942.2f, 1165.8f, 1f), vignetteScaleSpeed * (Time.time - startTime) * Time.deltaTime);
	        if (vignetteImage.GetComponent<RectTransform>().localScale.x< 1960)
	        {
                reloadScene();
                
	        }
        }
    }

    void reloadScene()
    {
        currentVignetteState = VignetteState.widen;

        if (SceneManager.GetActiveScene().name == "NewWorld")
        {
            if (howManyAreasComplete == 0)
            {
                scoreTracker.Area1Seconds = 0;
                SceneManager.LoadScene("NewWorld", LoadSceneMode.Single);
            }
            else if (howManyAreasComplete == 1)
            {
                scoreTracker.Area2Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint1", LoadSceneMode.Single);
            }
            else if (howManyAreasComplete == 2)
            {
                scoreTracker.Area3Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint2", LoadSceneMode.Single);
            }
            else if (howManyAreasComplete == 3)
            {
                scoreTracker.Area4Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint3", LoadSceneMode.Single);
            }
        }else if (SceneManager.GetActiveScene().name == "NewWorldCheckpoint1")
        {
            if (howManyAreasComplete==0)
            {
                scoreTracker.Area2Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint1", LoadSceneMode.Single);
            }
            else if (howManyAreasComplete == 1)
            {
                scoreTracker.Area3Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint2", LoadSceneMode.Single);
            }
            else if (howManyAreasComplete == 2)
            {
                scoreTracker.Area4Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint3", LoadSceneMode.Single);
            }
        }
        else if (SceneManager.GetActiveScene().name == "NewWorldCheckpoint2")
        {
            if (howManyAreasComplete == 0)
            {
                scoreTracker.Area3Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint2", LoadSceneMode.Single);
            }
            else if (howManyAreasComplete == 1)
            {
                scoreTracker.Area4Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint3", LoadSceneMode.Single);
            }
        }
        else if (SceneManager.GetActiveScene().name == "NewWorldCheckpoint3")
        {
            if (howManyAreasComplete == 0)
            {
                scoreTracker.Area4Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint3", LoadSceneMode.Single);
            }
            else
            {
                scoreTracker.Area4Seconds = 0;
                SceneManager.LoadScene("NewWorldCheckpoint3", LoadSceneMode.Single);
            }
        }






    }
}
