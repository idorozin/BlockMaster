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
        public float money=0;
        public float highScoreHeight=0;
        public string wheelTime = "";
        public string giftTime = "";
        public int offsetW , offsetG , GiftIndex , challengeIndex=0;
        public bool musicOn=true, soundOn=true;
        public List<Challenge> challenges;

        public void ReportProgress(int p , string a)
        {
            foreach (Challenge c in challenges)
            {
                if(c.isActive)
                    c.reportProcess(p,a);
            }
        }

        public void ActivateChallenge()
        {
            foreach (var c in challenges)
            {
                if (!c.isActive)
                {
                    c.isActive = true;
                    return;
                }
            }
        }

        /*private Challange shoot = new Challange("Shoot 10 shapes", 10 , "shot" , "100");
        private Challange shoot1 = new Challange("Shoot 10 shapes", 100 , "record" , "200");
        private Challange shoot2 = new Challange("Shoot 10 shapes", 10 , "shot" , "100");
        private Challange shoot3 = new Challange("Shoot 10 shapes", 10 , "shot" , "100");
        private Challange shoot4 = new Challange("Shoot 10 shapes", 10 , "shot" , "100");
        private Challange shoot5 = new Challange("Shoot 10 shapes", 10 , "shot" , "100");

        public Challange[] cs = new Challange[6];
        public void SetChallanges()
        {
            cs[0] = shoot;
            cs[1] = shoot1;
            cs[2] = shoot2;
            cs[3] = shoot3;
            cs[4] = shoot4;
            cs[5] = shoot5;
        }*/
    }