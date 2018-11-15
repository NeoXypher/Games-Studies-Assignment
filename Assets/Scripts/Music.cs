using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour {
    private void Start()
    {
        gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music");
    }
    void FixedUpdate()
    {
        PlayerPrefs.SetFloat("Music", gameObject.GetComponent<Slider>().value);
    }
}
