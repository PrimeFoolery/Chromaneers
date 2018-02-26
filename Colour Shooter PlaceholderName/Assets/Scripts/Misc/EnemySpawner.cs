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

	private int RanNumb;
	private int ranShieldEnemy;
	private int ranSecondaryEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;

		if (Timer >= 2) {
			
			RanNumb = Random.Range (0, 6);
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
		    if (RanNumb == 3) {
		        Instantiate(SpiderEnemy);
		        Timer = 0;
		    }
			if(RanNumb == 4||RanNumb == 5){
				ranSecondaryEnemy = Random.Range (0, 3);
				if(ranSecondaryEnemy==0){
					Instantiate (OrangeEnemy);
					Timer = 0;
				}
				if(ranSecondaryEnemy==1){
					Instantiate (GreenEnemy);
					Timer = 0;
				}
				if(ranSecondaryEnemy==2){
					Instantiate (PurpleEnemy);
					Timer = 0;
				}
			}
			if(RanNumb == 6){
				ranShieldEnemy = Random.Range (0,3);
				if(ranShieldEnemy==0){
					Instantiate (RedShieldEnemy);
					Timer = 0;
				}
				if(ranShieldEnemy==1){
					Instantiate (BlueShieldEnemy);
					Timer = 0;
				}
				if(ranShieldEnemy==2){
					Instantiate (YellowShieldEnemy);
					Timer = 0;
				}
			}

        }
	}
}
