using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
[Serializable]
public class PlayerData
    {
        public float highScore;
        public int lastCannon;
        public int lastFlame;
        public int lastPlatform;
        public int lastTrail;
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
        public bool noAds;

        
        public List<Challenge> challenges = new List<Challenge>();
        public ChallengesTemplates templates;
        
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
                    Debug.Log("activate");
                    c.Activate();
                    ChallengesAvailable--;
                    PlayerStats.saveFile();
                    return;
                }
            }

            foreach (var c in challenges)
            {
                c.NextLevel();
            }
            
            foreach (var c in challenges)
            {
                Debug.Log(!c.isActive + " " + !c.completed);
                if (!c.isActive && !c.completed)
                {
                    Debug.Log("activate");
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