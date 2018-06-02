using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passwordPuzzleController : MonoBehaviour
{

    public List<GameObject> passwordCubes = new List<GameObject>();
    public List<GameObject> extraObjectsToDestroy = new List<GameObject>();
    public int correctCubes = 0;

    public bool isPasswordCorrectlyInputted = false;
    public GameObject keyPrefab;

    public GameObject keySpawnPoint;

    public GameObject door;
    public bool hasKeySpawned = false;

    public GameObject dustExplosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    correctCubes = 0;
	    if (isPasswordCorrectlyInputted==false)
	    {
	        foreach (GameObject passwordCube in passwordCubes)
	        {
	            if (passwordCube.GetComponent<passwordEntry>().isCorrectColourSelected == true)
	            {
	                correctCubes += 1;
	            }
	        }
        }
	    

	    if (correctCubes == passwordCubes.Count)
	    {
	        isPasswordCorrectlyInputted = true;
	    }
        if(isPasswordCorrectlyInputted==true)
        {
            if (hasKeySpawned==false)
            {
                door.GetComponent<doorController>().OpenSesame();
                foreach (GameObject passwordCube in passwordCubes)
                {
                    Destroy(passwordCube.gameObject);
                }
                foreach (GameObject objectToDestroy in extraObjectsToDestroy)
                {
                    Destroy(objectToDestroy.gameObject);
                }

                gameObject.GetComponent<BoxCollider>().enabled = false;
                //Instantiate(dustExplosion, transform.position, Quaternion.identity);
                //Instantiate(keyPrefab, keySpawnPoint.transform.position, Quaternion.identity);

                //hasKeySpawned = true;
            }
        }
	}
}
