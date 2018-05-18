using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class CoopCharacterHealthControllerThree : MonoBehaviour
{

    [Header("General")]
    public string PlayerState;

    [Header("Health Variables")]
    public int health;
    public bool canBeDamaged;
    public float InvTimer;
    public float reviveTimer = 15f;
    public float maxRevive = 15;
	public GameObject ReviveParticle;

    [Header("HealthBar")]
    public Image HealthBarUI;
    public Sprite[] HeartSprites;
    public Slider ReviveSlider;
    public Image ReviveCircle;
    public reviveCircleRotation reviveCircleScript;
    public GameObject Indicator;

    [Header("Materials")]
    public Material matOne;
    public Material matTwo;
    public Material deadMat;
    public float duration;
    public Renderer rend;


    private float IndicatorTimer = 3f;
    private float IndicatorAlpha;
    private bool IndicatorFadedOut = false;

    [Header("UI")]
    public Text currentHP;

    [Header("Controller Vibration")]
    public float vibrationRightOn;
    public float vibrationLeftOn;
    public float vibrationRightOff;
    public float vibrationLeftOff;
    public float vibrationTimer;

    public CoopCharacterControllerThree coopCharacterControllerThree;

    public GameObject Coin;
    public coinController coinManager;
    private bool HasCoinSpawned = false;

    //Private variables
    private int currentHealth;

    void Start()
    {
        //ReviveSlider.gameObject.SetActive(false);
        ReviveCircle.gameObject.SetActive(false);
        coinManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<coinController>();
        reviveTimer = maxRevive;        
        //ReviveSlider.value = CalculateRevive();
        PlayerState = "Alive";
        canBeDamaged = true;
        //Setting the current health to be the health variable
        //so that when we start the game, the enemy has full HP
        currentHealth = health;

        //Getting the renderer
        //And setting the main material to its origin material
        rend = GetComponent<Renderer>();
        rend.material = matOne;
    }

    void Update()
    {
        Vector2 SliderPos = Camera.main.WorldToScreenPoint((this.transform.position));
        SliderPos.x = SliderPos.x - (Screen.width / 2);
        SliderPos.y = SliderPos.y - (Screen.height / 2);
        //ReviveSlider.transform.localPosition = SliderPos;
        ReviveCircle.transform.localPosition = SliderPos;
        HealthBarUI.sprite = HeartSprites[currentHealth];
        Vector2 indPos = Camera.main.WorldToScreenPoint((this.transform.position));
        indPos.x = indPos.x - (Screen.width / 2);
        indPos.y = indPos.y - (Screen.height * 0.37963f);
        Indicator.transform.localPosition = indPos;
        if (IndicatorFadedOut == false)
        {
            IndicatorTimer -= Time.deltaTime;
            if (IndicatorTimer < 0)
            {
                IndicatorAlpha = Indicator.GetComponent<Image>().color.a;
                IndicatorAlpha = Mathf.Lerp(IndicatorAlpha, 0, Time.deltaTime);
                Indicator.GetComponent<Image>().color = new Color(1, 1, 1, IndicatorAlpha);
                if (Indicator.GetComponent<Image>().color.a < 0.05f)
                {
                    Indicator.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    IndicatorFadedOut = true;
                }
            }
        }

        vibrationTimer -= Time.deltaTime;
        if (vibrationTimer <= 0) {
            GamePad.SetVibration(PlayerIndex.One, vibrationLeftOff, vibrationRightOff);
        }

        if (PlayerState == "Alive")
        {
			ReviveParticle.SetActive (false);
            //ReviveSlider.gameObject.SetActive(false);
            ReviveCircle.gameObject.SetActive(false);

            if (canBeDamaged == false)
            {
                //print("Getting HIT");

                InvTimer -= Time.deltaTime;
                rend.material.Lerp(matOne, matTwo, 2f);
                if (InvTimer <= 0)
                {
                    vibrationTimer = 0.35f;
                    rend.material = matOne;
                    canBeDamaged = true;
                }
            }
            if (currentHealth <= 0)
            {
                PlayerState = "Dead";
            }
        }
        if (PlayerState == "Dead")
        {
            //ReviveSlider.gameObject.SetActive(true);
            //ReviveSlider.value = CalculateRevive();
            if (HasCoinSpawned == false)
            {
                Vector2 tempVector;
                if (coinManager.yellowsCoins > 4)
                {
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);

                    coinManager.yellowsCoins -= 5;
                    //Debug.Log("Spawn the damn coins game plz");
                    HasCoinSpawned = true;
                }
                else if (coinManager.yellowsCoins == 4)
                {
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    coinManager.yellowsCoins -= 4;
                    HasCoinSpawned = true;
                }
                else if (coinManager.yellowsCoins == 3)
                {
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    coinManager.yellowsCoins -= 3;
                    HasCoinSpawned = true;
                }
                else if (coinManager.yellowsCoins == 2)
                {
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    coinManager.yellowsCoins -= 2;
                    HasCoinSpawned = true;
                }
                else if (coinManager.yellowsCoins == 1)
                {
                    tempVector = (UnityEngine.Random.insideUnitCircle.normalized) * 3f;
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3(tempVector.x, 0, tempVector.y)), transform.rotation);
                    coinManager.yellowsCoins -= 1;
                    HasCoinSpawned = true;
                }
            }
            ReviveCircle.gameObject.SetActive(true);
            ReviveCircle.fillAmount = CalculateRevive();
            coopCharacterControllerThree.CanPlayerMove = false;
            //coopCharacterControllerThree.moveSpeed = 0;
            coopCharacterControllerThree.canPlayerShoot = false;
            coopCharacterControllerThree.coopCharacterControllerThree.isFiring = false;
            reviveTimer -= Time.deltaTime/3;
			ReviveParticle.SetActive (true);
            if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("RedPlayer").transform.position) < 4f)
            {

                reviveTimer -= Time.deltaTime*2.25f;
            }
            if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) < 4f)
            {
                reviveTimer -= Time.deltaTime*2.25f;
            }
            if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) < 4f && Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("RedPlayer").transform.position) > 4f)
            {
                reviveCircleScript.peopleInCircle = 2;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) > 4f && Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("RedPlayer").transform.position) < 4f)
            {
                reviveCircleScript.peopleInCircle = 2;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) < 4f && Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("RedPlayer").transform.position) < 4f)
            {
                reviveCircleScript.peopleInCircle = 3;
            }
            else
            {
                reviveCircleScript.peopleInCircle = 1;
            }
        }
        if (reviveTimer <= 0)
        {
            coopCharacterControllerThree.canPlayerShoot = true;
            coopCharacterControllerThree.CanPlayerMove = true;
            currentHealth = 3;
            HasCoinSpawned = false;
            rend.material = matOne;
            InvTimer = 2;
            canBeDamaged = false;
            PlayerState = "Alive";
            reviveTimer = maxRevive;

        }
    }

    float CalculateRevive()
    {
        return (1-reviveTimer / maxRevive);
    }

    public void GetHit()
    {
        if (PlayerState == "Alive")
        {
            if (canBeDamaged == true)
            {
                currentHealth -= 1;
                InvTimer = 2;
                vibrationTimer = 0.35f;
                GamePad.SetVibration(PlayerIndex.One, vibrationLeftOn, vibrationRightOn);
                canBeDamaged = false;
            }
        }
    }

    public void Die()
    {
        currentHealth = 0;
        vibrationTimer = 0.35f;
        GamePad.SetVibration(PlayerIndex.Three, vibrationLeftOn, vibrationRightOn);
    }
    
    public void GetHeart()
    {
        if (currentHealth <= 3)
        {
            currentHealth += 2;
        }
        else if (currentHealth > 3 && currentHealth <= 6)
        {
            currentHealth = 6;
        }

    }
	public void ChangeToMatOne(){
		rend.material = matOne;
	}
}
