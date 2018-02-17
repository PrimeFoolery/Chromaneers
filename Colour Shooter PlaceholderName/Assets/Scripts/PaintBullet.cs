using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBullet : MonoBehaviour {

    // Use this for initialization
    private float bulletSpeed = 2f;//SPEED OF BULLET

    //THESE ARE THE PAINT SPLAT TEXTURES WHERE ONE WOULD BE CHOSEN AT RANDOM TO BE PLACED ON SURFACE;
    public GameObject paintSplat1;

    public int randomisedPaintValue;//THIS IS THE VALUE THAT IS RANDOMISED TO CHOOSE WHICH PAINT TEXTURE TO USE
    public GameObject chosenPaintSplat;//THIS IS THE PAINT TEXTURE THAT WILL BE INSTANTIATED

    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0f, bulletSpeed, 0f));//MAKES BULLETS MOVE
	}
    private void OnCollisionEnter(Collision collision)
    {   
        
        if (collision.gameObject.tag == "Wall")//WHEN THE PLAYER HITS THE WALL
        {
            RandomisePaintTexture();//CHOOSES A RANDOM TEXTURE
            Instantiate(chosenPaintSplat, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), collision.transform.rotation, collision.gameObject.transform);//SPAWNS THE PAINT BLOB
            Destroy(this.gameObject);//DESTROYS THE BULLET
            
        }
       
    }
    private void RandomisePaintTexture()//RANDOMISES WHICH TEXTURE IS USED FOR THE PAINT
    {
        randomisedPaintValue = 1;
        if (randomisedPaintValue == 1)
        {
            chosenPaintSplat = paintSplat1;
        }
    }
}
