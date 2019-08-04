using System;
using System.Collections;
using System.IO.IsolatedStorage;
using System.Net.Mime;
using Boo.Lang;
using UnityEngine;
using admob;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    private float timePassedTillLastAd;
    [SerializeField] private float minimumTimeBetweenAds;

    private string intersitialId;
    private string rewardedAdId;
    private string appId;

    public Admob ad;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetIds()
    {
        #if UNITY_ANDROID
             appId = "ca-app-pub-4356027285942374~1457037501";
             intersitialId = "ca-app-pub-4356027285942374/7256159091";
             rewardedAdId = "ca-app-pub-4356027285942374/4821567447"; 
        #elif UNITY_IPHONE
             appId = unexpected_platform;
             intersitialId = "unexpected_platform";
             rewardedAdId = "unexpected_platform; 
        #else
             appId = "unexpected_platform";
        #endif
        
    }

    public void Start()
    {
        Debug.Log("START");
        SetIds();
        // Initialize the Google Mobile Ads SDK.
        ad = Admob.Instance();
        AdProperties properties = new AdProperties();
        properties.isTesting = true;
        ad.initSDK(appId, properties);
        ad.rewardedVideoEventHandler += ReloadRewarded;
        ad.interstitialEventHandler += ReloadInterstitial;
        if (!PlayerStats.Instance.noAds)
            RequestInterstitial();
        RequestRewarded();
    }


    void Update()
    {
        timePassedTillLastAd += Time.deltaTime;
    }


    private void RequestInterstitial()
    {
        ad.loadInterstitial(intersitialId);
    }


    private void RequestRewarded()
    {
        ad.loadRewardedVideo(rewardedAdId);
    }

    public void ShowInterstitial()
    {
        if (ad.isInterstitialReady())
        {
            timePassedTillLastAd = 0;
            ad.showInterstitial();
        }
    }

    public void ShowRewarded()
    {
        if (ad.isRewardedVideoReady())
        {
            ad.showRewardedVideo();
        }
    }

    public bool CanPlayInterstitial()
    {
        return !PlayerStats.Instance.noAds &&
               timePassedTillLastAd >= minimumTimeBetweenAds && ad.isInterstitialReady();
    }

    public bool CanPlayRewarded()
    {
        return ad.isRewardedVideoReady();
    }

    private bool triedToReloadInterstitialAgain = false;
    [SerializeField] private TextMeshProUGUI text1;
    private void ReloadInterstitial(string eventName, string msg)
    {
        text1.text += " " + msg;
        if (eventName == AdmobEvent.onAdFailedToLoad && !triedToReloadInterstitialAgain)
        {
            triedToReloadInterstitialAgain = true;
            RequestInterstitial();
        }
        if (eventName == AdmobEvent.onAdClosed)
        {
            triedToReloadInterstitialAgain = false;
            RequestInterstitial();
        }
    }
    
    private bool triedToReloadRewardedAgain = false;
    [SerializeField] private TextMeshProUGUI text2;
    private void ReloadRewarded(string eventName, string msg)
    {
        text2.text += " " + msg;
        if (eventName == AdmobEvent.onAdFailedToLoad && !triedToReloadRewardedAgain)
        {
            triedToReloadRewardedAgain = true;
            RequestRewarded();
        }
        if (eventName == AdmobEvent.onAdClosed)
        {
            triedToReloadRewardedAgain = false;
            RequestRewarded();
        }
    }
    
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        ad.showBannerRelative(adUnitId, AdSize.BANNER, AdPosition.BOTTOM_CENTER, 0);
    }
}
