using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextChallenge : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI text;
	void Start ()
	{
		if(DailyReward3.timeRemaining > 1000 && AdManager.Instance.CanPlayRewarded())
			ad.SetActive(true);
		StartCoroutine(CountDown(DailyReward3.timeRemaining));	
	}

	private int countDown = 0;
	IEnumerator CountDown(int timeRemaining)
	{
		countDown = timeRemaining;
		while (countDown > 0)
		{
			yield return new WaitForSecondsRealtime(1f);
			countDown--;
			text.text = "next challenge in : " + ExtensionMethods.SecsToTime(countDown);
		}
		SceneManager.LoadScene("GameScene");
	}

	[SerializeField] private GameObject ad;
	
	public void Ad()
	{
		if (AdManager.Instance.CanPlayRewarded())
		{
			AdManager.Instance.rewardedAd.OnUserEarnedReward += HandleReward;
			AdManager.Instance.ShowRewarded();
		}
	}

	void HandleReward(object sender , EventArgs e)
	{
		AdManager.Instance.rewardedAd.OnUserEarnedReward -= HandleReward;
		StopAllCoroutines();
		DailyReward3 dr = FindObjectOfType<DailyReward3>();
		if (dr != null)
		{
			dr.StopAllCoroutines();
			StartCoroutine(dr.ResetTimer());
		}
		PlayerStats.Instance.IncrementChallengesAvailable(1);
		SceneManager.LoadScene("GameScene");
	}

}
