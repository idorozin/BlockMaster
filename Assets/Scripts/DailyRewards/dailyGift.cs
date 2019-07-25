
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
	private GameObject emptyGiftText;
	[SerializeField]
	private GameObject emptyGiftImage;

	[SerializeField]
	private RewardDialog rewardDialog;

	[SerializeField] private GameObject offline;
	
	private bool giftAllowed=true;
	

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
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			offline.SetActive(true);
			return;
		}

		if(DailyReward2.timeText=="READY!" && !DailyReward2.GiftAllowed)
			GameObject.Find("TimeManager").GetComponent<DailyReward2>().StartCoroutine("enableButton");
		if (!giftAllowed || !DailyReward2.GiftAllowed)
		{
			Debug.Log("returned");
			return;
		}

		DailyReward2.GiftAllowed = false;
		GameObject.Find("TimeManager").GetComponent<DailyReward2>().StartCoroutine("resetTimer");
		if (PlayerStats.Instance.GiftIndex >= gifts.Length - 1)
		{
			PlayerStats.Instance.GiftIndex = 0;
			ResetPanels();
		}

		getGift(gifts[PlayerStats.Instance.GiftIndex]);
		PlayerStats.Instance.GiftIndex++;
		PlayerStats.saveFile();
	}

	private void getGift(Reward_ gift)
	{
		RewardDialog r = Instantiate(rewardDialog);
		r.CollectPrizeWithAnimation(gift);
		gift.Collect();
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
