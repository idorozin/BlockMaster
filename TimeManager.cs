﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	private string path="www.korystudios.com/realtime.php";
	private string time="";
	private int sec, min, hour, day , month , year;
	private DateTime baseDate,currentDate,dateTime;

	public static TimeManager Instance;
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			baseDate = new DateTime(1970,1,1,1,1,1);
			StartCoroutine("getTime");
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	//gets time from the server and updates countDown
	public IEnumerator getTime()
	{
		yield return StartCoroutine("WaitForInteretConnection");
		WWW www = new WWW(path);
		yield return www;
		time = www.text;
		Debug.Log(time);
		if(!string.IsNullOrEmpty(time))
		setCurrentTime();
		GameObject.Find("DailyRewards").GetComponent<DailyReward>().updateTime();
	}

	public void setCurrentTime() // get the value of current secs,mins,hours,days in second from the beggining of the month
	{
		string[] currentTime = time.Split(' ');
		string[] timePeriods = currentTime[1].Split(':');
		hour = int.Parse(timePeriods[0]);
		min = int.Parse(timePeriods[1]);
		sec = int.Parse(timePeriods[2]);
		timePeriods = currentTime[0].Split('-');
		Debug.Log(hour +":"+ min +":"+ sec);
		day = int.Parse(timePeriods[1]);
		month = int.Parse(timePeriods[0]);
		year = int.Parse(timePeriods[2]);
		currentDate = new DateTime(year,month,day,hour,min,sec);
	}

	public string getFullTime()
	{
		return time;
	}

	public int getTimeInSecs() // current time in seconds
	{
		return (int)(currentDate-baseDate).TotalSeconds;
	}
	public int getTimeInSecs(string time)
	{
		if (time == "")
			time = this.time;
		
		string[] currentTime = time.Split(' ');
		string[] timePeriods = currentTime[1].Split(':');
		int hour_ = int.Parse(timePeriods[0]);
		int min_ = int.Parse(timePeriods[1]);
		int sec_ = int.Parse(timePeriods[2]);
		timePeriods =  currentTime[0].Split('-');
		int day_ = int.Parse(timePeriods[1]);
		int month_ = int.Parse(timePeriods[0]);
		int year_ = int.Parse(timePeriods[2]);
		dateTime = new DateTime(year_,month_,day_,hour_,min_,sec_);
		return (int)(dateTime-baseDate).TotalSeconds;
	} // time(string) to seconds

	public bool isRelevent(int year , int month) // check if the year and the month are not that far away
	{
		return year == this.year+1 && month <= this.month+1;
	}
	
	private IEnumerator WaitForInteretConnection()
	{
		while (Application.internetReachability == NetworkReachability.NotReachable)
		{
			Debug.Log("waiting");
			yield return null;
		}
	}

}