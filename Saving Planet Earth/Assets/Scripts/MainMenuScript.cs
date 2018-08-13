using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public Text highScoreShow;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BG");
    }

    public void ButtonClickSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void PlayGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Player.life = 3;
        Player.score = 0;
        Planet.playerDead = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game..");
        Application.Quit();
    }

    public void GetHighScore()
    {
        highScoreShow.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
