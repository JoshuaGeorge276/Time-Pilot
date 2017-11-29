using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionListener : MonoBehaviour {

    public AudioClip coinSound;

    private Animator anim;
    private new AudioSource audio;
    private bool startSoundPlayed = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (LevelManager.hasClicked) {
            if(startSoundPlayed == false) {
                audio.PlayOneShot(coinSound, 1);
                anim.SetTrigger("transition");
                startSoundPlayed = true;
            }
        }
	}
}
