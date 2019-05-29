
using System;
using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private void Awake()
    {
        Instance = this;
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
        RequestBanner();
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
        rewardedAd.OnAdFailedToLoad += OnRip2;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void ShowInterstitial(EventArgs e)
    {
        if(interstitial.IsLoaded())
            interstitial.Show();
    } 
    
    public void ShowBanner(EventArgs e)
    {
        bannerView.Show();
    }  
    
    public void ShowRewarded(EventHandler<EventArgs> e , EventHandler<AdErrorEventArgs> ef)
    {
        rewardedAd.OnAdClosed += e;
        rewardedAd.OnAdLoaded += e;
        rewardedAd.OnAdOpening += e;
        rewardedAd.OnAdFailedToShow += ef;
        
        if(rewardedAd.IsLoaded())
            rewardedAd.Show();
    }

}
