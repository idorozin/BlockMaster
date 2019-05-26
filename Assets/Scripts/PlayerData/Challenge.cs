
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
    public int progress;
    public int difficulty;
    public int level;
    public bool isActive;
    public bool completed;
    public bool oneRun;

    public void Activate()
    {
        isActive = true;
        if (oneRun)
        {
            description += " In One Run!";
            GameManager.NewGame += OnNewGame;
        }
    }

    public void OnNewGame()
    {
        progress = 0;
    }

    public void ReportProgress(int progress, string action)
    {
        if (this.action == action) this.progress += progress;
        if (this.progress >= goal) ChallengeCompleted();
        else PlayerStats.saveFile();
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
        return description;
    }
}
