using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public float Timer;

	public GameObject RedEnemy;
	public GameObject YellowEnemy;
	public GameObject BlueEnemy;
    public GameObject SpiderEnemy;

	private int RanNumb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;

		if (Timer >= 2) {
			
			RanNumb = Random.Range (0, 4);
			if (RanNumb == 0) {
				Instantiate (RedEnemy);
				Timer = 0;
			}
			if (RanNumb == 1) {
				Instantiate (YellowEnemy);
				Timer = 0;
			}
			if (RanNumb == 2) {
				Instantiate (BlueEnemy);
				Timer = 0;
			}
		    if (RanNumb == 3)
		    {
		        Instantiate(SpiderEnemy);
		        Timer = 0;
		    }

        }
	}
}
