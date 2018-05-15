using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour
{
    private EnemySpawner spawner;

    public bool IsTriggerForSpawnpointA = false;
    public bool IsTriggerForSpawnpointB = false;
    public bool IsTriggerForSpawnpointC = false;
    public bool IsTriggerForSpawnpointD = false;
    public bool IsTriggerForSpawnpointE = false;
    public bool IsTriggerForSpawnpointF = false;
    public bool IsTriggerForSpawnpointG = false;
    public bool IsTriggerForSpawnpointH = false;
    public bool IsTriggerForSpawnpointI = false;
    public bool IsTriggerForSpawnpointJ = false;

    // Use this for initialization
    void Start ()
    {
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemySpawner>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player")==true||other.CompareTag("RedPlayer")==true||other.CompareTag("BluePlayer")==true||other.CompareTag("YellowPlayer"))
        {
            if (IsTriggerForSpawnpointA == true)
            {
                spawner.SpawnGroupA();
            }
            if (IsTriggerForSpawnpointB == true)
            {
                spawner.SpawnGroupB();
            }
            if (IsTriggerForSpawnpointC == true)
            {
                spawner.SpawnGroupC();
            }
            if (IsTriggerForSpawnpointD == true)
            {
                spawner.SpawnGroupD();
            }
            if (IsTriggerForSpawnpointE == true)
            {
                spawner.SpawnGroupE();
            }
            if (IsTriggerForSpawnpointF == true)
            {
                spawner.SpawnGroupF();
            }
            if (IsTriggerForSpawnpointG == true)
            {
                spawner.SpawnGroupG();
            }
            if (IsTriggerForSpawnpointH == true)
            {
                spawner.SpawnGroupH();
            }
            if (IsTriggerForSpawnpointI == true)
            {
                spawner.SpawnGroupI();
            }
            if (IsTriggerForSpawnpointJ == true)
            {
                spawner.SpawnGroupJ();
            }

        }
        
    }*/
}
