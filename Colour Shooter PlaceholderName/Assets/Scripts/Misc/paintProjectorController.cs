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

	private bool brushHasBeenSet;
	private bool hasPaintBeenPainted = false;
    private float lifeTimer = 5f;
	private float distanceBetweenProjectorAndPlayer;
	public bool isPlayerOnSplat = true;
    

    // Use this for initialization
    void Start () {
		singlePlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<SingleplayerCharacterController> ();
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
