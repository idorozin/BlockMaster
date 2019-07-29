using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleAndroidNotifications;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class DailyReward_ : MonoBehaviour
{
	[SerializeField]
	private int countDownLenght=500,coolDown;
	private TimePassed timePassed = new TimePassed();
	[SerializeField] private GameObject TimeText, Button;
	private bool initilized=false;
	public static string timeText;


	
	private void Start()
	{
		if (initilized)
			return;
		if (timePassed.startTime == DateTime.MinValue)
		{
			TimeText.GetComponent<TextMeshProUGUI>().text = "READY!";
			return;
		}
		initilized = true;
		coolDown = 2;
		StartCoroutine("CountDown");
	}
	
	//reset the timer every roll
	public IEnumerator ResetTimer()
	{
		NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(countDownLenght) , "Block Master" , "Wheel Of Fortune is ready , come try your luck!" , Color.cyan);
		coolDown = countDownLenght;
		yield return TimeManager.Instance.StartCoroutine("getTime");
		timePassed.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now) - TimeManager.Instance.getTimeInSecs();
		timePassed.startTime = TimeManager.Instance.GetFullTime();
		PlayerStats.saveFile();
		StartCoroutine(CountDown());
	}

	public void UpdateTime() // updates countDown with internet time
	{
		coolDown = (countDownLenght) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(timePassed.startTime));
	}

	IEnumerator CountDown()
	{
		while (coolDown > 0 || coolDown > countDownLenght+1)
		{
			coolDown = TimeRemaining();
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
			OnTimePassed();
			yield break;
		}
		timePassed.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.saveFile();
		//start timer again
		StartCoroutine("CountDown");

	}

	protected abstract void OnTimePassed();


	int TimeRemaining()
	{
		return countDownLenght - (-TimeManager.Instance.getTimeInSecs(timePassed.startTime) + 
		                          TimeManager.Instance.getTimeInSecs(DateTime.Now)) + timePassed.offset;
	}

	public string SecsToTime() // convert seconds to time format 00:00:00
	{
		return coolDown / 60 / 60 + ":" + coolDown / 60 % 60 + ":" + coolDown % 60;
	}
		




}
