using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;
public class GameController : MonoBehaviour {

    
    public Transform[] SpawnPoints = new Transform[6];
    public Meteor[] meteors;
    public Player player;
    public Meteor meteor;

    //SpawnStates that defines whether it's ok to spawn or not
    public enum SpawnState { SPAWNING, WAITING };

    //Class for different kinds of waves
    [System.Serializable]
    public class Wave {

        public string name;
        public Transform[] enemy;
        public int count;
        public float delay;
    }

    public Wave[] waves;
    public int nextWave = 0;
    private int finalWave = 4;

    public float timeBetweenWaves = 5f;
    private float waveCountDown;

    private SpawnState state = SpawnState.WAITING;

	// Use this for initialization
	void Start () {
        //Chartboost.cacheInterstitial(CBLocation.HomeScreen);
        //Chartboost.cacheRewardedVideo(CBLocation.HomeScreen);
        //Chartboost.setAutoCacheAds(true);
        //PlayerPrefs.DeleteAll();
        FindObjectOfType<AudioManager>().Play("BG");
        waveCountDown = timeBetweenWaves;
        player = GameObject.Find("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        if (waveCountDown <= 0) {
            if (state != SpawnState.SPAWNING) {
                //Start Spawning
                StartCoroutine(SpawnWave(waves[nextWave]));
                if(nextWave == finalWave)
                {

                }
                else
                {
                    nextWave++;
                }
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
	}


    IEnumerator SpawnWave(Wave _wave)
    {
        if(state == SpawnState.WAITING)
        {
            waveCountDown = timeBetweenWaves;
            yield return new WaitForSeconds(timeBetweenWaves);

        }

        state = SpawnState.SPAWNING;
        //Spawn
        for(int i =0; i< _wave.count; i++)
        {
            //SpawnEnemy
            SpawnEnemy(_wave.enemy[Random.Range(0,3)]);
            SpawnEnemy(_wave.enemy[Random.Range(3,6)]);
            yield return new WaitForSeconds( _wave.delay);
        }
        //EndSpawn
        state = SpawnState.WAITING;
        yield break;
    }

    public void SpawnEnemy(Transform spawnLoc)
    {
        int ran = Random.Range(0, meteors.Length);
        meteor = meteors[ran];
        meteor.color = ran; 
        Instantiate(meteors[ran], spawnLoc.position, spawnLoc.rotation);
    }

    

    void Die() {

    }

    void Restart(){

    }

    
}
