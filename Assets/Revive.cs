using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

	private void Start()
	{
		hearts = GameObject.Find("CanvasHearts").GetComponent<HeartsUi>();
		lava = GameObject.Find("Lava").transform;
		if (AdManager.Instance.CanPlayRewarded() && PlayerStats.Instance.gold > 10)
		{
			both.SetActive(true);
			bothText.text = 10 + "";
		}
		else if(PlayerStats.Instance.gold > 10)
		{
			pay.SetActive(true);
			payText.text = 10 + "";
		}
		else
		{
			ad.SetActive(true);
		}
		StartCoroutine(Timer());
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
		PlayerStats.Instance.gold -= 10;
		RevivePlayer();
	}
	
	public void WatchAd()
	{
		AdManager.Instance.ShowRewarded(FailedToWatch , Watched);
	}

	void FailedToWatch(object sender , EventArgs e)
	{
		GameOver();
	}	
	void Watched(object sender , EventArgs e)
	{
		RevivePlayer();
	}

	void GameOver()
	{
		GameManager.Instance.LavaReached();
		Destroy(gameObject);
	}	
	void RevivePlayer()
	{
		hearts.SetInitialHearts((int)Math.Abs(GameManager.Instance.lives));
		GameManager.Instance.lives = 0;
		lava.position += Vector3.down * 3;
		PauseMenu.GameIsPaused = false;
		Destroy(gameObject);
	}
}
