using System;
using System.Collections.Generic;
using System.ComponentModel;
using Boo.Lang;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	
	public static bool GameIsPaused = false;
	public GameObject PauseMenuUI;
	public GameObject GameOverUI;
	public GameObject clickToStart;
	public Text gameOverScore;
	public Text gameOverMoney;
	public GameObject ChallangesDisplayPanel;
	public GameObject ChallengeDisplayPrefab;
	public GameObject ChallangesCompleteDisplayPanel;
	public GameObject ChallengeCompleteDisplayPrefab;
	public GameObject Score;

	private void Start()
	{
		Time.timeScale = 0;
		GameIsPaused = true;
		DisplayChallenges();
		GameManager.GameOver += OnGameOver;
	}

	private void OnDisable()
	{
		GameManager.GameOver -= OnGameOver;
	}

	private void DisplayChallenges()
	{
		foreach (var c in PlayerStats.Instance.challenges)
		{
			if(c.isActive){
				GameObject challengeDisplay = Instantiate(ChallengeDisplayPrefab , ChallangesDisplayPanel.transform);
				challengeDisplay.GetComponent<ChallengeDisplay>().ShowChallenge(c);
			}
		}
	}	
	private void DisplayCompletedChallenges()
	{
		Challenge[] completedCs = GameManager.Instance.challengesCompleted.ToArray();
		foreach (var c in completedCs)
		{
			GameObject challengeDisplay = Instantiate(ChallengeCompleteDisplayPrefab , ChallangesCompleteDisplayPanel.transform);
			challengeDisplay.GetComponent<ChallengeDisplay>().ShowCompleteChallenge(c);
		}
	}

	public void OnClickPause()
	{
		if(GameIsPaused)
		{
			Resume();
		}
		else 	
		{
			Pause();
		}
	}
	
	public void Resume()
	{
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	public void Pause()
	{
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void ClickToStart()
	{
		clickToStart.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		StartCoroutine(Camera.main.gameObject.GetComponent<CameraShake>().ShakeCamera());
	} 
	
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		GameOverUI.SetActive(false);
		
	}

	public void GameOverUi(float score)
	{
		GameOverUI.SetActive(true);
		gameOverScore.text=score.ToString();
		gameOverMoney.text = "+"+(score/10).ToString()+" (coins)";
		Score.SetActive(false);
	}
	
	private void OnGameOver()
	{
		GameIsPaused = true;
		PlayerStats.Instance.gold+=(int)GameManager.Instance.score;
		GameOverUi((GameManager.Instance.score));
		DisplayCompletedChallenges();
		PlayerStats.saveFile();
	}
}
