using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.Native.Cwrapper;
using UnityEditor.Animations;
using UnityEngine;

public class dailyGift : MonoBehaviour
{
	
	[SerializeField] 
	private Transform GiftPanel;
	[SerializeField] 
	private GameObject Tick;
	[SerializeField]
	string[] gifts = new string[30];
	private bool giftAllowed=true;

	private void OnMouseDown()
	{
		getGiftButton();
	}

	public void getGiftButton()
	{
		if (!giftAllowed || !DailyReward2.GiftAllowed)
			return;
		DailyReward2.GiftAllowed = false;
		GameObject.Find("DailyRewards").GetComponent<DailyReward2>().StartCoroutine("resetTimer");
		if (PlayerStats.Instance.playerStats.GiftIndex >= gifts.Length-1)
				PlayerStats.Instance.playerStats.GiftIndex = 0;
			getGift(gifts[PlayerStats.Instance.playerStats.GiftIndex]);
			PlayerStats.Instance.playerStats.GiftIndex++;
			PlayerStats.Instance.saveFile();
	}

	private void getGift(string gift)
	{
		for(int i=0;i<=PlayerStats.Instance.playerStats.GiftIndex;i++)
			Instantiate(Tick , GiftPanel);
//		if (gift == "200coins")
//			PlayerStats.Instance.playerStats.money += 200;
	}

	public void destroyTicks()
	{
		foreach (Transform child in GiftPanel)
		{
			Destroy(child.gameObject);
		}
	}

}
