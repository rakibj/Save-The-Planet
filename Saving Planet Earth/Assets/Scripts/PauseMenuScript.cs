using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChartboostSDK;
public class PauseMenuScript : MonoBehaviour {

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public static bool pausePressed = false;


    public void PauseButtonPressed()
    {
        //AdManager.instance.ShowRewardedAd();
        pausePressed = true;
        if (!gameIsPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
    }
    public void ButtonClickSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
    public void ResumeButtonPressed()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

  
	public void MenuButtonPressed()
    {
        Player.life = 3;
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        Debug.Log("Quitting Game..");
        Application.Quit();
    }

    public void PlayAgain()
    {
        Player.life = 3;
        Planet.playerDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

 
}
