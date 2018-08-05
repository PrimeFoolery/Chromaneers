using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObeliskTriggerCube : MonoBehaviour
{

    public GameObject obelisk;

    public int health1 = 1;
    public int health2 = 1;

    private float health1ResetTimer = 3f;
    private float health2ResetTimer = 3f;

    private bool colourBlindModeActive = false;
    public GameObject cbPurpleIndicator;
    public GameObject cbOrangeIndicator;
    public GameObject cbGreenIndicator;
    private GameObject cbCurrentIndicator;
    public bool isTriggerable = false;

    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<ColourSelectManager>().colourBlindMode == true)
        {
            
            colourBlindModeActive = true;
        }
        else
        {
            colourBlindModeActive = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.F1))
	    {
	        if (colourBlindModeActive == false)
	        {
	            if (isTriggerable==true)
	            {
	                SpawnColourBlindIndicator();
                }
	            colourBlindModeActive = true;
	        }
	        else
	        {
	            if (cbCurrentIndicator!=null)
	            {
	                Destroy(cbCurrentIndicator);
                }
	            colourBlindModeActive = false;
	        }
	    }
        if (health1==0)
	    {
	        health1ResetTimer -= Time.deltaTime;
	    }

	    if (health1ResetTimer<=0)
	    {
	        health1 = 1;
	        health1ResetTimer = 3f;
	    }
	    if (health2 == 0)
	    {
	        health2ResetTimer -= Time.deltaTime;
	    }

	    if (health2ResetTimer <= 0)
	    {
	        health2 = 1;
	        health2ResetTimer = 3f;
	    }

	    if (health1==0 && health2==0)
	    {
            obelisk.GetComponent<BossBattleObelisk>().Grow();
	    }
    }

    void OnCollisionEnter(Collision other)
    {
        if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.blue)
        {
            if (other.gameObject.tag=="RedBullet")
            {
                if (health1 >0)
                {
                    health1 -= 1;
                }
                health1ResetTimer = 3f;
            }else
            if (other.gameObject.tag == "YellowBullet")
            {
                if (health2 > 0)
                {
                    health2 -= 1;
                }
                health2ResetTimer = 3f;
            }else if (other.gameObject.tag =="BlueBullet")
            {
                Destroy(other.gameObject);
            }
        }
        if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.red)
        {
            if (other.gameObject.tag == "BlueBullet")
            {
                if (health1 >0)
                {
                    health1 -= 1;
                }
                health1ResetTimer = 3f;
            }else
            if (other.gameObject.tag == "YellowBullet")
            {
                if (health2 > 0)
                {
                    health2 -= 1;
                }
                health2ResetTimer = 3f;
            }
            else if (other.gameObject.tag == "RedBullet")
            {
                Destroy(other.gameObject);
            }
        }
        if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.yellow)
        {
            if (other.gameObject.tag == "BlueBullet")
            {
                if (health1 > 0)
                {
                    health1 -= 1;
                }
                health1ResetTimer = 3f;
            }else
            if (other.gameObject.tag == "RedBullet")
            {
                if (health2 > 0)
                {
                    health2 -= 1;
                }
                health2ResetTimer = 3f;
            }
            else if (other.gameObject.tag == "YellowBullet")
            {
                Destroy(other.gameObject);
            }
        }
    }
    public void SpawnColourBlindIndicator()
    {
        if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.blue)
        {
            cbCurrentIndicator = Instantiate(cbOrangeIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
        else if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.red)
        {
            cbCurrentIndicator = Instantiate(cbGreenIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
        else if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.yellow)
        {
            cbCurrentIndicator = Instantiate(cbPurpleIndicator,
                new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
                Quaternion.Euler(90, 0, 0));
            cbCurrentIndicator.transform.SetParent(transform);
        }
    }

}
