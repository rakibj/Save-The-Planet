using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;

public class AdManager : MonoBehaviour {

    public static AdManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
        Chartboost.cacheInterstitial(CBLocation.Default);
        Chartboost.cacheRewardedVideo(CBLocation.Default);
        Chartboost.setAutoCacheAds(true);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowInterstitialAd()
    {
        if (Chartboost.hasInterstitial(CBLocation.Default))
        {
            Chartboost.showInterstitial(CBLocation.Default);
        }
    }

    public void ShowRewardedAd()
    {
        if (Chartboost.hasRewardedVideo(CBLocation.Default))
        {
            Chartboost.showRewardedVideo(CBLocation.Default);
        }
    }

    void OnEnable()
    {
        // Listen related event
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
    }

    void OnDisable()
    {
        // Remove handler
        Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
    }

    void didCompleteRewardedVideo(CBLocation location, int reward)
    {
        Debug.Log("Completed video with reward: " + reward);
    }
}
