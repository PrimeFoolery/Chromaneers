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

    public GameObject dustExplosion;

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
                //Instantiate(dustExplosion, transform.position, Quaternion.identity);
                //Instantiate(keyPrefab, keySpawnPoint.transform.position, Quaternion.identity);
                if (transform.localScale.z > -0.1f)
                {
                    transform.localScale += new Vector3(-0f, -0f, -3f);
                    gameObject.GetComponent<ParticleSystem>().Play();

                }
                else if (transform.localScale.z <= -0.1f)
                {
                    Destroy(this.gameObject);
                }
                //hasKeySpawned = true;
            }
        }
	}
}
