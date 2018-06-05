using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMe : MonoBehaviour {

    private static bool created;

	void Awake ()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }
}
