using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandardEnemyBehaviour : MonoBehaviour {

    public GameObject Player;
    NavMeshAgent agent;
	public bool isItCoop;
	public int randomPersonPicker;

	// Use this for initialization
	void Start () {
		if (!isItCoop) {
			Player = GameObject.FindGameObjectWithTag ("Player");
		}
		if (isItCoop) {
			agent = gameObject.GetComponent<NavMeshAgent> ();
			randomPersonPicker = Random.Range (0, 3);

			if (randomPersonPicker == 0) {
				Player = GameObject.FindGameObjectWithTag ("BluePlayer");
			}
			if (randomPersonPicker == 1) {
				Player = GameObject.FindGameObjectWithTag ("RedPlayer");
			}
			if (randomPersonPicker == 2) {
				Player = GameObject.FindGameObjectWithTag ("YellowPlayer");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isItCoop) {
			agent.SetDestination (Player.transform.position);
		}
		if (isItCoop) {
			agent.SetDestination (Player.transform.position);
		}
	}
}
