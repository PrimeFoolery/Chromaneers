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

    public Transform doorTargetTransform;

    public GameObject dustExplosion;

    private ParticleSystem thisDoorsParticleSystem;
    private bool doorMoved = false;
    private float doorMoveSpeed = 0.01f;
    private GameObject camera;

    private float min;
    private float max;

    // Use this for initialization
    void Start ()
    {
        thisDoorsParticleSystem = gameObject.GetComponent<ParticleSystem>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        min = door.transform.position.x - 0.2f;
        max = door.transform.position.x + 0.2f;
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
            if (hasKeySpawned==false && doorMoved == false)
            {
                door.transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, door.transform.position.y, door.transform.position.z);
                camera.GetComponent<CameraScript>().SmallScreenShake();
                door.transform.Translate(0,-doorMoveSpeed*Time.deltaTime, 0);
                if(doorMoveSpeed<3)
                {
                    doorMoveSpeed *= 1.1f;
                }
                if (thisDoorsParticleSystem != null)
                {
                    thisDoorsParticleSystem.Play();
                }

                if (Vector3.Distance(door.transform.localPosition, doorTargetTransform.localPosition) < 0.5f)
                {
                    doorMoved = true;
                }
            }

            if (doorMoved == true && thisDoorsParticleSystem != null)
            {
                thisDoorsParticleSystem.Stop();
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }

            //door.GetComponent<doorController>().OpenSesame();
            foreach (GameObject passwordCube in passwordCubes)
            {
                Destroy(passwordCube.gameObject);
            }
            foreach (GameObject objectToDestroy in extraObjectsToDestroy)
            {
                Destroy(objectToDestroy.gameObject);
            }

            
            //Instantiate(dustExplosion, transform.position, Quaternion.identity);
            //Instantiate(keyPrefab, keySpawnPoint.transform.position, Quaternion.identity);

            //hasKeySpawned = true;
        
        }
	}
}
