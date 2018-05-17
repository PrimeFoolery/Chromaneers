using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class areaCompleteText : MonoBehaviour {

  

    public TextMeshProUGUI textField;
    public TextMeshProUGUI textField2;

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
        savour,
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
    private float TextsavourTime = 4f;
    private float CoinSavourTime = 4f;
    private Vector3 velocity;

    private int blueCoinsCollected;
    private int redCoinsCollected;
    private int yellowCoinsCollected;

    private int blueCoinsCounter;
    private int yellowCoinsCounter;
    private int redCoinsCounter;

    public Image blueCandy;
    public Image redCandy;
    public Image yellowCandy;

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
            textField2.text = "Complete";
        }

        if (thisTriggersState == TextStates.start)
        {
            /*
            textField.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            textField2.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField2.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.95f, 1.95f, 1.95f),
                ref velocity, textGrowSpeed*Time.deltaTime * 1.4f);
            */

            textField.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1f, 1f, 1f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            
            textField2.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField2.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.95f, 1.95f, 1.95f),
                ref velocity, textGrowSpeed * Time.deltaTime * 1.5f);
                
            if (textField.gameObject.GetComponent<RectTransform>().localScale.x > 0.95f)
            {
                thisTriggersState = TextStates.shrink;
            }
        }

        if (thisTriggersState == TextStates.shrink)
        {
            
            textField.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField.gameObject.GetComponent<RectTransform>().localScale, new Vector3(0.8f, 0.8f, 0.8f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            
            textField2.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField2.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.7f, 1.7f, 1.7f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            
            if (textField.gameObject.GetComponent<RectTransform>().localScale.x<=0.85f)
            {
                thisCoinState = CoinStates.start;
                blueCoinCounter.text = "x ";
                redCoinCounter.text = "x ";
                yellowCoinCounter.text = "x ";
                thisTriggersState = TextStates.savour;
            }
        }

        if (thisTriggersState == TextStates.savour)
        {
            TextsavourTime -= Time.deltaTime;
            if (TextsavourTime<0)
            {
                thisTriggersState = TextStates.fade;
            }
        }

        if (thisTriggersState== TextStates.fade)
        {
            textField.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField2.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            /*textField2.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity, textGrowSpeed * Time.deltaTime*1.4f);
            */
            if (textField.gameObject.GetComponent<RectTransform>().localScale.x <= 0f)
            {
                textField.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
                thisTriggersState = TextStates.complete;
            }
        }

        if (thisCoinState == CoinStates.start)
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
            blueCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                blueCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            redCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                redCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            yellowCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                yellowCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            if (blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale.x > 1f)
            {
                thisCoinState = CoinStates.shrink;
            }
        }
        if (thisCoinState == CoinStates.shrink)
        {
            blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(0.55f, 0.55f, 0.55f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            redCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                redCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(0.55f, 0.55f, 0.55f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            yellowCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                yellowCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(0.55f, 0.55f, 0.55f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            blueCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                blueCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(0.95f, 0.95f, 0.95f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            redCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                redCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(0.95f, 0.95f, 0.95f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            yellowCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                yellowCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(0.95f, 0.95f, 0.95f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            if (blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale.x <= 0.6f)
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

                blueCoinCounter.text = "x " + blueCoinsCounter.ToString();
                if (redCoinsCounter < redCoinsCollected)
                {
                    redCoinsCounter += 1;
                }

                redCoinCounter.text = "x " + redCoinsCounter.ToString();
                if (yellowCoinsCounter < yellowCoinsCollected)
                {
                    yellowCoinsCounter += 1;
                }
                yellowCoinCounter.text = "x " + yellowCoinsCounter.ToString();

                coinCountDelay = 0.005f;
            }

            coinCountDelay -= Time.deltaTime;
            if (blueCoinsCounter == blueCoinsCollected && redCoinsCounter == redCoinsCollected && yellowCoinsCounter == yellowCoinsCollected)
            {
                coinManager.SpawnRainbow();
                thisCoinState = CoinStates.savour;
            }

            
        }
        if (thisCoinState == CoinStates.savour)
        {
            CoinSavourTime -= Time.deltaTime;
            if (CoinSavourTime < 0)
            {
                thisCoinState = CoinStates.fade;
            }
        }

        if (thisCoinState == CoinStates.fade)
        {
            blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            redCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                redCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            yellowCoinCounter.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                yellowCoinCounter.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            if (blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale.x <= 0f)
            {
                coinManager.bluesCoins = 0;
                coinManager.redsCoins = 0;
                coinManager.yellowsCoins = 0;
                blueCoinCounter.text = "";
                redCoinCounter.text = "";
                yellowCoinCounter.text = "";
                thisCoinState = CoinStates.complete;
            }
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
