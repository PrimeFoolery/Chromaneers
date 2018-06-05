using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColourSpaceManager : MonoBehaviour {

	void Update () {
		if (SceneManager.GetActiveScene().name == "IdleTrailer")
        {
            UnityEditor.PlayerSettings.colorSpace = ColorSpace.Gamma;
        }

        if (SceneManager.GetActiveScene().name == "MenuTest" || SceneManager.GetActiveScene().name == "NewWorld" 
            || SceneManager.GetActiveScene().name == "NewWorldCheckpoint1"
            || SceneManager.GetActiveScene().name == "NewWorldCheckpoint2"
            || SceneManager.GetActiveScene().name == "NewWorldCheckpoint3")
        {
            UnityEditor.PlayerSettings.colorSpace = ColorSpace.Linear;
        }
	}
}
