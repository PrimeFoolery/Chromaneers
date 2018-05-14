using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{

    private Vector3 targetMovement;

    public bool doorOpen = false;

    [Range(0.0f, 10.0f)] public float doorSpeed = 5f;

    public int amountOfInputsBeforeOpening = 1;

    public bool doorOnScreen = false;

	// Use this for initialization
	void Start () {
		targetMovement =new Vector3(transform.localPosition.x,transform.localPosition.y, transform.localPosition.z+20);
	}
	
	// Update is called once per frame
	void Update () {
	    if (doorOpen == true && Vector3.Distance(transform.position, targetMovement)>0.5f && doorOnScreen == true)
	    {
	        if (transform.localScale.z > -0.1f)
	        {
	            transform.localScale += new Vector3(-0f, -0f, -1f);
	            gameObject.GetComponent<ParticleSystem>().Play();

	        }
	        else if (transform.localScale.z <= -0.1f)
	        {
	            Destroy(this.gameObject);
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
