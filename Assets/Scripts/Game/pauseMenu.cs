﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Boo.Lang;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	
	public static bool GameIsPaused = true;
	public GameObject PauseMenuUI;
	public GameObject GameOverUI;
	public GameObject clickToStart;
	public TextMeshProUGUI gameOverScore;
	public TextMeshProUGUI gameOverMoney;
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

	[SerializeField] private AnimatedLayout animatedLayout;
	private void DisplayCompletedChallenges()
	{
		Challenge[] completedCs = GameManager.Instance.challengesCompleted.ToArray();
		foreach (var c in completedCs)
		{
			GameObject challengeDisplay = Instantiate(ChallengeCompleteDisplayPrefab , ChallangesCompleteDisplayPanel.transform);
			challengeDisplay.GetComponent<ChallengeDisplay>().ShowCompleteChallenge(c);
			PlayerStats.Instance.ActivateChallenge();
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
		gameOverMoney.text = (score/10).ToString();
		Score.SetActive(false);
	}
	
	private void OnGameOver()
	{
		AdManager.Instance.ShowInterstitial();
		GameIsPaused = true;
		PlayerStats.Instance.gold+=(int)GameManager.Instance.score;
		DisplayCompletedChallenges();
		GameOverUi((GameManager.Instance.score));
		PlayerStats.saveFile();
	}
}
