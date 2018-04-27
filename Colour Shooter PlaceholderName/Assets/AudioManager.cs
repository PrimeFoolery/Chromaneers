using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip menuClick;
    public AudioClip menuScroll;
    AudioSource audioSource;

    public bool canInteract = true;
    private float InteractTimer;
    public float maxIntTimer;

    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
        Vector3 menuInput1;
        menuInput1 = new Vector3(Input.GetAxisRaw("XboxJoystick1LHorizontal"), 0f, Input.GetAxisRaw("XboxJoystick1LVertical"));
        if (canInteract == false)
        {
            InteractTimer -= Time.deltaTime;
            if (InteractTimer <= 0)
            {
                canInteract = true;
                InteractTimer = maxIntTimer;
            }
        }
        if (menuInput1.z < 0 || menuInput1.z > 0)
        {
            if (canInteract == true)
            {
                audioSource.PlayOneShot(menuScroll, 1f);
                canInteract = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) {
            if (canInteract == true)
            {
                audioSource.PlayOneShot(menuScroll, 1f);
                canInteract = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            audioSource.PlayOneShot(menuClick, 1f);
        }
	}
}
