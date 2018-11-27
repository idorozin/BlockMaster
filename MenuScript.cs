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
		DailyGift.SetActive(true);
		DailyGift.GetComponent<dailyGift>().getGiftButton();
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
		GameObject.Find("Rollete").GetComponent<Button>().enabled = false;
		WheelOfFortune.SetActive(true);
		GameObject.Find("DailyRewards").GetComponent<DailyReward>().StartCoroutine("resetTimer");
	}

	bool hasConnection()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
			return false;
		return true;
	}

}
