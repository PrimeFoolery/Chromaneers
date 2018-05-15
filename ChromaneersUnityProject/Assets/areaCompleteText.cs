using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class areaCompleteText : MonoBehaviour {

  

    public TextMeshProUGUI textField;

    public TextMeshProUGUI blueCoinCounter;
    public TextMeshProUGUI yellowCoinCounter;
    public TextMeshProUGUI redCoinCounter;


    public string textToType;

    enum CoinStates
    {
        na, 
        start,
        shrink,
        countingUp,
        fade,
        complete
    }

    enum TextStates
    {
        na,
        start,
        shrink,
        savour,
        fade,
        complete
    }

    private CoinStates thisCoinState = CoinStates.na;
    private TextStates thisTriggersState = TextStates.na;
    private int amountOfPlayersInArea;
    public float textGrowSpeed;
    private float savourTime = 4f;
    private Vector3 velocity;

    private int blueCoinsCollected;
    private int redCoinsCollected;
    private int yellowCoinsCollected;

    private int blueCoinsCounter;
    private int yellowCoinsCounter;
    private int redCoinsCounter;

    private float coinCountDelay = 0.005f;

    public coinController coinManager;

    // Use this for initialization
    void Start()
    {
        coinManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<coinController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfPlayersInArea==3 && thisTriggersState == TextStates.na)
        {
            thisTriggersState = TextStates.start;
            blueCoinsCollected = coinManager.bluesCoins;
            redCoinsCollected = coinManager.redsCoins;
            yellowCoinsCollected = coinManager.yellowsCoins;
            textField.text = textToType;
        }

        if (thisTriggersState == TextStates.start)
        {

            textField.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity, textGrowSpeed*Time.deltaTime);
            if (textField.gameObject.GetComponent<RectTransform>().localScale.x > 1.2f)
            {
                thisTriggersState = TextStates.shrink;
            }
        }

        if (thisTriggersState == TextStates.shrink)
        {
            textField.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.05f, 1.05f, 1.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            if (textField.gameObject.GetComponent<RectTransform>().localScale.x<=1.1f)
            {
                thisCoinState = CoinStates.start;
                blueCoinCounter.text = "X    ";
                redCoinCounter.text = "X    ";
                yellowCoinCounter.text = "X    ";
                thisTriggersState = TextStates.savour;
            }
        }

        if (thisTriggersState == TextStates.savour)
        {
            savourTime -= Time.deltaTime;
            if (savourTime<0)
            {
                thisTriggersState = TextStates.fade;
            }
        }

        if (thisTriggersState== TextStates.fade)
        {
            textField.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            if (textField.gameObject.GetComponent<RectTransform>().localScale.x <= 0f)
            {
                textField.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
                thisTriggersState = TextStates.complete;
            }
        }

        if (thisCoinState == CoinStates.start)
        {
            blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            redCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                redCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            yellowCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                yellowCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            if (blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale.x > 1.2f)
            {
                thisCoinState = CoinStates.shrink;
            }
        }
        if (thisCoinState == CoinStates.shrink)
        {
            blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.05f, 1.05f, 1.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            redCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                redCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.05f, 1.05f, 1.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            yellowCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                yellowCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.05f, 1.05f, 1.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            if (blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale.x <= 1.1f)
            {
                thisCoinState = CoinStates.countingUp;
            }
        }

        if (thisCoinState == CoinStates.countingUp)
        {
            if (coinCountDelay<=0)
            {
                if (blueCoinsCounter < blueCoinsCollected)
                {
                    blueCoinsCounter += 1;
                }

                blueCoinCounter.text = "X " + blueCoinsCounter.ToString();
                if (redCoinsCounter < redCoinsCollected)
                {
                    redCoinsCounter += 1;
                }

                redCoinCounter.text = "X " + redCoinsCounter.ToString();
                if (yellowCoinsCounter < yellowCoinsCollected)
                {
                    yellowCoinsCounter += 1;
                }
                yellowCoinCounter.text = "X " + yellowCoinsCounter.ToString();

                coinCountDelay = 0.005f;
            }

            coinCountDelay -= Time.deltaTime;
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("something in trigger");
        if ((other.CompareTag("BluePlayer") || other.CompareTag("RedPlayer") || other.CompareTag("YellowPlayer")))
        {
            amountOfPlayersInArea += 1;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("BluePlayer") || other.CompareTag("RedPlayer") || other.CompareTag("YellowPlayer")))
        {
            amountOfPlayersInArea -= 1;
        }
    }
}
