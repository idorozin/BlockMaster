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
	private string path="https://script.google.com/macros/s/AKfycbyd5AcbAnWi2Yn0xhFRbyzS4qMq1VucMVgVvhul5XqS9HkAyJY/exec";
	private string time="";
	private int sec, min, hour, day , month , year;
	private DateTime currentDate,dateTime;
	public DateTime baseDate;

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
		if(!string.IsNullOrEmpty(time))
			setCurrentTime();
		if(SceneManager.GetActiveScene().name == "MenuScene")
			GetComponent<DailyReward>().UpdateTime();
		GetComponent<DailyReward2>().updateTime();
		GetComponent<DailyReward3>().UpdateTime();
	}

	public void setCurrentTime() // get the value of current secs,mins,hours,days in second from the beggining of the month
	{
		FakeDate d = JsonUtility.FromJson<FakeDate>(time);
		currentDate = new DateTime(d.year , d.month , d.day , d.hours , d.minutes , d.seconds);
	}

	struct FakeDate
	{
		public int hours;
		public int minutes;
		public int seconds;
		public int day;
		public int month;
		public int year;
	}

	public string getFullTime()
	{
		return time;
	}
	
	public DateTime GetFullTime()
	{
		return currentDate;
	}

	public int getTimeInSecs() // current time in seconds
	{
		return (int)(currentDate-baseDate).TotalSeconds;
	}
	public int getTimeInSecs(DateTime time)
	{
		dateTime = time;
		return (int)(dateTime-baseDate).TotalSeconds;
	} // time(string) to seconds
	public int getTimeInSecs(string time)
	{
		if (time == "")
			time = this.time;
		FakeDate d = JsonUtility.FromJson<FakeDate>(time);
		dateTime = new DateTime(d.year , d.month , d.day , d.hours , d.minutes , d.seconds);
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
	public static string SecsToTime(int coolDown) // convert seconds to time format 00:00:00
	{
		return coolDown / 60 / 60 + ":" + coolDown / 60 % 60 + ":" + coolDown % 60;
	}
	
}