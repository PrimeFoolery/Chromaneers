using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleObeliskSphere : MonoBehaviour
{

    public GameObject obelisk;

    private bool hasObeliskBeenUsed = false; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Boss")
        {
            if (obelisk.GetComponent<BossBattleObelisk>().CurrentObeliskState == BossBattleObelisk.ObeliskState.idle)
            {
                if (other.gameObject.transform.parent.gameObject.GetComponent<BossController>().colourOfEnemy == "grey")
                {
                    if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.blue)
                    {
                        if (hasObeliskBeenUsed == false)
                        {
                            obelisk.GetComponent<BossBattleObelisk>().StartDraining();
                            other.gameObject.transform.parent.gameObject.GetComponent<BossController>().ChangeToBlue();
                            hasObeliskBeenUsed = true;
                        }
                    }
                    if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.red)
                    {
                        if (hasObeliskBeenUsed == false)
                        {
                            obelisk.GetComponent<BossBattleObelisk>().StartDraining();
                            other.gameObject.transform.parent.gameObject.GetComponent<BossController>().ChangeToRed();
                            hasObeliskBeenUsed = true;
                        }
                    }
                    if (obelisk.GetComponent<BossBattleObelisk>().colourOfThisObelisk == BossBattleObelisk.ColoursOfObelisk.yellow)
                    {
                        if (hasObeliskBeenUsed == false)
                        {
                            obelisk.GetComponent<BossBattleObelisk>().StartDraining();
                            other.gameObject.transform.parent.gameObject.GetComponent<BossController>().ChangeToYellow();
                            hasObeliskBeenUsed = true;
                        }
                    }
                }
            }
        }
    }
}
