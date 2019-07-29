using System.Collections;
using UnityEngine;
using System;
using Assets.SimpleAndroidNotifications;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DailyReward2: MonoBehaviour
{
	[SerializeField]
	private int countDownLenght=500,coolDown,resetTime;

	[SerializeField] private GameObject TimeText, Button;
	public static string timeText;
	public static bool run = false , GiftAllowed=false;
	private bool initilized=false;

	private void Start()
	{
		if(initilized)
			return;
		Debug.Log(PlayerStats.Instance.gift.startTime + "GIFT");
		if (PlayerStats.Instance.gift.startTime == DateTime.MinValue)
		{
			TimeText.GetComponent<TextMeshProUGUI>().text = "READY!";
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
		NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(countDownLenght) , "Block Master" , "Your daily reward is ready!" , Color.cyan);
		yield return TimeManager.Instance.StartCoroutine("getTime");
		PlayerStats.Instance.gift.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now) - TimeManager.Instance.getTimeInSecs();
		coolDown = countDownLenght;
		PlayerStats.Instance.gift.startTime = TimeManager.Instance.GetFullTime();
		PlayerStats.saveFile();
		StartCoroutine("CountDown");
	}

	public void updateTime() // updates countDown with internet time
	{
		coolDown = (countDownLenght) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.gift.startTime));
	}

	IEnumerator CountDown()
	{
		run = true;
		Debug.Log(timeRemaining());
		while (coolDown > 0 || coolDown > countDownLenght+1)
		{
			coolDown = timeRemaining();
			if(TimeText == null && SceneManager.GetActiveScene().name == "MainMenu")
				TimeText = GameObject.Find("Timer");
			if(TimeText != null && coolDown > 0)
			TimeText.GetComponent<TextMeshProUGUI>().text = secsToTime();
			timeText = secsToTime();
			yield return new WaitForSecondsRealtime(1);
		}
		StartCoroutine("enableButton");
	}

	// if countDown is over verify that with the server
	// true => button enabled false => button disabled and count down continues with updated time. 
	IEnumerator enableButton()
	{
		if(TimeText!=null){TimeText.GetComponent<TextMeshProUGUI>().text = "READY!";timeText = "READY!";}
		//validate 
		if (TimeManager.Instance.GetHtmlFromUri("http://google.com") == "")
			yield break;
		yield return TimeManager.Instance.StartCoroutine("getTime");
		if (coolDown <= 0)
		{
			if (Math.Abs(coolDown) > resetTime)
				PlayerStats.Instance.GiftIndex = 0;
			GiftAllowed = true;
			yield break;
		}
		PlayerStats.Instance.gift.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.saveFile();
		//start timer again
		StartCoroutine("CountDown");
	}

	public string secsToTime() // convert seconds to time format 00:00:00
	{
		TimeSpan t = TimeSpan.FromSeconds(coolDown);

		//here backslash is must to tell that colon is
		//not the part of format, it just a character that we want in output
		
		string answer = string.Format("{0:D2}:{1:D2}:{2:D2}", 
			t.Hours, 
			t.Minutes, 
			t.Seconds);
		return answer;
	}
		

	int timeRemaining()
	{
		return countDownLenght - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.gift.startTime) + 
		                  TimeManager.Instance.getTimeInSecs(DateTime.Now)) + PlayerStats.Instance.gift.offset;
	}


}
