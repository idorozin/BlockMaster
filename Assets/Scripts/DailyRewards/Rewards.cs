using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
	public static Rewards Instance;
	public static Prize p;

	private void Awake()
	{
		Instance = this;
	}

	[SerializeField] private GameObject lightEffect;
	private string filePath = "Rewards/";

	public enum Prize
	{
		GOLD_50,
		CANNON_DEFAULT,
		GEMS_50,
		gift
	};


	public void CollectPrize(Prize prize)
	{
		string[] TYPE_DESC = prize.ToString().Split('_');
		string type = TYPE_DESC[0];
		string desc = TYPE_DESC[1];
		switch (type)
		{
			case "GOLD":
				PlayerStats.Instance.money += Int32.Parse(desc);
				break;
			case "CANNON":
				PlayerStats.Instance.ItemsOwned.Add(desc);
				break;
		}
	}

	public void CollectPrizeWithAnimation(Prize prize)
	{
		GameObject canvas = Instantiate(lightEffect);
		Instantiate(Resources.Load(filePath + prize), canvas.transform);
		CollectPrize(prize);
		p = prize;
	}

	public void MultiplyPrizeButton()
	{
	//	if(RewardedAd)
	//	collectPrize(p);
		gameObject.SetActive(false);
	}

	public void CollectButton()
	{
		gameObject.SetActive(false);
	}

}
