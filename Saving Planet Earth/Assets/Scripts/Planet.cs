using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChartboostSDK;
public class Planet : MonoBehaviour {

    public GameObject planetExplosion;
    public GameObject deathPanelUI;
    public Text highScoreText;
    public Text score;
    public GameObject gameScreenPanelUI;
    public static bool playerDead = false;
	// Use this for initialization
	void Start () {
        Debug.Log("Started");
        Player.life = 3;
    }

    void SpawnRipple(Transform spawnLoc)
    {
        Instantiate(planetExplosion, spawnLoc.position, spawnLoc.rotation);
    }

    // Update is called once per frame
    void Update () {
        Rotation();
	}
    public void ActivateDeathPanel()
    {
        Debug.Log("Invoked");
        deathPanelUI.SetActive(true);
        
    }

    public void Death()
    {
        playerDead = true;
        SpawnRipple(gameObject.transform);
        gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().Play("PlanetExplosion1");
        FindObjectOfType<AudioManager>().Play("PlanetExplosion2");
        
        Invoke("ActivateDeathPanel", 2);
        
        gameScreenPanelUI.SetActive(false);
        score.text = Player.score.ToString();
        if (Player.score >= Player.highScore)
        {
            highScoreText.text = "NEW HIGH SCORE";
            PlayerPrefs.SetInt("HighScore", Player.score);
            PlayerPrefs.Save();
            Player.score = 0;
        }
        else
        {
            highScoreText.text = "";
            Player.score = 0;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Meteor")
        {
            playerDead = true;
            SpawnRipple(gameObject.transform);
            gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("PlanetExplosion1");
            Invoke("ActivateDeathPanel", 2);
            gameScreenPanelUI.SetActive(false);
            AdManager.instance.ShowInterstitialAd();
            score.text = Player.score.ToString();
            if (Player.score >= Player.highScore)
            {
                highScoreText.text = "NEW HIGH SCORE";
                PlayerPrefs.SetInt("HighScore", Player.score);
                PlayerPrefs.Save();
                Player.score = 0;
            }
            else
            {
                highScoreText.text = "";
                Player.score = 0;
            }

        }
    }

    void Rotation()
    {
        transform.Rotate(0, 30 * Time.deltaTime, 0);
    }

   
}
