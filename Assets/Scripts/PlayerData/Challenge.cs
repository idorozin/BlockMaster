
using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Challenge
{
    public string description;
    public string reward;
    public string action;
    public int goal;
    public int process;
    public int difficulty;
    public int level;
    public bool isActive;

    public Challenge(string description, int goal, string action, string reward)
    {
        this.description = description;
        this.goal = goal;
        this.action = action;
        this.reward = reward;
    }

    public void reportProcess(int process, string action)
    {
        if (this.action == action) this.process += process;
        if (this.process > goal) NextChallange();
    }

    public void setProcess(int process, string action)
    {
        if (this.action == action && process > this.process) this.process = process;
        if (this.process > goal) NextChallange();
    }

    public void skipChallange()
    {
    }

    public void NextChallange()
    {
        PlayerStats.Instance.ActivateChallenge();
        PlayerStats.Instance.challengeIndex++;
        isActive = false;
        //PlayerStats.Instance.challenges.Remove(this);
        pauseMenu.rewards.Push(this);
        PlayerStats.saveFile();
        //claimReward(reward);
    }

    public override string ToString()
    {
        return description;
    }
}
