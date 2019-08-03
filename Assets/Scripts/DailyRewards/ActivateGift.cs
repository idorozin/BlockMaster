using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleAndroidNotifications;
using TMPro;
using UnityEngine;

public class ActivateGift : DailyReward_
{
    [SerializeField] 
    private TextMeshProUGUI timer;
    [SerializeField]
    private int resetTime;

    public static bool GiftAllowed = false;
    
    protected override void SetTimePassed()
    {
        if(timePassed == null)
            timePassed = new TimePassed();
        timePassed = PlayerStats.Instance.gift;
    }

    protected override void OnTick()
    {
        timer.text = coolDown <= 0 ? "READY!" : ExtensionMethods.SecsToTime(coolDown);
    }

    protected override void OnTimePassed()
    {
        if (Math.Abs(coolDown) > resetTime)
            PlayerStats.Instance.GiftIndex = 0;
        GiftAllowed = true;
    }

    protected override void OnReset()
    {
        NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(countDownLenght) , "Block Master" , "Your daily reward is ready!" , Color.cyan);
    }

    protected override bool OnValidation()
    {
        timer.text = "READY!";
        return !(GiftAllowed);
    }

}
