using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX : MonoBehaviour {

    private void Start()
    {
        gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFX");
    }
    void FixedUpdate ()
    {
        PlayerPrefs.SetFloat("SFX", gameObject.GetComponent<Slider>().value);
	}
}
