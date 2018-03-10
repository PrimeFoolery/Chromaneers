using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> projectorsList = new List<GameObject>();

    private ColourSelectManager gameManager;
    public bool isGameSinglePlayer;

    private GameObject singlePlayer;

    private GameObject coopBluePlayer;
    private GameObject coopRedPlayer;
    private GameObject coopYellowPlayer;

    // Use this for initialization
    void Start ()
	{
	    gameManager = gameObject.GetComponent<ColourSelectManager>();
	    if (gameManager.isItSingleplayer==true)
	    {
	        isGameSinglePlayer = true;
	        singlePlayer = GameObject.FindGameObjectWithTag("Player");
	    }
	    else
	    {
	        isGameSinglePlayer = false;
            coopBluePlayer = GameObject.FindGameObjectWithTag("BluePlayer");
	        coopRedPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
	        coopYellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer");
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (isGameSinglePlayer == true) 
	    {
	        for (int i = projectorsList.Count - 1; i > 0; i--)
	        {
	            paintProjectorController currentProjectorScript = projectorsList[i].GetComponent<paintProjectorController>();
	            if (currentProjectorScript.isPlayerOnSplat == true)
	            {
	                if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().redColor)
	                {
	                    singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "red";
	                }
	                else if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().orangeColor)
	                {
	                    singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "orange";
	                }
	                else if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().yellowColor)
	                {
	                    singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "yellow";
	                }
	                else if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().greenColor)
	                {
	                    singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "green";
	                }
	                else if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().blueColor)
	                {
	                    singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "blue";
	                }
	                else if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().purpleColor)
	                {
	                    singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "purple";
	                }
	                break;
	            }
	            else
	            {
	                singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "null";
	            }

	        }

	        foreach (GameObject enemy in enemyList)
	        {
	            for (int i = projectorsList.Count-1; i > 0; i--)
	            {
	                paintProjectorController currentProjectorScript = projectorsList[i].GetComponent<paintProjectorController>();
                    if (enemy.GetComponent<PaintDetectionScript>().isEnemyOnPaint==true)
	                {
	                    if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().redColor)
	                    {
	                        enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "red";
	                    }else
	                    if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().orangeColor)
	                    {
	                        enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "orange";
	                    }
	                    else
	                    if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().yellowColor)
	                    {
	                        enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "yellow";
	                    }
	                    else
	                    if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().greenColor)
	                    {
	                        enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "green";
	                    }
	                    else
	                    if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().blueColor)
	                    {
	                        enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "blue";
	                    }
	                    else
	                    if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().purpleColor)
	                    {
	                        enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "purple";
	                    }

	                    break;
	                }
                    else
                    {
                        enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "null";
                    }
	            }
	        }
        } else if (isGameSinglePlayer==false)
	    {
            for (int i = projectorsList.Count - 1; i > 0; i--)
            {
                paintProjectorController currentProjectorScript = projectorsList[i].GetComponent<paintProjectorController>();
                if (currentProjectorScript.isBluePlayerOnSplat == true)
                {
                    if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().redColor)
                    {
                        coopBluePlayer.GetComponent<CoopCharacterControllerOne>().colourPlayerIsStandingOn = "red";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().orangeColor)
                    {
                        coopBluePlayer.GetComponent<CoopCharacterControllerOne>().colourPlayerIsStandingOn = "orange";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().yellowColor)
                    {
                        coopBluePlayer.GetComponent<CoopCharacterControllerOne>().colourPlayerIsStandingOn = "yellow";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().greenColor)
                    {
                        coopBluePlayer.GetComponent<CoopCharacterControllerOne>().colourPlayerIsStandingOn = "green";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().blueColor)
                    {
                        coopBluePlayer.GetComponent<CoopCharacterControllerOne>().colourPlayerIsStandingOn = "blue";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().purpleColor)
                    {
                        coopBluePlayer.GetComponent<CoopCharacterControllerOne>().colourPlayerIsStandingOn = "purple";
                    }
                    break;
                }
                else
                {
                    coopBluePlayer.GetComponent<CoopCharacterControllerOne>().colourPlayerIsStandingOn = "null";
                }

            }
            for (int i = projectorsList.Count - 1; i > 0; i--)
            {
                paintProjectorController currentProjectorScript = projectorsList[i].GetComponent<paintProjectorController>();
                if (currentProjectorScript.isRedPlayerOnSplat == true)
                {
                    if (currentProjectorScript.projectorsBrush.Color == coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().redColor)
                    {
                        coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().colourPlayerIsStandingOn = "red";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().orangeColor)
                    {
                        coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().colourPlayerIsStandingOn = "orange";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().yellowColor)
                    {
                        coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().colourPlayerIsStandingOn = "yellow";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().greenColor)
                    {
                        coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().colourPlayerIsStandingOn = "green";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().blueColor)
                    {
                        coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().colourPlayerIsStandingOn = "blue";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().purpleColor)
                    {
                        coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().colourPlayerIsStandingOn = "purple";
                    }
                    break;
                }
                else
                {
                    coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().colourPlayerIsStandingOn = "null";
                }

            }
            for (int i = projectorsList.Count - 1; i > 0; i--)
            {
                paintProjectorController currentProjectorScript = projectorsList[i].GetComponent<paintProjectorController>();
                if (currentProjectorScript.isYellowPlayerOnSplat == true)
                {
                    if (currentProjectorScript.projectorsBrush.Color == coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().redColor)
                    {
                        coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().colourPlayerIsStandingOn = "red";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().orangeColor)
                    {
                        coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().colourPlayerIsStandingOn = "orange";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().yellowColor)
                    {
                        coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().colourPlayerIsStandingOn = "yellow";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().greenColor)
                    {
                        coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().colourPlayerIsStandingOn = "green";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().blueColor)
                    {
                        coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().colourPlayerIsStandingOn = "blue";
                    }
                    else if (currentProjectorScript.projectorsBrush.Color == coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().purpleColor)
                    {
                        coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().colourPlayerIsStandingOn = "purple";
                    }
                    break;
                }
                else
                {
                    coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().colourPlayerIsStandingOn = "null";
                }

            }
            foreach (GameObject enemy in enemyList)
            {
                for (int i = projectorsList.Count - 1; i > 0; i--)
                {
                    paintProjectorController currentProjectorScript = projectorsList[i].GetComponent<paintProjectorController>();
                    if (enemy.GetComponent<PaintDetectionScript>().isEnemyOnPaint == true)
                    {
                        if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().redColor)
                        {
                            enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "red";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().orangeColor)
                        {
                            enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "orange";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().yellowColor)
                        {
                            enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "yellow";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().greenColor)
                        {
                            enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "green";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().blueColor)
                        {
                            enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "blue";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().purpleColor)
                        {
                            enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "purple";
                        }

                        break;
                    }
                    else
                    {
                        Debug.Log("is enemy on paint:  "+ enemy.GetComponent<PaintDetectionScript>().isEnemyOnPaint);
                        enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "null";
                    }
                }
            }
        }
	}
    public void RefreshPaint()
    {
        foreach (GameObject projector in projectorsList)
        {
            projector.GetComponent<paintProjectorController>().Repaint();
        }
    }

    public void MakeAllNull()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "null";
        }

        if (isGameSinglePlayer==true)
        {
            singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "null";
        }else if (isGameSinglePlayer==false)
        {
            coopBluePlayer.GetComponent<CoopCharacterControllerOne>().colourPlayerIsStandingOn = "null";
            coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().colourPlayerIsStandingOn = "null";
            coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().colourPlayerIsStandingOn = "null";
        }
    }
}
