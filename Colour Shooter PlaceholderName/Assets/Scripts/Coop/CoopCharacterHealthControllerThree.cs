using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoopCharacterHealthControllerThree : MonoBehaviour
{

    [Header("General")]
    public string PlayerState;

    [Header("Health Variables")]
    public int health;
    public bool canBeDamaged;
    public float InvTimer;
    private float reviveTimer = 15f;
    public float maxRevive = 15;

    [Header("HealthBar")]
    public Image HealthBarUI;
    public Sprite[] HeartSprites;
    public Slider ReviveSlider;

    [Header("Materials")]
    public Material matOne;
    public Material matTwo;
    public Material deadMat;
    public float duration;
    public Renderer rend;

    [Header("UI")]
    public Text currentHP;

    public CoopCharacterControllerThree coopCharacterControllerThree;

    //Private variables
    private int currentHealth;

    void Start()
    {
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
 //       Vector2 SliderPos = Camera.main.WorldToScreenPoint(this.transform.position);
 //       ReviveSlider.transform.position = SliderPos;
        //HealthBarUI.sprite = HeartSprites[currentHealth];

        if (PlayerState == "Alive")
        {
            Debug.Log(Vector3.Distance(gameObject.transform.position,
                GameObject.FindGameObjectWithTag("RedPlayer").transform.position));
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
            ReviveSlider.value = CalculateRevive();
            coopCharacterControllerThree.moveSpeed = 0;
            coopCharacterControllerThree.canPlayerShoot = false;
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
            coopCharacterControllerThree.canPlayerShoot = true;
            currentHealth = 6;
            rend.material = matOne;
            PlayerState = "Alive";
            reviveTimer = maxRevive;

        }
    }

    float CalculateRevive()
    {
        return reviveTimer / maxRevive;
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
}
