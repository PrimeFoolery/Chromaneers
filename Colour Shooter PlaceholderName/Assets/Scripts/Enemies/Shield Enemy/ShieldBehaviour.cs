using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour {

    int randomColour;

    public Material Yellow;
    public Material Red;
    public Material Blue;
	// Use this for initialization
	void Start () {
        randomColour = Random.Range(0, 3);
        if(randomColour == 0)
        {
            this.tag = "YellowEnemy";
            this.gameObject.GetComponent<YellowEnemyHealth>().enabled = true;
			gameObject.GetComponent<Renderer> ().material = Yellow;
        }
        if (randomColour == 1)
        {
            this.tag = "RedEnemy";
            this.gameObject.GetComponent<RedEnemyHealth>().enabled = true;
			gameObject.GetComponent<Renderer> ().material = Red;
        }
        if (randomColour == 2)
        {
            this.tag = "BlueEnemy";
            this.gameObject.GetComponent<BlueEnemyHealth>().enabled = true;
			gameObject.GetComponent<Renderer> ().material = Blue;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
