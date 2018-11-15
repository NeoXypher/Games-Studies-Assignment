using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

    public float Speed;
    public int Level;
    public Material Level1;
    public Material Level2;
    public Material Level3;
    //public int Level = 0;

    void Update () {
        Vector2 offset = new Vector2(Time.time * Speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}

    public void ChangeMaterial()
    {
        if (Level == 1)
        {
            gameObject.GetComponent<MeshRenderer>().material = Level1;
        }
        if (Level == 2)
        {
            gameObject.GetComponent<MeshRenderer>().material = Level2;
        }
        if (Level == 3)
        {
            gameObject.GetComponent<MeshRenderer>().material = Level3;
        }
        
    }
}
