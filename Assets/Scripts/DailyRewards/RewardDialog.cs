using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDialog : MonoBehaviour
{
	public static Reward_ p;

	[SerializeField]
	private Image rewardIcon;
	
	public void CollectPrize(Reward_ reward)
	{
	}

	public void CollectPrizeWithAnimation(Reward_ reward)
	{
		CollectPrize(reward);
		rewardIcon.sprite = reward.icon;
		p = reward;
	}

	public void MultiplyPrizeButton()
	{
	//	if(RewardedAd)
	//		collectPrize(p);
		gameObject.SetActive(false);
	}

	public void CollectButton()
	{
		gameObject.SetActive(false);
	}

}
