using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DailyReward2: MonoBehaviour
{
	[SerializeField]
	private int countDownLenght=500,coolDown;

	[SerializeField] private GameObject TimeText, Button;
	public static string timeText;
	public static bool run = false , GiftAllowed=false;
	private bool initilized=false;

	private void Start()
	{
		if(initilized)
			return;
		Debug.Log(PlayerStats.Instance.giftTime + "GIFT");
		if (PlayerStats.Instance.giftTime == "")
		{
			TimeText.GetComponent<Text>().text = "READY!";
			GiftAllowed = true;
			return;
		}
		initilized = true;
		coolDown = 2;
		StartCoroutine("CountDown");
	}
	
	//reset the timer every roll
	public IEnumerator resetTimer()
	{
		Debug.Log("Daily Reward 2  - Reset Timer");
		yield return TimeManager.Instance.StartCoroutine("getTime");
		PlayerStats.Instance.offsetG = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		coolDown = countDownLenght;
		PlayerStats.Instance.giftTime = TimeManager.Instance.getFullTime();
		PlayerStats.saveFile();
		StartCoroutine("CountDown");

		Debug.Log(PlayerStats.Instance.giftTime);
	}

	public void updateTime() // updates countDown with internet time
	{
		coolDown = (countDownLenght) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.giftTime));
	}

	IEnumerator CountDown()
	{
		run = true;
		while (coolDown > 0 || coolDown > countDownLenght+1)
		{
			coolDown = timeRemaining();
			if(TimeText != null && coolDown > 0)
			TimeText.GetComponent<Text>().text = secsToTime();
			timeText = secsToTime();
			yield return new WaitForSecondsRealtime(1);
		}
		StartCoroutine("enableButton");
	}

	// if countDown is over verify that with the server
	// true => button enabled false => button disabled and count down continues with updated time. 
	IEnumerator enableButton()
	{
		if(TimeText!=null){TimeText.GetComponent<Text>().text = "READY!";timeText = "READY!";}
		//validate 
		if (TimeManager.Instance.GetHtmlFromUri("http://google.com") == "")
			yield break;
		Debug.Log("Daily Reward 2  - enable button");
		yield return TimeManager.Instance.StartCoroutine("getTime");
		if (coolDown <= 0)
		{
			Debug.Log("Daily Reward 2 allowed");
			GiftAllowed = true;
			yield break;
		}
		PlayerStats.Instance.offsetG = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.saveFile();
		//start timer again
		StartCoroutine("CountDown");
	}

	public string secsToTime() // convert seconds to time format 00:00:00
	{
		return coolDown / 60 / 60 + ":" + coolDown / 60 % 60 + ":" + coolDown % 60;
	}
		

	int timeRemaining()
	{
		return countDownLenght - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.giftTime) + 
		                  TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"))) + PlayerStats.Instance.offsetG;
	}


}
