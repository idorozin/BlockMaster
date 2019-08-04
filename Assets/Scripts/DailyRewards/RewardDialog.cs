using System;
using System.Collections;
using System.Collections.Generic;
using admob;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardDialog : MonoBehaviour
{
	public static Reward_ p;

	[SerializeField]
	private Image rewardIcon;
	[SerializeField]
	private TextMeshProUGUI rewardText;
	[SerializeField] 
	private GameObject multyButton;
	[SerializeField] 
	private Color unavailableColor;
	

	public void CollectPrizeWithAnimation(Reward_ reward)
	{
		reward.Show(rewardIcon , rewardText);
		reward.Collect();
		//AudioManager.Instance.PlaySound(reward.sound);
		p = reward;
	}

	public void MultiplyPrizeButton()
	{
		if (AdManager.Instance.CanPlayRewarded())
		{
			AdManager.Instance.ad.rewardedVideoEventHandler += HandleAd;
			AdManager.Instance.ShowRewarded();
		}
	}

	void HandleAd(string eventName, string msg)
	{
		if(eventName == AdmobEvent.onRewarded)
			p.Collect();
		if (eventName == AdmobEvent.onAdClosed)
		{
			AdManager.Instance.ad.rewardedVideoEventHandler -= HandleAd;
			Destroy(gameObject);
		}
	}
	
	public void CollectButton()
	{
		//AudioManager.Instance.StopSound(p.sound);
		Destroy(gameObject);
	}

}
