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
    private GameObject challengesPanel;   
    [SerializeField] 
    private GameObject challengesDisplay;

    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private Tips tips;




    private NativeShare nativeShare;
    


    private void OnEnable()
    {
/*        GameObject go = (GameObject)Instantiate(scrollingText,transform);
        go.transform.position = scrollingText.transform.position;
        go.GetComponent<ScrollingText>().SetNum(20);*/
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

    private bool once = false;
    public void Continue()
    {
        if (!once)
        {
            StartCoroutine(Delay());
            once = true;
        }
    }

    private IEnumerator SetMoney()
    {
        score.text=GameManager.Instance.score.ToString();
        gold.text = PlayerStats.Instance.gold - GameManager.Instance.goldEarned + "";
        yield return new WaitForSecondsRealtime(1f);
        AudioManager.Instance.PlaySound(AudioManager.SoundName.coins);
        gold.gameObject.GetComponent<ScrollingText>().SetNum(PlayerStats.Instance.gold , PlayerStats.Instance.gold - GameManager.Instance.goldEarned);
    }

    private IEnumerator Delay()
    {
        challengesPanel.GetComponent<AnimatedLayout>().Ondisable();
        yield return new WaitForSecondsRealtime(1f);
        SetEndGameUI();
    }

}
