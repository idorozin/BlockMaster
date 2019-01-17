using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
	// Use this for initialization
	private void Update()
	{
		gameObject.GetComponent<Text>().text = DailyReward2.timeText;
		if(gameObject.GetComponent<Text>().text=="")
		{
			gameObject.GetComponent<Text>().text = "READY!";
		}
	}
}
