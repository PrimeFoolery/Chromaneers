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

    //VARIABLES FOR OFFSETTING THE PAINT SPRITE
    private float XDifferenceBetweenWallAndBullet;
    private float YDifferenceBetweenWallAndBullet;
    private float ZDifferenceBetweenWallAndBullet;
    private float XOffset;
    private float YOffset;
    private float ZOffset;

    private GameObject tempPaintSplatForOffsetting;


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
            CalculatePositionOffset(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
            RandomisePaintTexture();//CHOOSES A RANDOM TEXTURE
            tempPaintSplatForOffsetting = Instantiate(chosenPaintSplat, new Vector3(collision.gameObject.transform.position.x+XOffset, collision.gameObject.transform.position.y+YOffset, collision.gameObject.transform.position.z+ZOffset), collision.transform.rotation, collision.gameObject.transform);//SPAWNS THE PAINT BLOB
            Destroy(this.gameObject);//DESTROYS THE BULLET
            
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")//WHEN THE PLAYER HITS THE WALL
        {
            CalculatePositionOffset(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            RandomisePaintTexture();//CHOOSES A RANDOM TEXTURE
            tempPaintSplatForOffsetting = Instantiate(chosenPaintSplat, new Vector3(other.gameObject.transform.position.x + XOffset, other.gameObject.transform.position.y + YOffset, other.gameObject.transform.position.z + ZOffset), other.transform.rotation, other.gameObject.transform);//SPAWNS THE PAINT BLOB
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
    private void CalculatePositionOffset(float wallX, float wallY, float wallZ)
    {
        XDifferenceBetweenWallAndBullet = transform.position.x - wallX;
        YDifferenceBetweenWallAndBullet = transform.position.y - wallY;
        ZDifferenceBetweenWallAndBullet = transform.position.z - wallZ;
        XOffset = XDifferenceBetweenWallAndBullet ;
        YOffset = YDifferenceBetweenWallAndBullet ;
        ZOffset = ZDifferenceBetweenWallAndBullet ;
    }
}
