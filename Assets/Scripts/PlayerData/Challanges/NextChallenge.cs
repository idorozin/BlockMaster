using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using admob;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextChallenge : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI text;
	void Start ()
	{
		if(ActivateChallenge.timeRemaining > 1000 && AdManager.Instance.CanPlayRewarded())
			ad.SetActive(true);
		StartCoroutine(CountDown(ActivateChallenge.timeRemaining));
		ActivateChallenge.ChallengeActivated += Reload;
	}

	private void OnDisable()
	{
		ActivateChallenge.ChallengeActivated -= Reload;
	}

	void Reload()
	{
		SceneManager.LoadScene("GameScene");

	}

	private int countDown = 0;
	IEnumerator CountDown(int timeRemaining)
	{
		countDown = timeRemaining;
		while (countDown > 0)
		{
			text.text = "next challenge in : " + ExtensionMethods.SecsToTime(countDown);
			yield return new WaitForSecondsRealtime(1f);
			countDown--;
		}
	}

	[SerializeField] private GameObject ad;
	
	public void Ad()
	{
		if (AdManager.Instance.CanPlayRewarded())
		{
			AdManager.Instance.ad.rewardedVideoEventHandler += HandleReward;
			AdManager.Instance.ShowRewarded();
		}
	}

	void HandleReward(string eventName , string msg)
	{
		if(eventName != AdmobEvent.onRewarded)
			return;
		AdManager.Instance.ad.rewardedVideoEventHandler -= HandleReward;
		StopAllCoroutines();
		ActivateChallenge dr = FindObjectOfType<ActivateChallenge>();
		if (dr != null)
		{
			dr.StopAllCoroutines();
			StartCoroutine(dr.ResetTimer());
		}
		PlayerStats.Instance.IncrementChallengesAvailable(1);
		SceneManager.LoadScene("GameScene");
	}

}
