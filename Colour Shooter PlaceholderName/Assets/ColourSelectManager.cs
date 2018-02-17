using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSelectManager : MonoBehaviour {

    public static ColourSelectManager instance;

    //Handles singleton instnace
    void Awake () {
        if (instance != null) {
            Debug.LogError("More than one ColourSelectManager in Scene!");
            return;
        }
        instance = this;
    }

    [Header("Bullet Prefabs")]
    public bool ces;

	void Start () {
		//The game will start with the blue bullet
	}
	
	void Update () {
		
	}
}
