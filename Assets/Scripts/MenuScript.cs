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
		PauseMenu.GameIsPaused = true;
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
		DailyGift.GetComponent<DailyGift>().ResetPanels();
		DailyGift.GetComponent<DailyGift>().UpdateUI();
	}

	public void unLoadDailyGift()
	{
		DailyGift.SetActive(false);
		DailyGift.GetComponent<DailyGift>().ResetPanels();
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

	public void LoadCredits()
	{
		Settings.SetActive(false);
		credits.SetActive(true);
	}
	public void UnLoadCredits()
	{
		credits.SetActive(false);
		Settings.SetActive(true);
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
	[SerializeField] 
	private GameObject credits;
     	
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
