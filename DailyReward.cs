using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward: MonoBehaviour
{
	[SerializeField]
	private int coolDown=500;

	private int offset;
	private int minCoolDown=500;

	public void callStart()
	{
		Debug.Log(PlayerStats.Instance.wheelTime);
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
		coolDown = 500;
		minCoolDown = 500;
		PlayerStats.Instance.wheelTime = TimeManager.Instance.getFullTime();
		PlayerStats.Instance.saveFile();
		StartCoroutine("CountDown");

		Debug.Log(PlayerStats.Instance.wheelTime);
	}

	public void updateTime() // updates countDown with internet time
	{
		coolDown = (500) - (TimeManager.Instance.getTimeInSecs() - TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.wheelTime));
		minCoolDown = coolDown;
	}

	IEnumerator CountDown()
	{
		while (coolDown > 0)
		{
			coolDown = timePassed();
			if (minCoolDown >= coolDown)
			{
				minCoolDown = coolDown;
			}
			else
			{
				coolDown = 0;
			}
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
		return 500 - (-TimeManager.Instance.getTimeInSecs(PlayerStats.Instance.wheelTime) + 
		                  TimeManager.Instance.getTimeInSecs(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"))) + PlayerStats.Instance.offset;
	}

}
