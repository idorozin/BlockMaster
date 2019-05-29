
using System;
using UnityEngine;
using GoogleMobileAds.Api;


public class AdManager : MonoBehaviour {


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
        MobileAds.Initialize(appId);
    }

    


    
    /*public void ShowInterstitialAd(EventHandler<EventArgs> e)
    {
        if (interstitial.IsLoaded())
        {
            interstitial.OnAdClosed += e;
            interstitial.Show();
        }
    }*/

    
}
