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

	private void Start()
	{
		if (PlayerStats.Instance.playerStats.wheelTime == "")
		{
			TimeText.GetComponent<Text>().text = "READY!";
			RollAllowed = true;
			return;
		}

		coolDown = 2;
		StartCoroutine("CountDown");
	}
	
	//reset the timer every roll
	public IEnumerator resetTimer()
	{
		Debug.Log("reset");
		coolDown = countDownLenght;
		yield return TimeManager.Instance.StartCoroutine("getTime");
		PlayerStats.Instance.playerStats.offsetW = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.Instance.playerStats.wheelTime = TimeManager.Instance.getFullTime();
		PlayerStats.Instance.saveFile();
		StartCoroutine("CountDown");

		Debug.Log(PlayerStats.Instance.playerStats.wheelTime);
	}

	public void updateTime() // updates countDown with internet time
	{
		Debug.Log("updateTime " + TimeManager.Instance.getTimeInSecs());
		coolDown = (countDownLenght) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.playerStats.wheelTime));
	}

	IEnumerator CountDown()
	{
		while (!DailyReward2.run)
		{
			yield return null;
		}
		while (coolDown > 0 || coolDown > countDownLenght+1)
		{
			coolDown = timeRemaining();
			if(TimeText != null && coolDown > 0)
			TimeText.GetComponent<Text>().text = secsToTime(coolDown);
			timeText = secsToTime(coolDown);
			yield return new WaitForSeconds(1);
		}
		StartCoroutine("enableButton");
	}

	// if countDown is over check if verify that with the server
	// true => button enabled false => button disabled and count down continouse with updated time. 
	IEnumerator enableButton()
	{
		Debug.Log("enable");

		if(TimeText!=null){TimeText.GetComponent<Text>().text = "READY!";timeText = secsToTime(coolDown);}
		//validate 
		yield return TimeManager.Instance.StartCoroutine("getTime");
		if (coolDown <= 0 && TimeText != null)
		{
			RollAllowed = true;
			yield break;
		}
		PlayerStats.Instance.playerStats.offsetW = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.Instance.saveFile();
		//start timer again
		StartCoroutine("CountDown");

	}

	public string secsToTime(int coolDown) // convert seconds to time format 00:00:00
	{
		return coolDown / 60 / 60 + ":" + coolDown / 60 % 60 + ":" + coolDown % 60;
	}
		

	int timeRemaining()
	{
		return countDownLenght - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.playerStats.wheelTime) + 
		                  TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"))) + PlayerStats.Instance.playerStats.offsetW;
	}


}
