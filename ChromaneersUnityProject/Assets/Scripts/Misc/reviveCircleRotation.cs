using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reviveCircleRotation : MonoBehaviour
{
    public Transform outlineCircle;
    public float reviveCircleRotationSpeedNormal = 10f;
    public float reviveCircleRotationSpeedFaster = 20f;
    public float reviveCircleRotationSpeedFastest = 30f;

    public int peopleInCircle = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (peopleInCircle==1)
	    {
	        transform.Rotate(0, 0, reviveCircleRotationSpeedNormal * Time.deltaTime);
        }
	    else if(peopleInCircle==2)
	    {
	        transform.Rotate(0, 0, reviveCircleRotationSpeedFaster * Time.deltaTime);
        }
        else if (peopleInCircle==3)
	    {
	        transform.Rotate(0, 0, reviveCircleRotationSpeedFastest * Time.deltaTime);
        }

	    if (outlineCircle.gameObject.activeSelf==true)
	    {
	        gameObject.GetComponent<Image>().enabled = true;
        }
	    else
	    {
	        gameObject.GetComponent<Image>().enabled = false;
        }

	    gameObject.transform.position = outlineCircle.position;
	}
}
