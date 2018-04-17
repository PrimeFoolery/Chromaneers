using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoopCharacterHealthControllerTwo : MonoBehaviour {

    [Header("General")]
    public string PlayerState;

    [Header("Health Variables")]
    public int health;
    public bool canBeDamaged;
    public float InvTimer;

    [Header("HealthBar")]
    public Image HealthBarUI;
    public Sprite[] HeartSprites;

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

    void Start () {
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
	
    void Update () {
        HealthBarUI.sprite = HeartSprites[currentHealth];

        if (PlayerState == "Alive")
        {
            if (canBeDamaged == false)
            {
                print("Getting HIT");

                InvTimer -= Time.deltaTime;
                rend.material.Lerp(matOne, matTwo, 2f);
                if (InvTimer <= 0)
                {
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
            coopCharacterControllerTwo.moveSpeed = 0;
        }
    }

    public void GetHit()
    {
        if (canBeDamaged == true)
        {
            currentHealth -= 1;
            InvTimer = 2;
            canBeDamaged = false;
        }
    }
}