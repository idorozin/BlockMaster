
using System;
using System.Collections;
using System.IO.IsolatedStorage;
using UnityEngine;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    private float timePassedTillLastAd;
    [SerializeField]
    private float minimumTimeBetweenAds;

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

    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-4356027285942374~1457037501";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-4356027285942374~1457037501";
        #else
            string appId = "unexpected_platform";
        #endif
        // Initialize the Google Mobile Ads SDK.
        //MobileAds.Initialize(appId);
        RequestInterstitial();
        RequestRewarded();
    }


    void Update()
    {
        timePassedTillLastAd += Time.deltaTime;
    }

    private InterstitialAd interstitial;

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #else
            string adUnitId = "unexpected_platform";
        #endif
        this.interstitial = new InterstitialAd(adUnitId);
        interstitial.OnAdClosed += ReloadInterstitial;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    private BannerView bannerView;
    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }

    private RewardedAd rewardedAd;
    private void RequestRewarded()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #else
            string adUnitId = "unexpected_platform";
        #endif
        this.rewardedAd = new RewardedAd(adUnitId);
        rewardedAd.OnAdClosed += ReloadRewarded;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void ShowInterstitial(EventArgs handle)
    {
        if (interstitial.IsLoaded())
        {
            timePassedTillLastAd = 0;
            interstitial.Show();
        }
    }
    
    public void ShowInterstitial()
    {
        if (CanPlay() && interstitial.IsLoaded())
        {
            timePassedTillLastAd = 0;
            interstitial.Show();
        }
    }

    bool CanPlay()
    {
        return timePassedTillLastAd >= minimumTimeBetweenAds;
    }

    public bool CanPlayRewarded()
    {
        return rewardedAd!=null && rewardedAd.IsLoaded();
    }

    public void ShowBanner(EventArgs e)
    {
        bannerView.Show();
    }


    public void ShowRewarded(EventHandler<AdErrorEventArgs> handleFailed, EventHandler<Reward> handleReward)
    {
        rewardedAd.OnUserEarnedReward += handleReward;
        rewardedAd.OnAdFailedToShow += handleFailed;
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
    }  
    
    public void ShowRewarded(EventHandler<Reward> handleReward)
    {
        rewardedAd.OnUserEarnedReward += handleReward;
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
    }

    private void ReloadInterstitial(object sender , EventArgs e)
    {
        RequestInterstitial();
    } 
    private void ReloadRewarded(object sender , EventArgs e)
    {
        RequestRewarded();
    }
    
}
