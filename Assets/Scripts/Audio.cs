using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

	void FixedUpdate () {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
    }
}
