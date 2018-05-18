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
    public AudioMixer audioMixer;

    public GameObject[] buttonList;
    public GameObject[] buttonListOpt;
    public bool canInteract = true;
    private float InteractTimer;
    public float maxIntTimer;


    public Image TickBoxImage;
    public Sprite[] TickBoxSprites;
    public bool highResWater = true;
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
        if(MenuState == "OptionsMenu")
        {
			if (eventSys.currentSelectedGameObject == buttonListOpt [0].gameObject) {
				volumeText.GetComponent<TextMeshProUGUI> ().color = new Color (1, 1, 1, 1f);
			} else {
				volumeText.GetComponent<TextMeshProUGUI> ().color = new Color (1, 1, 1, 0.5f);
			}
            if(eventSys.currentSelectedGameObject == buttonListOpt [1])
            {
                TickBoxImage.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
				hiResWaterText.GetComponent<TextMeshProUGUI> ().color = new Color (1, 1, 1, 1f);
            }
            else
            {
                TickBoxImage.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
				hiResWaterText.GetComponent<TextMeshProUGUI> ().color = new Color (1, 1, 1, 0.5f);
            }
			if (eventSys.currentSelectedGameObject == buttonListOpt [2]) {
				backText.GetComponent<TextMeshProUGUI> ().color = new Color (1, 1, 1, 1f);
			} else {
				backText.GetComponent<TextMeshProUGUI> ().color = new Color (1, 1, 1, 0.5f);
			}
            if (eventSys.currentSelectedGameObject == buttonListOpt[3])
            {
                qualityText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
            }
            else
            {
                qualityText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
            }

            if (eventSys.currentSelectedGameObject == buttonListOpt[4])
            {
                highText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
            }
            else
            {
                highText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListOpt[5])
            {
                mediumText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
            }
            else
            {
                mediumText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
            }
            if (eventSys.currentSelectedGameObject == buttonListOpt[6])
            {
                lowText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
            }
            else
            {
                lowText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
            }

            if (highResWater == true)
            {
                TickBoxImage.sprite = TickBoxSprites[0];
            }
            if (highResWater == false)
            {
                TickBoxImage.sprite = TickBoxSprites[1];
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
    public void SetWater()
    {
        if(highResWater == true)
        {
            highResWater = false;
        }
        else if(highResWater == false)
        {
            highResWater = true;
        }
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
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
    public void BackToMenu()
    {
        MenuState = "MainMenu";
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
		eventSys.SetSelectedGameObject (buttonList [0]);
    }
}
