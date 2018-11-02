using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endScoreScreen : MonoBehaviour
{

    public bool endGameStarted = false;

    public GameObject fade;
    public GameObject endScreen;
    public GameObject objectToGrow;
    public TextMeshProUGUI redCandy;
    public TextMeshProUGUI blueCandy;
    public TextMeshProUGUI yellowCandy;
    public TextMeshProUGUI totalCandy;

    public TextMeshProUGUI area1Time;
    public TextMeshProUGUI area2Time;
    public TextMeshProUGUI area3Time;
    public TextMeshProUGUI area4Time;
    public TextMeshProUGUI totalTime;

    public Image gem1;
    public Image gem2;
    public Image gem3;

    public GameObject ContinueText;

    private int Area4time;
    private scoreTracker scoreTrackerVar;

    public int countingA1Time = 0;
    public int countingA2Time = 0;
    public int countingA3Time = 0;
    public int countingA4Time = 0;
    private int countingTotalTime = 0;

    private int countingRedCandy = 0;
    private int countingBlueCandy = 0;
    private int countingYellowCandy = 0;
    private int countingTotalCandy = 0;

    public enum endScreenStage
    {
        fade,
        grow,
        countingTime,
        countingCandy,
        countingGems,
        savour,
        load
    }

    public endScreenStage currentStage = endScreenStage.fade;

    private float currentAlpha = 0;
    private float currentScaling = 0;

    private Color goldColor = new Color(1f,0.8f,0f,1f);

    

	// Use this for initialization
	void Start ()
	{
	    scoreTrackerVar = GameObject.FindGameObjectWithTag("Settings").GetComponent<scoreTracker>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (endGameStarted==true)
	    {
            if(endScreen.activeSelf==false)
            {
                Area4time = scoreTrackerVar.Area4Seconds;
	            endScreen.SetActive(true);
            }
	        
            if (currentStage == endScreenStage.fade)
	        {
	            currentAlpha += 0.4f * Time.deltaTime;
	            fade.GetComponent<Image>().color = new Color(0, 0, 0, currentAlpha);
	            if (currentAlpha>=1)
	            {
	                currentStage = endScreenStage.grow;
	            }
	        }else if (currentStage == endScreenStage.grow)
            {
                currentScaling += 0.4f * Time.deltaTime;
                objectToGrow.GetComponent<RectTransform>().localScale = new Vector3(currentScaling,currentScaling,1);
                if (currentScaling>=1)
                {
                    currentStage = endScreenStage.countingTime;
                }
            }else if (currentStage == endScreenStage.countingTime)
            {
                if (countingTotalTime<scoreTrackerVar.totalTime)
                {
                   
                        
                        if (countingA1Time <= scoreTrackerVar.Area1Seconds)
                        {
                            //Debug.Log(scoreTrackerVar.Area1Seconds);
                            if (countingA1Time<=(scoreTrackerVar.Area1Seconds/2))
                            {
                                countingA1Time += 6;
                            }else if (countingA1Time<=(scoreTrackerVar.Area1Seconds - 50))
                            {
                                countingA1Time += 3;
                            }
                            else
                            {
                                countingA1Time += 1;
                            }
                            
                        }
                        if (countingA2Time <= scoreTrackerVar.Area2Seconds)
                        {
                            if (countingA2Time <= (scoreTrackerVar.Area2Seconds / 2))
                            {
                                countingA2Time += 6;
                            }
                            else if (countingA2Time <= (scoreTrackerVar.Area2Seconds - 50))
                            {
                                countingA2Time += 3;
                            }
                            else
                            {
                                countingA2Time += 1;
                            }
                        }
                        if (countingA3Time <= scoreTrackerVar.Area3Seconds)
                        {
                            if (countingA3Time <= (scoreTrackerVar.Area3Seconds / 2))
                            {
                                countingA3Time += 6;
                            }
                            else if (countingA3Time <= (scoreTrackerVar.Area3Seconds - 50))
                            {
                                countingA3Time += 3;
                            }
                            else
                            {
                                countingA3Time += 1;
                            }
                        }
                        if (countingA4Time <= scoreTrackerVar.Area4Seconds)
                        {
                            if (countingA4Time <= (scoreTrackerVar.Area4Seconds / 2))
                            {
                                countingA4Time += 4;
                            }
                            else if (countingA4Time <= (scoreTrackerVar.Area4Seconds - 50))
                            {
                                countingA4Time += 2;
                            }
                            else
                            {
                                countingA4Time += 1;
                            }
                        }

                        countingTotalTime = countingA1Time + countingA2Time + countingA3Time + countingA4Time;
                    
                    
                    Debug.Log(countingA1Time);
                    TimeSpan A1TimeSpan = TimeSpan.FromSeconds(countingA1Time);
                    TimeSpan A2TimeSpan = TimeSpan.FromSeconds(countingA2Time);
                    TimeSpan A3TimeSpan = TimeSpan.FromSeconds(countingA3Time);
                    TimeSpan A4TimeSpan = TimeSpan.FromSeconds(countingA4Time);
                    TimeSpan TotalTimeSpan = TimeSpan.FromSeconds(countingTotalTime);


                    area1Time.text = A1TimeSpan.Minutes.ToString()+":"+A1TimeSpan.Seconds.ToString();
                    area2Time.text = A2TimeSpan.Minutes.ToString() + ":" + A2TimeSpan.Seconds.ToString();
                    area3Time.text = A3TimeSpan.Minutes.ToString() + ":" + A3TimeSpan.Seconds.ToString();
                    area4Time.text = A4TimeSpan.Minutes.ToString() + ":" + A4TimeSpan.Seconds.ToString();
                    totalTime.text = TotalTimeSpan.Minutes.ToString() + ":" + TotalTimeSpan.Seconds.ToString();
                }
                else
                {
                    if (countingA1Time < PlayerPrefs.GetInt("Area1HighScore"))
                    {
                        area1Time.color = goldColor;
                        PlayerPrefs.SetInt("Area1HighScore", countingA1Time);
                        
                    }
                    if (countingA2Time < PlayerPrefs.GetInt("Area2HighScore"))
                    {
                        area2Time.color = goldColor;
                        PlayerPrefs.SetInt("Area2HighScore", countingA2Time);

                    }
                    if (countingA3Time < PlayerPrefs.GetInt("Area3HighScore"))
                    {
                        area3Time.color = goldColor;
                        PlayerPrefs.SetInt("Area3HighScore", countingA3Time);

                    }
                    if (countingA4Time < PlayerPrefs.GetInt("Area4HighScore"))
                    {
                        area4Time.color = goldColor;
                        PlayerPrefs.SetInt("Area4HighScore", countingA4Time);

                    }
                    if (countingTotalTime < PlayerPrefs.GetInt("TimeHighScore"))
                    {
                        totalTime.color = goldColor;
                        PlayerPrefs.SetInt("TimeHighScore", countingTotalTime);

                    }
                    currentStage = endScreenStage.countingCandy;
                }
            }else if (currentStage==endScreenStage.countingCandy)
            {
                if (countingTotalCandy<scoreTrackerVar.totalCandiesCollected)
                {
                    if(countingBlueCandy<=scoreTrackerVar.BlueCandiesCollected)
                    {
                        if (countingBlueCandy <= (scoreTrackerVar.BlueCandiesCollected / 2))
                        {
                            countingBlueCandy += 6;
                        }
                        else if (countingBlueCandy <= (scoreTrackerVar.BlueCandiesCollected - 50))
                        {
                            countingBlueCandy += 3;
                        }
                        else
                        {
                            countingBlueCandy += 1;
                        }
                    }
                    if (countingRedCandy <= scoreTrackerVar.RedCandiesCollected)
                    {
                        if (countingRedCandy <= (scoreTrackerVar.RedCandiesCollected / 2))
                        {
                            countingRedCandy += 6;
                        }
                        else if (countingRedCandy <= (scoreTrackerVar.RedCandiesCollected-50))
                        {
                            countingRedCandy += 3;
                        }
                        else
                        {
                            countingRedCandy += 1;
                        }
                    }
                    if (countingYellowCandy <= scoreTrackerVar.YellowCandiesCollected)
                    {
                        if (countingYellowCandy <= (scoreTrackerVar.YellowCandiesCollected / 2))
                        {
                            countingYellowCandy += 6;
                        }
                        else if (countingYellowCandy <= (scoreTrackerVar.YellowCandiesCollected-50) )
                        {
                            countingYellowCandy += 3;
                        }
                        else
                        {
                            countingYellowCandy += 1;
                        }
                    }

                    countingTotalCandy = countingBlueCandy + countingRedCandy + countingYellowCandy;

                    redCandy.text = countingRedCandy.ToString();
                    blueCandy.text = countingBlueCandy.ToString();
                    yellowCandy.text = countingYellowCandy.ToString();
                    totalCandy.text = countingTotalCandy.ToString();

                }
                else
                {
                    if (countingTotalCandy > PlayerPrefs.GetInt("CandyHighScore"))
                    {
                        totalCandy.color = goldColor;
                        PlayerPrefs.SetInt("CandyHighScore", countingA1Time);

                    }

                    currentStage = endScreenStage.countingGems;
                }
            }else if (currentStage == endScreenStage.countingGems)
            {
                if (scoreTrackerVar.gem1Collected==true)
                {
                    gem1.enabled = true;
                    PlayerPrefs.SetInt("Gem1Got", 1);
                }
                if (scoreTrackerVar.gem2Collected == true)
                {
                    gem2.enabled = true;
                    PlayerPrefs.SetInt("Gem2Got", 1);
                }
                if (scoreTrackerVar.gem3Collected == true)
                {
                    gem3.enabled = true;
                    PlayerPrefs.SetInt("Gem3Got", 1);
                }

                currentStage = endScreenStage.savour;
            }
            else if (currentStage == endScreenStage.savour)
            {
                scoreTrackerVar.DeleteTempProgress();
                ContinueText.SetActive(true);
                if (Input.GetButtonDown("Submit")||Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.Joystick3Button0) || Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.Joystick3Button1)||Input.GetKeyDown(KeyCode.Return))
                {
                    currentStage = endScreenStage.load;
                }
            }else if (currentStage == endScreenStage.load)
            {
                SceneManager.LoadScene("MenuTest");
            }
	    }
	}

    public void Trigger()
    {
        endGameStarted = true;
        
    }
}
