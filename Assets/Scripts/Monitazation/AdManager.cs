using System;
using System.Collections;
using System.IO.IsolatedStorage;
using Boo.Lang;
using UnityEngine;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    private float timePassedTillLastAd;
    [SerializeField] private float minimumTimeBetweenAds;

    private string testId = "3863B0DE3158C0FD8295B27D56C2F6D5";

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
        if (!PlayerStats.Instance.noAds)
            RequestInterstitial();
        RequestRewarded();
    }


    void Update()
    {
        timePassedTillLastAd += Time.deltaTime;
    }

    public InterstitialAd interstitial;

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4356027285942374/7256159091";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #else
            string adUnitId = "unexpected_platform";
        #endif
        interstitial?.Destroy();
        this.interstitial = new InterstitialAd(adUnitId);
        //interstitial.OnAdClosed += ReloadInterstitial;
        AdRequest request = new AdRequest.Builder().AddTestDevice(testId).Build();
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

    public RewardedAd rewardedAd;

    private void RequestRewarded()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4356027285942374/4821567447";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #else
            string adUnitId = "unexpected_platform";
        #endif
        this.rewardedAd = new RewardedAd(adUnitId);
        rewardedAd.OnAdClosed += ReloadRewarded;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice(testId).Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (CanPlayInterstitial())
        {
            interstitial.OnAdClosed += ReloadInterstitial;
            timePassedTillLastAd = 0;
            interstitial.Show();
        }
    }

    public void ShowBanner(EventArgs e)
    {
        bannerView.Show();
    }

    public void ShowRewarded()
    {
        if (CanPlayRewarded())
        {
            rewardedAd.OnAdClosed += ReloadRewarded;
           // timePassedTillLastAd = 0;
            rewardedAd.Show();
        }
    }

    public bool CanPlayInterstitial()
    {
        return !PlayerStats.Instance.noAds && interstitial != null &&
               timePassedTillLastAd >= minimumTimeBetweenAds && interstitial.IsLoaded();
    }

    public bool CanPlayRewarded()
    {
        return rewardedAd != null && rewardedAd.IsLoaded();
    }

    private void ReloadInterstitial(object sender, EventArgs e)
    {
        interstitial.OnAdClosed -= ReloadInterstitial;
        RequestInterstitial();
    }

    private bool triedReloadRewarded = false;
    private void ReloadRewarded(object sender, EventArgs e)
    {
        triedReloadRewarded = false;
        rewardedAd.OnAdClosed -= ReloadRewarded;
        RequestRewarded();
    }   
    
    private void TryReloadRewardedAgain(object sender, EventArgs e)
    {
        triedReloadRewarded = true;
        if(triedReloadRewarded)
        rewardedAd.OnAdClosed -= ReloadRewarded;
        RequestRewarded();
    }
}