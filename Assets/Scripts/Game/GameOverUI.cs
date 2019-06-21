using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("EndScreen")]
    [SerializeField] 
    private GameObject share, double_, cannon, tip , others;
    [SerializeField] 
    private TextMeshProUGUI record, coins , tipText;    
    [Header("challenges")]
    [SerializeField] 
    private GameObject challengesDisplay;


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
        Debug.Log("Set End...");
        tipText.text = "nice tip";
        nativeShare = GetComponent<NativeShare>();
        int count = 0;
        if (GameManager.Instance.recordBroke)
        {
            share.SetActive(true);
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
            tip.SetActive(true);
    }

    public void Share()
    {
        nativeShare.ShareButtonPress();
    } 
    
    public void Double()
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
