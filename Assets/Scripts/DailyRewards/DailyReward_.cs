using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleAndroidNotifications;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class DailyReward_ : MonoBehaviour
{
	[SerializeField]
	protected int countDownLenght=500,coolDown;
	protected TimePassed timePassed = new TimePassed();
	private bool initialized;
	[SerializeField]
	private bool autoReset;
	
	private void Start()
	{
		SetTimePassed();
		if (timePassed.startTime == DateTime.MinValue)
		{
			OnTimePassed();
			if (autoReset)
				StartCoroutine(ResetTimer());
			initialized = true;
			return;
		}
		initialized = true;
		coolDown = 2;
		StartCoroutine(CountDown());
	}

	private void OnEnable()
	{
		if(initialized && timePassed.startTime != DateTime.MinValue)
			StartCoroutine(CountDown());
	}

	protected abstract void SetTimePassed();
	
	//reset the timer every roll
	public IEnumerator ResetTimer()
	{	
		coolDown = countDownLenght;
		while (TimeManager.Instance.isRunning)
			yield return null;
		if (!TimeManager.Instance.veryUpdated)
		{
			yield return StartCoroutine(TimeManager.Instance.getTime(
				(success) =>
				{
					if (success)
					{
						timePassed.offset =  TimeManager.Instance.GetOffset();
						timePassed.startTime = TimeManager.Instance.GetFullTime();
						PlayerStats.saveFile();
						StartCoroutine(CountDown());
						OnReset();
					}
					else
					{
						
					}
				}));
		}
		else
		{
			timePassed.offset =  TimeManager.Instance.GetOffset();
			timePassed.startTime = TimeManager.Instance.GetFullTime();
			PlayerStats.saveFile();
			StartCoroutine(CountDown());
			OnReset();
		}
	}

	protected virtual void OnReset(){}

	private IEnumerator CountDown()
	{
		coolDown = TimeRemaining();
		while (coolDown > 0 || coolDown > countDownLenght+1)
		{
			coolDown = TimeRemaining();
			OnTick();
			yield return new WaitForSecondsRealtime(1);
		}
		StartCoroutine(ValidateTime());
	}

	protected virtual void OnTick(){}

	private int TimeRemaining()
	{
		if (TimeManager.Instance.isUpdated)
			return countDownLenght - (TimeManager.Instance.getTimeInSecs(DateTime.Now)
			                          - TimeManager.Instance.getTimeInSecs(timePassed.startTime)) + TimeManager.Instance.GetOffset();
		return countDownLenght - (TimeManager.Instance.getTimeInSecs(DateTime.Now) 
		                          - TimeManager.Instance.getTimeInSecs(timePassed.startTime)) + timePassed.offset;
	}
	
	public bool validationRequired;
	// if countDown is over verify that with the server
	// true => button enabled false => button disabled and count down continouse with updated time. 
	private IEnumerator ValidateTime()
	{
		//validate 
		if (!OnValidation())
		{
			validationRequired = false;
			yield break;
		}
		
		while (TimeManager.Instance.isRunning)
			yield return null;

		if (TimeManager.Instance.veryUpdated)
		{
			validationRequired = false;
			OnTimePassed();
			if (autoReset)
				StartCoroutine(ResetTimer());
			yield break;
		}

		yield return StartCoroutine(TimeManager.Instance.getTime(
			(success) =>
			{
				if (success)
				{
					if (TimeRemaining() <= 0)
					{
						validationRequired = false;
						OnTimePassed();
						if (autoReset)
							StartCoroutine(ResetTimer());
					}
					else
					{
						validationRequired = true;
						timePassed.offset = TimeManager.Instance.GetOffset();
						StartCoroutine(CountDown());
					}
				}
				else
				{
					validationRequired = true;
				}
			}));
	}

	protected virtual bool OnValidation()
	{
		return true;
	}

	protected abstract void OnTimePassed();



}
