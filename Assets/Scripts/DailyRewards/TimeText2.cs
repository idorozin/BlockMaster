using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeText2 : MonoBehaviour
{
	private TextMeshProUGUI text;
	void Start()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		text.text = DailyReward2.timeText;
		if(gameObject.GetComponent<TextMeshProUGUI>().text=="")
		{
			gameObject.GetComponent<TextMeshProUGUI>().text = "READY!";
		}
	}
}
