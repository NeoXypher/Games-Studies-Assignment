using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject Player, Enemy, Background;
    public AudioSource GameMusic;
    public AudioClip Level1Clip, Level2Clip, Level3Clip;
	public Text UIScore, UIHealth, UILives;
	public int PlayerLives, PlayerHealth, Score = 0;

	private int Level = 1, EnemyCount = 0;

    public void Start () {
		Level = 2;
		GameMusic.volume = PlayerPrefs.GetFloat("Music");
		UIScore.text = "Score: " + Score;
		GameObject PlayerClone = Instantiate(Player);
		Enemy.GetComponent<Enemy1>().Player = PlayerClone;
		LevelStart();
    }

    public void FixedUpdate()
    {
        UIScore.text = "Score: " + Score;
        UILives.text = "Lives: " + PlayerLives;
		UIHealth.text = "Health: " + PlayerHealth;
	}



    public void LevelStart()
    {
        if (Level == 1)
        {

            GameMusic.clip = Level1Clip;    
            Background.GetComponent<Scroller>().Level = Level;
            Background.GetComponent<Scroller>().ChangeMaterial();
            EnemyCount = 3;
			SetEnemies(Enemy, 1, 3);
            
        }
        if (Level == 2)
        {
            GameMusic.clip = Level2Clip;
            GameMusic.Play();
            Background.GetComponent<Scroller>().Level = Level;
            Background.GetComponent<Scroller>().ChangeMaterial();
            EnemyCount = 2;
			SetEnemies(Enemy, 2, 2);
		}
        if (Level == 3)
        {
            GameMusic.clip = Level3Clip;
            GameMusic.Play();
            Background.GetComponent<Scroller>().Level = Level;
            Background.GetComponent<Scroller>().ChangeMaterial();
            EnemyCount = 1;
			SetEnemies(Enemy, 3, 1);

		}
        if (Level >= 4)
        {
            Score += ((PlayerLives * 100) + PlayerHealth);
            PlayerPrefs.SetString("Victory", "You Win!");
            PlayerPrefs.SetInt("Score", Score);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
	
	public void CheckLevel ()
    {
        EnemyCount -= 1;
        Score += 100 * Level;
        if (EnemyCount <= 0)
        {
            Level++;
            LevelStart();
        }
	}

	public void SetEnemies(GameObject enemy, int type, int enemyCount)
	{
		
		for (int i = 0; i < enemyCount; i++)
		{	
			GameObject NewEnemy = Instantiate(enemy);
			NewEnemy.GetComponent<Enemy1>().thisEnemyHealth = 15 * type;
			NewEnemy.GetComponent<Enemy1>().thisEnemyMoveSpeed = 0.04F + (0.01F * type * ((float)Math.Pow(-1, type)));
			NewEnemy.GetComponent<Enemy1>().thisEnemyDamage = 5 * type;
			Vector3 Movement = new Vector3(-(i*3), 0, 0);
			NewEnemy.transform.Translate(Movement, Space.World);
		}
	}
}
