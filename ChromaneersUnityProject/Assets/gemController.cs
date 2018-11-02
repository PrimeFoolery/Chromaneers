using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemController : MonoBehaviour
{

    private scoreTracker scoreKeeper;
    public int whichGemIsThis;
    public float rotateSpeed = 50f;

	// Use this for initialization
	void Start ()
	{
	    scoreKeeper = GameObject.FindGameObjectWithTag("Settings").GetComponent<scoreTracker>();
	    if (scoreKeeper.gem1Collected==true&&whichGemIsThis==1)
	    {
            Destroy(gameObject);
	    }
	}

    void Update()
    {
        transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="BluePlayer"|| other.tag == "RedPlayer"||other.tag == "YellowPlayer")
        {
            if (whichGemIsThis==1)
            {
                scoreKeeper.gem1Got();
            }else if (whichGemIsThis==2)
            {
                scoreKeeper.gem2Got();
            }else if (whichGemIsThis==3)
            {
                scoreKeeper.gem3Got();
            }
            Destroy(gameObject);
        }

    }
}
