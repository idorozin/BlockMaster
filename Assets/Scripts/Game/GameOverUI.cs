using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using MyAsset;
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
    [SerializeField] 
    private TextMeshProUGUI gold;
    [SerializeField] 
    private TextMeshProUGUI score;

    [Space] 
    [SerializeField] 
    private GameObject watchAd;
    [SerializeField] 
    private GameObject doubled;
    
    [Header("Challenges")]
    [SerializeField] 
    private GameObject challengesPanel;   
    [SerializeField] 
    private GameObject challengesDisplay;

    [SerializeField] private Tips tips;




    private NativeShare nativeShare;
    


    private void OnEnable()
    {
        StartCoroutine(WaitForAdToFinish());
    }

    
    private IEnumerator WaitForAdToFinish()
    {
        while (PauseMenu.AdPlaying)
            yield return null;
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
        
        StartCoroutine(SetMoney());
        score.text=GameManager.Instance.fixedScore.ToString();
        
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
        if (GameManager.Instance.score > 50 && AdManager.Instance.CanPlayRewarded())
        {
            double_.SetActive(true);
            coins.text = "(" + GameManager.Instance.goldEarned + ")";
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

    private bool once_;
    public void Double()
    {
        if (once_) return;
        once_ = true;
        if (AdManager.Instance.CanPlayRewarded())
        {
            AdManager.Instance.rewardedAd.OnUserEarnedReward += WatchedAd;
            AdManager.Instance.ShowRewarded();
        }
    }

    private void WatchedAd(object sender , EventArgs e)
    {
        AdManager.Instance.rewardedAd.OnUserEarnedReward -= WatchedAd;
        PlayerStats.Instance.gold += GameManager.Instance.goldEarned;
        watchAd.SetActive(false);
        doubled.SetActive(true);
        StartCoroutine(SetMoney(0f));
    }

    public void Buy()
    {
        SceneManager.LoadScene("Shop");
    }

    private bool once;
    public void Continue()
    {
        if (once) return;
        StartCoroutine(Delay());
        once = true;
    }

    private IEnumerator SetMoney(float delay = 1f)
    {
        var previousWallet = PlayerStats.Instance.gold - GameManager.Instance.goldEarned;
        gold.text = previousWallet + "";
        if (GameManager.Instance.goldEarned == 0)
            yield break;
        yield return new WaitForSecondsRealtime(delay);
        AudioManager.Instance.PlaySound(AudioManager.SoundName.coins);
        gold.gameObject.GetComponent<ScrollingText>().SetNum(PlayerStats.Instance.gold , previousWallet);
    }

    private IEnumerator Delay()
    {
        challengesPanel.GetComponent<AnimatedLayout>().Ondisable();
        yield return new WaitForSecondsRealtime(1f);
        SetEndGameUI();
    }

}
