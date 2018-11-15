using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public int PlayerHealth = 100, PlayerLives = 3, FireRate = 10, Count = 0, DamageTaken = 25, Score = 0, Damage = 5;
	public float Speed = 1F;
    public GameObject Bullet;
    public GameManager gameManager;
	public GameObject GameController;
	

    void Start ()
    {
        Bullet.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFX"); 
		PlayerHealth = 100;
		PlayerLives = 3;
		FireRate = 10;
		DamageTaken = 5;
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
		GameController = GameObject.Find("GameManager");
		GameController.GetComponent<GameManager>().PlayerLives = PlayerLives;
		GameController.GetComponent<GameManager>().PlayerHealth = PlayerHealth;
	}

	void FixedUpdate ()
	{
		
		float mV = Input.GetAxis("Vertical");
		float mH = Input.GetAxis("Horizontal");
		Vector2 m = new Vector2(0, mV * 0.1F * Speed);
		Vector2 m2 = new Vector2(mH * 0.075F * Speed, 0);
		transform.Translate(m, Space.World);
		transform.Translate(m2, Space.World);

        if (Input.GetKey(KeyCode.Space))
        {
            Count++;
            if (Count % FireRate == 0)
            {
                GameObject bullet = Instantiate(Bullet);
                Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
                Rigidbody2D rbPlayer = GetComponent<Rigidbody2D>();
                rbBullet.velocity = new Vector2(8, 0);
                rbBullet.position = rbPlayer.position;
                bullet.transform.position = gameObject.transform.position;
                bullet.GetComponent<Bullet>().Damage = Damage;
            }
        }
        else
            Count = 0;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        { 
            if (collision.gameObject.name == "Health(Clone)")
            {
                if (PlayerHealth <= 75)
                    PlayerHealth += 25;
                else
                    PlayerHealth = 100;
            }
            else if (collision.gameObject.name == "Life(Clone)")
                PlayerLives++;
            else if (collision.gameObject.name == "Speed(Clone)")
                Speed += 0.1F;
            else if (collision.gameObject.name == "FireRate(Clone)")
            {
                if (FireRate > 2)
                    FireRate -= 2;
                else
                    FireRate = 1;   
            }
            else if (collision.gameObject.name == "Damage(Clone)")
            {
                Damage += 5;
            }   
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "EnemyProjectile" || collision.gameObject.tag == "Enemy")
        {
            PlayerHealth -= DamageTaken;
            if (collision.gameObject.tag == "EnemyProjectile")
            {
                Destroy(collision.gameObject);
            }
            if (PlayerHealth <= 0)
            { 
                if (PlayerLives > 0)
                {
                    PlayerLives -= 1;
					PlayerHealth = 100;
                    FireRate = 10;
                    Speed = 1F;
                    Damage = 5;
                    Vector3 m = new Vector2(-3, 0);
                    Vector2 m2 = m - gameObject.transform.position;         
                    transform.Translate(m2, Space.World);
                }
                else
                {
                    PlayerPrefs.SetString("Victory", "You Lose!");
                    PlayerPrefs.SetInt("Score", Score);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
        
		GameController.GetComponent<GameManager>().PlayerLives = PlayerLives;
		GameController.GetComponent<GameManager>().PlayerHealth = PlayerHealth;
	}
}
