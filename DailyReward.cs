using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward: MonoBehaviour
{
	[SerializeField]
	private int coolDown=500;

	public void callStart()
	{
		if (PlayerStats.wheelTime == "")
		{
			GameObject.Find("TimeText").GetComponent<Text>().text = "READY!";
			GameObject.Find("Rollete").GetComponent<Button>().enabled = true;
			return;
		}
		StartCoroutine("CountDown");
	}

	public IEnumerator resetTimer()
	{
		yield return TimeManager.Instance.StartCoroutine("getTime");
		coolDown = 500;
		StartCoroutine("CountDown");
		PlayerStats.wheelTime = TimeManager.Instance.getFullTime();
		GameObject.Find("PlayerStats").GetComponent<updatePlayerStats>().saveFile();
		Debug.Log(PlayerStats.wheelTime);
	}

	public void updateTime()
	{
		coolDown = (500) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.wheelTime));
	}

	IEnumerator CountDown()
	{
		while (coolDown > 0)
		{
			int g = timePassed();
			coolDown  = (500) - (-TimeManager.Instance.getTimeInSecs(PlayerStats.wheelTime)-2*60*60 + TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")));
			if(GameObject.Find("TimeText") != null && coolDown > 0)
			GameObject.Find("TimeText").GetComponent<Text>().text = secsToTime(coolDown);
			yield return new WaitForSeconds(1);
		}
		StartCoroutine("enableButton");
	}

	IEnumerator enableButton()
	{
		GameObject.Find("TimeText").GetComponent<Text>().text = "READY!";
		yield return TimeManager.Instance.StartCoroutine("getTime");
		if (coolDown <= 0 && GameObject.Find("Rollete")!=null && GameObject.Find("TimeText") != null)
		{
			GameObject.Find("Rollete").GetComponent<Button>().enabled = true;
			yield break;
		}

		StartCoroutine("CountDown");

	}

	string secsToTime(int coolDown)
	{
		return coolDown / 60 / 60 + ":" + coolDown / 60 % 60 + ":" + coolDown % 60;
	}

	//TODO: count - (timepassed  - timezonediff) ; 
	int timePassed()
	{
		int timepassed = (-TimeManager.Instance.getTimeInSecs(PlayerStats.wheelTime)  + TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")));
		
		return 0;
	}

}
