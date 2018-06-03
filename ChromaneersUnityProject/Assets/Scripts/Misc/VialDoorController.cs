using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialDoorController : MonoBehaviour
{

    public GameObject leftVial;
    public GameObject middleVial;
    public GameObject rightVial;

    public Transform leftVialTargetTransform;
    public Transform middleVialTargetTransform;
    public Transform rightVialTargetTransform;

    public Transform doorTargetTransform;

    public float vialMoveSpeed = 1f;
    public float doorMoveSpeed = 3f;

    public bool doorFullyFilled = false;

    private ParticleSystem thisDoorsParticleSystem;
    private bool doorMoved = false;

	// Use this for initialization
	void Start ()
	{
	    thisDoorsParticleSystem = gameObject.GetComponent<ParticleSystem>();
	    //leftVialTargetTransform = new Vector3(leftVial.transform.position.x, leftVial.transform.position.y, leftVial.transform.position.z+2f);
	    //middleVialTargetTransform = new Vector3(middleVial.transform.position.x, middleVial.transform.position.y , middleVial.transform.position.z+2f);
	    //rightVialTargetTransform = new Vector3(rightVial.transform.position.x, rightVial.transform.position.y , rightVial.transform.position.z+2f);

	    //doorTargetTransform = new Vector3(transform.position.x-22.5f, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	    if (leftVial.GetComponentInChildren<VialController>().VialCorrectlyFilled==true && Vector3.Distance(leftVial.transform.position, leftVialTargetTransform.position)>0f )
	    {
	        leftVial.transform.position = Vector3.MoveTowards(leftVial.transform.position, leftVialTargetTransform.position,vialMoveSpeed * Time.deltaTime);
	    }
	    if (middleVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true && Vector3.Distance(middleVial.transform.position, middleVialTargetTransform.position) > 0f )
	    {
	        middleVial.transform.position = Vector3.MoveTowards(middleVial.transform.position, middleVialTargetTransform.position, vialMoveSpeed * Time.deltaTime);
	    }
	    if (rightVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true && Vector3.Distance(rightVial.transform.position, rightVialTargetTransform.position) > 0f )
	    {
	        rightVial.transform.position = Vector3.MoveTowards(rightVial.transform.position, rightVialTargetTransform.position, vialMoveSpeed * Time.deltaTime);
	    }

	    if (leftVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true && middleVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true && (rightVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true))
	    {
	        doorFullyFilled = true;
	       
	    }

	    if (doorFullyFilled == true && doorMoved == false) 
	    {
	        transform.position = Vector3.MoveTowards(transform.position, doorTargetTransform.position, doorMoveSpeed * Time.deltaTime);
	        if (thisDoorsParticleSystem != null)
	        {
                thisDoorsParticleSystem.Play();
	        }

	        if (Vector3.Distance(transform.position, doorTargetTransform.position) < 0.5f)
	        {
	            doorMoved = true;
	        }
        }

	    if (doorMoved == true && thisDoorsParticleSystem != null)
	    {
            thisDoorsParticleSystem.Stop();
	    }
    }
}
