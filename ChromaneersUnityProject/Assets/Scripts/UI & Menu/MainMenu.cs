using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour {
    [Header ("MainMenu")]
    public EventSystem eventSys;
    private GameObject storedSelected;
    public bool usingXboxController = true;

    public GameObject OptionsPanel;
    public GameObject MainMenuPanel;
    public GameObject CheckpointsPanel;
    public AudioMixer audioMixer;

    public GameObject[] buttonList;
    public GameObject[] buttonListOpt;
    public GameObject[] buttonListCheck;
    public bool canInteract = true;
    private float InteractTimer;
    public float maxIntTimer;


    public Image TickBoxImage;
    public Sprite[] TickBoxSprites;
    public bool highResWater = true;
    public bool isFullScreen = true;
    public Image TickBoxImage2;
    public GameObject hiWater;
    public GameObject loWater;

	[Header("Text Holders")]
	public GameObject playText;
	public GameObject optionsText;
	public GameObject creditsText;
	public GameObject exitText;
	public GameObject volumeText;
	public GameObject hiResWaterText;
	public GameObject backText;
    public GameObject qualityText;
    public GameObject highText;
    public GameObject mediumText;
    public GameObject lowText;
	public GameObject fullScreeenText;
    public GameObject checkpointsText;
    public GameObject checkpointOneText;
    public GameObject checkpointTwoText;
    public GameObject checkpointThreeText;
    public GameObject back2;


    [Header("State")]
    public string MenuState;

    void Start()
    {
        MenuState = "MainMenu";
    }
    void Update()
    {
        if (canInteract == false)
        {
            InteractTimer -= Time.deltaTime;
            if (InteractTimer <= 0)
            {
                canInteract = true;
                InteractTimer = maxIntTimer;
            }
        }
        print(eventSys.currentSelectedGameObject);
        if (MenuState == "MainMenu")
        {
			if (eventSys.currentSelectedGameObject == buttonList [0]) {
				playText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 1f);
			} else {
				playText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 0.5f);
			}
			if (eventSys.currentSelectedGameObject == buttonList [1]) {
				optionsText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 1f);
			} else {
				optionsText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 0.5f);
			}
			if (eventSys.currentSelectedGameObject == buttonList [2]) {
				creditsText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 1f);
			} else {
				creditsText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 0.5f);
			}
			if (eventSys.currentSelectedGameObject == buttonList [3]) {
				exitText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 1f);
			} else {
				exitText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 0.5f);
			}
            if (eventSys.currentSelectedGameObject == buttonList[4])
            {
                checkpointsText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                checkpointsText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (usingXboxController == true)
            {
                Vector3 menuInput1;
                menuInput1 = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
                if (menuInput1.z < 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonList[0]);
                        canInteract = false;
                    }
                }
                if (menuInput1.z > 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonList[2]);
                        canInteract = false;
                    }
                }
                Vector3 menuInput2;
                menuInput2 = new Vector3(Input.GetAxisRaw("XboxJoystick2LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick2LVertical"));
                if (menuInput2.z < 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonList[0]);
                        canInteract = false;
                    }
                }
                if (menuInput2.z > 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonList[2]);
                        canInteract = false;
                    }
                }
                Vector3 menuInput3;
                menuInput3 = new Vector3(Input.GetAxisRaw("XboxJoystick3LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick3LVertical"));
                if (menuInput3.z < 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonList[0]);
                        canInteract = false;
                    }
                }
                if (menuInput3.z > 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonList[2]);
                        canInteract = false;
                    }
                }
            }
            if (usingXboxController == false)
            {
                Vector3 menuInput1;
                menuInput1 = new Vector3(Input.GetAxisRaw("Joystick1LHorizontal"), 0f, Input.GetAxisRaw("Joystick1LVertical"));
                if (menuInput1.z < 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonList[0]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonList[0] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonList[1]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonList[1] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonList[2]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonList[2] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonList[0]);
                        canInteract = false;
                    }
                }
                if (menuInput1.z > 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonList[2]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonList[0] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonList[2]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonList[1] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonList[0]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonList[2] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonList[1]);
                        canInteract = false;
                    }
                }
	        }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonList[0]);
                    canInteract = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonList[2]);
                    canInteract = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonList[0]);
                    canInteract = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonList[2]);
                    canInteract = false;
                }
            }
        }
        if (MenuState == "CheckpointsMenu")
        {
            if (eventSys.currentSelectedGameObject == buttonListCheck[0])
            {
                checkpointOneText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                checkpointOneText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListCheck[1])
            {
                checkpointTwoText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                checkpointTwoText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListCheck[2])
            {
                checkpointThreeText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                checkpointThreeText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListCheck[3])
            {
                back2.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                back2.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (usingXboxController == true)
            {
                Vector3 menuInput1;
                menuInput1 = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
                if (menuInput1.z < 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonListCheck[0]);
                        canInteract = false;
                    }
                }
                if (menuInput1.z > 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonListCheck[3]);
                        canInteract = false;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonListCheck[0]);
                    canInteract = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonListCheck[3]);
                    canInteract = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonListCheck[0]);
                    canInteract = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonListCheck[3]);
                    canInteract = false;
                }
            }
        }
            if (MenuState == "OptionsMenu")
        {
            //text alpha changes
			if (eventSys.currentSelectedGameObject == buttonListOpt [0].gameObject) {
				fullScreeenText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 1f);
                TickBoxImage2.color = new Color(0, 0, 0, 1f);
			} else {
				fullScreeenText.GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 0.5f);
                TickBoxImage2.color = new Color(0, 0, 0, 0.5f);
            }
           /* if(eventSys.currentSelectedGameObject == buttonListOpt [1])
            {
                buttonListOpt[1].GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[1].GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 0.5f);
            }*/
			if (eventSys.currentSelectedGameObject == buttonListOpt [2]) {
                buttonListOpt[2].GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 1f);
                buttonListOpt[1].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            } else {
                buttonListOpt[2].GetComponent<TextMeshProUGUI> ().color = new Color (0, 0, 0, 0.5f);
                buttonListOpt[1].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListOpt[3])
            {
                buttonListOpt[3].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
                buttonListOpt[1].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[3].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
                buttonListOpt[1].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }

            if (eventSys.currentSelectedGameObject == buttonListOpt[4])
            {
                buttonListOpt[4].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
                buttonListOpt[1].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[4].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
                buttonListOpt[1].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListOpt[5])
            {
                buttonListOpt[5].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
                buttonListOpt[1].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[5].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
                buttonListOpt[1].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListOpt[6])
            {
                volumeText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                volumeText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            /*if (eventSys.currentSelectedGameObject == buttonListOpt[7])
            {
                buttonListOpt[7].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[7].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }*/
            if (eventSys.currentSelectedGameObject == buttonListOpt[8])
            {
                buttonListOpt[8].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
                buttonListOpt[7].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[8].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
                buttonListOpt[7].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListOpt[9])
            {
                buttonListOpt[9].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
                buttonListOpt[7].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[9].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
                buttonListOpt[7].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListOpt[10])
            {
                buttonListOpt[10].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
                buttonListOpt[7].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[10].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
                buttonListOpt[7].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            /*if (eventSys.currentSelectedGameObject == buttonListOpt[11])
            {
                buttonListOpt[11].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[11].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }*/
            if (eventSys.currentSelectedGameObject == buttonListOpt[12])
            {
                buttonListOpt[12].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
                buttonListOpt[11].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[12].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
                buttonListOpt[11].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListOpt[13])
            {
                buttonListOpt[13].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
                buttonListOpt[11].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
                buttonListOpt[13].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
                buttonListOpt[11].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListOpt[14])
            {
                backText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
            }
            else
            {
				backText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
            }

            //toggles
            if (isFullScreen == true)
            {
                Screen.fullScreen = true;
                TickBoxImage2.sprite = TickBoxSprites[0];
            }
            if(isFullScreen == false)
            {
                Screen.fullScreen = false;
                TickBoxImage2.sprite = TickBoxSprites[1];
            }
           
			
            if (usingXboxController == true)
            {
                Vector3 menuInput1;
                menuInput1 = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
//
                if (menuInput1.z < 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[0]);
                        canInteract = false;
                    }
                }
                if (menuInput1.z > 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[3]);
                        canInteract = false;
                    }
                }
            }
            if (usingXboxController == false)
            {
                Vector3 menuInput1;
                menuInput1 = new Vector3(Input.GetAxisRaw("Joystick1LHorizontal"), 0f, Input.GetAxisRaw("Joystick1LVertical"));
                if (menuInput1.z < 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[0]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonListOpt[0] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[1]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonListOpt[1] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[2]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonListOpt[2] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[3]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonListOpt[3] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[0]);
                        canInteract = false;
                    }
                }
                if (menuInput1.z > 0f)
                {
                    if (eventSys.currentSelectedGameObject == null)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[3]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonListOpt[3] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[2]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonListOpt[2] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[1]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonListOpt[1] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[0]);
                        canInteract = false;
                    }
                    if (eventSys.currentSelectedGameObject == buttonListOpt[0] && canInteract == true)
                    {
                        eventSys.SetSelectedGameObject(buttonListOpt[3]);
                        canInteract = false;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonListOpt[0]);
                    canInteract = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonListOpt[3]);
                    canInteract = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonListOpt[0]);
                    canInteract = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(buttonListOpt[3]);
                    canInteract = false;
                }
            }
        }
    }
      
    //main menu stuff
    public void Play()
    {
        SceneManager.LoadScene("NewWorld");
    }
    public void Checkpoints()
    {
        MainMenuPanel.SetActive(false);
        CheckpointsPanel.SetActive(true);
        MenuState = "CheckpointsMenu";
        eventSys.SetSelectedGameObject(buttonListCheck[0]);
    }
    public void CheckpointOne()
    {
        SceneManager.LoadScene("NewWorldCheckpoint1");
    }
    public void CheckpointTwo()
    {
        SceneManager.LoadScene("NewWorldCheckpoint2");
    }
    public void CheckpointThree()
    {
        SceneManager.LoadScene("NewWorldCheckpoint3");
    }
    public void Options()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
        MenuState = "OptionsMenu";
		eventSys.SetSelectedGameObject (buttonListOpt [0]);
    }
	public void Credits()
	{

	}
    public void Exit()
    {
        Application.Quit();
    }
    //Settings menu stuff
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }   
    public void SetWaterHigh()
    {
        
        highResWater = true;
        
    }
    public void SetWaterLow()
    {
        highResWater = false;
    }
/*    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }*/
    public void SetQualityHigh()
    {
        QualitySettings.SetQualityLevel(0);
    }
    public void SetQualityMedium()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void SetQualityLow()
    {
        QualitySettings.SetQualityLevel(2);
    }
    public void SetFullScreen()
    {
        if (isFullScreen == true)
        {
            isFullScreen = false;
        }
        else if (isFullScreen == false)
        {
            isFullScreen = true;
        }        
    }
    public void set1920()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void set1600()
    {
        Screen.SetResolution(1600, 900, true);
    }
    public void set1440()
    {
        Screen.SetResolution(1440, 900, true);
    }
    public void set1280()
    {
        Screen.SetResolution(1280, 800, true);
    }
    public void BackToMenu()
    {
        MenuState = "MainMenu";
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        CheckpointsPanel.SetActive(false);
		eventSys.SetSelectedGameObject (buttonList [0]);
    }
}
