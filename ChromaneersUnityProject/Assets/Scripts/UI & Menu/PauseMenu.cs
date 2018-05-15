using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    // Use this for initialization
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public EventSystem eventSys;
    private GameObject storedSelected;

    public bool UsingXboxController = true;

    public GameObject[] buttonList;
    public bool canInteract = true;
    private float InteractTimer;
    public float maxIntTimer;

    // Update is called once per frame
    void Update () {
        if (canInteract == false)
        {
            InteractTimer -= Time.deltaTime;
            if (InteractTimer <= 0)
            {
                canInteract = true;
                InteractTimer = maxIntTimer;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (UsingXboxController == true)
        {
            Vector3 menuInput1;
            menuInput1 = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
            if (menuInput1.z < 0f)
            {
                if(eventSys.currentSelectedGameObject == null)
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
                    eventSys.SetSelectedGameObject(buttonList[2]);
                    canInteract = false;
                }
            }
            if (Input.GetButtonDown("Pause1"))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            if (Input.GetButtonDown("Pause2"))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            if (Input.GetButtonDown("Pause3"))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
    public void Resume() {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuTest");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
