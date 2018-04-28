using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialDoorController : MonoBehaviour
{

    public GameObject leftVial;
    public GameObject middleVial;
    public GameObject rightVial;

    private Vector3 leftVialTargetTransform;
    private Vector3 middleVialTargetTransform;
    private Vector3 rightVialTargetTransform;

    private Vector3 doorTargetTransform;

    public float vialMoveSpeed = 1f;
    public float doorMoveSpeed = 3f;

    public bool doorFullyFilled = false;

	// Use this for initialization
	void Start () {
		leftVialTargetTransform = new Vector3(leftVial.transform.position.x, leftVial.transform.position.y, leftVial.transform.position.z+2f);
	    middleVialTargetTransform = new Vector3(middleVial.transform.position.x, middleVial.transform.position.y , middleVial.transform.position.z+2f);
	    rightVialTargetTransform = new Vector3(rightVial.transform.position.x, rightVial.transform.position.y , rightVial.transform.position.z+2f);

        doorTargetTransform = new Vector3(transform.position.x-22.5f, transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
	    if (leftVial.GetComponentInChildren<VialController>().VialCorrectlyFilled==true && Vector3.Distance(leftVial.transform.position, leftVialTargetTransform)>0f && doorFullyFilled==false)
	    {
	        leftVial.transform.position = Vector3.MoveTowards(leftVial.transform.position, leftVialTargetTransform,vialMoveSpeed * Time.deltaTime);
	    }
	    if (middleVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true && Vector3.Distance(middleVial.transform.position, middleVialTargetTransform) > 0f && doorFullyFilled==false)
	    {
	        middleVial.transform.position = Vector3.MoveTowards(middleVial.transform.position, middleVialTargetTransform, vialMoveSpeed * Time.deltaTime);
	    }
	    if (rightVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true && Vector3.Distance(rightVial.transform.position, rightVialTargetTransform) > 0f && doorFullyFilled == false)
	    {
	        rightVial.transform.position = Vector3.MoveTowards(rightVial.transform.position, rightVialTargetTransform, vialMoveSpeed * Time.deltaTime);
	    }

	    if (leftVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true && middleVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true && (rightVial.GetComponentInChildren<VialController>().VialCorrectlyFilled == true))
	    {
	        doorFullyFilled = true;
	        transform.position = Vector3.MoveTowards(transform.position, doorTargetTransform, doorMoveSpeed * Time.deltaTime);
	    }
    }
}
