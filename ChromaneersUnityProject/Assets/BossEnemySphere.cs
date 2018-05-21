using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySphere : MonoBehaviour
{

    public GameObject BossMain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (BossMain.GetComponent<BossController>().currentBossPhase == BossController.BossPhases.pens)
        {
            if (BossMain.GetComponent<BossController>().colourOfEnemy == "blue")
            {
                if (other.gameObject.tag == "BlueBullet")
                {
                    BossMain.GetComponent<BossController>().enemyHealth -= 1;
                    BossMain.GetComponent<ParticleSystem>().Play();
                    Destroy(other.gameObject);
                }
            }
            if (BossMain.GetComponent<BossController>().colourOfEnemy == "red")
            {
                if (other.gameObject.tag == "RedBullet")
                {
                    BossMain.GetComponent<BossController>().enemyHealth -= 1;
                    BossMain.GetComponent<ParticleSystem>().Play();
                    Destroy(other.gameObject);
                }
            }
            if (BossMain.GetComponent<BossController>().colourOfEnemy == "yellow")
            {
                if (other.gameObject.tag == "YellowBullet")
                {
                    BossMain.GetComponent<BossController>().enemyHealth -= 1;
                    BossMain.GetComponent<ParticleSystem>().Play();
                    Destroy(other.gameObject);
                }
            }
        }else if (BossMain.GetComponent<BossController>().currentBossPhase == BossController.BossPhases.panic)
        {
            if (other.gameObject.tag == "RainbowBullet" || other.gameObject.tag == "RedBullet" || other.gameObject.tag == "BlueBullet" || other.gameObject.tag == "YellowBullet")
            {
                BossMain.GetComponent<BossController>().enemyHealth -= 1;
                BossMain.transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
                BossMain.GetComponent<BossController>().SpawnCandy();
                BossMain.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("BluePlayer"))
        {
            other.gameObject.GetComponent<CoopCharacterHealthControllerOne>().GetHit();
        }

        if (other.gameObject.CompareTag("RedPlayer"))
        {
            other.gameObject.GetComponent<CoopCharacterHealthControllerTwo>().GetHit();
        }

        if (other.gameObject.CompareTag("YellowPlayer"))
        {
            other.gameObject.GetComponent<CoopCharacterHealthControllerThree>().GetHit();
        }

    }
}
