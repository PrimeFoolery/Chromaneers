using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip menuClick;
    public AudioClip menuScroll;
    AudioSource audioSource;

	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) {
            audioSource.PlayOneShot(menuScroll, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            audioSource.PlayOneShot(menuClick, 1f);
        }
	}
}
