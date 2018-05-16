using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour {

    public GameObject canvasHolder;
    public bool highResWaterHolder;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        canvasHolder = GameObject.FindGameObjectWithTag("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
        highResWaterHolder = canvasHolder.GetComponent<MainMenu>().highResWater;

	}
}
