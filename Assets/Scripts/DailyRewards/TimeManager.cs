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
	public bool isUpdated = false;

	public static TimeManager Instance;
	private int offest;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			baseDate = new DateTime(1970,1,1,1,1,1);
			StartCoroutine(getTime());
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	//gets time from the server and updates countDown
	public IEnumerator getTime(Action<bool> result = null)
	{
		using (WWW www = new WWW(path))
		{
			while (!www.isDone)
			{
				yield return null;
			}
			time = www.text;
		}
		if (!setCurrentTime())
		{
			result?.Invoke(false);
			yield break;
		}
		offest = (getTimeInSecs(DateTime.Now) - getTimeInSecs());
		isUpdated = true;
		if(!veryUpdated)
			StartCoroutine(UpdatedEnough());
		result?.Invoke(true);
	}

	public bool setCurrentTime() // get the value of current secs,mins,hours,days in second from the beggining of the month
	{
		if (string.IsNullOrEmpty(time))
			return false;
		try
		{
			FakeDate d = JsonUtility.FromJson<FakeDate>(time);
			currentDate = new DateTime(d.year, d.month, d.day, d.hours, d.minutes, d.seconds);
		}
		catch
		{
			return false;
		}

		return true;
	}

	public bool veryUpdated;
	public IEnumerator UpdatedEnough()
	{
		veryUpdated = true;
		var i = 0;
		while (i <= 10)
		{
			i++;
			yield return new WaitForSecondsRealtime(1f);
			currentDate = currentDate.AddSeconds(1);
		}
		veryUpdated = false;
	}

	public int GetOffset()
	{
		return offest;
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
	}


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