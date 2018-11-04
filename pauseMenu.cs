using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour {
	
	public static bool GameIsPaused = false;
	public GameObject PauseMenuUI;
	public GameObject playerStats;
	public GameObject GameOverUI;
	public Text gameOverScore;
	public Text gameOverMoney;
	public GameObject Score;
	
	void Start(){
		playerStats = GameObject.Find("PlayerStats");		
	}
	
	void Update()
	{
		OnGameOver();
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

	public void playAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		GameOverUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void gameOverUI(float score)
	{
		GameOverUI.SetActive(true);
		gameOverScore.text=score.ToString();
		gameOverMoney.text = "+"+(score/10).ToString()+" (coins)";
		Score.SetActive(false);
	}
	
	private void OnGameOver()
	{
		if(HeightFinder.lives<=-3){
			HeightFinder.lives=0;
			PlayerStats.Instance.money+=HeightFinder.score;
			gameOverUI((HeightFinder.score));
			GameIsPaused = true;
			Time.timeScale = 0;
			PlayerStats.Instance.saveFile();
		}
	}
}
