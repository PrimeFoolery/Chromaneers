using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDetectionScript : MonoBehaviour {


	public bool isEnemyOnPaint=false;
	public string rawColourOfPaint;
    public string finalColourOfPaint;

    private bool IsSinglePlayer = true;
    private SingleplayerCharacterController playerScript;


	// Use this for initialization
	void Start () {
	    if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>().isItSingleplayer == true)
	    {
	        IsSinglePlayer = true;
	    }
	    else
	    {
	        IsSinglePlayer = false;
	    }
        if(IsSinglePlayer==true)
        {
            playerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SingleplayerCharacterController>();
        }
        else
        {
            //WHEN CO OP IS ADDED IT'll GO HERE
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (IsSinglePlayer==true)
	    {

	    }
	}
}
