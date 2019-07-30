
using System;
using Boo.Lang;
using GooglePlayGames.Native.Cwrapper;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Challenge
{
    public string description;
    public int reward;
    public string action;
    public int goal;
    public bool oneRun;
    public float timeToComplete;
    public int difficulty;
    public int level;
    public bool incrementable = true;
    public List<int> previousGoals;
    
    [HideInInspector]
    public int progress;
    [HideInInspector]
    public bool isActive = false;
    [HideInInspector]
    public bool completed;
    [HideInInspector]
    public bool timePassed = false;

    public static event Action<Challenge> OnChallengeCompleted = delegate {  };

    public Challenge(Challenge c)
    {
        if(c == null)
            return;
        description = c.description;
        reward = c.reward;
        action = c.action;
        goal = c.goal;
        oneRun = c.oneRun;
        timeToComplete = c.timeToComplete;
        difficulty = c.difficulty;
        level = c.level;
        incrementable = c.incrementable;
        previousGoals = new List<int>();
    }

    public void Activate()
    {
        //if(PlayerStats.Instance.ChallengesActive() >= 3)
        //    return;
        Debug.Log("active" + " " + this);
        isActive = true;
        progress = 0;
        if (oneRun)
        {
            GameManager.NewGame += OnNewGame;
        }
    }

    public void SubscribeToNewGame()
    {
        if (oneRun)
        {
            GameManager.NewGame += OnNewGame;
        }
    }

    public void NextLevel()
    {
        completed = false;
        goal = (int)Math.Round((double) (goal * 1.2));
    }

    public void OnNewGame()
    {
        progress = 0;
        if (timeToComplete > 0)
        {
            timePassed = false;
            ExtensionMethods.DisableAfterTimePassed(this);
        }
    }

    public void ReportProgress(int progress, string action)
    {
        if(timePassed || this.action != action)
            return;
        
        if (incrementable)
            this.progress += progress;
        else
            this.progress = progress;
        
        if (this.progress >= goal) 
            ChallengeCompleted();
        else 
            PlayerStats.saveFile();
    }

    private void ChallengeCompleted()
    {
        completed = true;
        isActive = false;
        if(oneRun)
            GameManager.GameOver -= OnNewGame;
        GameManager.Instance.ChallengeComplete(this);
        PlayerStats.Instance.gold += reward;
        previousGoals.Add(goal);
        PlayerStats.saveFile();
    }

    public override string ToString()
    {
        if(!oneRun)        
            return description.Replace("%s" , ""+goal);
        if((int)timeToComplete == 0)
            return description.Replace("%s" , ""+goal) + " In One Run!";
        return description.Replace("%s", ""+ goal) + " In " + timeToComplete + " seconds!";
    }
    
    public string ToString(int goal)
    {
        if(!oneRun)        
            return description.Replace("%s" , ""+goal);
        if((int)timeToComplete == 0)
            return description.Replace("%s" , ""+goal) + " In One Run!";
        return description.Replace("%s", ""+ goal) + " In " + timeToComplete + " seconds!";
    }
}
