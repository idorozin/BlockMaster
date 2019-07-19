
using System;
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
    
    [HideInInspector]
    public int progress;
    [HideInInspector]
    public bool isActive = false;
    [HideInInspector]
    public bool completed;
    [HideInInspector]
    public bool timePassed = false;

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
    }

    public void Activate()
    {
        Debug.Log("ACTIVATE");
        isActive = true;
        if (oneRun)
        {
            GameManager.NewGame += OnNewGame;
        }
    }
    
    public void NextLevel()
    {
        completed = false;
        goal = (int)Math.Round((double) (goal * 1.2));
        Debug.Log("re activate");
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
        Debug.Log(this.progress + " / " + goal + action);
        this.progress += progress;
        if (this.progress >= goal) 
            ChallengeCompleted();
        else 
            PlayerStats.saveFile();
    }

    public void setProcess(int process, string action)
    {
        if (this.action == action && process > this.progress) this.progress = process;
        if (this.progress >= goal) ChallengeCompleted();
    }

    public void skipChallange()
    {
    }

    private void ChallengeCompleted()
    {
        PlayerStats.Instance.ActivateChallenge();
        completed = true;
        isActive = false;
        //PlayerStats.Instance.challenges.Remove(this);
        PlayerStats.saveFile();
        GameManager.GameOver -= OnNewGame;
        GameManager.Instance.challengesCompleted.Enqueue(this);
        GameManager.Instance.ChallengeComplete(this);
        //claimReward(reward);
    }

    public override string ToString()
    {
        if(!oneRun)        
            return description.Replace("%s" , ""+goal);
        if((int)timeToComplete == 0)
            return description.Replace("%s" , ""+goal) + " In One Run!";
        return description.Replace("%s", "" + goal) + " In " + timeToComplete + " seconds!";
    }
}
