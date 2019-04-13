
using UnityEngine;

public class dailyGift : MonoBehaviour
{
	
	[SerializeField] 
	private Transform GiftPanel , TickPanel;
	[SerializeField] 
	private GameObject Tick;
	[SerializeField]
	Rewards.Reward[] gifts = new Rewards.Reward[30];
	private bool giftAllowed=true;

	private void OnMouseDown()
	{
		getGiftButton();
	}

	public void loadGifts()
	{
		for(int i=0;i<=PlayerStats.Instance.GiftIndex;i++)
			Instantiate(Tick , TickPanel);
		foreach (var t in gifts)
			Instantiate(Resources.Load("Gifts/"+t) , GiftPanel);
	}

	public void getGiftButton()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
			return;
		if(DailyReward2.timeText=="READY!" && !DailyReward2.GiftAllowed)
			GameObject.Find("TimeManager").GetComponent<DailyReward2>().StartCoroutine("enableButton");
		if (!giftAllowed || !DailyReward2.GiftAllowed)
		{
			Debug.Log("returned");
			return;
		}

		DailyReward2.GiftAllowed = false;
		GameObject.Find("TimeManager").GetComponent<DailyReward2>().StartCoroutine("resetTimer");
		if (PlayerStats.Instance.GiftIndex >= gifts.Length-1)
				PlayerStats.Instance.GiftIndex = 0;
			getGift(gifts[PlayerStats.Instance.GiftIndex]);
			PlayerStats.Instance.GiftIndex++;
			PlayerStats.saveFile();
	}

	private void getGift(Rewards.Reward gift)
	{
		Instantiate(Tick , TickPanel);
		PlayerStats.Instance.GiftIndex++;
		Rewards.Instance.CollectPrizeWithAnimation(Rewards.Reward.GOLD_50);
	}

	public void destroyTicks()
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
