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

	private bool brushHasBeenSet;
	private bool hasPaintBeenPainted = false;
    private float lifeTimer = 5f;
	private float distanceBetweenProjectorAndPlayer;
	public bool isPlayerOnSplat = true;
    

    // Use this for initialization
    void Start () {
		singlePlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<SingleplayerCharacterController> ();
		enemyManagerScript = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<EnemyManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (hasPaintBeenPainted==false)
	    {
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
			singlePlayer.projectorsList.Remove (gameObject);
			singlePlayer.RefreshPaint ();
            Destroy(gameObject);
	    }
		distanceBetweenProjectorAndPlayer = Vector3.Distance (transform.position, singlePlayer.gameObject.transform.position);
		if(distanceBetweenProjectorAndPlayer<=3.5f){
			isPlayerOnSplat = true;
		}
		if(distanceBetweenProjectorAndPlayer>3.5f){
			isPlayerOnSplat = false;
		}
		foreach(GameObject enemy in enemyManagerScript.enemyList){
			float distanceBetweenThisEnemyAndProjector = Vector3.Distance (transform.position, enemy.gameObject.transform.position);
		    if (distanceBetweenThisEnemyAndProjector<=3.5f)
		    {
		        PaintDetectionScript thisEnemiesPaintDetectionScript = enemy.GetComponent<PaintDetectionScript>();

                thisEnemiesPaintDetectionScript.isEnemyOnPaint = true;
		        if (projectorsBrush.Color==Color.red)
		        {
		            thisEnemiesPaintDetectionScript.rawColourOfPaint = "red";
		        } else
		        if (projectorsBrush.Color == Color.yellow)
		        {
		            thisEnemiesPaintDetectionScript.rawColourOfPaint = "yellow";
		        }
		        else
		        if (projectorsBrush.Color == new Color(1,0.75f,0,1))
		        {
		            thisEnemiesPaintDetectionScript.rawColourOfPaint = "orange";
		        }
		        else
		        if (projectorsBrush.Color == Color.green)
		        {
		            thisEnemiesPaintDetectionScript.rawColourOfPaint = "green";
		        }
		        else
		        if (projectorsBrush.Color == Color.blue)
		        {
		            thisEnemiesPaintDetectionScript.rawColourOfPaint = "blue";
		        }
		        else
		        if (projectorsBrush.Color == new Color(0.6f,0,1,1))
		        {
		            thisEnemiesPaintDetectionScript.rawColourOfPaint = "purple";
		        }

            }
		}
    }

    public void PaintStart(RaycastHit hit,InkCanvas hitCanvas,Brush brush)
    {
		if(brushHasBeenSet==false){
			Debug.Log ("ProjectorBrush Changing");
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
