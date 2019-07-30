using System;
using UnityEngine;
using System.Collections;
using System.Timers;
using TMPro;

public class ExtensionMethodHelper : MonoBehaviour
{
	private static ExtensionMethodHelper _Instance;

	public static ExtensionMethodHelper Instance
	{
		get
		{
			if (_Instance == null)
			{
				_Instance = new GameObject("ExtensionMethodHelper").AddComponent<ExtensionMethodHelper>();
			}

			return _Instance;
		}
	}
}

public static class ExtensionMethods
{
	public static void DisableAfterTimePassed(Challenge c)
	{
		ExtensionMethodHelper.Instance.StartCoroutine(DisableAfterTimePassed_(c));
		GameManager.GameOver += StopAllCoroutines;
	}

	public static void StopAllCoroutines()
	{
		ExtensionMethodHelper.Instance.StopAllCoroutines();
	}

	public static IEnumerator DisableAfterTimePassed_(Challenge c)
	{
		yield return new WaitForSecondsRealtime(0.1f);
		TextMeshProUGUI timer = GameManager.Instance.timer.GetComponent<TextMeshProUGUI>();
		timer.gameObject.SetActive(true);
		float timeRemaining = (float)c.timeToComplete;
		while (timeRemaining > 0)
		{
			timeRemaining -= 0.1f;
			timer.text = timeRemaining.ToString("F1");
			yield return new WaitForSeconds(0.1f);
		}
		timer.gameObject.SetActive(false);
		c.timePassed = true;
	}
	
	public static string SecsToTime(int secs) // convert seconds to time format 00:00:00
	{
		TimeSpan t = TimeSpan.FromSeconds(secs);
		string answer = $"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
		return answer;
	}
}