using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAggroPingScript : MonoBehaviour {

    public enum PingState
    {
        grow,
        shrink
    }

    public GameObject boss;
    public PingState currentPingState = PingState.grow;
    private GameObject playerThatGotPinged;
    private float pingTime = 1.5f;

    private Color redPingColor = new Color(1,0,0,0.5f);
    private Color bluePingColor = new Color(0, 0, 1, 0.5f);
    private Color yellowPingColor = new Color(1, 1, 0, 0.5f);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (currentPingState == PingState.grow)
	    {
	        transform.localScale += new Vector3(5f, 5f, 0) * Time.deltaTime;
	        pingTime -= Time.deltaTime;
	        if (pingTime< 0)
	        {
	            currentPingState = PingState.shrink;
	        }
	    }

	    if (currentPingState == PingState.shrink)
	    {
            transform.localScale-= new Vector3(5f, 5f, 0)*Time.deltaTime;
	        if (transform.localScale.y < 1)
	        {
	            if (playerThatGotPinged!=null)
	            {
	                boss.GetComponent<BossController>().targetPlayer = playerThatGotPinged;
	                if (boss.GetComponent<BossController>().isAggroPlayer==false)
	                {
	                    boss.GetComponent<BossController>().isAggroPlayer = true;
	                }
                }
	            
                Destroy(this.gameObject);
	        }
	    }
	}

    void OnTriggerEnter(Collider other)
    {
        if (currentPingState == PingState.grow)
        {
            if (other.CompareTag("BluePlayer")|| other.CompareTag("RedPlayer")|| other.CompareTag("YellowPlayer"))
            {
                playerThatGotPinged = other.gameObject;
                if (playerThatGotPinged.tag=="BluePlayer")
                {
                    gameObject.GetComponent<SpriteRenderer>().color = bluePingColor;
                }else
                if (playerThatGotPinged.tag == "RedPlayer")
                {
                    gameObject.GetComponent<SpriteRenderer>().color = redPingColor;
                }
                else
                if (playerThatGotPinged.tag == "YellowPlayer")
                {
                    gameObject.GetComponent<SpriteRenderer>().color = yellowPingColor;
                }
                
                currentPingState = PingState.shrink;
            }
        }
    }
}
