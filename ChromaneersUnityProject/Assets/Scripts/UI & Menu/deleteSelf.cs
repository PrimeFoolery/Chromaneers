using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteSelf : MonoBehaviour {

    public float delay = 0f;
    public float timer = 1f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer<= 0.5)
        {
            StartCoroutine("Fade");
        }
        //transform.Translate(Vector3.up * Time.deltaTime);
        
    }
    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.04f)
        {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = f;
            GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }
    }
}
