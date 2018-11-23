using System;
using System.Collections;
using System.Collections.Generic;
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

	public void getGiftButton()
	{
		if (!giftAllowed)
			return;
		getGift(gifts[PlayerStats.Instance.GiftIndex]);
		PlayerStats.Instance.GiftIndex++;
		PlayerStats.Instance.saveFile();
	}

	private void getGift(string gift)
	{
		for(int i=0;i<PlayerStats.Instance.GiftIndex;i++)
		Instantiate(Tick , GiftPanel);
//		if (gift == "200coins")
//			PlayerStats.Instance.money += 200;
	}

}
