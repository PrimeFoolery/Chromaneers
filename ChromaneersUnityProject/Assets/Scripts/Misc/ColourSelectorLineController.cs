using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSelectorLineController : MonoBehaviour {


    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        lineRenderer.SetPosition(0, gameObject.GetComponent<RectTransform>().position);
        //lineRenderer.SetPosition(1, new Vector2(Screen.width / 2, Screen.height / 2));
	}
}
