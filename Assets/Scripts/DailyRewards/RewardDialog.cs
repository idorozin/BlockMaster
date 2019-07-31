using System;
using System.Collections;
using System.Collections.Generic;
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
			AdManager.Instance.rewardedAd.OnAdClosed += handleFailed;
			AdManager.Instance.rewardedAd.OnUserEarnedReward += handleSuccess;
			AdManager.Instance.ShowRewarded();
		}
	}

	void handleFailed(object sender , EventArgs e)
	{
		AdManager.Instance.rewardedAd.OnAdClosed -= handleFailed;
		//AudioManager.Instance.StopSound(p.sound);
		Destroy(gameObject);
	}
	
	void handleSuccess(object sender , EventArgs e)
	{
		AdManager.Instance.rewardedAd.OnUserEarnedReward -= handleSuccess;
		p.Collect();
		//AudioManager.Instance.StopSound(p.sound);
		Destroy(gameObject);
	}

	public void CollectButton()
	{
		//AudioManager.Instance.StopSound(p.sound);
		Destroy(gameObject);
	}

}
