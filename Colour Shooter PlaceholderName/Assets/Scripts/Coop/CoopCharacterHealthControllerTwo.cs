using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoopCharacterHealthControllerTwo : MonoBehaviour
{

    [Header("General")]
    public string PlayerState;

    [Header("Health Variables")]
    public int health;
    public bool canBeDamaged;
    public float InvTimer;
    private float reviveTimer = 15f;
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

    [Header("UI")]
    public Text currentHP;

    public CoopCharacterControllerTwo coopCharacterControllerTwo;

    //Private variables
    private int currentHealth;


    private float IndicatorTimer = 3f;
    private float IndicatorAlpha;
    private bool IndicatorFadedOut = false;

    void Start()
    {
        //ReviveSlider.gameObject.SetActive(false);
        ReviveCircle.gameObject.SetActive(false);
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
        SliderPos.x = SliderPos.x - 960f;
        SliderPos.y = SliderPos.y - 540f;
        //ReviveSlider.transform.localPosition = SliderPos;
        ReviveCircle.transform.localPosition = SliderPos;
        HealthBarUI.sprite = HeartSprites[currentHealth];

        Vector2 indPos = Camera.main.WorldToScreenPoint((this.transform.position));
        indPos.x = indPos.x - 960f;
        indPos.y = indPos.y - 410;
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
            ReviveCircle.gameObject.SetActive(true);
            ReviveCircle.fillAmount = CalculateRevive();
            coopCharacterControllerTwo.moveSpeed = 0;
            coopCharacterControllerTwo.canPlayerShoot = false;
            coopCharacterControllerTwo.canPlayerMove = false;
            coopCharacterControllerTwo.coopCharacterControllerTwo.isFiring = false;
            
            reviveTimer -= Time.deltaTime;
			ReviveParticle.SetActive (true);
            if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) < 2f)
            {
                reviveTimer -= Time.deltaTime;
            }
            if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("YellowPlayer").transform.position) < 2f)
            {
                reviveTimer -= Time.deltaTime;
            }
            if(Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("YellowPlayer").transform.position) < 4f && Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) > 4f)
            {
                reviveCircleScript.peopleInCircle = 2;
            }else if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("YellowPlayer").transform.position) > 4f && Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) < 4f)
            {
                reviveCircleScript.peopleInCircle = 2;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("YellowPlayer").transform.position) < 4f && Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) < 4f)
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
            coopCharacterControllerTwo.canPlayerShoot = true;
            coopCharacterControllerTwo.canPlayerMove = true;
            currentHealth = 3;
            rend.material = matOne;
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
                canBeDamaged = false;
            }
        }
    }
    public void Die()
    {
        currentHealth = 0;
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