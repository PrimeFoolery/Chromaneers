using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
	// Use this for initialization
	public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetGrass(bool highResGrass)
    {
        //
    }
    public void BackToMenu()
    {
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
