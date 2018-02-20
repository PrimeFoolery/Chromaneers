using UnityEngine;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour {

    [Header("Colour Picker Variables")]
    public string currentColourHighligted;

	[Header ("Colour Picker GameObjects")]
	public GameObject colourPicker;
	[Space (10)]
	public GameObject blueColourSelect;
	public GameObject redColourSelect;
	public GameObject yellowColourSelect;
	public GameObject purpleColourSelect;
	public GameObject greenColourSelect;
	public GameObject orangeColourSelect;

	[Header ("Colour Picker Text")]
	public Text currentColourSelected;

    [Header("Colour Picker Script References")]
	public ColourSelectManager colourSelectManager;

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
        colourPicker.GetComponent<Image> ().enabled = false;
		blueColourSelect.GetComponent<Image> ().enabled = false;
		redColourSelect.GetComponent<Image> ().enabled = false;
		yellowColourSelect.GetComponent<Image> ().enabled = false;
		purpleColourSelect.GetComponent<Image> ().enabled = false;
		greenColourSelect.GetComponent<Image> ().enabled = false;
		orangeColourSelect.GetComponent<Image> ().enabled = false;
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
			//All images being turned on
			colourPicker.GetComponent<Image> ().enabled = true;
			blueColourSelect.GetComponent<Image> ().enabled = true;
			redColourSelect.GetComponent<Image> ().enabled = true;
			yellowColourSelect.GetComponent<Image> ().enabled = true;
			purpleColourSelect.GetComponent<Image> ().enabled = true;
			greenColourSelect.GetComponent<Image> ().enabled = true;
			orangeColourSelect.GetComponent<Image> ().enabled = true;
			currentColourSelected.GetComponent<Text> ().enabled = true;

			//Unlocking the mouse to use and making it visible
			//Cursor.lockState = CursorLockMode.None;
			//Cursor.visible = true;
		} else {
			//Else all images being turned off
			colourPicker.GetComponent<Image> ().enabled = false;
			blueColourSelect.GetComponent<Image> ().enabled = false;
			redColourSelect.GetComponent<Image> ().enabled = false;
			yellowColourSelect.GetComponent<Image> ().enabled = false;
			purpleColourSelect.GetComponent<Image> ().enabled = false;
			greenColourSelect.GetComponent<Image> ().enabled = false;
			orangeColourSelect.GetComponent<Image> ().enabled = false;
			currentColourSelected.GetComponent<Text> ().enabled = false;

            //Locking the mouse and hiding it
           // Cursor.lockState = CursorLockMode.Locked;
			//Cursor.visible = false;
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
