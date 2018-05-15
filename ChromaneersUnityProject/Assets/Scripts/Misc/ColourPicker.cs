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
    public Color selectorColor;
    public RectTransform blueColourSelectTransform;
    public RectTransform redColourSelectTransform;
    public RectTransform yellowColourSelectTransform;
    public RectTransform purpleColourSelectTransform;
    public RectTransform greenColourSelectTransform;
    public RectTransform orangeColourSelectTransform;
    public RectTransform colourSelectorTransform;
    public RectTransform colourWheelCentre;

    public float dampTime = 0.3f;

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
        colourSelector.gameObject.GetComponent<LineRenderer>().enabled = false;
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
		//Debug.Log ("Colour Blue Selected");
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
        //Debug.Log ("Colour Red Selected");
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
		//Debug.Log ("Colour Yellow Selected");
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
		//Debug.Log ("Colour Purple Selected");
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
		//Debug.Log ("Colour Green Selected");
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
		//Debug.Log ("Colour Orange Selected");
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
            colourSelector.gameObject.GetComponent<LineRenderer>().enabled = true;
		    //colourSelector.enabled = true;
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
            
		    float distanceBetweenSelectorAndBlue = Vector2.Distance(colourSelectorTransform.anchoredPosition,new Vector2(6.4f,4.5f));
		    float distanceBetweenSelectorAndRed = Vector2.Distance(colourSelectorTransform.anchoredPosition, new Vector2(-2f, 0f));
		    float distanceBetweenSelectorAndYellow = Vector2.Distance(colourSelectorTransform.anchoredPosition, new Vector2(6.4f, -4.5f));
		    float distanceBetweenSelectorAndOrange = Vector2.Distance(colourSelectorTransform.anchoredPosition, new Vector2(0.4f, -4.5f));
		    float distanceBetweenSelectorAndGreen = Vector2.Distance(colourSelectorTransform.anchoredPosition, new Vector2(8.75f, 0f));
		    float distanceBetweenSelectorAndPurple = Vector2.Distance(colourSelectorTransform.anchoredPosition, new Vector2(0.4f, 4.5f));

            Vector2 diffBetweenSelectorAndStart = colourSelectorTransform.anchoredPosition - new Vector2(3.34f, 0f);
            Vector2 diffBetweenLockedAndUnlockedMousePos = tempMousePos-(new Vector2(lockedMousePosX,lockedMousePosY));
		    diffBetweenLockedAndUnlockedMousePos.y = 1- diffBetweenLockedAndUnlockedMousePos.y;
            
		    float distanceBetweenSelectorAndCentre = diffBetweenSelectorAndStart.magnitude;
		    float distanceBetweenLockedAndUnlockedMousePos = diffBetweenLockedAndUnlockedMousePos.magnitude;

            
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
                colourSelector.gameObject.GetComponent<LineRenderer>().SetPosition(1, colourWheelCentre.GetComponent<RectTransform>().position+new Vector3(0,1,0));
            }
		    if (distanceBetweenMouseLockedAndMouseUnlocked>545f)
		    {
                colourSelectorTransform.anchoredPosition = (new Vector2(3.34f,0f)+(diffBetweenLockedAndUnlockedMousePos/distanceBetweenLockedAndUnlockedMousePos) * 5.4f);
            }
            
		    colourSelector.gameObject.GetComponent<LineRenderer>().SetWidth(2 + distanceBetweenSelectorAndCentre / 4, 2 - (distanceBetweenSelectorAndCentre / 4));
		    colourSelector.gameObject.GetComponent<LineRenderer>().SetColors(selectorColor, Color.black);
            //Debug.Log("dist between Mouse and centre:  "+distanceBetweenSelectorAndCentre/4);
            
            selectorColor = (((Color.blue*(1- (Mathf.Abs(distanceBetweenSelectorAndBlue)/10)))+ (Color.red * (1 - (Mathf.Abs(distanceBetweenSelectorAndRed) /10)))+ (Color.yellow * (1 - (Mathf.Abs(distanceBetweenSelectorAndYellow) / 10)))+(Color.green * (1 - (Mathf.Abs(distanceBetweenSelectorAndGreen) / 10))) + (new Color(1,0.75f,0,1) * (1 - (Mathf.Abs(distanceBetweenSelectorAndOrange) / 10))) + (new Color(0.6f,0,1,1) * (1 - (Mathf.Abs(distanceBetweenSelectorAndPurple) / 10)))) / 6);
            selectorColor.a = 1;
            if (Mathf.Min(distanceBetweenSelectorAndBlue,distanceBetweenSelectorAndGreen,distanceBetweenSelectorAndOrange,distanceBetweenSelectorAndPurple,distanceBetweenSelectorAndRed,distanceBetweenSelectorAndYellow)==distanceBetweenSelectorAndBlue)
            {
                selectorColor = (selectorColor + (Color.blue * (1 - (Mathf.Abs(distanceBetweenSelectorAndBlue) / 2.8f))) / 2);
                if (selectorColor.b<=0.2f)
                {
                    selectorColor.b = 0.2f;
                }
            } else
            if (Mathf.Min(distanceBetweenSelectorAndBlue, distanceBetweenSelectorAndGreen, distanceBetweenSelectorAndOrange, distanceBetweenSelectorAndPurple, distanceBetweenSelectorAndRed, distanceBetweenSelectorAndYellow) == distanceBetweenSelectorAndPurple)
            {
                selectorColor = (selectorColor + (new Color(0.6f,0,1,1) * (1 - (Mathf.Abs(distanceBetweenSelectorAndPurple) / 2.8f))) / 2);
                if (selectorColor.b <= 0.2f)
                {
                    selectorColor.b = 0.2f;
                }
                if (selectorColor.r <= 0.12f)
                {
                    selectorColor.r = 0.12f;
                }
            } else
            if (Mathf.Min(distanceBetweenSelectorAndBlue, distanceBetweenSelectorAndGreen, distanceBetweenSelectorAndOrange, distanceBetweenSelectorAndPurple, distanceBetweenSelectorAndRed, distanceBetweenSelectorAndYellow) == distanceBetweenSelectorAndRed)
            {
                selectorColor = (selectorColor + (Color.red * (1 - (Mathf.Abs(distanceBetweenSelectorAndRed) / 2.8f))) / 2);
                if (selectorColor.r <= 0.2f)
                {
                    selectorColor.r = 0.2f;
                }
            } else
            if (Mathf.Min(distanceBetweenSelectorAndBlue, distanceBetweenSelectorAndGreen, distanceBetweenSelectorAndOrange, distanceBetweenSelectorAndPurple, distanceBetweenSelectorAndRed, distanceBetweenSelectorAndYellow) == distanceBetweenSelectorAndOrange)
            {
                selectorColor = (selectorColor + (new Color(1,0.6f,0,1) * (1 - (Mathf.Abs(distanceBetweenSelectorAndOrange) / 2.8f))) / 2);
                if (selectorColor.r <= 0.2f)
                {
                    selectorColor.r = 0.2f;
                }
                if (selectorColor.g <= 0.15f)
                {
                    selectorColor.g = 0.15f;
                }
            } else
            if (Mathf.Min(distanceBetweenSelectorAndBlue, distanceBetweenSelectorAndGreen, distanceBetweenSelectorAndOrange, distanceBetweenSelectorAndPurple, distanceBetweenSelectorAndRed, distanceBetweenSelectorAndYellow) == distanceBetweenSelectorAndYellow)
            {
                selectorColor = (selectorColor + (Color.yellow * (1 - (Mathf.Abs(distanceBetweenSelectorAndYellow) / 2.8f))) / 2);
                if (selectorColor.r <= 0.2f)
                {
                    selectorColor.r = 0.2f;
                }
                if (selectorColor.g <= 0.18f)
                {
                    selectorColor.g = 0.18f;
                }
            } else
            if (Mathf.Min(distanceBetweenSelectorAndBlue, distanceBetweenSelectorAndGreen, distanceBetweenSelectorAndOrange, distanceBetweenSelectorAndPurple, distanceBetweenSelectorAndRed, distanceBetweenSelectorAndYellow) == distanceBetweenSelectorAndGreen)
            {
                selectorColor = (selectorColor + (Color.green * (1 - (Mathf.Abs(distanceBetweenSelectorAndGreen) / 2.8f))) / 2);
                if (selectorColor.g <= 0.2f)
                {
                    selectorColor.g = 0.2f;
                }
            }
            Cursor.visible = false;
            colourSelector.color = selectorColor;
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
            colourSelector.gameObject.GetComponent<LineRenderer>().enabled = false;
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
        Vector2 velocity = Vector2.zero;
        if (currentColourHighligted == "Blue") {
            blueColourSelect.GetComponent<Image>().color = new Color (0.008f, 0.48f, 0.78f, 1f);
            redColourSelect.GetComponent<Image>().color = new Color (1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color (1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color (0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color (0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color (1f, 0.5f, 0f, 0.6f);
            if (blueColourSelectTransform.anchoredPosition != new Vector2(6.63f,5f))
            {
                blueColourSelectTransform.anchoredPosition = Vector2.MoveTowards(blueColourSelectTransform.anchoredPosition, new Vector2(6.63f,5f),dampTime * Time.deltaTime );
            }
            if (redColourSelectTransform.anchoredPosition!=new Vector2(-2f,0))
            {
                redColourSelectTransform.anchoredPosition = Vector2.MoveTowards(redColourSelectTransform.anchoredPosition, new Vector2(-2f, 0), dampTime * Time.deltaTime);
            }
            if (yellowColourSelectTransform.anchoredPosition != new Vector2(6.4f, -4.5f))
            {
                yellowColourSelectTransform.anchoredPosition = Vector2.MoveTowards(yellowColourSelectTransform.anchoredPosition, new Vector2(6.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (orangeColourSelectTransform.anchoredPosition != new Vector2(0.4f, -4.5f))
            {
                orangeColourSelectTransform.anchoredPosition = Vector2.MoveTowards(orangeColourSelectTransform.anchoredPosition, new Vector2(0.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (greenColourSelectTransform.anchoredPosition != new Vector2(8.75f, 0f))
            {
                greenColourSelectTransform.anchoredPosition = Vector2.MoveTowards(greenColourSelectTransform.anchoredPosition, new Vector2(8.75f, 0f), dampTime * Time.deltaTime);
            }
            if (purpleColourSelectTransform.anchoredPosition != new Vector2(0.4f, 4.5f))
            {
                purpleColourSelectTransform.anchoredPosition = Vector2.MoveTowards(purpleColourSelectTransform.anchoredPosition, new Vector2(0.4f, 4.5f), dampTime * Time.deltaTime);
            }
        }

        //CurrentColourSelected is red
        if (currentColourHighligted == "Red") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 1f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 0.6f);
            if (blueColourSelectTransform.anchoredPosition != new Vector2(6.4f, 4.5f))
            {
                blueColourSelectTransform.anchoredPosition = Vector2.MoveTowards(blueColourSelectTransform.anchoredPosition, new Vector2(6.4f, 4.5f), dampTime * Time.deltaTime);
            }
            if (redColourSelectTransform.anchoredPosition != new Vector2(-2.62f, 0))
            {
                redColourSelectTransform.anchoredPosition = Vector2.MoveTowards(redColourSelectTransform.anchoredPosition, new Vector2(-2.62f, 0), dampTime * Time.deltaTime);
            }
            if (yellowColourSelectTransform.anchoredPosition != new Vector2(6.4f, -4.5f))
            {
                yellowColourSelectTransform.anchoredPosition = Vector2.MoveTowards(yellowColourSelectTransform.anchoredPosition, new Vector2(6.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (orangeColourSelectTransform.anchoredPosition != new Vector2(0.4f, -4.5f))
            {
                orangeColourSelectTransform.anchoredPosition = Vector2.MoveTowards(orangeColourSelectTransform.anchoredPosition, new Vector2(0.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (greenColourSelectTransform.anchoredPosition != new Vector2(8.75f, 0f))
            {
                greenColourSelectTransform.anchoredPosition = Vector2.MoveTowards(greenColourSelectTransform.anchoredPosition, new Vector2(8.75f, 0f), dampTime * Time.deltaTime);
            }
            if (purpleColourSelectTransform.anchoredPosition != new Vector2(0.4f, 4.5f))
            {
                purpleColourSelectTransform.anchoredPosition = Vector2.MoveTowards(purpleColourSelectTransform.anchoredPosition, new Vector2(0.4f, 4.5f), dampTime * Time.deltaTime);
            }
        }

        //CurrentColourSelected is yellow
        if (currentColourHighligted == "Yellow") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 1f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 0.6f);
            if (blueColourSelectTransform.anchoredPosition != new Vector2(6.4f, 4.5f))
            {
                blueColourSelectTransform.anchoredPosition = Vector2.MoveTowards(blueColourSelectTransform.anchoredPosition, new Vector2(6.4f, 4.5f), dampTime * Time.deltaTime);
            }
            if (redColourSelectTransform.anchoredPosition != new Vector2(-2f, 0))
            {
                redColourSelectTransform.anchoredPosition = Vector2.MoveTowards(redColourSelectTransform.anchoredPosition, new Vector2(-2f, 0), dampTime * Time.deltaTime);
            }
            if (yellowColourSelectTransform.anchoredPosition != new Vector2(6.59f, -5.09f))
            {
                yellowColourSelectTransform.anchoredPosition = Vector2.MoveTowards(yellowColourSelectTransform.anchoredPosition, new Vector2(6.59f, -5.09f), dampTime * Time.deltaTime);
            }
            if (orangeColourSelectTransform.anchoredPosition != new Vector2(0.4f, -4.5f))
            {
                orangeColourSelectTransform.anchoredPosition = Vector2.MoveTowards(orangeColourSelectTransform.anchoredPosition, new Vector2(0.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (greenColourSelectTransform.anchoredPosition != new Vector2(8.75f, 0f))
            {
                greenColourSelectTransform.anchoredPosition = Vector2.MoveTowards(greenColourSelectTransform.anchoredPosition, new Vector2(8.75f, 0f), dampTime * Time.deltaTime);
            }
            if (purpleColourSelectTransform.anchoredPosition != new Vector2(0.4f, 4.5f))
            {
                purpleColourSelectTransform.anchoredPosition = Vector2.MoveTowards(purpleColourSelectTransform.anchoredPosition, new Vector2(0.4f, 4.5f), dampTime * Time.deltaTime);
            }
        }

        //CurrentColourSelected is purple
        if (currentColourHighligted == "Purple") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 1f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 0.6f);
            if (blueColourSelectTransform.anchoredPosition != new Vector2(6.4f, 4.5f))
            {
                blueColourSelectTransform.anchoredPosition = Vector2.MoveTowards(blueColourSelectTransform.anchoredPosition, new Vector2(6.4f, 4.5f), dampTime * Time.deltaTime);
            }
            if (redColourSelectTransform.anchoredPosition != new Vector2(-2f, 0))
            {
                redColourSelectTransform.anchoredPosition = Vector2.MoveTowards(redColourSelectTransform.anchoredPosition, new Vector2(-2f, 0), dampTime * Time.deltaTime);
            }
            if (yellowColourSelectTransform.anchoredPosition != new Vector2(6.4f, -4.5f))
            {
                yellowColourSelectTransform.anchoredPosition = Vector2.MoveTowards(yellowColourSelectTransform.anchoredPosition, new Vector2(6.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (orangeColourSelectTransform.anchoredPosition != new Vector2(0.4f, -4.5f))
            {
                orangeColourSelectTransform.anchoredPosition = Vector2.MoveTowards(orangeColourSelectTransform.anchoredPosition, new Vector2(0.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (greenColourSelectTransform.anchoredPosition != new Vector2(8.75f, 0f))
            {
                greenColourSelectTransform.anchoredPosition = Vector2.MoveTowards(greenColourSelectTransform.anchoredPosition, new Vector2(8.75f, 0f), dampTime * Time.deltaTime);
            }
            if (purpleColourSelectTransform.anchoredPosition != new Vector2(0.3f, 4.85f))
            {
                purpleColourSelectTransform.anchoredPosition = Vector2.MoveTowards(purpleColourSelectTransform.anchoredPosition, new Vector2(0.3f, 4.85f), dampTime * Time.deltaTime);
            }
        }

        //CurrentColourSelected is green
        if (currentColourHighligted == "Green") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 1f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 0.6f);
            if (blueColourSelectTransform.anchoredPosition != new Vector2(6.4f, 4.5f))
            {
                blueColourSelectTransform.anchoredPosition = Vector2.MoveTowards(blueColourSelectTransform.anchoredPosition, new Vector2(6.4f, 4.5f), dampTime * Time.deltaTime);
            }
            if (redColourSelectTransform.anchoredPosition != new Vector2(-2f, 0))
            {
                redColourSelectTransform.anchoredPosition = Vector2.MoveTowards(redColourSelectTransform.anchoredPosition, new Vector2(-2f, 0), dampTime * Time.deltaTime);
            }
            if (yellowColourSelectTransform.anchoredPosition != new Vector2(6.4f, -4.5f))
            {
                yellowColourSelectTransform.anchoredPosition = Vector2.MoveTowards(yellowColourSelectTransform.anchoredPosition, new Vector2(6.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (orangeColourSelectTransform.anchoredPosition != new Vector2(0.4f, -4.5f))
            {
                orangeColourSelectTransform.anchoredPosition = Vector2.MoveTowards(orangeColourSelectTransform.anchoredPosition, new Vector2(0.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (greenColourSelectTransform.anchoredPosition != new Vector2(9.45f, 0f))
            {
                greenColourSelectTransform.anchoredPosition = Vector2.MoveTowards(greenColourSelectTransform.anchoredPosition, new Vector2(9.45f, 0f), dampTime * Time.deltaTime);
            }
            if (purpleColourSelectTransform.anchoredPosition != new Vector2(0.4f, 4.5f))
            {
                purpleColourSelectTransform.anchoredPosition = Vector2.MoveTowards(purpleColourSelectTransform.anchoredPosition, new Vector2(0.4f, 4.5f), dampTime * Time.deltaTime);
            }
        }

        //CurrentColourSelected is orange
        if (currentColourHighligted == "Orange") {
            blueColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.48f, 0.78f, 0.6f);
            redColourSelect.GetComponent<Image>().color = new Color(1f, 0.004f, 0.05f, 0.6f);
            yellowColourSelect.GetComponent<Image>().color = new Color(1f, 0.89f, 0f, 0.6f);
            purpleColourSelect.GetComponent<Image>().color = new Color(0.51f, 0.11f, 0.52f, 0.6f);
            greenColourSelect.GetComponent<Image>().color = new Color(0.008f, 0.59f, 0.2f, 0.6f);
            orangeColourSelect.GetComponent<Image>().color = new Color(1f, 0.5f, 0f, 1f);
            if (blueColourSelectTransform.anchoredPosition != new Vector2(6.4f, 4.5f))
            {
                blueColourSelectTransform.anchoredPosition = Vector2.MoveTowards(blueColourSelectTransform.anchoredPosition, new Vector2(6.4f, 4.5f), dampTime * Time.deltaTime);
            }
            if (redColourSelectTransform.anchoredPosition != new Vector2(-2f, 0))
            {
                redColourSelectTransform.anchoredPosition = Vector2.MoveTowards(redColourSelectTransform.anchoredPosition, new Vector2(-2f, 0), dampTime * Time.deltaTime);
            }
            if (yellowColourSelectTransform.anchoredPosition != new Vector2(6.4f, -4.5f))
            {
                yellowColourSelectTransform.anchoredPosition = Vector2.MoveTowards(yellowColourSelectTransform.anchoredPosition, new Vector2(6.4f, -4.5f), dampTime * Time.deltaTime);
            }
            if (orangeColourSelectTransform.anchoredPosition != new Vector2(0.08f, -5.15f))
            {
                orangeColourSelectTransform.anchoredPosition = Vector2.MoveTowards(orangeColourSelectTransform.anchoredPosition, new Vector2(0.08f, -5.15f), dampTime * Time.deltaTime);
            }
            if (greenColourSelectTransform.anchoredPosition != new Vector2(8.75f, 0f))
            {
                greenColourSelectTransform.anchoredPosition = Vector2.MoveTowards(greenColourSelectTransform.anchoredPosition, new Vector2(8.75f, 0f), dampTime * Time.deltaTime);
            }
            if (purpleColourSelectTransform.anchoredPosition != new Vector2(0.4f, 4.5f))
            {
                purpleColourSelectTransform.anchoredPosition = Vector2.MoveTowards(purpleColourSelectTransform.anchoredPosition, new Vector2(0.4f, 4.5f), dampTime * Time.deltaTime);
            }
        }
    }
}
