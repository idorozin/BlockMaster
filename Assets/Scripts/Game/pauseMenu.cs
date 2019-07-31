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
	public GameObject ChallangesDisplayPanel;
	public GameObject ChallengeDisplayPrefab;
	public GameObject ChallangesCompleteDisplayPanel;
	public GameObject ChallengeCompleteDisplayPrefab;
	public GameObject ChallengeOpen;
	public GameObject Score;

	private void Start()
	{
		Time.timeScale = 0;
		GameIsPaused = true;
		PlayerStats.Instance.ActivateChallenge();
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
			if(c.isActive)
			{
				GameObject challengeDisplay = Instantiate(ChallengeDisplayPrefab , ChallangesDisplayPanel.transform);
				challengeDisplay.GetComponent<ChallengeDisplay>().ShowChallenge(c);
			}
		}
		
		if (ChallangesDisplayPanel.transform.childCount < 3)
		{
			Instantiate(ChallengeOpen, ChallangesDisplayPanel.transform);
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
		AudioListener.pause = false;
		AudioListener.volume = 1;
		GameIsPaused = false;
	}
	public void Pause()
	{
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		AudioListener.pause = true;
		AudioListener.volume = 0;
		GameIsPaused = true;
	}

	public void ClickToStart()
	{
		clickToStart.SetActive(false);
		Time.timeScale = 1f;
		//AudioListener.pause = false;
		//AudioListener.volume = 1;
		GameIsPaused = false;
		StartCoroutine(Camera.main.gameObject.GetComponent<CameraShake>().ShakeCamera());
	} 
	
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
		AudioListener.pause = false;
		AudioListener.volume = 1;
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		GameOverUI.SetActive(false);
		
	}
	
	public void GameOverUi()
	{
		DisplayCompletedChallenges();
		GameOverUI.SetActive(true);
		Score.SetActive(false);
	}
	
	private void OnGameOver()
	{
		GameIsPaused = true;
		AdManager.Instance.interstitial.OnAdClosed += handleAdFinished;
		AdManager.Instance.ShowInterstitial();
		GameOverUi();
	}

	private void handleAdFinished(object sender, EventArgs e)
	{
		//bool shmossi = true;
		//GameOverUi();
		AdManager.Instance.interstitial.OnAdClosed -= handleAdFinished;
	}
}
