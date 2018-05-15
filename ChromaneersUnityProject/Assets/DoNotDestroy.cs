using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class DoNotDestroy : MonoBehaviour {

    public GameObject canvasHolder;
    
    public bool highResWater;
    public int volume;
    public AudioMixer mixerHolder;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start () {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "MenuTest")
        {
            canvasHolder = GameObject.FindGameObjectWithTag("Canvas");   
        }
        if (sceneName == "NewWorld")
        {
            canvasHolder = GameObject.FindGameObjectWithTag("Canvas");
        }
    }
	
	// Update is called once per frame
	void Update () {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(sceneName == "MenuTest")
        {            
            highResWater = canvasHolder.GetComponent<MainMenu>().highResWater;
        }
        if(sceneName == "NewWorld")
        {

        }        
	}
}
