using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleObelisk : MonoBehaviour
{

    public Renderer SphereRenderer;
    public GameObject obeliskCircle;
    public GameObject obeliskHolder;
    public GameObject triggerCube;
    public enum ColoursOfObelisk
    {
        blue,
        red,
        yellow
    }

    public ColoursOfObelisk colourOfThisObelisk;

    public Texture2D penObeliskEmissionMap0;
    public Texture2D penObeliskEmissionMap1;
    public Texture2D penObeliskEmissionMap2;
    public Texture2D penObeliskEmissionMap3;
    public Texture2D penObeliskEmissionMap4;
    public Texture2D penObeliskEmissionMap5;
    public Texture2D penObeliskEmissionMap6;
    public Texture2D penObeliskEmissionMap7;
    public Texture2D penObeliskEmissionMap8;
    public Texture2D penObeliskEmissionMap9;
    public Texture2D penObeliskEmissionMap10;

    public Material blueDomeMaterial;
    public Material redDomeMaterial;
    public Material yellowDomeMaterial;

    public Material triggerCubePurple;
    public Material triggerCubeOrange;
    public Material triggerCubeGreen;

    public enum ObeliskState
    {
        flat,
        activatable,
        growing,
        idle,
        draining,
        done
    }

    public ObeliskState CurrentObeliskState = ObeliskState.flat;
    public float obeliskGrowSpeed;
    public float triggerCubeGrowSpeed;

    public float drainTimer = 4f;

    public GameObject boss;

    public GameObject obeliskTip;
    private bool lineSpawned;
    public GameObject linePulser;
    private GameObject tempLinePulser;

    private bool triggerCubesSpawned = false;

    // Use this for initialization
    void Start () {
	    if (colourOfThisObelisk == ColoursOfObelisk.blue)
	    {
	        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0,0.5f,1,1));
        }else if
	        (colourOfThisObelisk == ColoursOfObelisk.red)
	    {
	        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        }
	    else if
	        (colourOfThisObelisk == ColoursOfObelisk.yellow)
	    {
	        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
        }
        
        SphereRenderer.enabled = false;
        obeliskCircle.GetComponent<SpriteRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (CurrentObeliskState == ObeliskState.activatable)
	    {
	        if (triggerCubesSpawned == false)
	        {

                triggerCube.transform.localScale+= new Vector3(0, triggerCubeGrowSpeed* Time.deltaTime, 0);
	            if (triggerCube.transform.localScale.y>1)
	            {
                    triggerCube.GetComponentInChildren<BossObeliskTriggerCube>().SpawnColourBlindIndicator();
	                triggerCube.GetComponentInChildren<BossObeliskTriggerCube>().isTriggerable = true;
	                triggerCubesSpawned = true;
	            }
	        }
	    }
	    if (CurrentObeliskState == ObeliskState.growing)
	    {
	        obeliskHolder.transform.localScale += new Vector3(0, obeliskGrowSpeed * Time.deltaTime, 0);
	        if (obeliskHolder.transform.localScale.y > 1f)
	        {
	            CurrentObeliskState = ObeliskState.idle;
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap10);
                SphereRenderer.enabled = true;
	            obeliskCircle.GetComponent<SpriteRenderer>().enabled = true;
	        }
	    }

	    if (CurrentObeliskState == ObeliskState.draining)
	    {
	        drainTimer -= Time.deltaTime;
	        if (lineSpawned==false)
	        {
	            tempLinePulser = Instantiate(linePulser, obeliskTip.transform.position, Quaternion.identity);
	            tempLinePulser.GetComponent<linePulser>().targetGameObject = boss;
	            if (colourOfThisObelisk == ColoursOfObelisk.blue)
	            {
	                tempLinePulser.GetComponent<linePulser>().colourOfLine = "blue";
	            }else if (colourOfThisObelisk == ColoursOfObelisk.red)
	            {
	                tempLinePulser.GetComponent<linePulser>().colourOfLine = "red";
                }
	            else if (colourOfThisObelisk == ColoursOfObelisk.yellow)
	            {
	                tempLinePulser.GetComponent<linePulser>().colourOfLine = "red";
	            }

	            lineSpawned = true;
	        }
	        if (drainTimer >= 3.6f && drainTimer<4f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap9);
            }else if
	        (drainTimer >= 3.2f && drainTimer < 3.6f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap8);
	        }
	        else if
	            (drainTimer >= 2.8f && drainTimer < 3.2f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap7);
	        }
	        else if
	            (drainTimer >= 2.4f && drainTimer < 2.8f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap6);
	        }
	        else if
	            (drainTimer >= 2f && drainTimer < 2.4f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap5);
	        }
	        else if
	            (drainTimer >= 1.6f && drainTimer < 2f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap4);
	        }
	        else if
	            (drainTimer >= 1.2f && drainTimer < 1.6f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap3);
	        }
	        else if
	            (drainTimer >= 0.8f && drainTimer < 1.2f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap2);
	        }
	        else if
	            (drainTimer >= 0.4f && drainTimer < 0.8f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap1);
	        }
            if (drainTimer<=0.4 && drainTimer > 0f)
	        {
	            gameObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", penObeliskEmissionMap0);
	            
	        }

	        if (drainTimer<0)
	        {
	            CurrentObeliskState = ObeliskState.done;
            }
	    }

	    if (CurrentObeliskState == ObeliskState.done)
	    {
	        if (obeliskCircle!=null)
	        {
	            Destroy(SphereRenderer.gameObject);
	            Destroy(obeliskCircle);
            }
            
	    }
    }

    public void StartPhase2()
    {
        if (CurrentObeliskState == ObeliskState.flat)
        {
            if (colourOfThisObelisk == ColoursOfObelisk.blue)
            {
                triggerCube.GetComponentInChildren<BoxCollider>().enabled = true;
                triggerCube.GetComponentInChildren<Renderer>().material = triggerCubeOrange;
            }
            else if (colourOfThisObelisk == ColoursOfObelisk.red)
            {
                triggerCube.GetComponentInChildren<BoxCollider>().enabled = true;
                triggerCube.GetComponentInChildren<Renderer>().material = triggerCubeGreen;
            }
            else if (colourOfThisObelisk == ColoursOfObelisk.yellow)
            {
                triggerCube.GetComponentInChildren<BoxCollider>().enabled = true;
                triggerCube.GetComponentInChildren<Renderer>().material = triggerCubePurple;
            }
            CurrentObeliskState = ObeliskState.activatable;
           
        }
    }

    public void Grow()
    {
        if (CurrentObeliskState == ObeliskState.activatable)
        {
            Destroy(triggerCube);
            CurrentObeliskState = ObeliskState.growing;
        }
    }

    public void StartDraining()
    {
        if (CurrentObeliskState == ObeliskState.idle)
        {
            if (colourOfThisObelisk == ColoursOfObelisk.blue)
            {
                SphereRenderer.material = blueDomeMaterial;
            }else
            if (colourOfThisObelisk == ColoursOfObelisk.red)
            {
                SphereRenderer.material = redDomeMaterial;
            }
            else if (colourOfThisObelisk == ColoursOfObelisk.yellow)
            {
                SphereRenderer.material = yellowDomeMaterial;
            }
            CurrentObeliskState = ObeliskState.draining;
        }
    }
}
