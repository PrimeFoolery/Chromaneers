using UnityEngine;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour {

    [Header("Colour Picker Variables")]
    public string currentColourHighligted="Blue";

	[Header ("Colour Picker GameObjects")]
	public Image colourPicker;
	[Space (10)]
	public Image blueColourSelect;
	public Image redColourSelect;
	public Image yellowColourSelect;
    public Image purpleColourSelect;
	public Image greenColourSelect;
	public Image orangeColourSelect;
    public Image colourSelector;
    public RectTransform blueColourSelectTransform;
    public RectTransform redColourSelectTransform;
    public RectTransform yellowColourSelectTransform;
    public RectTransform purpleColourSelectTransform;
    public RectTransform greenColourSelectTransform;
    public RectTransform orangeColourSelectTransform;
    public RectTransform colourSelectorTransform;

    [Header ("Colour Picker Text")]
	public Text currentColourSelected;

    [Header("Colour Picker Script References")]
	public ColourSelectManager colourSelectManager;

    private bool mouseLock = false;

    private float lockedMousePosX;
    private float lockedMousePosY;

    void Start () {
		colourSelectManager = ColourSelectManager.instance;

		//Setting the current colour to the starting bullet
		currentColourSelected.text = "Current Colour: Blue".ToString ();
		currentColourSelected.color = new Color (0.008f, 0.48f, 0.78f, 1f);

        //Representing the current colour selected on the colour wheel
        blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 1f);
        redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 0.6f);
        yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 0.6f);
        purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 0.6f);
        greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 0.6f);
        orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 0.6f);

        //Setting the UI components to false
        colourPicker.enabled = false;

		blueColourSelect.enabled = false;
		redColourSelect.enabled = false;
		yellowColourSelect.enabled = false;
		purpleColourSelect.enabled = false;
		greenColourSelect.enabled = false;
		orangeColourSelect.enabled = false;
	    colourSelector.enabled = false;
		currentColourSelected.GetComponent<Text> ().enabled = false;
	}

	void Update () {
		//Calling function
		isUIEnabled ();
        whatColourIsCurrentlySelected ();
	}

    //Blue bullet selecter
	public void SelectColourBlue () {
        //Selected the respective bullet prefab
		Debug.Log ("Colour Blue Selected");
		colourSelectManager.SetBulletToShoot (colourSelectManager.blueBulletPrefab);
        //Changes text and colour
		currentColourSelected.text = "Current Colour: Blue".ToString ();
		currentColourSelected.color = new Color (0.008f, 0.48f, 0.78f, 1f);
        //References the function
        currentColourHighligted = "Blue";
	}

    //Red bullet selecter
	public void SelectColourRed () {
        //Selected the respective bullet prefab
        Debug.Log ("Colour Red Selected");
		colourSelectManager.SetBulletToShoot (colourSelectManager.redBulletPrefab);
        //Changes text and colour
		currentColourSelected.text = "Current Colour: Red".ToString ();
		currentColourSelected.color = new Color (1f, 0.004f, 0.05f, 1f);
        //References the function
        currentColourHighligted = "Red";
	}

    //Yellow bullet selecter
	public void SelectColourYellow () {
        //Selected the respective bullet prefab
		Debug.Log ("Colour Yellow Selected");
		colourSelectManager.SetBulletToShoot (colourSelectManager.yellowBulletPrefab);
        //Changes text and colour
		currentColourSelected.text = "Current Colour: Yellow".ToString ();
		currentColourSelected.color = new Color (1f, 0.89f, 0f, 1f);
        //References the function
        currentColourHighligted = "Yellow";
	}

	//Purple bullet selecter
	public void SelectColourPurple () {
        //Selected the respective bullet prefab
		Debug.Log ("Colour Purple Selected");
		colourSelectManager.SetBulletToShoot (colourSelectManager.purpleBulletPrefab);
        //Changes text and colour
		currentColourSelected.text = "Current Colour: Purple".ToString ();
		currentColourSelected.color = new Color (0.51f, 0.11f, 0.52f, 1f);
        //References the function
        currentColourHighligted = "Purple";
	}

	//Green bullet selecter
	public void SelectColourGreen () {
        //Selected the respective bullet prefab
		Debug.Log ("Colour Green Selected");
		colourSelectManager.SetBulletToShoot (colourSelectManager.greenBulletPrefab);
        //Changes text and colour
		currentColourSelected.text = "Current Colour: Green".ToString ();
		currentColourSelected.color = new Color (0.008f, 0.59f, 0.2f, 1f);
        //References the function 
        currentColourHighligted = "Green";
	}

	//Orange bullet selecter
	public void SelectColourOrange () {
        //Selected the respective bullet prefab
		Debug.Log ("Colour Orange Selected");
		colourSelectManager.SetBulletToShoot (colourSelectManager.orangeBulletPrefab);
        //Changes text and colour
		currentColourSelected.text = "Current Colour: Orange".ToString ();
		currentColourSelected.color = new Color (1f, 0.5f, 0f, 1f);
        //References the function
        currentColourHighligted = "Orange";
	}

	//Enables or disables the colourSelect UI
	void isUIEnabled () {
		//Input to activate the UI
		if (Input.GetKey (KeyCode.E)) {
            //Slow motion activates
            Time.timeScale = 0.1f;

            //All images being turned on
            colourPicker.enabled = true;
			blueColourSelect.enabled = true;
			redColourSelect.enabled = true;
			yellowColourSelect.enabled = true;
			purpleColourSelect.enabled = true;
			greenColourSelect.enabled = true;
			orangeColourSelect.enabled = true;
			currentColourSelected.enabled = true;
		    colourSelector.enabled = true;
		    Vector2 tempMousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (mouseLock==false)
            {
                lockedMousePosX = tempMousePos.x;
                lockedMousePosY = tempMousePos.y;
                mouseLock = true;
            }
		    float unlockedMousePosX = tempMousePos.x;
		    float unlockedMousePosY = tempMousePos.y;
		    float differenceBetweenLockedAndUnlockedMousePosX = unlockedMousePosX - lockedMousePosX;
		    float differenceBetweenLockedAndUnlockedMousePosY = lockedMousePosY - unlockedMousePosY;
            float distanceBetweenMouseLockedAndMouseUnlocked = (new Vector2(unlockedMousePosX-lockedMousePosX,unlockedMousePosY-lockedMousePosY)).magnitude;
            
		    float distanceBetweenSelectorAndBlue = Vector2.Distance(colourSelectorTransform.anchoredPosition,blueColourSelectTransform.anchoredPosition);
		    float distanceBetweenSelectorAndRed = Vector2.Distance(colourSelectorTransform.anchoredPosition, redColourSelectTransform.anchoredPosition);
		    float distanceBetweenSelectorAndYellow = Vector2.Distance(colourSelectorTransform.anchoredPosition, yellowColourSelectTransform.anchoredPosition);
		    float distanceBetweenSelectorAndOrange = Vector2.Distance(colourSelectorTransform.anchoredPosition, orangeColourSelectTransform.anchoredPosition);
		    float distanceBetweenSelectorAndGreen = Vector2.Distance(colourSelectorTransform.anchoredPosition, greenColourSelectTransform.anchoredPosition);
		    float distanceBetweenSelectorAndPurple = Vector2.Distance(colourSelectorTransform.anchoredPosition, purpleColourSelectTransform.anchoredPosition);
		    
		    Vector2 diffBetweenSelectorAndStart = colourSelectorTransform.anchoredPosition - new Vector2(3.34f, 0f);
		    float distanceBetweenSelectorAndCentre = diffBetweenSelectorAndStart.magnitude;


            //Debug.Log("R: "+(1-(Mathf.Abs(distanceBetweenSelectorAndRed)/10))+ " Y: "+ (1-(Mathf.Abs(distanceBetweenSelectorAndYellow) / 10)) + " B: "+ (1-(Mathf.Abs(distanceBetweenSelectorAndBlue) / 10)));
		    if (distanceBetweenSelectorAndBlue<distanceBetweenSelectorAndGreen && distanceBetweenSelectorAndBlue < distanceBetweenSelectorAndYellow && distanceBetweenSelectorAndBlue < distanceBetweenSelectorAndOrange && distanceBetweenSelectorAndBlue < distanceBetweenSelectorAndRed && distanceBetweenSelectorAndBlue < distanceBetweenSelectorAndPurple)
		    {
                SelectColourBlue();
		    }
		    if (distanceBetweenSelectorAndRed < distanceBetweenSelectorAndGreen && distanceBetweenSelectorAndRed < distanceBetweenSelectorAndYellow && distanceBetweenSelectorAndRed < distanceBetweenSelectorAndOrange && distanceBetweenSelectorAndRed < distanceBetweenSelectorAndBlue && distanceBetweenSelectorAndRed < distanceBetweenSelectorAndPurple)
		    {
		        SelectColourRed();
		    }
		    if (distanceBetweenSelectorAndYellow < distanceBetweenSelectorAndGreen && distanceBetweenSelectorAndYellow < distanceBetweenSelectorAndBlue && distanceBetweenSelectorAndYellow < distanceBetweenSelectorAndOrange && distanceBetweenSelectorAndYellow < distanceBetweenSelectorAndRed && distanceBetweenSelectorAndYellow < distanceBetweenSelectorAndPurple)
		    {
		        SelectColourYellow();
		    }
		    if (distanceBetweenSelectorAndGreen < distanceBetweenSelectorAndBlue && distanceBetweenSelectorAndGreen < distanceBetweenSelectorAndYellow && distanceBetweenSelectorAndGreen < distanceBetweenSelectorAndOrange && distanceBetweenSelectorAndGreen < distanceBetweenSelectorAndRed && distanceBetweenSelectorAndGreen < distanceBetweenSelectorAndPurple)
		    {
		        SelectColourGreen();
		    }
		    if (distanceBetweenSelectorAndPurple < distanceBetweenSelectorAndGreen && distanceBetweenSelectorAndPurple < distanceBetweenSelectorAndYellow && distanceBetweenSelectorAndPurple < distanceBetweenSelectorAndOrange && distanceBetweenSelectorAndPurple < distanceBetweenSelectorAndRed && distanceBetweenSelectorAndPurple < distanceBetweenSelectorAndBlue)
		    {
		        SelectColourPurple();
		    }
		    if (distanceBetweenSelectorAndOrange < distanceBetweenSelectorAndGreen && distanceBetweenSelectorAndOrange < distanceBetweenSelectorAndYellow && distanceBetweenSelectorAndOrange < distanceBetweenSelectorAndBlue && distanceBetweenSelectorAndOrange < distanceBetweenSelectorAndRed && distanceBetweenSelectorAndOrange < distanceBetweenSelectorAndPurple)
		    {
		        SelectColourOrange();
		    }

            if (distanceBetweenMouseLockedAndMouseUnlocked<=545f)
		    {
		        colourSelectorTransform.anchoredPosition = new Vector2(3.34f + (differenceBetweenLockedAndUnlockedMousePosX / 100), 0f + (differenceBetweenLockedAndUnlockedMousePosY / 100));
            }
            Debug.Log(distanceBetweenSelectorAndCentre);
            Debug.Log(distanceBetweenMouseLockedAndMouseUnlocked);
		    if (distanceBetweenMouseLockedAndMouseUnlocked>545f)
		    {
                colourSelectorTransform.anchoredPosition = (new Vector2(3.34f,0f)+(diffBetweenSelectorAndStart/distanceBetweenSelectorAndCentre) * 5.4f);
            }
            Debug.Log("Selector Position: "+colourSelectorTransform.anchoredPosition);
            colourSelector.color = new Color(1 - (1 - (Mathf.Abs(distanceBetweenSelectorAndBlue) / 10)), 1 - (1 - (Mathf.Abs(distanceBetweenSelectorAndRed) / 10)), 1 - (1 - (Mathf.Abs(distanceBetweenSelectorAndYellow) / 10)));
            //Debug.Log("R: "+ (1 - (1 - (Mathf.Abs(distanceBetweenSelectorAndBlue) / 10))) + " G: "+ (1 - (1 - (Mathf.Abs(distanceBetweenSelectorAndRed) / 10))) + " B: "+ (1 - (1 - (Mathf.Abs(distanceBetweenSelectorAndYellow) / 10))));
            Cursor.visible = false;

		    //Unlocking the mouse to use and making it visible
		    //Cursor.lockState = CursorLockMode.None;
		    //Cursor.visible = true;
		} else {
            //Slow Motion turned off
            Time.timeScale = 1f;
            //Else all images being turned off
            colourPicker.enabled = false;
			blueColourSelect.enabled = false;
			redColourSelect.enabled = false;
			yellowColourSelect.enabled = false;
			purpleColourSelect.enabled = false;
			greenColourSelect.enabled = false;
			orangeColourSelect.enabled = false;
			currentColourSelected.enabled = false;
		    colourSelector.enabled = false;
		    mouseLock = false;

		    //Locking the mouse and hiding it
		    // Cursor.lockState = CursorLockMode.Locked;
		    //Cursor.visible = false;
		    Cursor.visible = true;
		}
	}

    //Setting alphas of the colour wheel to represent which colour is selected
    void whatColourIsCurrentlySelected () {
        //CurrentColourSelected is blue
        if (currentColourHighligted == "Blue") {
            blueColourSelect.GetComponent<Image>().color = new Color (0.008f, 0.48f, 0.78f, 1f);
            redColourSelect.GetComponent<Image>().color = new Color (1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color (1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color (0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color (0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color (1f, 0.5f, 0f, 0.6f);
        }

        //CurrentColourSelected is red
        if (currentColourHighligted == "Red") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 1f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 0.6f);
        }

        //CurrentColourSelected is yellow
        if (currentColourHighligted == "Yellow") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 1f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 0.6f);
        }

        //CurrentColourSelected is purple
        if (currentColourHighligted == "Purple") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 1f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 0.6f);
        }

        //CurrentColourSelected is green
        if (currentColourHighligted == "Green") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 1f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 0.6f);
        }

        //CurrentColourSelected is orange
        if (currentColourHighligted == "Orange") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 1f);
        }
    }
}
