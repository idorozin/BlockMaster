using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward3: MonoBehaviour
{
	[SerializeField]
	private int countDownLenght=500,coolDown;
	private bool initilized=false;
	
	private void Start()
	{
		if (initilized)
			return;
		initilized = true;
		coolDown = 2;
		StartCoroutine("ResetTimer");
	}
	
	//reset the timer every roll
	public IEnumerator ResetTimer()
	{
		if (TimeManager.Instance.GetHtmlFromUri("http://google.com") == "")
			yield break;
		coolDown = countDownLenght;
		yield return TimeManager.Instance.StartCoroutine("getTime");
		PlayerStats.Instance.challenge.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.Instance.challenge.startTime = TimeManager.Instance.getFullTime();
		UpdateTime();
		PlayerStats.saveFile();
		StartCoroutine("CountDown");

		Debug.Log(PlayerStats.Instance.challenge.startTime);
	}

	public void UpdateTime() // updates countDown with internet time
	{
		coolDown = (countDownLenght) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.challenge.startTime));
	}

	IEnumerator CountDown()
	{
		while (coolDown > 0 || coolDown > countDownLenght+1)
		{
			coolDown = TimeRemaining();
			yield return new WaitForSecondsRealtime(1);
		}
		StartCoroutine("EnableButton");
	}

	// if countDown is over verify that with the server
	// true => button enabled false => button disabled and count down continouse with updated time. 
	IEnumerator EnableButton()
	{
		//validate 
		yield return TimeManager.Instance.StartCoroutine("getTime");
		if (coolDown <= 0)
		{
			PlayerStats.Instance.IncrementChallengesAvailable(1);
			StartCoroutine("ResetTimer");
			yield break;
		}
		PlayerStats.Instance.challenge.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.saveFile();
		//start timer again
		StartCoroutine("CountDown");

	}

	public string SecsToTime() // convert seconds to time format 00:00:00
	{
		return coolDown / 60 / 60 + ":" + coolDown / 60 % 60 + ":" + coolDown % 60;
	}
		

	int TimeRemaining()
	{
		return countDownLenght - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.challenge.startTime) + 
		                  TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"))) + PlayerStats.Instance.challenge.offset;
	}


}
