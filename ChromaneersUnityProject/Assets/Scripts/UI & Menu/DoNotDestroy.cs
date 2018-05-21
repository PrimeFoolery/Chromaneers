using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour {

    public GameObject canvasHolder;
    public bool highResWaterHolder;
    public float volumeHolder;
    public GameObject volumeSliderHolder;
    Scene currentScene;

    public static DoNotDestroy Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        Scene currentScene = SceneManager.GetActiveScene();
        canvasHolder = GameObject.FindGameObjectWithTag("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
        string sceneName = currentScene.name;

        if (sceneName == "MenuTest")
        {
            highResWaterHolder = canvasHolder.GetComponent<MainMenu>().highResWater;
            volumeHolder = canvasHolder.GetComponent<MainMenu>().buttonListOpt[6].GetComponent<Slider>().value;
        }
        if(sceneName == "NewWorld")
        {

        }
	}
}
