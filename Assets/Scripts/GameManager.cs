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
    public int PlayerLives, PlayerHealth, Score = 0, remainder = 0;

    private int Level = 1, EnemyCount = 0;
    System.Random rnd = new System.Random();

    public void Start() {
        Level = 3;
        remainder = 0;
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
            EnemyCount = SetEnemies(Enemy, Level, 3, remainder, 3);
        }
        if (Level == 2)
        {
            GameMusic.clip = Level2Clip;
            GameMusic.Play();
            Background.GetComponent<Scroller>().Level = Level;
            Background.GetComponent<Scroller>().ChangeMaterial();    
            EnemyCount = SetEnemies(Enemy, Level, 3, remainder, 3);

        }
        if (Level == 3)
        {
            GameMusic.clip = Level3Clip;
            GameMusic.Play();
            Background.GetComponent<Scroller>().Level = Level;
            Background.GetComponent<Scroller>().ChangeMaterial();
            EnemyCount = SetEnemies(Enemy, Level, 6, remainder, 1);
            remainder = 0;
        }
        if (Level >= 4)
        {
            Score += ((PlayerLives * 100) + PlayerHealth);
            PlayerPrefs.SetString("Victory", "You Win!");
            PlayerPrefs.SetInt("Score", Score);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void CheckLevel()
    {
        EnemyCount -= 1;
        Score += 100 * Level;
        if (EnemyCount <= 0)
        {
            Level++;
            LevelStart();
        }
    }

    public int SetEnemies(GameObject enemy, int type, int enemyCount, int remainder, int formation)
    {
        float position = 6F;
        int EnemyCount = 0;
        for (int count = 1; count <= enemyCount; count++)
        {
            for (int count2 = 1; count2 <= formation; count2++)
            {
                GameObject NewEnemy = Instantiate(enemy);
                NewEnemy.GetComponent<Enemy1>().thisEnemyHealth = 15 * type;
                NewEnemy.GetComponent<Enemy1>().thisEnemyMoveSpeed = 0.04F + (0.01F * type * ((float)Math.Pow(-1, type)));
                NewEnemy.GetComponent<Enemy1>().thisEnemyDamage = 5 * type;

                if (count % 2 == 0 && (type == 1 || type == 2))
                    position = 8F;
                else
                    position = 6F;
                if (count2 % 2 == 0 && type == 1)
                    position += 0.75F;
                else if (count2 % 2 == 0 && type == 2)
                    position -= 0.75F;
                Vector3 Movement = new Vector3(position, -4F + EnemyCount , 0);
                if (type == 3)
                    Vector3 Movement = new Vector3(position, -)
                NewEnemy.transform.Translate(Movement, Space.World);
                EnemyCount += 1;
            }    
        }
        return EnemyCount;
    }

    public int Remainder
    {
        get { return remainder; }
        set { remainder += value; }
    }
}
