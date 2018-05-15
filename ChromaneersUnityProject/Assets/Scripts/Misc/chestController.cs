using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestController : MonoBehaviour
{

    public CharacterOneGunController.currentWeapon thisChestsWeapon =
        CharacterOneGunController.currentWeapon.TrishotWeapon;

    public GameObject chestBase;
    public GameObject chestLid;

    public GameObject trishotPickup;
    public GameObject sniperPickup;
	public GameObject smgPickup;
	public GameObject rainbowPickup;

    private bool hasChestBeenOpened = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (hasChestBeenOpened==true)
	    {
            //gameObject.GetComponent<ParticleSystem>().Stop();
	        gameObject.GetComponent<BoxCollider>().enabled = false;
	    }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("BlueBullet")||other.collider.CompareTag("RedBullet")||other.collider.CompareTag("YellowBullet") 
            || other.collider.CompareTag("RainbowBullet") &&hasChestBeenOpened==false)
        {
            
            if (thisChestsWeapon == CharacterOneGunController.currentWeapon.TrishotWeapon)
            {
                Instantiate(trishotPickup, new Vector3(chestLid.transform.position.x, chestLid.transform.position.y - 2.5f, chestLid.transform.position.z), Quaternion.identity);
            } else
            if (thisChestsWeapon == CharacterOneGunController.currentWeapon.SniperWeapon)
            {
                Instantiate(sniperPickup, new Vector3(chestLid.transform.position.x,chestLid.transform.position.y-2.5f, chestLid.transform.position.z), Quaternion.identity);
            } else
			if (thisChestsWeapon == CharacterOneGunController.currentWeapon.SMGWeapon)
			{
				Instantiate(smgPickup, new Vector3(chestLid.transform.position.x,chestLid.transform.position.y-2.5f, chestLid.transform.position.z), Quaternion.identity);
			}else
			if (thisChestsWeapon == CharacterOneGunController.currentWeapon.RainbowWeapon)
			{
				Instantiate(rainbowPickup, new Vector3(chestLid.transform.position.x,chestLid.transform.position.y-2.5f, chestLid.transform.position.z), Quaternion.identity);
			}
						
            Destroy(chestBase.gameObject);
            Destroy(chestLid.gameObject);
            gameObject.GetComponent<ParticleSystem>().Play();
            hasChestBeenOpened = true;
        }
    }
}
