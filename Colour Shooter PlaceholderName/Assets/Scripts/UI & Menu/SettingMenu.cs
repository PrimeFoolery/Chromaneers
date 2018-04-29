using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;

    public EventSystem eventSys;
    private GameObject storedSelected;

    [Header("Input Selection")]
    public GameObject[] buttonList;
    public GameObject volumeText;
    public bool canInteract = true;
    private float InteractTimer;
    public float maxIntTimer;

    void Update()
    {
        /*if(eventSys.currentSelectedGameObject == buttonList[0].gameObject)
          {
              volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 230f);
          }
          if(eventSys.currentSelectedGameObject != buttonList[0].gameObject)
          {
              volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 160f);
          }*/
        Vector3 menuInput1;
        menuInput1 = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
        if (canInteract == false)
        {
            InteractTimer -= Time.deltaTime;
            if (InteractTimer <= 0)
            {
                canInteract = true;
                InteractTimer = maxIntTimer;
            }
        }
        if (menuInput1.z < 0f)
        {
            if(eventSys.currentSelectedGameObject == null)
            {
                eventSys.SetSelectedGameObject(buttonList[0]);
                //volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 230f);
                canInteract = false;
            }
            if(eventSys.currentSelectedGameObject == buttonList[0] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[1]);
               // volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 160f);
                canInteract = false;
            }
            if(eventSys.currentSelectedGameObject == buttonList[1] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[2]);
               // volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 160f);
                canInteract = false;
            }
            if(eventSys.currentSelectedGameObject == buttonList[2] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[3]);
                //volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 160f);
                canInteract = false;
            }
            if(eventSys.currentSelectedGameObject == buttonList[3] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[0]);
                //volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 230f);
                canInteract = false;
            }
        }
        if (menuInput1.z > 0f)
        {
            if(eventSys.currentSelectedGameObject == null)
            {
                eventSys.SetSelectedGameObject(buttonList[3]);
                canInteract = false;
            }
            if(eventSys.currentSelectedGameObject == buttonList[3] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[2]);
                canInteract = false;
            }
            if(eventSys.currentSelectedGameObject == buttonList[2] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[1]);
                canInteract = false;
            }
            if(eventSys.currentSelectedGameObject == buttonList[1] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[0]);
                canInteract = false;
            }
            if(eventSys.currentSelectedGameObject == buttonList[0] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[3]);
                canInteract = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (eventSys.currentSelectedGameObject == null)
            {
                eventSys.SetSelectedGameObject(buttonList[0]);
                //volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 230f);
                canInteract = false;
            }
            if (eventSys.currentSelectedGameObject == buttonList[0] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[1]);
                // volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 160f);
                canInteract = false;
            }
            if (eventSys.currentSelectedGameObject == buttonList[1] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[2]);
                // volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 160f);
                canInteract = false;
            }
            if (eventSys.currentSelectedGameObject == buttonList[2] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[3]);
                //volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 160f);
                canInteract = false;
            }
            if (eventSys.currentSelectedGameObject == buttonList[3] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[0]);
                //volumeText.GetComponent<Image>().color = new Color(0, 0, 0, 230f);
                canInteract = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (eventSys.currentSelectedGameObject == null)
            {
                eventSys.SetSelectedGameObject(buttonList[3]);
                canInteract = false;
            }
            if (eventSys.currentSelectedGameObject == buttonList[3] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[2]);
                canInteract = false;
            }
            if (eventSys.currentSelectedGameObject == buttonList[2] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[1]);
                canInteract = false;
            }
            if (eventSys.currentSelectedGameObject == buttonList[1] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[0]);
                canInteract = false;
            }
            if (eventSys.currentSelectedGameObject == buttonList[0] && canInteract == true)
            {
                eventSys.SetSelectedGameObject(buttonList[3]);
                canInteract = false;
            }
        }
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetGrass(bool highResGrass)
    {
        //
    }
    public void SetWater(bool highResWater)
    {
        //
    }
    public void BackToMenu()
    {
        //eventSys.SetSelectedGameObject(MainMenuPanel.GetComponent<MainMenu>().buttonList[0]);
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }
}
