using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTriggerScript : MonoBehaviour {

    private int talkLine = 1;
    private bool nextLine = true;
    private float Delay = 0f;
    private bool timeToType = false;
    private bool textTyped = false;
	bool stopDeletingText = false;

    public TextMeshProUGUI textField;
    public string textToType;
    public int lettersToType;
    public Image textBackdrop;
    private float textFadeout = 3f;
    private bool fadeTextIn = false;
    public float fadeTime = 2f;
    private bool hasTextFaded = false;
	private bool hasTextHappened = false;
    private bool stop = false;

    private Color lerpedColour = new Color(0,0,0,0);
    private Color startingBoxColour = new Color(0,0,0,0);
    private Color endBoxColour = new Color(0, 0, 0, 0.5f);

    private IEnumerator currentCoroutine;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (textTyped==true)
	    {
	        textFadeout -= Time.deltaTime;
	    }
        //Debug.Log(textFadeout);
		//Debug.Log (hasTextFaded);
	    if (textFadeout<=0&&hasTextFaded==false)
	    {
            //Debug.Log("fading out");
	        lerpedColour = Color.Lerp(lerpedColour, startingBoxColour, (Time.deltaTime * fadeTime));

			if(stopDeletingText==false){
				textField.text = "";
				stopDeletingText = true;
			}
	        textBackdrop.GetComponent<Image>().color = lerpedColour;
	        if (lerpedColour.a<0.05f)
	        {

	            hasTextFaded = true;

	        }
	    }

	    if (hasTextFaded == true&&stop==false)
	    {

            textBackdrop.GetComponent<Image>().color = new Color(lerpedColour.r, lerpedColour.g, lerpedColour.b, 0);
	        stop = true;
	    }
		if (fadeTextIn==true&&hasTextHappened==false)
	    {
            //Debug.Log("fade in");
	        lerpedColour = Color.Lerp(lerpedColour, endBoxColour, (Time.deltaTime * fadeTime));
	        textBackdrop.GetComponent<Image>().color = lerpedColour;
	        if (lerpedColour.a>0.45f)
	        {
	            fadeTextIn = false;
				hasTextHappened = true;
	        }
        }

	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("something in trigger");
		if ((other.CompareTag("BluePlayer")||other.CompareTag("RedPlayer")||other.CompareTag("YellowPlayer"))&&timeToType==false)
        {
			//Debug.Log ("STOP HAPPENING: "+ timeToType);
			textField.text = "";
            StartText();
            fadeTextIn = true;
            timeToType = true;
        }
        
    }

    void StartText()
    {
        //Debug.Log("player in trigger");

        currentCoroutine = TypeText(textToType);
        StartCoroutine(currentCoroutine);
    }
    IEnumerator TypeText(string inputText)
    {
        //Debug.Log("function happening");
        if (textTyped==false)
        {
            //Debug.Log("function 2 happening");
            foreach (char letter in inputText.ToCharArray())
            {
                //Debug.Log("for loop happening");
                lettersToType -= 1;
                textField.text += letter;
                yield return new WaitForSeconds(Delay);
            }
        }
            
        if (lettersToType <= 0)
        {
            textTyped = true;
        }

    }
}
