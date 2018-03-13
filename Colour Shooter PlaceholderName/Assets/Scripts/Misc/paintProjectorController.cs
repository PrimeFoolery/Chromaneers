using System.Collections;
using System.Collections.Generic;
using Es.InkPainter;
using UnityEngine;

public class paintProjectorController : MonoBehaviour
{
    public Brush projectorsBrush;
    private RaycastHit projectorHit;
    private InkCanvas projectorTargetInkCanvas;
	private SingleplayerCharacterController singlePlayer;
	private EnemyManager enemyManagerScript;
	public List<bool> enemyOnPaintList = new List<bool> ();
    private bool isGameSinglePlayer;

    private CoopCharacterControllerOne blueCoopController;
    private CoopCharacterControllerTwo redCoopController;
    private CoopCharacterControllerThree yellowCoopController;

    private bool brushHasBeenSet;
	private bool hasPaintBeenPainted = false;
    private float lifeTimer = 5f;
	private float distanceBetweenProjectorAndPlayer;
    private float distanceBetweenProjectorAndBluePlayer;
    private float distanceBetweenProjectorAndRedPlayer;
    private float distanceBetweenProjectorAndYellowPlayer;
    public bool isPlayerOnSplat = true;
    public bool isBluePlayerOnSplat = true;
    public bool isRedPlayerOnSplat = true;
    public bool isYellowPlayerOnSplat = true;


    // Use this for initialization
    void Start () {
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        isGameSinglePlayer = enemyManagerScript.isGameSinglePlayer;
        if (isGameSinglePlayer==true)
        {
            singlePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SingleplayerCharacterController>();
        }
        else
        {
            blueCoopController = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<CoopCharacterControllerOne>();
            redCoopController = GameObject.FindGameObjectWithTag("RedPlayer").GetComponent<CoopCharacterControllerTwo>();
            yellowCoopController = GameObject.FindGameObjectWithTag("YellowPlayer").GetComponent<CoopCharacterControllerThree>();
        }
		AssignBoolListToObjectList ();
		
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (hasPaintBeenPainted==false)
	    {
		    //Debug.Log("paint should be getting painted");
	        projectorTargetInkCanvas.Paint(projectorsBrush, projectorHit);
	        hasPaintBeenPainted = true;
	    }

	    if (hasPaintBeenPainted==true)
	    {
	        lifeTimer -= Time.deltaTime;
	    }

	    if (lifeTimer<=0f)
	    {
	        projectorsBrush.Scale = projectorsBrush.Scale + 0.01f;
	        projectorTargetInkCanvas.Erase(projectorsBrush, projectorHit);
	        projectorsBrush.Scale = 0.068f;
			enemyManagerScript.projectorsList.Remove (gameObject);
			enemyManagerScript.RefreshPaint ();
	        //enemyManagerScript.MakeAllNull();
	        if (isGameSinglePlayer==true)
	        {
	            if (distanceBetweenProjectorAndPlayer<=3.75f)
	            {
	                enemyManagerScript.singlePlayer.GetComponent<SingleplayerCharacterController>().colourPlayerIsStandingOn = "null";
	            }

	            foreach (GameObject enemy in enemyManagerScript.enemyList)
	            {
	                float distanceBetweenThisEnemyAndProjector = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
                    if (distanceBetweenThisEnemyAndProjector<=3.75f)
                    {
                        enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "null";
                    }
	            }
            }
	        else
	        {
	            if (distanceBetweenProjectorAndRedPlayer <= 3.75f)
	            {
	                enemyManagerScript.coopRedPlayer.GetComponent<CoopCharacterControllerTwo>().colourPlayerIsStandingOn = "null";
	            }
	            if (distanceBetweenProjectorAndYellowPlayer <= 3.75f)
	            {
	                enemyManagerScript.coopYellowPlayer.GetComponent<CoopCharacterControllerThree>().colourPlayerIsStandingOn = "null";
	            }
	            if (distanceBetweenProjectorAndBluePlayer <= 3.75f)
	            {
	                enemyManagerScript.coopBluePlayer.GetComponent<CoopCharacterControllerOne>().colourPlayerIsStandingOn = "null";
	            }
                foreach (GameObject enemy in enemyManagerScript.enemyList)
	            {
	                float distanceBetweenThisEnemyAndProjector = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
	                if (distanceBetweenThisEnemyAndProjector <= 3.75f)
	                {
	                    enemy.GetComponent<PaintDetectionScript>().colourOfPaint = "null";
	                }
	            }
            }
            Destroy(gameObject);
        }

	    if (isGameSinglePlayer==true)
	    {
	        distanceBetweenProjectorAndPlayer = Vector3.Distance(transform.position, singlePlayer.gameObject.transform.position);
	        if (distanceBetweenProjectorAndPlayer <= 3.75f)
	        {
	            isPlayerOnSplat = true;
	        }
	        if (distanceBetweenProjectorAndPlayer > 3.75f)
	        {
	            isPlayerOnSplat = false;
	        }
        } else if (isGameSinglePlayer==false)
	    {
	        distanceBetweenProjectorAndBluePlayer =Vector3.Distance(transform.position, blueCoopController.gameObject.transform.position);
	        if (distanceBetweenProjectorAndBluePlayer <= 3.75f)
	        {
	            isBluePlayerOnSplat = true;
	        }
	        if (distanceBetweenProjectorAndBluePlayer > 3.75f)
	        {
	            isBluePlayerOnSplat = false;
	        }
	        distanceBetweenProjectorAndRedPlayer = Vector3.Distance(transform.position, redCoopController.gameObject.transform.position);
	        if (distanceBetweenProjectorAndRedPlayer <= 3.75f)
	        {
	            isRedPlayerOnSplat = true;
	        }
	        if (distanceBetweenProjectorAndRedPlayer > 3.75f)
	        {
	            isRedPlayerOnSplat = false;
	        }
	        distanceBetweenProjectorAndYellowPlayer = Vector3.Distance(transform.position, yellowCoopController.gameObject.transform.position);
	        if (distanceBetweenProjectorAndYellowPlayer <= 3.75f)
	        {
	            isYellowPlayerOnSplat = true;
	        }
	        if (distanceBetweenProjectorAndYellowPlayer > 3.75f)
	        {
	            isYellowPlayerOnSplat = false;
	        }
        }
		for(int i = 0; i<enemyManagerScript.enemyList.Count ;i++){
			float distanceBetweenThisEnemyAndProjector = Vector3.Distance (transform.position, enemyManagerScript.enemyList[i].gameObject.transform.position);
			if (distanceBetweenThisEnemyAndProjector<=6.5f)
			{
				enemyOnPaintList[i] = true;
			}
			if (distanceBetweenThisEnemyAndProjector>6.5f)
			{
				enemyOnPaintList[i] = false;
			}
		}
		
    }
	private void AssignBoolListToObjectList(){
		for(int i = 0;i<enemyManagerScript.enemyList.Count;i++){
			enemyOnPaintList.Add (false);
		}
	}
    public void PaintStart(RaycastHit hit,InkCanvas hitCanvas,Brush brush)
    {
		if(brushHasBeenSet==false){
			//Debug.Log ("ProjectorBrush Changing");
			projectorsBrush.Color = brush.Color;
			projectorsBrush.Scale = brush.Scale;
			projectorsBrush.BrushTexture = brush.BrushTexture;
			projectorHit = hit;
			projectorTargetInkCanvas = hitCanvas;
			brushHasBeenSet = true;
		}
       
    }
	public void Repaint(){
		hasPaintBeenPainted = false;
	}
   
}
