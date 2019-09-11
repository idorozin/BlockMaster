
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DailyGift : MonoBehaviour
{
	
	[SerializeField] 
	private Transform GiftPanel , TickPanel;
	[SerializeField] 
	private GameObject TickPrefab;
	[SerializeField]
	Reward_[] gifts = new Reward_[30];

	[SerializeField]
	private RewardDialog rewardDialog;

	[SerializeField] private GameObject offline;
	
	private bool giftAllowed=true;
	[SerializeField]
	private bool testing;

	[SerializeField] public ActivateGift dailyReward;
	private bool tryValidate;

	private void OnMouseDown()
	{
		getGiftButton();
	}

	public void UpdateUI()
	{
		for(int i=0;i<PlayerStats.Instance.GiftIndex;i++)
			Instantiate(TickPrefab , TickPanel);
		foreach (var gift in gifts)
		{	
			gift.Show(GiftPanel);
		}
	}

	public void getGiftButton()
	{
		if (!testing)
		{
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				offline.SetActive(true);
				return;
			}

			if (dailyReward.validationRequired && !tryValidate)
			{
				dailyReward.StartCoroutine("ValidateTime");
				tryValidate = true;
			}

			if (!giftAllowed || !ActivateGift.GiftAllowed)
			{
				Debug.Log("returned");
				return;
			}
			ActivateGift.GiftAllowed = false;
			dailyReward.StartCoroutine("ResetTimer");
		}

		if (PlayerStats.Instance.GiftIndex >= gifts.Length)
		{
			PlayerStats.Instance.GiftIndex = 0;
			ResetPanels();
			UpdateUI();
		}
		getGift(gifts[PlayerStats.Instance.GiftIndex]);
	}

	private void getGift(Reward_ gift)
	{
		RewardDialog r = Instantiate(rewardDialog);
		r.CollectPrizeWithAnimation(gift);
		gift.Collect();
		PlayerStats.Instance.GiftIndex++;
		PlayerStats.saveFile();
		Instantiate(TickPrefab , TickPanel);
	}

	public void ResetPanels()
	{
		foreach (Transform child in GiftPanel)
		{
			Destroy(child.gameObject);
		}
		foreach (Transform child in TickPanel)
		{
			Destroy(child.gameObject);
		}
	}

}
