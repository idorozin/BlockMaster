using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
	private TextMeshProUGUI text;
	// Use this for initialization
	void Start()
	{
		text = gameObject.GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		text.text = DailyReward2.timeText;
		if(gameObject.GetComponent<Text>().text=="")
		{
			gameObject.GetComponent<Text>().text = "READY!";
		}
	}
}
