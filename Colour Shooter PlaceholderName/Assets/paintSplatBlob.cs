using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

public class paintSplatBlob : MonoBehaviour
{

    private ColourPicker colourPickerScript;

    private string colourOfThisBlob;
    public Brush blobsBrush;
    private bool hasPaintBeenSet = false;
    public GameObject paintProjector;
    private RaycastHit whereRaycastHit;
    private InkCanvas objectHit;
    private EnemyManager listManager;
    
    //movementVariables
    public float gravity = -3f;
    public float upwardVelocity = 6f;
    public float velocityDecel = 0.99f;
    private float startingYPos;


	// Use this for initialization
	void Start ()
	{
	    colourPickerScript = GameObject.FindGameObjectWithTag("ColourPicker").GetComponent<ColourPicker>();
	    startingYPos = transform.position.y;
	    listManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
	    if (colourPickerScript.currentColourHighligted=="Blue")
	    {
	        colourOfThisBlob = "Blue";
            gameObject.GetComponent<Renderer>().material.color=Color.blue;
	    }
	    else if(colourPickerScript.currentColourHighligted == "Purple")
	    {
	        colourOfThisBlob = "Purple";
	        gameObject.GetComponent<Renderer>().material.color = new Color(0.6f,0,1,1);
        }
	    else if (colourPickerScript.currentColourHighligted == "Red")
	    {
	        colourOfThisBlob = "Red";
	        gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
	    else if (colourPickerScript.currentColourHighligted == "Orange")
	    {
	        colourOfThisBlob = "Orange";
	        gameObject.GetComponent<Renderer>().material.color = new Color(1,0.75f,0,1);
        }
	    else if (colourPickerScript.currentColourHighligted == "Yellow")
	    {
	        colourOfThisBlob = "Yellow";
	        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
	    else if (colourPickerScript.currentColourHighligted == "Green")
	    {
	        colourOfThisBlob = "Green";
	        gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0,(gravity+upwardVelocity)*Time.deltaTime,0);
	    upwardVelocity = upwardVelocity * velocityDecel;
	    if (transform.position.y<(startingYPos/2))
	    {
	        GameObject tempPaintProjector = Instantiate(paintProjector, transform.position, Quaternion.identity);
            tempPaintProjector.GetComponent<paintProjectorController>().PaintStart(whereRaycastHit, objectHit, blobsBrush);
            listManager.projectorsList.Add(tempPaintProjector);
            Destroy(gameObject);
	    }
	}

    public void SetPaintVariables(Brush inputBrush, RaycastHit inputHit, InkCanvas inputInkCanvas)
    {
        Debug.Log("setting blob variables");
        Brush tempBrush = inputBrush;
        if (hasPaintBeenSet==false)
        {
            blobsBrush.Color = tempBrush.Color;
            blobsBrush.Scale = tempBrush.Scale;
            blobsBrush.BrushTexture = tempBrush.BrushTexture;
            hasPaintBeenSet = true;
        }
        whereRaycastHit = inputHit;
        objectHit = inputInkCanvas;
    }
}
