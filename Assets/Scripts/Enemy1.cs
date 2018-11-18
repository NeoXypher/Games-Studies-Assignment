using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour {

    private GameObject gameManager;
    public int thisEnemyHealth = 0, thisEnemyRarity = 5, thisEnemyFireRate = 20, RandomInt = 0, Count = 0, thisEnemyDamage = 0, recievedDamage = 5, totalHealth;
    public float thisEnemyMoveSpeed = 0.05F, thisEnemyDirection = 1F;
    public GameObject Health, Life, Speed, Damage, FireRate, Player, Bullet;
    public Slider HealthBar;

    System.Random rnd = new System.Random();

    public void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true; 
		gameManager = GameObject.Find("GameManager");
        totalHealth = thisEnemyHealth;
	}

    void FixedUpdate()
    {

		/*if (gameObject.transform.position.y <= -4)
			thisEnemyDirection = 1F;
		else if (gameObject.transform.position.y >= 4)
			thisEnemyDirection = -1F;*/

		Vector3 Movement = new Vector3(-(thisEnemyDirection * thisEnemyMoveSpeed), 0 , 0);
		gameObject.transform.Translate(Movement, Space.World);

		if (Player.transform.position.y + 1F >= gameObject.transform.position.y && Player.transform.position.y - 1F <= gameObject.transform.position.y)
        {
            Count++;
            if (Count % thisEnemyFireRate == 0)
            {
                GameObject bullet = Instantiate(Bullet);
                Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
                Rigidbody2D rbPlayer = GetComponent<Rigidbody2D>();
                rbBullet.velocity = new Vector2(-4, 0);
                rbBullet.position = rbPlayer.position;
                bullet.transform.position = gameObject.transform.position;
				bullet.GetComponent<Bullet>().Damage = thisEnemyDamage;
			}
        }
        else
            Count = 19;

    }

    public void DropPickUp()
    {   
        RandomInt = rnd.Next(1, thisEnemyRarity + 1);
        switch(RandomInt)
        {
            case 1:
                GameObject Drop = Instantiate(Health);
                Drop.transform.Translate(gameObject.transform.position, Space.World);
                break;
            case 2:
                Drop = Instantiate(Life);
                Drop.transform.Translate(gameObject.transform.position, Space.World);
                break;
            case 3:
                Drop = Instantiate(Speed);
                Drop.transform.Translate(gameObject.transform.position, Space.World);
                break;
            case 4:
                Drop = Instantiate(Damage);
                Drop.transform.Translate(gameObject.transform.position, Space.World);
                break;
            case 5:
                Drop = Instantiate(FireRate);
                Drop.transform.Translate(gameObject.transform.position, Space.World);
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            thisEnemyHealth -= collision.GetComponent<Bullet>().Damage;
            HealthBar.value = ((float)thisEnemyHealth / totalHealth);
            if (thisEnemyHealth == 0)
            {
                Destroy(gameObject);
                gameManager.GetComponent<GameManager>().CheckLevel();
                //DropPickUp();
            }
        }
        if (collision.gameObject.tag == "GameController")
        {
            gameManager.GetComponent<GameManager>().Remainder = 1;
            Destroy(gameObject);
            gameManager.GetComponent<GameManager>().CheckLevel();
        }
    }

    
}
