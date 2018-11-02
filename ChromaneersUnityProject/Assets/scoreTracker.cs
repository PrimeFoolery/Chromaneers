using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scoreTracker : MonoBehaviour
{

    public int RedCandiesCollected;
    public int YellowCandiesCollected;
    public int BlueCandiesCollected;
    public int totalCandiesCollected;

    public int stageIn;

    public int Area1Seconds;
    public int Area2Seconds;
    public int Area3Seconds;
    public int Area4Seconds;
    public int totalTime;

    public bool gem1Collected;
    public bool gem2Collected;
    public bool gem3Collected;

    void Start()
    {
        if (PlayerPrefs.GetInt("Gem1Got")==1)
        {
            gem1Collected = true;
        }
        if (PlayerPrefs.GetInt("Gem2Got") == 1)
        {
            gem2Collected = true;
        }
        if (PlayerPrefs.GetInt("Gem3Got") == 1)
        {
            gem3Collected = true;
        }
        if (PlayerPrefs.GetInt("Area1HighScore")==0)
        {
            PlayerPrefs.SetInt("Area1HighScore", 100000000);
        }
        if (PlayerPrefs.GetInt("Area2HighScore") ==0)
        {
            PlayerPrefs.SetInt("Area2HighScore", 100000000);
        }
        if (PlayerPrefs.GetInt("Area3HighScore") == 0)
        {
            PlayerPrefs.SetInt("Area3HighScore", 100000000);
        }
        if (PlayerPrefs.GetInt("Area4HighScore") == 0)
        {
            PlayerPrefs.SetInt("Area4HighScore", 100000000);
        }
        if (PlayerPrefs.GetInt("TimeHighScore") == 0)
        {
            PlayerPrefs.SetInt("TimeHighScore", 100000000);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name=="MenuTest")
        {
            Debug.Log("on menu");
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("deleting all progress");
                DeleteAllProgress();
            }
        }
        if (stageIn==1)
        {
            Area1Seconds += (int) Time.deltaTime;
        }else if (stageIn==2)
        {
            Area2Seconds += (int)Time.deltaTime;
        }
        else if (stageIn==3)
        {
            Area3Seconds += (int)Time.deltaTime;
        }
        else if (stageIn==4)
        {
            Area4Seconds += (int)Time.deltaTime;
        }

        totalTime = Area1Seconds + Area2Seconds + Area3Seconds + Area4Seconds;
        totalCandiesCollected = BlueCandiesCollected + RedCandiesCollected + YellowCandiesCollected;
    }

    public void UpdateArea1(int BlueCandy, int redCandy, int yellowCandy)
    {
        BlueCandiesCollected = BlueCandiesCollected + BlueCandy;
        RedCandiesCollected = RedCandiesCollected + redCandy;
        YellowCandiesCollected = YellowCandiesCollected + yellowCandy;
    }
    public void UpdateArea2(int BlueCandy, int redCandy, int yellowCandy)
    {
        BlueCandiesCollected = BlueCandiesCollected + BlueCandy;
        RedCandiesCollected = RedCandiesCollected + redCandy;
        YellowCandiesCollected = YellowCandiesCollected + yellowCandy;
    }
    public void UpdateArea3(int BlueCandy, int redCandy, int yellowCandy)
    {
        BlueCandiesCollected = BlueCandiesCollected + BlueCandy;
        RedCandiesCollected = RedCandiesCollected + redCandy;
        YellowCandiesCollected = YellowCandiesCollected + yellowCandy;
    }
    public void UpdateArea4(int BlueCandy, int redCandy, int yellowCandy)
    {
        BlueCandiesCollected = BlueCandiesCollected + BlueCandy;
        RedCandiesCollected = RedCandiesCollected + redCandy;
        YellowCandiesCollected = YellowCandiesCollected + yellowCandy;
    }

    public void gem1Got()
    {
        gem1Collected = true;
    }
    public void gem2Got()
    {
        gem2Collected = true;
    }
    public void gem3Got()
    {
        gem3Collected = true;
    }
    public void DeleteTempProgress()
    {
        RedCandiesCollected = 0;
        BlueCandiesCollected = 0;
        YellowCandiesCollected = 0;
        totalCandiesCollected = 0;
        stageIn = 0;
        Area1Seconds = 0;
        Area2Seconds = 0;
        Area3Seconds = 0;
        Area4Seconds = 0;
        totalTime = 0;
    }
    public void DeleteAllProgress()
    {
        PlayerPrefs.SetInt("Area1HighScore", 100000000);
        PlayerPrefs.SetInt("Area2HighScore", 100000000);
        PlayerPrefs.SetInt("Area3HighScore", 100000000);
        PlayerPrefs.SetInt("Area4HighScore", 100000000);
        PlayerPrefs.SetInt("TimeHighScore", 100000000);
        RedCandiesCollected = 0;
        BlueCandiesCollected = 0;
        YellowCandiesCollected = 0;
        totalCandiesCollected = 0;
        gem1Collected = false;
        gem2Collected = false;
        gem3Collected = false;
        stageIn = 0;
        Area1Seconds = 0;
        Area2Seconds = 0;
        Area3Seconds = 0;
        Area4Seconds = 0;
        totalTime = 0;
        PlayerPrefs.SetInt("Gem1Got",0);
        PlayerPrefs.SetInt("Gem2Got", 0);
        PlayerPrefs.SetInt("Gem3Got", 0);
        PlayerPrefs.SetInt("CandyHighScore",0);
    }
}
