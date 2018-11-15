using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelView : MonoBehaviour {

    public Text Level;

	void Start () {
        Level.text = "Level: " + PlayerPrefs.GetInt("Level");
        StartCoroutine (Wait());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Over");
    }
}
