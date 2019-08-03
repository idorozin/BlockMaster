using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleAndroidNotifications;
using TMPro;
using UnityEngine;

public class ActivateChallenge : DailyReward_
{
    public static int timeRemaining;

    public static event Action ChallengeActivated;

    protected override void SetTimePassed()
    {
        if(timePassed == null)
            timePassed = new TimePassed();
        timePassed = PlayerStats.Instance.challenge;
    }

    protected override void OnTick()
    {
        timeRemaining = coolDown;
    }

    protected override void OnTimePassed()
    {
        PlayerStats.Instance.IncrementChallengesAvailable(Math.Max(1,Math.Abs((int)coolDown/(int)3600)));
        ChallengeActivated?.Invoke();
    }

}
