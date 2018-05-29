using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinController : MonoBehaviour
{

    public int bluesCoins;
    public int redsCoins;
    public int yellowsCoins;

    public GameObject bluePlayer;
    public GameObject redPlayer;
    public GameObject yellowPlayer;

    public GameObject rainbow;
    private GameObject tempRainbow;

	public CharacterOneGunController oneGunController;
	public CharacterTwoGunController twoGunController;
	public CharacterThreeGunController threeGunController;

    public enum RainbowState
    {
        preStart,
        spawn,
        grow,
        savour,
        stop
    }

    public int rainbowGrowSpeed = 8;
    private float rainbowTimer = 1.4f;
    public RainbowState CurrentRainbowState = RainbowState.preStart;

	// Use this for initialization
	void Start () {
		bluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");

	    redPlayer = GameObject.FindGameObjectWithTag("RedPlayer");

	    yellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");

    }
	
	// Update is called once per frame
	void Update () {
	    if (CurrentRainbowState == RainbowState.spawn)
	    {
			Debug.Log(Mathf.Max(bluesCoins, redsCoins, yellowsCoins));
	        if (Mathf.Max(bluesCoins, redsCoins, yellowsCoins) == bluesCoins)
	        {
				Debug.Log ("spawn blueRainbow");
	            tempRainbow = Instantiate(rainbow, new Vector3(bluePlayer.transform.position.x, bluePlayer.transform.position.y + 50f, bluePlayer.transform.position.z), Quaternion.identity, bluePlayer.transform);
	        }
	        if (Mathf.Max(bluesCoins, redsCoins, yellowsCoins) == redsCoins)
	        {
	            tempRainbow = Instantiate(rainbow, new Vector3(redPlayer.transform.position.x, redPlayer.transform.position.y + 50f, redPlayer.transform.position.z), Quaternion.identity, redPlayer.transform);
            }
	        if (Mathf.Max(bluesCoins, redsCoins, yellowsCoins) == yellowsCoins)
	        {
				Debug.Log ("spawn yellowRainbow");
	            tempRainbow = Instantiate(rainbow, new Vector3(yellowPlayer.transform.position.x, yellowPlayer.transform.position.y + 50f, yellowPlayer.transform.position.z), Quaternion.identity, yellowPlayer.transform);
            }

	        CurrentRainbowState = RainbowState.grow;
	    }

	    if (CurrentRainbowState == RainbowState.grow)
	    {
            tempRainbow.transform.localScale += new Vector3(0,rainbowGrowSpeed,0);
	        if (tempRainbow.transform.localScale.y > 35.5f)
	        {
	            CurrentRainbowState = RainbowState.savour;
	        }
	    }

	    if (CurrentRainbowState == RainbowState.savour)
	    {
	        
	        rainbowTimer -= Time.deltaTime;
	        if (rainbowTimer <= 0)
	        {
	            if (Mathf.Max(bluesCoins, redsCoins, yellowsCoins) == bluesCoins)
	            {
	                bluePlayer.GetComponentInChildren<CharacterOneGunController>().stateOfWeapon =
	                    CharacterOneGunController.currentWeapon.RainbowWeapon;
					oneGunController.imgOriginal.enabled = false;
					oneGunController.imgRainbow.enabled = true;
					oneGunController.imgSMG.enabled = false;
					oneGunController.imgSniper.enabled = false;
					oneGunController.imgTrishot.enabled = false;
	            }else
	            if (Mathf.Max(bluesCoins, redsCoins, yellowsCoins) == redsCoins)
	            {
	                redPlayer.GetComponentInChildren<CharacterTwoGunController>().stateOfWeapon =
	                    CharacterOneGunController.currentWeapon.RainbowWeapon;
						twoGunController.imgOriginal.enabled = false;
						twoGunController.imgRainbow.enabled = true;
						twoGunController.imgSMG.enabled = false;
						twoGunController.imgSniper.enabled = false;
						twoGunController.imgTrishot.enabled = false;
	            }else
	            if (Mathf.Max(bluesCoins, redsCoins, yellowsCoins) == yellowsCoins)
	            {
	                yellowPlayer.GetComponentInChildren<CharacterThreeGunController>().stateOfWeapon =
	                    CharacterOneGunController.currentWeapon.RainbowWeapon;
							threeGunController.imgOriginal.enabled = false;
							threeGunController.imgRainbow.enabled = true;
							threeGunController.imgSMG.enabled = false;
							threeGunController.imgSniper.enabled = false;
							threeGunController.imgTrishot.enabled = false;
	            }
                CurrentRainbowState = RainbowState.stop;
	        }
	    }

	    if (CurrentRainbowState == RainbowState.stop)
	    {
			bluesCoins = 0;
			redsCoins = 0;
			yellowsCoins = 0;
            Destroy(tempRainbow.gameObject);
	        rainbowTimer = 1.4f;
	        CurrentRainbowState = RainbowState.preStart;
	    }
	}

    public void SpawnRainbow()
    {
        CurrentRainbowState = RainbowState.spawn;
    }
}
