using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeText2 : MonoBehaviour
{
	// Use this for initialization
	private void Update()
	{
		Debug.Log("poop");
		gameObject.GetComponent<TextMeshProUGUI>().text = DailyReward.timeText;
		if(gameObject.GetComponent<TextMeshProUGUI>().text=="")
		{
			gameObject.GetComponent<TextMeshProUGUI>().text = "READY!";
		}
	}
}
