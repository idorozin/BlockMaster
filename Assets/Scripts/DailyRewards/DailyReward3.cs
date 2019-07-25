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
	private bool isRunning;

	public static int timeRemaining;
	
	private void Start()
	{
		if (initilized)
			return;
		initilized = true;
		coolDown = 2;
		if (string.IsNullOrEmpty(PlayerStats.Instance.challenge.startTime))
			PlayerStats.Instance.challenge.startTime = "06-01-2016 19:12:07";
		StartCoroutine("CountDown");
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
			timeRemaining = TimeRemaining();
			yield return new WaitForSecondsRealtime(1);
		}
		StartCoroutine("EnableButton");
	}

	// if countDown is over verify that with the server
	// true => button enabled false => button disabled and count down continouse with updated time. 
	IEnumerator EnableButton()
	{
		//validate 
		Debug.Log(coolDown);
		yield return TimeManager.Instance.StartCoroutine("getTime");
		Debug.Log(coolDown);
		if (coolDown <= 0)
		{
			PlayerStats.Instance.IncrementChallengesAvailable(Math.Max(1,Math.Abs((int)coolDown/(int)3600)));
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
