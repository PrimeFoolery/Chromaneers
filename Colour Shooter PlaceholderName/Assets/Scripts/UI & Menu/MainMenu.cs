using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public EventSystem eventSys;
    private GameObject storedSelected;
    public bool usingXboxController = true;

    public GameObject OptionsPanel;
    public GameObject MainMenuPanel;

    public GameObject[] buttonList;
    public bool canInteract = true;
    private float InteractTimer;
    public float maxIntTimer;

    void Start()
    {
        //storedSelected = eventSys.firstSelectedGameObject;
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
        if (usingXboxController==true) {
            Vector3 menuInput1;
            menuInput1 = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
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
            Vector3 menuInput2;
            menuInput2 = new Vector3(Input.GetAxisRaw("XboxJoystick2LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick2LVertical"));
            if (menuInput2.z < 0f)
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
            if (menuInput2.z > 0f)
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
            Vector3 menuInput3;
            menuInput3 = new Vector3(Input.GetAxisRaw("XboxJoystick3LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick3LVertical"));
            if (menuInput3.z < 0f)
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
            if (menuInput3.z > 0f)
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
        if(usingXboxController == false)
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

        if (Input.GetKeyDown(KeyCode.S))
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
        if (Input.GetKeyDown(KeyCode.W))
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
        /* if(eventSys.currentSelectedGameObject != storedSelected)
           {
               if(eventSys.currentSelectedGameObject == null)
                {
                    eventSys.SetSelectedGameObject(storedSelected);
                }
                else
                {
                    storedSelected = eventSys.currentSelectedGameObject;
                }
           }*/
    }
    /*    public void SinglePlayer()
        {
            SceneManager.LoadScene("DemoWorldSinglePlayer");
        }*/

    

    public void Play()
    {
        SceneManager.LoadScene("MainLevelOne");
    }
    public void Options()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
        eventSys.SetSelectedGameObject(OptionsPanel.GetComponent<SettingMenu>().buttonList[0]);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
