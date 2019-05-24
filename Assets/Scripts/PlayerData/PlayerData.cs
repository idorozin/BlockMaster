using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
[Serializable]
public class PlayerData
    {
        public float highScore=0;
        public string lastCannon="default";
        public List<string> ItemsOwned = new List<string>();
        public int gold=0;
        public int diamonds=0;
        public float highScoreHeight=0;
        public TimePassed wheel = new TimePassed();
        public TimePassed gift = new TimePassed();
        public TimePassed challenge = new TimePassed();
        public string wheelTime = "";
        public string giftTime = "";
        public int offsetW , offsetG , GiftIndex , challengeIndex=0;
        public int ChallengesAvailable;
        public bool musicOn=true, soundOn=true;
        public List<Challenge> challenges;

        public void ReportProgress(int progress , string action)
        {
            foreach (Challenge c in challenges)
            {
                if(c.isActive)
                    c.ReportProgress(progress,action);
            }
        }

        public void ActivateChallenge()
        {
            if (ChallengesAvailable <= 0)
                return;
            foreach (var c in challenges)
            {
                if (!c.isActive && !c.completed)
                {
                    c.Activate();
                    ChallengesAvailable--;
                    return;
                }
            }
        }

        public void IncrementChallengesAvailable(int n)
        {
            Debug.Log(ChallengesAvailable);
            if(ChallengesAvailable<6)
                ChallengesAvailable++;
        }
    }