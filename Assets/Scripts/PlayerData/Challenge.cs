
using System;
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

    public Challenge(string description, int goal, string action, int reward)
    {
        this.description = description;
        this.goal = goal;
        this.action = action;
        this.reward = reward;
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
        //claimReward(reward);
    }

    public override string ToString()
    {
        return description;
    }
}
