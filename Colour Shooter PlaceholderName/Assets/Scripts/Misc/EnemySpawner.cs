using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public float Timer;

	public GameObject RedEnemy;
	public GameObject YellowEnemy;
	public GameObject BlueEnemy;
    public GameObject SpiderEnemy;
	public GameObject RedShieldEnemy;
	public GameObject BlueShieldEnemy;
	public GameObject YellowShieldEnemy;
	public GameObject OrangeEnemy;
	public GameObject PurpleEnemy;
	public GameObject GreenEnemy;

	public GameObject tempEnemy;
	private EnemyManager enemyManagerScript;

	private int RanNumb;
	private int ranShieldEnemy;
	private int ranSecondaryEnemy;

	// Use this for initialization
	void Start () {
		enemyManagerScript = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<EnemyManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;

		if (Timer >= 2) {
			
			RanNumb = Random.Range (0, 6);
			if (RanNumb == 0) {
				tempEnemy = Instantiate (RedEnemy);
				//enemyManagerScript.enemyList.Add (tempEnemy);
				Timer = 0;
			}
			if (RanNumb == 1) {
				tempEnemy = Instantiate (YellowEnemy);
				//enemyManagerScript.enemyList.Add (tempEnemy);
				Timer = 0;
			}
			if (RanNumb == 2) {
				tempEnemy = Instantiate (BlueEnemy);
				//enemyManagerScript.enemyList.Add (tempEnemy);
				Timer = 0;
			}
		    if (RanNumb == 3) {
				tempEnemy = Instantiate(SpiderEnemy);
				//enemyManagerScript.enemyList.Add (tempEnemy);
		        Timer = 0;
		    }
			if(RanNumb == 4||RanNumb == 5){
				ranSecondaryEnemy = Random.Range (0, 3);
				if(ranSecondaryEnemy==0){
					//tempEnemy = Instantiate (OrangeEnemy);
					//enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
				if(ranSecondaryEnemy==1){
					//tempEnemy = Instantiate (GreenEnemy);
					//enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
				if(ranSecondaryEnemy==2){
					//tempEnemy = Instantiate (PurpleEnemy);
					//enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
			}
			if(RanNumb == 6){
				ranShieldEnemy = Random.Range (0,3);
				if(ranShieldEnemy==0){
					tempEnemy = Instantiate (RedShieldEnemy);
					//enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
				if(ranShieldEnemy==1){
					tempEnemy = Instantiate (BlueShieldEnemy);
					//enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
				if(ranShieldEnemy==2){
					tempEnemy = Instantiate (YellowShieldEnemy);
					//enemyManagerScript.enemyList.Add (tempEnemy);
					Timer = 0;
				}
			}

        }
	}
}
