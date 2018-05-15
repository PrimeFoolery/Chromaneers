using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class areaCompleteText : MonoBehaviour {

  

    public TextMeshProUGUI textField;
    public string textToType;
    
    

    enum TextStates
    {
        na,
        start,
        shrink,
        savour,
        fade,
        complete
    }

    private TextStates thisTriggersState = TextStates.na;
    private int amountOfPlayersInArea;
    public float textGrowSpeed;
    private float savourTime = 4f;
    private Vector3 velocity;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfPlayersInArea==3 && thisTriggersState == TextStates.na)
        {
            thisTriggersState = TextStates.start;
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
