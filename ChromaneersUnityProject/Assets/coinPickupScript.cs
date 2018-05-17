using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickupScript : MonoBehaviour {

    private enum CoinState
    {
        up,
        down,
        spinning
    }

    private CoinState thisCoinsState = CoinState.up;
    private float jumpSpeed = 8;
    private float rotationSpeed = 20f;
    public Vector3 randomDirection;

    private float upTime = 0.25f;

    private coinController gamesCoinController;

	// Use this for initialization
	void Start () {
		randomDirection = Random.onUnitSphere;
	    randomDirection.y = 1f;
	    gamesCoinController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<coinController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (thisCoinsState == CoinState.up)
	    {
	        this.transform.position += randomDirection * jumpSpeed * Time.deltaTime;
	        upTime -= Time.deltaTime;
	        if (upTime<=0)
	        {
	            thisCoinsState = CoinState.down;
	            randomDirection.y = -1f;
	        }
	    }else
	    if (thisCoinsState == CoinState.down)
	    {
	        this.transform.position += randomDirection * jumpSpeed *1.2f * Time.deltaTime;
	        upTime += Time.deltaTime;
	        if (upTime >= 0.163f)
	        {
	            thisCoinsState = CoinState.spinning;
	        }
	    }else
        if (thisCoinsState == CoinState.spinning)
	    {
            this.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
	    }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BluePlayer")
        {
            gamesCoinController.bluesCoins += 1;
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "RedPlayer")
        {
            gamesCoinController.redsCoins += 1;
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "YellowPlayer")
        {
            gamesCoinController.yellowsCoins += 1;
            Destroy(this.gameObject);
        }
    }

    public void ForceDirection(Vector3 directionInput)
    {
        randomDirection = directionInput;
    }
}
