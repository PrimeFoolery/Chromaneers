using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePropScript : MonoBehaviour
{
    public enum DestructiblePropType
    {
        dust,
        clay,
        wood,
        glass
    }

    public DestructiblePropType thisPropsMaterialType;

    public GameObject dustExplosion;
    public GameObject clayExplosion;
    public GameObject woodExplosion;
    public GameObject glassExplosion;

    public GameObject Candy;
    public GameObject Heart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("somethingColliding");
        if (other.gameObject.CompareTag("BlueBullet")|| other.gameObject.CompareTag("RedBullet")|| other.gameObject.CompareTag("YellowBullet"))
        {
            //Debug.Log("bulletColliding");
            if (thisPropsMaterialType==DestructiblePropType.dust)
            {
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
            }else if (thisPropsMaterialType==DestructiblePropType.clay)
            {
                Instantiate(clayExplosion, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), Quaternion.identity);
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
            }
            else if (thisPropsMaterialType == DestructiblePropType.glass)
            {
                Instantiate(glassExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
            }
            else if (thisPropsMaterialType == DestructiblePropType.wood)
            {
                Instantiate(woodExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                Instantiate(dustExplosion, transform.position, Quaternion.identity);
            }

            DestroyDestructible();
        }

        if (other.gameObject.CompareTag("BluePlayer"))
        {
            if (other.gameObject.GetComponent<CoopCharacterControllerOne>().currentlyDodging == true)
            {
                if (thisPropsMaterialType == DestructiblePropType.dust)
                {
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.clay)
                {
                    Instantiate(clayExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.glass)
                {
                    Instantiate(glassExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.wood)
                {
                    Instantiate(woodExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                DestroyDestructible();
            }
        }
        if (other.gameObject.CompareTag("RedPlayer"))
        {
            if (other.gameObject.GetComponent<CoopCharacterControllerTwo>().currentlyDodging == true)
            {
                if (thisPropsMaterialType == DestructiblePropType.dust)
                {
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.clay)
                {
                    Instantiate(clayExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.glass)
                {
                    Instantiate(glassExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.wood)
                {
                    Instantiate(woodExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                DestroyDestructible();
            }
        }
        if (other.gameObject.CompareTag("YellowPlayer"))
        {
            if (other.gameObject.GetComponent<CoopCharacterControllerThree>().currentlyDodging == true)
            {
                if (thisPropsMaterialType == DestructiblePropType.dust)
                {
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.clay)
                {
                    Instantiate(clayExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.glass)
                {
                    Instantiate(glassExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                else if (thisPropsMaterialType == DestructiblePropType.wood)
                {
                    Instantiate(woodExplosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Instantiate(dustExplosion, transform.position, Quaternion.identity);
                }
                DestroyDestructible();
            }
        }
    }

    void DestroyDestructible()
    {
        int randomNumber = Random.Range(0, 100);
        if (randomNumber>0 && randomNumber<=20)
        {
            //Spawn standard enemy

        }else if (randomNumber>20 && randomNumber<=40)
        {
            //Spawn fast enemy

        }
        else if (randomNumber>40&&randomNumber<=60)
        {
            //Spawn candy
            Instantiate(Candy, transform.position, Quaternion.identity);
        }
        else if (randomNumber>60 && randomNumber <= 65 )
        {
            //Spawn Heart
            Instantiate(Heart, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
