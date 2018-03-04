using System.Collections;
using System.Collections.Generic;
using Es.InkPainter;
using UnityEngine;

public class paintProjectorController : MonoBehaviour
{
    private Brush projectorsBrush;
    private RaycastHit projectorHit;
    private InkCanvas projectorTargetInkCanvas;

    private bool hasPaintBeenPainted = false;
    private float lifeTimer = 5f;
    

    // Use this for initialization
    void Start () {
		
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
            Destroy(gameObject);
	    }
    }

    public void PaintStart(RaycastHit hit,InkCanvas hitCanvas,Brush brush)
    {
        projectorsBrush = brush;
        projectorHit = hit;
        projectorTargetInkCanvas = hitCanvas;
    }
   
}
