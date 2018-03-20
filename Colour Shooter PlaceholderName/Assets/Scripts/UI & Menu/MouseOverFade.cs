using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverFade : MonoBehaviour {

    public Text buttonText;

	// Use this for initialization
	void Start () {
        buttonText.GetComponent<Text>().color = new Color(0, 0, 0, 0.5f);

    }
	
	// Update is called once per frame
	void Update () {
        

    }
    void OnPointerEnter(PointerEventData eventData)
    {
        print("OVER");
        buttonText.GetComponent<Text>().color = new Color (0, 0, 0, 1f);
    }
    void OnPointerExit(PointerEventData eventData)
    {
        print("EXIT");
        buttonText.GetComponent<Text>().color = new Color(0, 0, 0, 0.5f);
    }
}
