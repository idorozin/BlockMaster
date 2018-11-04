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
		if (PlayerStats.Instance.wheelTime == "")
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
		PlayerStats.Instance.wheelTime = TimeManager.Instance.getFullTime();
		PlayerStats.Instance.saveFile();
		Debug.Log(PlayerStats.Instance.wheelTime);
	}

	public void updateTime() // updates countDown with internet time
	{
		coolDown = (500) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.wheelTime));
	}

	IEnumerator CountDown()
	{
		while (coolDown > 0)
		{
			//TODO
			coolDown  = (500) - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.wheelTime)-2*60*60 + TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")));
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
		GameObject.Find("TimeText").GetComponent<Text>().text = "READY!";
		//validate 
		yield return TimeManager.Instance.StartCoroutine("getTime");
		if (coolDown <= 0 && GameObject.Find("Rollete")!=null && GameObject.Find("TimeText") != null)
		{
			GameObject.Find("Rollete").GetComponent<Button>().enabled = true;
			yield break;
		}
		//start timer again
		StartCoroutine("CountDown");

	}

	string secsToTime(int coolDown) // convert seconds to time format 00:00:00
	{
		return coolDown / 60 / 60 + ":" + coolDown / 60 % 60 + ":" + coolDown % 60;
	}

	//TODO: count - (timepassed  - timezonediff) ; 
	int timePassed()
	{
		int timepassed = (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.wheelTime)  + TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")));
		
		return 0;
	}

}
