﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{


	#region LoadButtons
	public void LoadMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadGame(){
		SceneManager.LoadScene("GameScene");
	}
	
	public void LoadShop(){
		SceneManager.LoadScene("Shop");
	}

	public void unLoadSettings()
	{
		Settings.SetActive(false);
	}


	public void ShowLeaderboards()
	{
		PlayServices.Instance.ShowLeaderboards();
	}

	public void LoadDailyGift()
	{
		if (!hasConnection())
		{
			Debug.Log("PLZ CHECK YOUR INTERNET CONNECTION");
			return;
			//Do Something
		}
		DailyGift.SetActive(true);
		//DailyGift.GetComponent<dailyGift>().getGiftButton();
	}

	public void unLoadDailyGift()
	{
		DailyGift.SetActive(false);
		DailyGift.GetComponent<dailyGift>().destroyTicks();
	}

	public void LoadChallanges()
	{
		Challanges.SetActive(true);
		Challange.Instance.LoadChallanges();
	}

	public void unLoadChallanges()
	{
		Challanges.SetActive(false);
	}

	#endregion
	
	
	[SerializeField]
     private GameObject recordText;
	[SerializeField]
	private GameObject WheelOfFortune;
	[SerializeField]
	private GameObject Settings;
	[SerializeField]
	private GameObject DailyGift;
	[SerializeField] 
	private GameObject Challanges;
     	
	 public void setRecordText()
     {
		if(PlayerStats.Instance.playerStats!=null)
     	recordText.GetComponent<TextMeshProUGUI>().text = PlayerStats.Instance.playerStats.highScore.ToString();
     }
    
     void Start()
     {
     	setRecordText();
     }
	public void LoadWheelOfFortune(){
		if (!hasConnection())
		{
			Debug.Log("PLZ CHECK YOUR INTERNET CONNECTION");
			return;
			//Do Something
		}
		WheelOfFortune.SetActive(true);
	}

	bool hasConnection()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
			return false;
		return true;
	}

}
