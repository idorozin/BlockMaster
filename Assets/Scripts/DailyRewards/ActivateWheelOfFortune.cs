using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleAndroidNotifications;
using TMPro;
using UnityEngine;

public class ActivateWheelOfFortune : DailyReward_
{
    [SerializeField] 
    private TextMeshProUGUI timer;

    public static bool RollAllowed;
    
    protected override void SetTimePassed()
    {
        if(timePassed == null)
            timePassed = new TimePassed();
        timePassed = PlayerStats.Instance.wheel;
    }

    protected override void OnTick()
    {
        timer.text = coolDown <= 0 ? "READY!" : ExtensionMethods.SecsToTime(coolDown);
    }

    protected override void OnTimePassed()
    {
        RollAllowed = true;
    }

    protected override void OnReset()
    {
        NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(countDownLenght) , "Block Master" , "Your daily reward is ready!" , Color.cyan);
    }

    protected override bool OnValidation()
    {
        timer.text = "READY!";
        return !(RollAllowed);
    }

}
