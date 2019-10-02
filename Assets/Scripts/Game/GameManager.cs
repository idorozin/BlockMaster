using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using GooglePlayGames.Native.Cwrapper;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public float height;
	public int score;
	public float lives;
	public float timePassed;

	public bool recordBroke;
	public int oldRecord;
	
	public List<GameObject> shapes = new List<GameObject>();
	

	public Queue<Challenge> challengesCompleted = new Queue<Challenge>();
	public NotflicationAnimation anim;
	
	public static event Action GameOver = delegate { };
	public static event Action NewGame = delegate { };
	
	private float minimumLives = -3f;
	public int fixedScore = 0;
	[SerializeField] private GameObject surface;

	public int goldEarned;

	private bool reviveUsed = false;
	
	public float startHeight;

	public GameObject timer;

	[SerializeField]
	private Transform sounds;

	public LevelManager levelManager;

	private void Awake()
	{
		if(PlayerStats.Instance != null)
		if (PlayerStats.Instance.soundOn && !PlayerStats.Instance.musicOn)
		{
			AudioSource[] sources = sounds.GetComponentsInChildren<AudioSource>();
			foreach (var s in sources)
			{
				s.mute = true;
			}
		}
		else if (PlayerStats.Instance.soundOn)
		{
			AudioListener.pause = false;
			AudioListener.volume = 1;
		}
		else
		{
			AudioListener.pause = true;
			AudioListener.volume = 0;
		}
		NewGame();
		Instance = this;
	}

	// Use this for initialization
	void Start()
	{
		startHeight = surface.transform.position.y;
		ShapeBehaviour.ShapeFell += HealthDown;
		Challenge.OnChallengeCompleted += ChallengeComplete;
	}
	
	private void OnDisable()
	{
		ShapeBehaviour.ShapeFell -= HealthDown;
		Challenge.OnChallengeCompleted -= ChallengeComplete;
	}

	[SerializeField] private GameObject revive;
	[SerializeField] private Transform canvasPause;
	
	void HealthDown()
	{
		if (PauseMenu.GameIsPaused)
			return;
		lives--;
		if (lives <= minimumLives)
		{
			TryRevive();
		}
	}

	public void LavaReached()
	{
		TryRevive();
	}

	private void TryRevive()
	{
		if (!reviveUsed && fixedScore > 50 && (PlayerStats.Instance.gold > 10 || AdManager.Instance.CanPlayRewarded()))
		{
			reviveUsed = true;
			PauseMenu.GameIsPaused = true;
			Instantiate(revive, canvasPause);
		}
		else
		{
			var goldEarenByHeight = (int) fixedScore /  10;
			goldEarned += goldEarenByHeight;
			PlayerStats.Instance.gold += goldEarenByHeight;
			PlayerStats.saveFile();
			GameOver();
		}
	}

	public void ChallengeComplete(Challenge c)
	{
		anim.animate(c);
		challengesCompleted.Enqueue(c);
		goldEarned += c.reward;
	}

	void Update ()
    {
		timePassed += Time.deltaTime;
	}

	public bool NextLevel = false;
	





}
