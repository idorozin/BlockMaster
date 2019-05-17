
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
            GameManager.GameOver += OnGameOver;
    }

    public void OnGameOver()
    {
        progress = 0;
    }

    public void ReportProgress(int progress, string action)
    {
        if (this.action == action) this.progress += progress;
        if (this.progress > goal) NextChallange();
    }

    public void setProcess(int process, string action)
    {
        if (this.action == action && process > this.progress) this.progress = process;
        if (this.progress > goal) NextChallange();
    }

    public void skipChallange()
    {
    }

    public void NextChallange()
    {
        PlayerStats.Instance.ActivateChallenge();
        PlayerStats.Instance.challengeIndex++;
        completed = true;
        isActive = false;
        //PlayerStats.Instance.challenges.Remove(this);
        PauseMenu.rewards.Enqueue(this);
        PlayerStats.saveFile();
        GameManager.GameOver -= OnGameOver;
        //claimReward(reward);
    }

    public override string ToString()
    {
        return description;
    }
}
