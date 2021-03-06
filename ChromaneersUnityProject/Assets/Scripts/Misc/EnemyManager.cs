﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> projectorsList = new List<GameObject>();
    public GameObject paintProjector;

    private GameObject fillerGameObject;

    private ColourSelectManager gameManager;
    public bool isGameSinglePlayer;

    public GameObject singlePlayer;

    public GameObject coopBluePlayer;
    public GameObject coopRedPlayer;
    public GameObject coopYellowPlayer;

    // Use this for initialization
    void Start ()
    {
        fillerGameObject = Instantiate(paintProjector, new Vector3(0, -100, 0),Quaternion.identity);
        fillerGameObject.name = "fillerProjector";
        projectorsList.Add(fillerGameObject);
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
	        if (projectorsList.Count==1)
	        {
	            singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "null";
            }
	        for (int i = projectorsList.Count-1; i > 0; i--)
	        {
	            paintProjectorController currentProjectorScript = projectorsList[i].GetComponent<paintProjectorController>();
	            if (currentProjectorScript.isPlayerOnSplat == true)
	            {
	                if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().redColor)
	                {
                        //Debug.Log("Change colour to red");
	                    singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "red";
	                }
	                else if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().orangeColor)
	                {
	                    singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "orange";
	                }
	                else if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().yellowColor)
	                {
                        //Debug.Log("Change colour to yellow");
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
                    //Debug.Log("not on any projectors");
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

	        for (int enemy = 0;  enemy < enemyList.Count;  enemy++)
	        {
	            for (int i = projectorsList.Count-1; i > 0 ; i--)
	            {
	                paintProjectorController currentProjectorScript = projectorsList[i].GetComponent<paintProjectorController>();
	                if (currentProjectorScript.enemyOnPaintList[enemy]==true)
	                {
                        if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().redColor)
                        {
                            enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "red";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().orangeColor)
                        {
                            enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "orange";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().yellowColor)
                        {
                            enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "yellow";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().greenColor)
                        {
                            enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "green";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().blueColor)
                        {
                            enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "blue";
                        }
                        else
                        if (currentProjectorScript.projectorsBrush.Color == singlePlayer.GetComponent<SingleplayerCharacterController>().purpleColor)
                        {
                            enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "purple";
                        }

                        break;
                    }
	                else
	                {
	                    enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "null";
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
					Debug.Log ("yellow on a splat");
                    if (currentProjectorScript.projectorsBrush.Color == coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().redColor)
                    {
						Debug.Log ("Yellow On Red");
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
			for (int enemy = 0;  enemy < enemyList.Count;  enemy++)
			{
				for (int i = projectorsList.Count-1; i > 0 ; i--)
				{
					paintProjectorController currentProjectorScript = projectorsList[i].GetComponent<paintProjectorController>();
					if (currentProjectorScript.enemyOnPaintList[enemy]==true)
					{
						if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().redColor)
						{
							enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "red";
						}
						else
							if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().orangeColor)
							{
								enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "orange";
							}
							else
								if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().yellowColor)
								{
									enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "yellow";
								}
								else
									if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().greenColor)
									{
										enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "green";
									}
									else
										if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().blueColor)
										{
											enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "blue";
										}
										else
											if (currentProjectorScript.projectorsBrush.Color == coopBluePlayer.GetComponent<CoopCharacterControllerOne>().purpleColor)
											{
												enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "purple";
											}

						break;
					}
					else
					{
						enemyList[enemy].GetComponent<PaintDetectionScript>().colourOfPaint = "null";
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
        
    }

    public void AddExtraBoolToProjectorsScript()
    {
        for (int i = 0; i<projectorsList.Count;i++)
        {
            projectorsList[i].GetComponent<paintProjectorController>().enemyOnPaintList.Add(false);
        }
    }
}
