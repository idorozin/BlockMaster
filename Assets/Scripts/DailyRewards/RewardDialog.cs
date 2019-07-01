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
	

	public void CollectPrizeWithAnimation(Reward_ reward)
	{
		reward.Show(rewardIcon , rewardText);
		p = reward;
	}

	public void MultiplyPrizeButton()
	{
		AdManager.Instance.ShowRewarded(handleFailed , handleSuccess);
	}

	void handleFailed(object sender , EventArgs e)
	{
		Destroy(gameObject);
	}	
	void handleSuccess(object sender , EventArgs e)
	{
		p.Collect();
		Destroy(gameObject);
	}

	public void CollectButton()
	{
		Destroy(gameObject);
	}

}
