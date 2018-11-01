﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour {

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

	public void ShowLeaderboards()
	{
		PlayServices.Instance.ShowLeaderboards();
	}

	[SerializeField]
     	private GameObject recordText;
     	
     	public void setRecordText()
     	{
     		recordText.GetComponent<TextMeshProUGUI>().text = PlayerStats.highScore.ToString();
     	}
     
     	void Start()
     	{
     		setRecordText();
     	}
	public void LoadWheelOfFortune(){
		SceneManager.LoadScene("WheelOfFortune");
	}

}
