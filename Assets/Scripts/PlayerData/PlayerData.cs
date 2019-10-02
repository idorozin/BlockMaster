using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[Serializable]
public partial class PlayerData
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
        
        public bool musicOn=true, soundOn=true;
        public bool noAds = false;
        public bool didTutorial = false;
        
        public ChallengesTemplates templates;
        
        public int ChallengesAvailable;    
        public List<Challenge> challenges;
}