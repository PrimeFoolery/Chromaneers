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

    public enum CoinStates
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

    public CoinStates thisCoinState = CoinStates.na;
    private TextStates thisTriggersState = TextStates.na;
    private int amountOfPlayersInArea;
    public float textGrowSpeed;
    private float TextsavourTime = 4f;
    private float CoinSavourTime = 4f;
    private Vector3 velocity;
    private Vector3 velocity2;
    private Vector3 velocity3;
    private Vector3 velocity4;

    private int rainbowRotationSpeed = 20;

    private int blueCoinsCollected;
    private int redCoinsCollected;
    private int yellowCoinsCollected;

    private int blueCoinsCounter;
    private int yellowCoinsCounter;
    private int redCoinsCounter;

    public Image blueCandy;
    public Image redCandy;
    public Image yellowCandy;

    public Image blueRainbow;
    public Image redRainbow;
    public Image yellowRainbow;

    private float coinCountDelay = 0.005f;

    public coinController coinManager;
    private deathTracker deathTrackerScript;

    // Use this for initialization
    void Start()
    {
        coinManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<coinController>();
        deathTrackerScript = GameObject.FindGameObjectWithTag("DeathTracker").GetComponent<deathTracker>();
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
                textField.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.1f, 1.1f, 1.1f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            
            textField2.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField2.gameObject.GetComponent<RectTransform>().localScale, new Vector3(2.1f, 2.1f, 2.1f),
                ref velocity2, textGrowSpeed * Time.deltaTime * 0.5f);
             
            if (textField.gameObject.GetComponent<RectTransform>().localScale.x > 0.95f)
            {
                deathTrackerScript.howManyAreasComplete += 1;
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
                ref velocity2, textGrowSpeed * Time.deltaTime);
            
            if (textField.gameObject.GetComponent<RectTransform>().localScale.x<=0.85f)
            {
                thisCoinState = CoinStates.start;
                blueCoinCounter.text = "";
                redCoinCounter.text = "";
                yellowCoinCounter.text = "";
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
                textField.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity, textGrowSpeed * Time.deltaTime);
            textField2.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                textField2.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity, textGrowSpeed * Time.deltaTime*0.2f);
            
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
                ref velocity3, textGrowSpeed * Time.deltaTime);
            redCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                redCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity3, textGrowSpeed * Time.deltaTime);
            yellowCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                yellowCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(1.25f, 1.25f, 1.25f),
                ref velocity3, textGrowSpeed * Time.deltaTime);
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
                ref velocity3, textGrowSpeed * Time.deltaTime);
            redCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                redCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(0.95f, 0.95f, 0.95f),
                ref velocity3, textGrowSpeed * Time.deltaTime);
            yellowCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                yellowCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(0.95f, 0.95f, 0.95f),
                ref velocity3, textGrowSpeed * Time.deltaTime);
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
                    UnityEngine.Debug.Log("blueCoinsGoingUp");
                }
                blueCoinCounter.text = "x " + blueCoinsCounter.ToString();
                UnityEngine.Debug.Log("blueCoinsCounted");
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
                
                thisCoinState = CoinStates.savour;
            }

            
        }
        if (thisCoinState == CoinStates.savour)
        {
            if (Mathf.Max(blueCoinsCollected,redCoinsCollected,yellowCoinsCollected)== blueCoinsCollected)
            {
                blueRainbow.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                    blueRainbow.gameObject.GetComponent<RectTransform>().localScale, new Vector3(4.05f, 4.05f, 4.05f),ref velocity4,
                    textGrowSpeed * Time.deltaTime * 0.5f);
                blueRainbow.gameObject.GetComponent<RectTransform>().Rotate(0,0,rainbowRotationSpeed*Time.deltaTime);
            }else if (Mathf.Max(blueCoinsCollected, redCoinsCollected, yellowCoinsCollected) == redCoinsCollected)
            {
                redRainbow.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                    redRainbow.gameObject.GetComponent<RectTransform>().localScale, new Vector3(4.05f, 4.05f, 4.05f), ref velocity4,
                    textGrowSpeed * Time.deltaTime * 0.5f);
                redRainbow.gameObject.GetComponent<RectTransform>().Rotate(0, 0, rainbowRotationSpeed * Time.deltaTime);
            }
            else if (Mathf.Max(blueCoinsCollected, redCoinsCollected, yellowCoinsCollected) == yellowCoinsCollected)
            {
                yellowRainbow.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                    yellowRainbow.gameObject.GetComponent<RectTransform>().localScale, new Vector3(4.05f, 4.05f, 4.05f), ref velocity4,
                    textGrowSpeed * Time.deltaTime * 0.5f);
                yellowRainbow.gameObject.GetComponent<RectTransform>().Rotate(0, 0, rainbowRotationSpeed * Time.deltaTime);
            }
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
            blueCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                blueCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity3, textGrowSpeed * Time.deltaTime*0.8f);
            redCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                redCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity3, textGrowSpeed * Time.deltaTime * 0.8f);
            yellowCandy.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                yellowCandy.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f),
                ref velocity3, textGrowSpeed * Time.deltaTime*0.8f);
            if (Mathf.Max(blueCoinsCollected, redCoinsCollected, yellowCoinsCollected) == blueCoinsCollected)
            {
                blueRainbow.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                    blueRainbow.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f), ref velocity4,
                    textGrowSpeed * Time.deltaTime * 0.4f);
            }else
            if (Mathf.Max(blueCoinsCollected, redCoinsCollected, yellowCoinsCollected) == redCoinsCollected)
            {
                redRainbow.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                    redRainbow.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f), ref velocity4,
                    textGrowSpeed * Time.deltaTime * 0.4f);
            }else
            if (Mathf.Max(blueCoinsCollected, redCoinsCollected, yellowCoinsCollected) == yellowCoinsCollected)
            {
                yellowRainbow.gameObject.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(
                    yellowRainbow.gameObject.GetComponent<RectTransform>().localScale, new Vector3(-0.05f, -0.05f, -0.05f), ref velocity4,
                    textGrowSpeed * Time.deltaTime * 0.4f);
            }

            if (blueCoinCounter.gameObject.GetComponent<RectTransform>().localScale.x <= 0f)
            {
                coinManager.SpawnRainbow();
                blueCandy.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
                redCandy.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                yellowCandy.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                blueRainbow.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                redRainbow.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                yellowRainbow.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
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
