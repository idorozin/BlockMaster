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

	[SerializeField] private SceneTransition transition;
	public void LoadGame()
	{
		//Time.timeScale = 0f;
		//PauseMenu.GameIsPaused = true;
		if (!PlayerStats.Instance.didTutorial)
		{
			PlayerStats.Instance.didTutorial = true;
			PlayerStats.saveFile();
			transition.FadeOutAndLoadScene("Test");
		}
		else
		{
			transition.FadeOutAndLoadScene("GameScene");
		}
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

	public void LoadChallenges()
	{
		PlayerStats.Instance.ActivateChallenge();
		ChallangesCanvas.SetActive(true);
	}

	public void unLoadChallanges()
	{
		ChallangesCanvas.SetActive(false);
	}

	public void unLoadWheelOfFortune()
	{
		if (WheelOfFortune.rollAllowed)
			wheelOfFortune.SetActive(false);
	}

	#endregion


	[SerializeField]
     private GameObject recordText;
	[SerializeField]
	private GameObject wheelOfFortune;
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
		wheelOfFortune.SetActive(true);
	}



}
