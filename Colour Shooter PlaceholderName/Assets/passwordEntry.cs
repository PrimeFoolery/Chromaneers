using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passwordEntry : MonoBehaviour {

    public enum ColoursOfCube
    {
        white,
        blue,
        red,
        yellow,
        purple,
        orange,
        green
    }

    public ColoursOfCube ColourOfThisCube;

    public ColoursOfCube correctColourForPassword;

    public bool isCorrectColourSelected = false;

    public Material whiteCube;
    public Material blueCube;
    public Material redCube;
    public Material yellowCube;
    public Material orangeCube;
    public Material purpleCube;
    public Material greenCube;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (ColourOfThisCube == correctColourForPassword)
	    {
	        isCorrectColourSelected = true;
        }
	    else
	    {
	        isCorrectColourSelected = false;
	    }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BlueBullet"))
        {
            if ((ColourOfThisCube == ColoursOfCube.blue || ColourOfThisCube == ColoursOfCube.purple || ColourOfThisCube == ColoursOfCube.green || ColourOfThisCube == ColoursOfCube.orange || ColourOfThisCube == ColoursOfCube.white))
            {
                ColourOfThisCube = ColoursOfCube.blue;
                this.gameObject.GetComponent<Renderer>().material = blueCube;
            }else if (ColourOfThisCube == ColoursOfCube.red)
            {
                ColourOfThisCube = ColoursOfCube.purple;
                this.gameObject.GetComponent<Renderer>().material = purpleCube;
            }
            else if (ColourOfThisCube == ColoursOfCube.yellow)
            {
                ColourOfThisCube = ColoursOfCube.green;
                this.gameObject.GetComponent<Renderer>().material = greenCube;
            }
            Destroy(other.gameObject);
            
        }else if (other.gameObject.CompareTag("RedBullet"))
        {
            if ((ColourOfThisCube == ColoursOfCube.red || ColourOfThisCube == ColoursOfCube.purple || ColourOfThisCube == ColoursOfCube.green || ColourOfThisCube == ColoursOfCube.orange || ColourOfThisCube == ColoursOfCube.white))
            {
                ColourOfThisCube = ColoursOfCube.red;
                this.gameObject.GetComponent<Renderer>().material = redCube;
            }
            else if (ColourOfThisCube == ColoursOfCube.blue)
            {
                ColourOfThisCube = ColoursOfCube.purple;
                this.gameObject.GetComponent<Renderer>().material = purpleCube;
            }
            else if (ColourOfThisCube == ColoursOfCube.yellow)
            {
                ColourOfThisCube = ColoursOfCube.orange;
                this.gameObject.GetComponent<Renderer>().material = orangeCube;
            }
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("YellowBullet"))
        {
            if ((ColourOfThisCube == ColoursOfCube.yellow || ColourOfThisCube == ColoursOfCube.purple || ColourOfThisCube == ColoursOfCube.green || ColourOfThisCube == ColoursOfCube.orange || ColourOfThisCube == ColoursOfCube.white))
            {
                ColourOfThisCube = ColoursOfCube.yellow;
                this.gameObject.GetComponent<Renderer>().material = yellowCube;
            }
            else if (ColourOfThisCube == ColoursOfCube.red)
            {
                ColourOfThisCube = ColoursOfCube.orange;
                this.gameObject.GetComponent<Renderer>().material = orangeCube;
            }
            else if (ColourOfThisCube == ColoursOfCube.blue)
            {
                ColourOfThisCube = ColoursOfCube.green;
                this.gameObject.GetComponent<Renderer>().material = greenCube;
            }
            Destroy(other.gameObject);
        }
    }
}
