using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int Damage = 5;
	void Update ()
    {
        Destroy(gameObject, 4);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameController")
            Destroy(gameObject);
		if (collision.gameObject.tag == "Player")
			collision.GetComponent<Player>().DamageTaken = Damage;
	}


}
