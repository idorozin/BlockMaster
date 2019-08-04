using System;
using System.Collections;
using admob;
using TMPro;
using UnityEngine;

public class Revive : MonoBehaviour
{
	[SerializeField]
	private GameObject both;
	[SerializeField] 
	private TextMeshProUGUI bothText;
	[SerializeField]
	private GameObject ad;
	[SerializeField]
	private GameObject pay;
	[SerializeField] 
	private TextMeshProUGUI payText;
	[SerializeField] 
	private TextMeshProUGUI text;

	[SerializeField] private Transform lava;
	[SerializeField] private HeartsUi hearts;
	
	[Space]
	[SerializeField]
	private float time = 5f;
	[SerializeField]
	private int price = 10;
	
	private void Start()
	{
		price = GetPrice();
		hearts = GameObject.Find("CanvasHearts").GetComponent<HeartsUi>();
		lava = GameObject.Find("Lava").transform;
		if (AdManager.Instance.CanPlayRewarded() && PlayerStats.Instance.gold > price)
		{
			both.SetActive(true);
			bothText.text = "" + price;
		}
		else if(PlayerStats.Instance.gold > price)
		{
			pay.SetActive(true);
			payText.text = price.ToString();
		}
		else
		{
			ad.SetActive(true);
		}
		StartCoroutine(Timer());
	}

	private int GetPrice()
	{
		int gold = PlayerStats.Instance.gold;
		if (gold > 800)
			return 250;
		if (gold > 500)
			return 200;
		if (gold > 300)
			return 150;
		if (gold > 150)
			return 100;
		return 50;
	}

	private IEnumerator Timer()
	{
		text.text = time + "";
		while (time > 0)
		{
			time--;
			yield return new WaitForSecondsRealtime(1f);
			text.text = time + "";
		}
		GameManager.Instance.LavaReached();
		Destroy(gameObject);
	}

	public void Pay()
	{
		PlayerStats.Instance.gold -= price;
		RevivePlayer();
	}
	
	public void WatchAd()
	{
		if (AdManager.Instance.CanPlayRewarded())
		{
			AdManager.Instance.ad.rewardedVideoEventHandler += HandleAd;
			AdManager.Instance.ShowRewarded();
		}
	}
	
	private bool watched;
	[SerializeField]
	private int lavaDownFactor = 3;

	void HandleAd(string eventName , string msg)
	{
		
		if (eventName == AdmobEvent.onRewarded)
		{
			watched = true;
			RevivePlayer();
		}
		if (eventName == AdmobEvent.onAdClosed)
		{
			if (!watched)
				time = 0;
			AdManager.Instance.ad.rewardedVideoEventHandler -= HandleAd;
		}
	}

	void RevivePlayer()
	{
		hearts.SetInitialHearts(3);
		GameManager.Instance.lives = 0;
		if (lava.position.y > Camera.main.transform.position.y)
		{
			lava.position += Vector3.down * lavaDownFactor;
		}
		PauseMenu.GameIsPaused = false;
		Destroy(gameObject);
	}
}
