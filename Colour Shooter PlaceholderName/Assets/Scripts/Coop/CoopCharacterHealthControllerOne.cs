using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoopCharacterHealthControllerOne : MonoBehaviour
{

    [Header("General")]
    public string PlayerState;

    [Header("Health Variables")]
    public int health;
    public bool canBeDamaged;
    public float InvTimer;
    private float reviveTimer;
    public float maxRevive = 15;

    [Header("HealthBar")]
    public Image HealthBarUI;
    public Sprite[] HeartSprites;
    public Slider ReviveSlider;
    public Image ReviveCircle;


    [Header("Materials")]
    public Material matOne;
    public Material matTwo;
    public Material deadMat;
    public float duration;
    public Renderer rend;

    [Header("UI")]
    public Text currentHP;

    public CoopCharacterControllerOne coopCharacterControllerOne;

    //Private variables
    private int currentHealth;

    void Start()
    {
        ReviveSlider.gameObject.SetActive(false);
        ReviveCircle.gameObject.SetActive(false);
        reviveTimer = maxRevive;
        ReviveSlider.value = CalculateRevive();
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
        SliderPos.x =SliderPos.x- 960f;
        SliderPos.y = SliderPos.y - 540f;
        //ReviveSlider.transform.localPosition = SliderPos;
        ReviveCircle.transform.localPosition = SliderPos;
        HealthBarUI.sprite = HeartSprites[currentHealth];

        if (PlayerState == "Alive")
        {
            ReviveSlider.gameObject.SetActive(false);
            ReviveCircle.gameObject.SetActive(false);

            if (canBeDamaged == false)
            {
                print("Getting HIT");

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
            coopCharacterControllerOne.moveSpeed = 0;
            coopCharacterControllerOne.canPlayerShoot = false;
            reviveTimer -= Time.deltaTime;
            if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("RedPlayer").transform.position) < 2f)
            {
                reviveTimer -= Time.deltaTime;
            }
            if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("YellowPlayer").transform.position) < 2f)
            {
                reviveTimer -= Time.deltaTime;
            }
        }
        if (reviveTimer <= 0)
        {
            coopCharacterControllerOne.canPlayerShoot = true;
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
        if (currentHealth<=3)
        {
            currentHealth += 2;
        }else if (currentHealth>3&&currentHealth<=6)
        {
            currentHealth = 6;
        }
        
    }
    
}
