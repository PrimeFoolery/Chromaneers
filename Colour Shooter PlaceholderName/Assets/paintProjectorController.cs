using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintProjectorController : MonoBehaviour
{

    public Material BluePaintSplat;
    public Material RedPaintSplat;
    public Material YellowPaintSplat;

    [Header("How much time before the paint dissapears")]
    public float lifeTimer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    lifeTimer -= Time.deltaTime;
	    if (lifeTimer<=0)
	    {
            Destroy(gameObject);
	    }
	}

    public void ChangeToRed()
    {
        GetComponent<Projector>().material = RedPaintSplat;
    }
    public void ChangeToBlue()
    {
        GetComponent<Projector>().material = BluePaintSplat;
    }
    public void ChangeToYellow()
    {
        GetComponent<Projector>().material = YellowPaintSplat;
    }
}
