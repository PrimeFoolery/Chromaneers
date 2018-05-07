using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passwordPuzzleController : MonoBehaviour
{

    public List<GameObject> passwordCubes = new List<GameObject>();
    public int correctCubes = 0;

    public bool isPasswordCorrectlyInputted = false;
    public GameObject keyPrefab;

    public GameObject keySpawnPoint;

    public bool hasKeySpawned = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    correctCubes = 0;
	    foreach (GameObject passwordCube in passwordCubes)
	    {
	        if (passwordCube.GetComponent<passwordEntry>().isCorrectColourSelected==true)
	        {
	            correctCubes += 1;
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
                Instantiate(keyPrefab, keySpawnPoint.transform.position, Quaternion.identity);
                hasKeySpawned = true;
            }
        }
	}
}
