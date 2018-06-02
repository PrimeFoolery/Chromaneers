using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{

    private Vector3 targetMovement;

    public bool doorOpen = false;

    public List<GameObject> gameObjectsToDissolve = new List<GameObject>();

    [Range(0.0f, 10.0f)] public float doorSpeed = 5f;

    public Color doorColor;

    public int amountOfInputsBeforeOpening = 1;
    private float dissolveValue = 0;

    public bool doorOnScreen = false;

	// Use this for initialization
	void Start () {
		targetMovement =new Vector3(transform.localPosition.x,transform.localPosition.y, transform.localPosition.z+20);
	    foreach (GameObject gameObjectToDissolve in gameObjectsToDissolve)
	    {
	        gameObjectToDissolve.GetComponent<Renderer>().material.SetColor("_BurnColor", doorColor);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    if (doorOpen == true )
	    {
            Debug.Log("dissolvingDoor");
	        dissolveValue += 0.1f*Time.deltaTime;
	        gameObject.GetComponent<BoxCollider>().enabled = false;
	        foreach (GameObject gameObjectToDissolve in gameObjectsToDissolve)
	        {
	            gameObjectToDissolve.GetComponent<Renderer>().material.SetFloat("_SliceAmount", dissolveValue);
	        }
        }
	}

    public void OpenSesame()
    {
        amountOfInputsBeforeOpening -= 1;
        if (amountOfInputsBeforeOpening<1)
        {
            doorOpen = true;

        }
    }

    public void OnBecameInvisible()
    {
        doorOnScreen = false;
    }

    public void OnBecameVisible()
    {
        doorOnScreen = true;
    }
}
