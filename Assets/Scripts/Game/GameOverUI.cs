using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("EndScreen")]
    [SerializeField] 
    private GameObject share;
    [SerializeField] 
    private GameObject double_;
    [SerializeField] 
    private GameObject cannon;
    [SerializeField] 
    private GameObject tip;
    [SerializeField] 
    private GameObject others;
    [SerializeField] 
    private TextMeshProUGUI record, coins , tipText;    
    [Header("challenges")]
    [SerializeField] 
    private GameObject challengesDisplay;

    [SerializeField] private Tips tips;


    private NativeShare nativeShare;

    private void Start()
    {
        if (GameManager.Instance.challengesCompleted.Count > 0)
        {
            challengesDisplay.SetActive(true);
        }
        else
        {
            SetEndGameUI();
        }
    }

    private void SetEndGameUI()
    {
        others.SetActive(true);
        nativeShare = GetComponent<NativeShare>();
        int count = 0;
        if (GameManager.Instance.recordBroke)
        {
            share.SetActive(true);
            record.text = PlayerStats.Instance.highScore + "(+" + (PlayerStats.Instance.highScore - GameManager.Instance.oldRecord) + ")";
            count++;
        }
        if (AssetDatabase.Instance.CanBuyItem())
        {
            cannon.SetActive(true);
            count++;
        }
        if (GameManager.Instance.score > 50)
        {
            double_.SetActive(true);
            coins.text = "(" + GameManager.Instance.score + ")"; 
            count++;
        }
        if (count < 3)
        {
            tips = (Tips)Resources.Load("Tips");
            tipText.text = tips.GetRandomSentence();
            tip.SetActive(true);
        }
    }

    public void Share()
    {
        nativeShare.ShareButtonPress();
    } 
    
    public void Double()
    {
        AdManager.Instance.ShowRewarded(WatchedAd);
    }

    private void WatchedAd(object sender , EventArgs e)
    {
        PlayerStats.Instance.gold += (int)GameManager.Instance.score;
    }

    public void Buy()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Continue()
    {
        challengesDisplay.SetActive(false);
        SetEndGameUI();
    }

}
