
using UnityEngine;

public class dailyGift : MonoBehaviour
{
	
	[SerializeField] 
	private Transform GiftPanel , TickPanel;
	[SerializeField] 
	private GameObject Tick;
	[SerializeField]
	string[] gifts = new string[30];
	private bool giftAllowed=true;

	private void OnMouseDown()
	{
		getGiftButton();
	}

	public void loadGifts()
	{
		for(int i=0;i<=PlayerStats.Instance.playerStats.GiftIndex;i++)
			Instantiate(Tick , TickPanel);
		foreach (var t in gifts)
			Instantiate(Resources.Load("Gifts/"+t) , GiftPanel);
	}

	public void getGiftButton()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
			return;
		if(DailyReward2.timeText=="READY!" && !DailyReward2.GiftAllowed)
			GameObject.Find("DailyRewards").GetComponent<DailyReward2>().StartCoroutine("enableButton");
		if (!giftAllowed || !DailyReward2.GiftAllowed)
		{
			Debug.Log("returned");
			return;
		}

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
		Instantiate(Tick , TickPanel);
		PlayerStats.Instance.playerStats.GiftIndex++;
//		if (gift == "200coins")
//			PlayerStats.Instance.playerStats.money += 200;
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
