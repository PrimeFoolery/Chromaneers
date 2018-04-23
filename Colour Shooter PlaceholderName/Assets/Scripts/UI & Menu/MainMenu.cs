using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject OptionsPanel;
    public GameObject MainMenuPanel;

    public void SinglePlayer()
    {
        SceneManager.LoadScene("DemoWorldSinglePlayer");
    }

    public void LocalCoop()
    {
        SceneManager.LoadScene("DemoWorldCOOP");
    }
    public void Options()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }
    public void Exit()
    {

    }
}
