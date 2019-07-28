using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleAndroidNotifications;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
			TimeText.GetComponent<TextMeshProUGUI>().text = "READY!";
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
		NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(countDownLenght) , "Block Master" , "Your daily reward is ready!" , Color.cyan);
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
		while (coolDown > 0 || coolDown > countDownLenght+1)
		{
			coolDown = TimeRemaining();
			if(TimeText == null && SceneManager.GetActiveScene().name == "MainMenu")
				TimeText = GameObject.Find("Timer2");
			if(TimeText != null && coolDown > 0)
			TimeText.GetComponent<TextMeshProUGUI>().text = SecsToTime();
			timeText = SecsToTime();
			yield return new WaitForSecondsRealtime(1);
		}
		StartCoroutine("EnableButton");
	}

	// if countDown is over verify that with the server
	// true => button enabled false => button disabled and count down continouse with updated time. 
	IEnumerator EnableButton()
	{
		if(TimeText!=null){TimeText.GetComponent<TextMeshProUGUI>().text = "READY!";timeText = "READY!";}
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
