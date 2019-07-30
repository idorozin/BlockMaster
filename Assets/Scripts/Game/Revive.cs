﻿using System;
using System.Collections;
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
	private float time = 5f;

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
		AdManager.Instance.ShowRewarded(Watched);
	}

	void FailedToWatch(object sender , EventArgs e)
	{
		if(!watched)
			GameOver();
	}

	private bool watched;
	void Watched(object sender , EventArgs e)
	{
		watched = true;
		RevivePlayer();
	}

	void GameOver()
	{
		GameManager.Instance.LavaReached();
		Destroy(gameObject);
	}	
	void RevivePlayer()
	{
		hearts.SetInitialHearts(3);
		GameManager.Instance.lives = 0;
		if(lava.position.y > Camera.main.transform.position.y)
			lava.position += Vector3.down * 3;
		PauseMenu.GameIsPaused = false;
		Destroy(gameObject);
	}
}
