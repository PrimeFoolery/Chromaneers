using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialController : MonoBehaviour {

    public enum Colours
    {
        blue,
        red,
        yellow,
        purple,
        orange,
        green
    }

    public Colours thisVialsColour = Colours.blue;

    public Material VialBaseBlue;
    public Material VialBaseRed;
    public Material VialBaseYellow;
    public Material VialBaseOrange;
    public Material VialBasePurple;
    public Material VialBaseGreen;

    private readonly Color redTintColor = new Color(1,0,0,1);
    private readonly Color blueTintColor = new Color(0,0.05f,1,1);
    private readonly Color yellowTintColor = new Color(0.8f, 0.8f, 0, 1);
    private readonly Color purpleTintColor = new Color(0.5f, 0f, 0.5f, 1);
    private readonly Color orangeTintColor = new Color(0.9f, 0.5f, 0, 1);
    private readonly Color greenTintColor = new Color(0, 0.6f, 0, 1);

    private readonly Color redTopColor = new Color(1, 0.5f, 0.5f, 1);
    private readonly Color blueTopColor = new Color(0.4f, 0.4f, 1, 1);
    private readonly Color yellowTopColor = new Color(1f, 1f, 0.5f, 1);
    private readonly Color purpleTopColor = new Color(1f, 0.3f, 1f, 1);
    private readonly Color orangeTopColor = new Color(1f, 0.5f, 0.33f, 1);
    private readonly Color greenTopColor = new Color(0.25f, 1f, 0.1f, 1);

    private readonly Color redFoamLineColor = new Color(1, 0.75f, 0.75f, 1);
    private readonly Color blueFoamLineColor = new Color(0.74f, 0.74f, 1, 1);
    private readonly Color yellowFoamLineColor = new Color(1f, 1f, 1f, 1);
    private readonly Color purpleFoamLineColor = new Color(1f, 0.6f, 1f, 1);
    private readonly Color orangeFoamLineColor = new Color(1f, 1f, 0.5f, 1);
    private readonly Color greenFoamLineColor = new Color(0.75f, 1f, 0.75f, 1);

    public Material VialLiquidMaterial;
    public GameObject VialLiquid;

    private float totalFillLevel = 0;
    public float blueFillLevel = 0;
    private float redFillLevel = 0;
    public float yellowFillLevel = 0;

    private float blueDrainTimer = 1f;
    private float redDrainTimer = 1f;
    private float yellowDrainTimer = 1f;

    public bool VialCorrectlyFilled = false;

    // Use this for initialization
    void Start ()
	{
	    VialLiquidMaterial = VialLiquid.GetComponent<Renderer>().material;
	    if (thisVialsColour==Colours.blue)
	    {
	        gameObject.GetComponent<Renderer>().material = VialBaseBlue;
            VialLiquidMaterial.SetColor("_Tint",blueTintColor);
            VialLiquidMaterial.SetColor("_TopColor",blueTopColor);
            VialLiquidMaterial.SetColor("_FoamColor", blueFoamLineColor);
	        blueFillLevel = 2;
	    }
	    else if (thisVialsColour == Colours.red)
	    {
	        gameObject.GetComponent<Renderer>().material = VialBaseRed;
	        VialLiquidMaterial.SetColor("_Tint", redTintColor);
	        VialLiquidMaterial.SetColor("_TopColor", redTopColor);
	        VialLiquidMaterial.SetColor("_FoamColor", redFoamLineColor);
	        redFillLevel = 2;
	    }
	    else if (thisVialsColour == Colours.yellow)
	    {
	        gameObject.GetComponent<Renderer>().material = VialBaseYellow;
	        VialLiquidMaterial.SetColor("_Tint", yellowTintColor);
	        VialLiquidMaterial.SetColor("_TopColor", yellowTopColor);
	        VialLiquidMaterial.SetColor("_FoamColor", yellowFoamLineColor);
	        yellowFillLevel = 2;
	    }
	    else if (thisVialsColour == Colours.orange)
	    {
	        gameObject.GetComponent<Renderer>().material = VialBaseOrange;
	        VialLiquidMaterial.SetColor("_Tint", orangeTintColor);
	        VialLiquidMaterial.SetColor("_TopColor",orangeTopColor);
	        VialLiquidMaterial.SetColor("_FoamColor", orangeFoamLineColor);
	        yellowFillLevel = 1;
	        redFillLevel = 1;
        }
	    else if (thisVialsColour == Colours.green)
	    {
	        gameObject.GetComponent<Renderer>().material = VialBaseGreen;
	        VialLiquidMaterial.SetColor("_Tint", greenTintColor);
	        VialLiquidMaterial.SetColor("_TopColor", greenTopColor);
	        VialLiquidMaterial.SetColor("_FoamColor", greenFoamLineColor);
	        yellowFillLevel = 1;
	        blueFillLevel = 1;
        }
	    else if (thisVialsColour == Colours.purple)
	    {
	        gameObject.GetComponent<Renderer>().material = VialBasePurple;
	        VialLiquidMaterial.SetColor("_Tint", purpleTintColor);
	        VialLiquidMaterial.SetColor("_TopColor", purpleTopColor);
	        VialLiquidMaterial.SetColor("_FoamColor", purpleFoamLineColor);
	        redFillLevel = 1;
	        blueFillLevel = 1;
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    totalFillLevel = blueFillLevel + yellowFillLevel + redFillLevel;
	    if (thisVialsColour == Colours.blue)
	    {
	        VialLiquidMaterial.SetFloat("_FillAmount", (-blueFillLevel / 1.25f) + 4.5f);
	        if (blueFillLevel>=10)
	        {
	            VialCorrectlyFilled = true;
	        }
	    }
	    else if (thisVialsColour == Colours.red)
	    {
	        VialLiquidMaterial.SetFloat("_FillAmount", (-redFillLevel / 1.25f) + 4.5f);
	        if (redFillLevel >= 10)
	        {
	            VialCorrectlyFilled = true;
	        }
        }
	    else if (thisVialsColour == Colours.yellow)
	    {
	        VialLiquidMaterial.SetFloat("_FillAmount", (-yellowFillLevel / 1.25f) + 4.5f);
	        if (yellowFillLevel >= 10)
	        {
	            VialCorrectlyFilled = true;
	        }
        }
	    else if (thisVialsColour == Colours.orange)
        {
            if (totalFillLevel!=0)
            {
                VialLiquidMaterial.SetColor("_Tint", ((redTintColor * (redFillLevel / totalFillLevel)) + (yellowTintColor * (yellowFillLevel / totalFillLevel))) / 2);
                VialLiquidMaterial.SetColor("_TopColor", ((redTopColor * (redFillLevel / totalFillLevel)) + (yellowTopColor * (yellowFillLevel / totalFillLevel))) / 2);
                VialLiquidMaterial.SetColor("_FoamColor", ((redFoamLineColor * (redFillLevel / totalFillLevel)) + (yellowFoamLineColor * (yellowFillLevel / totalFillLevel))) / 2);
            }
            VialLiquidMaterial.SetFloat("_FillAmount", (-totalFillLevel / 1.25f) + 4.5f);
            if (redFillLevel==5&&yellowFillLevel<5)
            {
                gameObject.GetComponent<Renderer>().material = VialBaseYellow;
            }else
            if (redFillLevel < 5 && yellowFillLevel == 5)
            {
                gameObject.GetComponent<Renderer>().material = VialBaseRed;
            }else
            if (redFillLevel<5&&yellowFillLevel<5)
            {
                gameObject.GetComponent<Renderer>().material = VialBaseOrange;
            }

            if (redFillLevel==5 && yellowFillLevel==5)
            {
                gameObject.GetComponent<Renderer>().material = VialBaseOrange;
                VialCorrectlyFilled = true;
            }
        }
	    else if (thisVialsColour == Colours.purple)
	    {
	        if (totalFillLevel!=0)
	        {
	            VialLiquidMaterial.SetColor("_Tint", ((redTintColor * (redFillLevel / totalFillLevel)) + (blueTintColor * (blueFillLevel / totalFillLevel))) / 2);
	            VialLiquidMaterial.SetColor("_TopColor", ((redTopColor * (redFillLevel / totalFillLevel)) + (blueTopColor * (blueFillLevel / totalFillLevel))) / 2);
	            VialLiquidMaterial.SetColor("_FoamColor", ((redFoamLineColor * (redFillLevel / totalFillLevel)) + (blueFoamLineColor * (blueFillLevel / totalFillLevel))) / 2);
	        }
            VialLiquidMaterial.SetFloat("_FillAmount", (-totalFillLevel / 1.25f) + 4.5f);
	        if (redFillLevel == 5 && blueFillLevel < 5)
	        {
	            gameObject.GetComponent<Renderer>().material = VialBaseBlue;
	        }
	        else
	        if (redFillLevel < 5 && blueFillLevel == 5)
	        {
	            gameObject.GetComponent<Renderer>().material = VialBaseRed;
	        }
	        else
	        if (redFillLevel < 5 && blueFillLevel < 5)
	        {
	            gameObject.GetComponent<Renderer>().material = VialBasePurple;
	        }
	        if (redFillLevel == 5 && blueFillLevel == 5)
	        {
	            gameObject.GetComponent<Renderer>().material = VialBasePurple;
                VialCorrectlyFilled = true;
	        }
        }
	    else if (thisVialsColour == Colours.green)
	    {
	        if (totalFillLevel!=0)
	        {
	            VialLiquidMaterial.SetColor("_Tint", ((blueTintColor * (blueFillLevel / totalFillLevel)) + (yellowTintColor * (yellowFillLevel / totalFillLevel))) / 2);
	            VialLiquidMaterial.SetColor("_TopColor", ((blueTopColor * (blueFillLevel / totalFillLevel)) + (yellowTopColor * (yellowFillLevel / totalFillLevel))) / 2);
	            VialLiquidMaterial.SetColor("_FoamColor", ((blueFoamLineColor * (blueFillLevel / totalFillLevel)) + (yellowFoamLineColor * (yellowFillLevel / totalFillLevel))) / 2);
            }
            VialLiquidMaterial.SetFloat("_FillAmount", (-totalFillLevel / 1.25f) + 4.5f);
	        if (blueFillLevel == 5 && yellowFillLevel < 5)
	        {
	            gameObject.GetComponent<Renderer>().material = VialBaseYellow;
	        }
	        else
	        if (blueFillLevel < 5 && yellowFillLevel == 5)
	        {
	            gameObject.GetComponent<Renderer>().material = VialBaseBlue;
	        }
	        else
	        if (blueFillLevel < 5 && yellowFillLevel < 5)
	        {
	            gameObject.GetComponent<Renderer>().material = VialBaseGreen;
	        }
	        if (yellowFillLevel == 5 && blueFillLevel == 5)
	        {
	            gameObject.GetComponent<Renderer>().material = VialBaseGreen;
                VialCorrectlyFilled = true;
	        }
        }

	    if (blueFillLevel > 2 && VialCorrectlyFilled == false) 
	    {
	        blueDrainTimer -= Time.deltaTime;
        }
	    if (blueDrainTimer < 0)
	    {
	        if (blueFillLevel>2)
	        {
	            blueFillLevel -= 1;
	        }

	        blueDrainTimer = 1f;
	    }
	    if (redFillLevel >2 && VialCorrectlyFilled == false)
	    {
	        redDrainTimer -= Time.deltaTime;
	    }
	    if (redDrainTimer < 0)
	    {
	        if (redFillLevel > 2)
	        {
	            redFillLevel -= 1;
	        }

	        redDrainTimer = 1f;
	    }
	    if (yellowFillLevel > 2 && VialCorrectlyFilled == false)
	    {
	        yellowDrainTimer -= Time.deltaTime;
	    }
	    if (yellowDrainTimer < 0)
	    {
	        if (yellowFillLevel > 2)
	        {
	            yellowFillLevel -= 1;
	        }

	        yellowDrainTimer = 1f;
	    }
	}

    void OnCollisionEnter(Collision other)
    {
        if (thisVialsColour==Colours.blue)
        {
            if (other.gameObject.CompareTag("BlueBullet"))
            {
                if (blueFillLevel<10)
                {
                    blueFillLevel += 1;
                    
                }
                blueDrainTimer = 1f;
                Destroy(other.gameObject);
            }
        }
        else if (thisVialsColour == Colours.red)
        {
            if (other.gameObject.CompareTag("RedBullet"))
            {
                if (redFillLevel < 10)
                {
                    redFillLevel += 1;
                }
                redDrainTimer = 1f;
                Destroy(other.gameObject);
            }
        }
        else if (thisVialsColour == Colours.yellow)
        {
            if (other.gameObject.CompareTag("YellowBullet"))
            {
                if (yellowFillLevel < 10)
                {
                    yellowFillLevel += 1;
                }
                yellowDrainTimer = 1f;
                Destroy(other.gameObject);
            }
        }
        else if (thisVialsColour == Colours.orange)
        {
            if (other.gameObject.CompareTag("RedBullet"))
            {
                if (redFillLevel < 5)
                {
                    redFillLevel += 1;
                };
                redDrainTimer = 1f;
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("YellowBullet"))
            {
                if (yellowFillLevel < 5)
                {
                    yellowFillLevel += 1;
                }
                yellowDrainTimer = 1f;
                Destroy(other.gameObject);
            }
        }
        else if (thisVialsColour == Colours.green)
        {
            if (other.gameObject.CompareTag("BlueBullet"))
            {
                if (blueFillLevel < 5)
                {
                    blueFillLevel += 1;
                }
                blueDrainTimer = 1f;
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("YellowBullet"))
            {
                if (yellowFillLevel < 5)
                {
                    yellowFillLevel += 1;
                }
                yellowDrainTimer = 1f;
                Destroy(other.gameObject);
            }
        }
        else if (thisVialsColour == Colours.purple)
        {
            if (other.gameObject.CompareTag("BlueBullet"))
            {
                if (blueFillLevel < 5)
                {
                    blueFillLevel += 1;
                }
                blueDrainTimer = 1f;
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("RedBullet"))
            {
                if (redFillLevel < 5)
                {
                    redFillLevel += 1;
                }
                redDrainTimer = 1f;
                Destroy(other.gameObject);
            }
        }
    }

    public void ResetToBlue()
    {
        blueFillLevel = 0;
        redFillLevel = 0;
        yellowFillLevel = 0;
        totalFillLevel = 0;
        blueDrainTimer = 1f;
        redDrainTimer = 1f;
        yellowDrainTimer = 1f;
        VialCorrectlyFilled = false;
        thisVialsColour = Colours.blue;
        gameObject.GetComponent<Renderer>().material = VialBaseBlue;
        VialLiquidMaterial.SetColor("_Tint", blueTintColor);
        VialLiquidMaterial.SetColor("_TopColor", blueTopColor);
        VialLiquidMaterial.SetColor("_FoamColor", blueFoamLineColor);
        blueFillLevel = 2;
    }
    public void ResetToRed()
    {
        blueFillLevel = 0;
        redFillLevel = 0;
        yellowFillLevel = 0;
        totalFillLevel = 0;
        blueDrainTimer = 1f;
        redDrainTimer = 1f;
        yellowDrainTimer = 1f;
        thisVialsColour = Colours.red;
        VialCorrectlyFilled = false;
        gameObject.GetComponent<Renderer>().material = VialBaseRed;
        VialLiquidMaterial.SetColor("_Tint", redTintColor);
        VialLiquidMaterial.SetColor("_TopColor", redTopColor);
        VialLiquidMaterial.SetColor("_FoamColor", redFoamLineColor);
        redFillLevel = 2;
    }
    public void ResetToYellow()
    {
        blueFillLevel = 0;
        redFillLevel = 0;
        yellowFillLevel = 0;
        totalFillLevel = 0;
        blueDrainTimer = 1f;
        redDrainTimer = 1f;
        yellowDrainTimer = 1f;
        thisVialsColour = Colours.yellow;
        VialCorrectlyFilled = false;
        gameObject.GetComponent<Renderer>().material = VialBaseYellow;
        VialLiquidMaterial.SetColor("_Tint", yellowTintColor);
        VialLiquidMaterial.SetColor("_TopColor", yellowTopColor);
        VialLiquidMaterial.SetColor("_FoamColor", yellowFoamLineColor);
        yellowFillLevel = 2;
    }

    public void ResetToPurple()
    {
        blueFillLevel = 0;
        redFillLevel = 0;
        yellowFillLevel = 0;
        totalFillLevel = 0;
        blueDrainTimer = 1f;
        redDrainTimer = 1f;
        yellowDrainTimer = 1f;
        thisVialsColour = Colours.purple;
        VialCorrectlyFilled = false;
        gameObject.GetComponent<Renderer>().material = VialBasePurple;
        VialLiquidMaterial.SetColor("_Tint", purpleTintColor);
        VialLiquidMaterial.SetColor("_TopColor", purpleTopColor);
        VialLiquidMaterial.SetColor("_FoamColor", purpleFoamLineColor);
        redFillLevel = 1;
        blueFillLevel = 1;
    }
    public void ResetToOrange()
    {
        blueFillLevel = 0;
        redFillLevel = 0;
        yellowFillLevel = 0;
        totalFillLevel = 0;
        blueDrainTimer = 1f;
        redDrainTimer = 1f;
        yellowDrainTimer = 1f;
        VialCorrectlyFilled = false;
        thisVialsColour = Colours.orange;
        gameObject.GetComponent<Renderer>().material = VialBaseOrange;
        VialLiquidMaterial.SetColor("_Tint", orangeTintColor);
        VialLiquidMaterial.SetColor("_TopColor", orangeTopColor);
        VialLiquidMaterial.SetColor("_FoamColor", orangeFoamLineColor);
        redFillLevel = 1;
        yellowFillLevel = 1;
    }
    public void ResetToGreen()
    {
        blueFillLevel = 0;
        redFillLevel = 0;
        yellowFillLevel = 0;
        totalFillLevel = 0;
        blueDrainTimer = 1f;
        redDrainTimer = 1f;
        yellowDrainTimer = 1f;
        thisVialsColour = Colours.green;
        VialCorrectlyFilled = false;
        gameObject.GetComponent<Renderer>().material = VialBaseGreen;
        VialLiquidMaterial.SetColor("_Tint", greenTintColor);
        VialLiquidMaterial.SetColor("_TopColor", greenTopColor);
        VialLiquidMaterial.SetColor("_FoamColor", greenFoamLineColor);
        yellowFillLevel = 1;
        blueFillLevel = 1;
    }
}
