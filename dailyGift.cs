using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dailyGift : MonoBehaviour {
	
	[SerializeField]
	Transform[] ticks = new Transform[30];
	[SerializeField]
	string[] gifts = new string[30];
	private bool timePassed=true;
	private int index=0;

	public void getGiftButton()
	{
		if (!timePassed)
			return;
		index++;
		getGift(gifts[index]);
		
	}

	private void getGift(string gift)
	{
		if (gift == "200coins")
			PlayerStats.Instance.money += 200;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
