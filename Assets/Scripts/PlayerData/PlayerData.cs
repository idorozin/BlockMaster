using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Random = UnityEngine.Random;

[Serializable]
public class PlayerData
{
        public TimePassed test = new TimePassed();
        public int highScore;
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
        public int GiftIndex;
        public int ChallengesAvailable;
        public bool musicOn=true, soundOn=true;
        public bool noAds = false;
        public bool didTutorial = false;

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
            int max = 0;
            while (!(ChallengesAvailable <= 0 || ChallengesActive() >= 3) && max<=3)
            {
                var canSelectTime = from c in challenges where c.isActive && c.timeToComplete > 0 select c;
                bool time = !canSelectTime.Any();
                var nonActiveChallenges = from c in challenges where !c.isActive && !c.completed && !(!time && c.timeToComplete>0) select c;

                int count = nonActiveChallenges.Count();
                if (count == 0)
                {
                    foreach (var c in challenges)
                    {
                        if (c.completed)
                            c.NextLevel();
                    }
                }
                var nonActiveChallenges_ = from c in challenges where !c.isActive && !c.completed && !(!time && c.timeToComplete>0) select c;
                int r = Random.Range(0, nonActiveChallenges_.Count());
                int i = 0;
                foreach (var c in nonActiveChallenges_)
                {
                    if (i == r)
                    {
                        c.Activate();
                        ChallengesAvailable--;
                        break;
                    }
                    i++;
                }
                max++;
            }
        }
        

        public void IncrementChallengesAvailable(int n)
        {
            ChallengesAvailable = Math.Min(ChallengesAvailable+n,2);
            ActivateChallenge();
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
        
        public void SubscribeToNewGame()
        {
            foreach (var c in challenges)
            {
                if (c.isActive)
                {
                    c.SubscribeToNewGame();
                }
            }
        }
    }