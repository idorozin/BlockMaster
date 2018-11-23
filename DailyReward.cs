using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward: MonoBehaviour
{
	[SerializeField]
	private int countDownLenght=500,coolDown;


	public void callStart()
	{
	if (PlayerStats.Instance.wheelTime == "")
		{
			GameObject.Find("TimeText").GetComponent<Text>().text = "READY!";
			GameObject.Find("Rollete").GetComponent<Button>().enabled = true;
			return;
		}
		StartCoroutine("CountDown");
	}
	
	//reset the timer every roll
	public IEnumerator resetTimer()
	{
		yield return TimeManager.Instance.StartCoroutine("getTime");
		PlayerStats.Instance.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		coolDown = countDownLenght;
		PlayerStats.Instance.wheelTime = TimeManager.Instance.getFullTime();
		PlayerStats.Instance.saveFile();
		StartCoroutine("CountDown");

		Debug.Log(PlayerStats.Instance.wheelTime);
	}

	public void updateTime() // updates countDown with internet time
	{
		coolDown = (countDownLenght) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.wheelTime));
	}

	IEnumerator CountDown()
	{
		while (coolDown > 0 || coolDown > countDownLenght+1)
		{
			coolDown = timePassed();
			if(GameObject.Find("TimeText") != null && coolDown > 0)
			GameObject.Find("TimeText").GetComponent<Text>().text = secsToTime(coolDown);
			yield return new WaitForSeconds(1);
		}

		StartCoroutine("enableButton");
	}

	// if countDown is over check if verify that with the server
	// true => button enabled false => button disabled and count down continouse with updated time. 
	IEnumerator enableButton()
	{
		if(GameObject.Find("TimeText")!=null)GameObject.Find("TimeText").GetComponent<Text>().text = "READY!";
		//validate 
		while ((Application.internetReachability == NetworkReachability.NotReachable))
			Debug.Log("no internet connection");
		yield return TimeManager.Instance.StartCoroutine("getTime");
		if (coolDown <= 0 && GameObject.Find("Rollete")!=null && GameObject.Find("TimeText") != null)
		{
			GameObject.Find("Rollete").GetComponent<Button>().enabled = true;
			yield break;
		}
		PlayerStats.Instance.offset = TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")) - TimeManager.Instance.getTimeInSecs();
		PlayerStats.Instance.saveFile();
		//start timer again
		StartCoroutine("CountDown");

	}

	 string secsToTime(int coolDown) // convert seconds to time format 00:00:00
	{
		return coolDown / 60 / 60 + ":" + coolDown / 60 % 60 + ":" + coolDown % 60;
	}
	
	

	int timePassed()
	{
		return countDownLenght - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.wheelTime) + 
		                  TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"))) + PlayerStats.Instance.offset;
	}

	public string getCoolDown()
	{
		return secsToTime(coolDown);
	}

}
