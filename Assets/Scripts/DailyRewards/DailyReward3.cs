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
		if (PlayerStats.Instance.challenge.startTime == DateTime.MinValue)
		{
			PlayerStats.Instance.challenge.startTime = new DateTime(2010 , 10 ,1);
			Debug.Log(PlayerStats.Instance.challenge.startTime);
		}

		StartCoroutine("CountDown");
	}
	
	//reset the timer every roll
	public IEnumerator ResetTimer()
	{
		if (TimeManager.Instance.GetHtmlFromUri("http://google.com") == "")
			yield break;
		coolDown = countDownLenght;
		yield return TimeManager.Instance.StartCoroutine("getTime");
		PlayerStats.Instance.challenge.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.Instance.challenge.startTime = TimeManager.Instance.GetFullTime();
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
		PlayerStats.Instance.challenge.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.saveFile();
		//start timer again
		StartCoroutine("CountDown");

	}

	int TimeRemaining()
	{
		return countDownLenght - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.challenge.startTime) + 
		                  TimeManager.Instance.getTimeInSecs(DateTime.Now)) + PlayerStats.Instance.challenge.offset;
	}


}
