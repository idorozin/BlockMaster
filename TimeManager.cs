using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	private string path="www.korystudios.com/realtime.php";
	private string time="";
	private int sec, min, hour, day , month , year;

	public static TimeManager Instance;
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
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
		GetComponent<DailyReward>().updateTime();
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
	}

	public string getFullTime()
	{
		return time;
	}

	public int getTimeInSecs() // current time in seconds
	{
		return (sec)+(min*60)+(hour*60*60)+(day*60*60*24);
	}
	public int getTimeInSecs(string time) 
	{
		string[] currentTime = time.Split(' ');
		string[] timePeriods = currentTime[1].Split(':');
		int hour_ = int.Parse(timePeriods[0]);
		int min_ = int.Parse(timePeriods[1]);
		int sec_ = int.Parse(timePeriods[2]);
		timePeriods =  currentTime[0].Split('-');
		int day_ = int.Parse(timePeriods[1]);
		//month_ = int.Parse(timePeriods[2]);
		//year_ = int.Parse(timePeriods[3]);
		return (sec_)+(min_*60)+(hour_*60*60)+(day_*60*60*24);
	} // time(string) to seconds

	public bool isRelevent(int year , int month) // check if the year and the month are not that far away
	{
		return year == this.year+1 && month <= this.month+1;
	}

}
