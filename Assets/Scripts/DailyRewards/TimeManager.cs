﻿using System;
using System.Collections;
using System.Collections.Generic;
 using System.IO;
 using System.Net;
 using System.Runtime.InteropServices;
 using UnityEngine;
 using UnityEngine.Experimental.AI;
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

	private bool firstTime = true;
	//gets time from the server and updates countDown
	public IEnumerator getTime(Action<bool> result = null)
	{
		Debug.Log("GET TIME");
		isRunning = true;
		if (firstTime)
		{
			yield return new WaitForSecondsRealtime(1f);
			firstTime = false;
		}
		Debug.Log("GET TIME_");
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
			isRunning = false;
			yield break;
		}
		offest = (getTimeInSecs(DateTime.Now) - getTimeInSecs());
		isUpdated = true;
		isRunning = false;
		if(!veryUpdated)
			StartCoroutine(UpdatedEnough());
		result?.Invoke(true);

	}

	public bool setCurrentTime() // get the value of current secs,mins,hours,days in second from the beggining of the month
	{
		Debug.Log(time);
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
	public bool isRunning;

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
	
}