using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public float Speed = 2;

    private Rigidbody2D rb2d;
	
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	void FixedUpdate ()
    {
        Vector3 Movement = new Vector2(-1, 0);  
        rb2d.AddForce(Movement * Speed);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameController")
            Destroy(gameObject);
    }
}
