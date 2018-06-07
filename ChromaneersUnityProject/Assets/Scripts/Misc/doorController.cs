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

    private ParticleSystem thisDoorsParticleSystem;
    private bool doorMoved = false;
    private float doorMoveSpeed = 0.01f;
    private GameObject camera;

    public Transform doorTargetTransform;

    private float min;
    private float max;

    // Use this for initialization
    void Start () {
		targetMovement =new Vector3(transform.localPosition.x,transform.localPosition.y, transform.localPosition.z+20);
	    foreach (GameObject gameObjectToDissolve in gameObjectsToDissolve)
	    {
	        gameObjectToDissolve.GetComponent<Renderer>().material.SetColor("_BurnColor", doorColor);
	    }
        thisDoorsParticleSystem = gameObject.GetComponent<ParticleSystem>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        min = transform.position.x - 0.2f;
        max = transform.position.x + 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
	    if (doorOpen == true && doorMoved == false && doorOnScreen == true)
	    {
	        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
	        camera.GetComponent<CameraScript>().SmallScreenShake();
	        transform.Translate(0, -doorMoveSpeed * Time.deltaTime, 0);
	        if (doorMoveSpeed < 3)
	        {
	            doorMoveSpeed *= 1.1f;
	        }
	        if (thisDoorsParticleSystem != null)
	        {
	            thisDoorsParticleSystem.Play();
	        }

	        if (Vector3.Distance(transform.localPosition, doorTargetTransform.localPosition) < 0.5f)
	        {
	            doorMoved = true;
	        }
	    }

	    if (doorMoved == true && thisDoorsParticleSystem != null)
	    {
	        thisDoorsParticleSystem.Stop();
	        gameObject.GetComponent<BoxCollider>().enabled = false;
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
        Debug.Log("goingOffScreen");
        doorOnScreen = false;
    }

    public void OnBecameVisible()
    {
        Debug.Log("goingOnScreen");
        doorOnScreen = true;
    }
}
