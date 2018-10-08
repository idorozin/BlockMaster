using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {
	
	public static bool GameIsPaused = false;
	public GameObject PauseMenuUI;
	public GameObject playerStats;
	
	void Start(){
		playerStats = GameObject.Find("PlayerStats");
		
	}
	
	void Update(){
		if(HeightFinder.lives<=-3){
			LoadMainMenu();
			HeightFinder.lives=0;
			
			PlayerStats.money+=(float)Math.Round(HeightFinder.score*10);
			Debug.Log(PlayerStats.money);
		}
	}
	
	public void OnClick(){
		if(GameIsPaused)
		{
			Resume();
		}
		else 	
		{
			Pause();
		}
	}
	
	void Resume()
	{
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	void Pause()
	{
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
	
	public void LoadMainMenu(){
		SceneManager.LoadScene("MainMenu");
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
}
