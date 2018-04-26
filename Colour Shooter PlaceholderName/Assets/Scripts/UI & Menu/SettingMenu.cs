using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;

    public EventSystem eventSys;
    private GameObject storedSelected;

    [Header("Input Selection")]
    public GameObject[] buttonList;
    public bool canInteract = true;
    public float InteractTimer;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (eventSys.currentSelectedGameObject == null)
            {
                eventSys.SetSelectedGameObject(buttonList[0]);
            }
            if (eventSys.currentSelectedGameObject == buttonList[0])
            {
                eventSys.SetSelectedGameObject(buttonList[1]);
            }
            if (eventSys.currentSelectedGameObject == buttonList[1])
            {
                eventSys.SetSelectedGameObject(buttonList[2]);
            }
            if (eventSys.currentSelectedGameObject == buttonList[2])
            {
                eventSys.SetSelectedGameObject(buttonList[3]);
            }
            if (eventSys.currentSelectedGameObject == buttonList[3])
            {
                eventSys.SetSelectedGameObject(buttonList[0]);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (eventSys.currentSelectedGameObject == null)
            {
                eventSys.SetSelectedGameObject(buttonList[3]);
            }
            if (eventSys.currentSelectedGameObject == buttonList[3])
            {
                eventSys.SetSelectedGameObject(buttonList[2]);
            }
            if (eventSys.currentSelectedGameObject == buttonList[2])
            {
                eventSys.SetSelectedGameObject(buttonList[1]);
            }
            if (eventSys.currentSelectedGameObject == buttonList[1])
            {
                eventSys.SetSelectedGameObject(buttonList[0]);
            }
            if (eventSys.currentSelectedGameObject == buttonList[0])
            {
                eventSys.SetSelectedGameObject(buttonList[3]);
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
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
