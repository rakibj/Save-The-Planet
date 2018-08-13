using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public GameObject particle;
    public GameObject[] ps_touch;
    public Text scoreText;
    public Text lifeText;
    public Camera gameCamera;
    [HideInInspector]
    public static int score = 0;
    public static int life = 3;
    [HideInInspector]
    public static int highScore;
    public Planet planet;
    public GameController gc;
    public Transform pos;
    public GameObject obj;

    void GetHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    // Use this for initialization
    void Start() {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        if (!Planet.playerDead)
        {
            planet = GameObject.Find("Planet").GetComponent<Planet>();
        }
        gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        GetHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript.gameIsPaused && !Planet.playerDead)
        {
            CheckHealth();
            ShootByMouse();
            //ShootByTouch();
            UpdateScore();
            UpdateLife();
        }
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateLife()
    {
        lifeText.text = life.ToString();
    }

    //Controls the shooting of the player in android touch
    public void ShootByTouch()
    {
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                RaycastHit hit;
                Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;

                    if (objectHit.tag == "Meteor")
                    {
                        Destroy(objectHit.gameObject);
                        //FindObjectOfType<AudioManager>().Play("MeteorExplosion");
                        int color = hit.transform.gameObject.GetComponent<Meteor>().color;
                        string c = "c" + color.ToString();
                        FindObjectOfType<AudioManager>().Play(c);
                        SpawnRipple(objectHit, color);
                        score++;
                    }
                    else
                    {
                        Debug.Log("inside else of raycast");
                    }
                }
                else
                {
                    if (!EventSystem.current.IsPointerOverGameObject(id))
                    {
                        Debug.Log("Missed Shot!!");
                        FindObjectOfType<AudioManager>().Play("Error");
                        life--;
                    }

                }

            }
        }
    }
    
    public void Kill()
    {
        Instantiate(obj, pos.position, pos.rotation);
    }
    public void CheckHealth()
    {
        if (life <= 0)
        {
            Kill();
        }
    }

    void MissedShot()
    {

    }

    void SuccessfulShot()
    {

    }

    void SpawnRipple(Transform spawnLoc, int idx)
    {
        Instantiate(ps_touch[idx], spawnLoc.position, spawnLoc.rotation);
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    //Controls the shooting of the player in windows mouse
    void ShootByMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
    
            RaycastHit hit;
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
               
                if (objectHit.tag == "Meteor")
                {
                    Destroy(objectHit.gameObject);
                    //FindObjectOfType<AudioManager>().Play("MeteorExplosion");
                    int color = hit.transform.gameObject.GetComponent<Meteor>().color;
                    string c = "c" + color.ToString();
                    FindObjectOfType<AudioManager>().Play(c);
                    SpawnRipple(objectHit,color);
                    score++;
                }
                else
                {
                    Debug.Log("inside else of raycast");
                }
            }
            else
            {
                if (!IsPointerOverUIObject())
                {
                    Debug.Log("Missed Shot!!");
                    FindObjectOfType<AudioManager>().Play("Error");
                    life--;
                }
                   
            }
        }

    }

 
    
}
