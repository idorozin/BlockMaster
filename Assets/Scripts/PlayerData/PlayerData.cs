using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
[Serializable]
public class PlayerData
    {
        public float highScore;
        public int lastCannon= 0;
        public List<int> ItemsOwned = new List<int>();
        public List<int> ItemsUnlocked = new List<int>(); 
        public int gold;
        public int diamonds;
        public float highScoreHeight;
        public TimePassed wheel = new TimePassed();
        public TimePassed gift = new TimePassed();
        public TimePassed challenge = new TimePassed();
        public string wheelTime = "";
        public string giftTime = "";
        public int offsetW , offsetG , GiftIndex;
        public int ChallengesAvailable;
        public bool musicOn=true, soundOn=true;
        public List<Challenge> challenges;

        public class Challanges
        {
            public List<Challenge> challenges;
        }

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
                    PlayerStats.saveFile();
                    return;
                }
            }
        }

        public void IncrementChallengesAvailable(int n)
        {
            if(ChallengesAvailable>=6) return;
            ChallengesAvailable+=n;
            PlayerStats.saveFile();
        }
    }