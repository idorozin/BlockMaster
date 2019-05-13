using System.Collections;
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

	public void LoadGame()
	{
		Time.timeScale = 0f;
		pauseMenu.GameIsPaused = true;
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
		DailyGift.SetActive(true);
		DailyGift.GetComponent<dailyGift>().loadGifts();
	}

	public void unLoadDailyGift()
	{
		DailyGift.SetActive(false);
		DailyGift.GetComponent<dailyGift>().destroyTicks();
	}

	public void LoadChallanges()
	{
		ChallangesCanvas.SetActive(true);
		//Debug.Log(Challanges.Instance);
		//Challanges.Instance.LoadChallanges();
	}

	public void unLoadChallanges()
	{
		ChallangesCanvas.SetActive(false);
	}

	public void unLoadWheelOfFortune()
	{
		WheelOfFortune.SetActive(false);
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
	private GameObject ChallangesCanvas;
     	
	 public void setRecordText()
     {
		if(PlayerStats.Instance!=null)
     	recordText.GetComponent<TextMeshProUGUI>().text = PlayerStats.Instance.highScore.ToString();
     }
    
     void Start()
     {
     	setRecordText();
     }
	public void LoadWheelOfFortune(){
		WheelOfFortune.SetActive(true);
	}



}
