using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward: MonoBehaviour
{
	[SerializeField]
	private int countDownLenght=500,coolDown;
	public static bool RollAllowed=false;
	public static string timeText;
	[SerializeField] private GameObject TimeText, Button;
	private bool initilized=false;
	
	private void Start()
	{
		if (initilized)
			return;
		if (PlayerStats.Instance.wheel.startTime == "")
		{
			TimeText.GetComponent<Text>().text = "READY!";
			RollAllowed = true;
			return;
		}
		initilized = true;
		coolDown = 2;
		StartCoroutine("CountDown");
	}
	
	//reset the timer every roll
	public IEnumerator ResetTimer()
	{
		coolDown = countDownLenght;
		yield return TimeManager.Instance.StartCoroutine("getTime");
		PlayerStats.Instance.wheel.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.Instance.wheel.startTime = TimeManager.Instance.getFullTime();
		PlayerStats.saveFile();
		StartCoroutine("CountDown");

		Debug.Log(PlayerStats.Instance.wheel.startTime);
	}

	public void UpdateTime() // updates countDown with internet time
	{
		coolDown = (countDownLenght) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.wheel.startTime));
	}

	IEnumerator CountDown()
	{
		while (!DailyReward2.run)
		{
			yield return null;
		}
		while (coolDown > 0 || coolDown > countDownLenght+1)
		{
			coolDown = TimeRemaining();
			if(TimeText != null && coolDown > 0)
			TimeText.GetComponent<Text>().text = SecsToTime();
			timeText = SecsToTime();
			yield return new WaitForSecondsRealtime(1);
		}
		StartCoroutine("EnableButton");
	}

	// if countDown is over verify that with the server
	// true => button enabled false => button disabled and count down continouse with updated time. 
	IEnumerator EnableButton()
	{
		if(TimeText!=null){TimeText.GetComponent<Text>().text = "READY!";timeText = "READY";}
		//validate 
		yield return TimeManager.Instance.StartCoroutine("getTime");
		if (coolDown <= 0)
		{
			RollAllowed = true;
			yield break;
		}
		PlayerStats.Instance.wheel.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
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
		return countDownLenght - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.wheel.startTime) + 
		                  TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"))) + PlayerStats.Instance.wheel.offset;
	}


}
