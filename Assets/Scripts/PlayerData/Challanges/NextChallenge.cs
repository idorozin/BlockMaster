using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextChallenge : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI text;
	void Start ()
	{
		if(DailyReward3.timeRemaining > 1000)
			StartCoroutine(CountDown(DailyReward3.timeRemaining));	
	}

	private int countDown = 0;
	IEnumerator CountDown(int timeRemaining)
	{
		countDown = timeRemaining;
		Debug.Log(countDown);
		while (countDown > 0)
		{
			yield return new WaitForSecondsRealtime(1f);
			countDown--;
			text.text = "next challenge in : " + TimeManager.SecsToTime(countDown);
		}
		SceneManager.LoadScene("GameScene");
	}

	[SerializeField] private GameObject ad;
	
	public void Ad()
	{
		if (AdManager.Instance.CanPlayRewarded())
		{
			AdManager.Instance.ShowRewarded(HandleReward);
		}
	}

	void HandleReward(object sender , EventArgs e)
	{
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
