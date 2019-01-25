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
	private void Start()
	{
		Debug.Log(PlayerStats.Instance.playerStats.giftTime + "GIFT");
		if (PlayerStats.Instance.playerStats.giftTime == "")
		{
			TimeText.GetComponent<Text>().text = "READY!";
			GiftAllowed = true;
			return;
		}

		coolDown = 2;
		StartCoroutine("CountDown");
	}
	
	//reset the timer every roll
	public IEnumerator resetTimer()
	{
		yield return TimeManager.Instance.StartCoroutine("getTime");
		PlayerStats.Instance.playerStats.offsetG = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		coolDown = countDownLenght;
		PlayerStats.Instance.playerStats.giftTime = TimeManager.Instance.getFullTime();
		PlayerStats.Instance.saveFile();
		StartCoroutine("CountDown");

		Debug.Log(PlayerStats.Instance.playerStats.giftTime);
	}

	public void updateTime() // updates countDown with internet time
	{
		coolDown = (countDownLenght) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.playerStats.giftTime));
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
			yield return new WaitForSeconds(1);
		}
		StartCoroutine("enableButton");
	}

	// if countDown is over check if verify that with the server
	// true => button enabled false => button disabled and count down continouse with updated time. 
	IEnumerator enableButton()
	{
		if(TimeText!=null){TimeText.GetComponent<Text>().text = "READY!";timeText = "READY!";}
		//validate 
		if (TimeManager.Instance.GetHtmlFromUri("http://google.com") == "")
			yield break;
		yield return TimeManager.Instance.StartCoroutine("getTime");
		if (coolDown <= 0 && TimeText != null)
		{
			GiftAllowed = true;
			yield break;
		}
		PlayerStats.Instance.playerStats.offsetG = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.Instance.saveFile();
		//start timer again
		StartCoroutine("CountDown");
	}

	 public string secsToTime() // convert seconds to time format 00:00:00
	{
		return coolDown / 60 / 60 + ":" + coolDown / 60 % 60 + ":" + coolDown % 60;
	}
		

	int timeRemaining()
	{
		return countDownLenght - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.playerStats.giftTime) + 
		                  TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"))) + PlayerStats.Instance.playerStats.offsetG;
	}


}
