using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

    public Planet planet;
    public GameController gameController;
    public float speed;
    public float increment;
    public int color;

    // Use this for initialization
    void Start ()
    {
        increment = .1f;
        if (!Planet.playerDead)
        {
            planet = GameObject.Find("Planet").GetComponent<Planet>();
        }
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
	}

    //Controls the movement of the meteors
    void Movement()
    {
       
        float step = (speed + ((gameController.nextWave + 1.0f) * increment)) * Time.deltaTime;
        if (!Planet.playerDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, planet.transform.position, step);
        }
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

}
