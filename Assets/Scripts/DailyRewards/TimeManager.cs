﻿using System;
using System.Collections;
using System.Collections.Generic;
 using System.IO;
 using System.Net;
 using System.Runtime.InteropServices;
using UnityEngine;
 using UnityEngine.SceneManagement;

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
		WWW www = new WWW(path);
		yield return www;
		time = www.text;
		Debug.Log(time);
		if(!string.IsNullOrEmpty(time))
			setCurrentTime();
		if(SceneManager.GetActiveScene().name == "MenuScene")
			GetComponent<DailyReward>().UpdateTime();
		GetComponent<DailyReward2>().updateTime();
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


	//use this to be sure there is internet connection
	public string GetHtmlFromUri(string resource)
	{
		string html = string.Empty;
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
		try
		{
			using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
			{
				bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
				if (isSuccess)
				{
					using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
					{
						//We are limiting the array to 80 so we don't have
						//to parse the entire html document feel free to 
						//adjust (probably stay under 300)
						char[] cs = new char[80];
						reader.Read(cs, 0, cs.Length);
						foreach(char ch in cs)
						{
							html +=ch;
						}
					}
				}
			}
		}
		catch
		{
			return "";
		}
		return html;
	}

	
}