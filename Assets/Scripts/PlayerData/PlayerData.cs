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
        public bool noAds = false;

        
        public List<Challenge> challenges;
        public ChallengesTemplates templates;
        
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
            while (!(ChallengesAvailable <= 0 || ChallengesActive() >= 3))
            {
                foreach (var c in challenges)
                {
                    if (!c.isActive && !c.completed)
                    {
                        c.Activate();
                        ChallengesAvailable--;
                        PlayerStats.saveFile();
                        break;
                    }
                }

                foreach (var c in challenges)
                {
                    if (c.completed)
                        c.NextLevel();
                }

                foreach (var c in challenges)
                {
                    if (!c.isActive && !c.completed)
                    {
                        c.Activate();
                        ChallengesAvailable--;
                        PlayerStats.saveFile();
                        break;
                    }
                }
            }
        }
        

        public void IncrementChallengesAvailable(int n)
        {
            ChallengesAvailable = Math.Min(ChallengesAvailable+n,2);
            
            for (int i = 0; i < 3 - ChallengesActive(); i++)
            {
                ActivateChallenge();
            }
            PlayerStats.saveFile();
        }

        public int ChallengesActive()
        {
            int count=0;
            foreach (var c in challenges)
            {
                if (c.isActive)
                    count++;
            }

            return count;
        }
    }