using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public float height;
	public float score;
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
	private Rigidbody2D rb2d;
	private Transform camera;
	private float fixedScore = 0;
	[SerializeField] private GameObject surface;
	[SerializeField] private GameObject highScoreSign;
	[SerializeField] private TextMeshProUGUI text_;

	private bool reviveUsed = false;
	
	private float startHeight;

	private void Awake()
	{
		NewGame();
		Instance = this;
	}

	// Use this for initialization
	void Start()
	{
		startHeight = surface.transform.position.y;
		camera = GameObject.Find("Main Camera").transform;
		rb2d = GetComponent<Rigidbody2D>();
		OnScoreChanged(-1);
		ShapeBehaviour.ShapeFell += HealthDown;
		HeightFinder.ScoreChanged += OnScoreChanged;
	}
	
	private void OnDisable()
	{
		HeightFinder.ScoreChanged -= OnScoreChanged;
		ShapeBehaviour.ShapeFell -= HealthDown;
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
			GameOver();
		}
	}

	public void ChallengeComplete(Challenge c)
	{
		anim.animate(c);
	}

	void Update () {
		timePassed += Time.deltaTime;
		surface.transform.position = new Vector3(surface.transform.position.x , DestroyShapes.height , surface.transform.position.z);
	}
	


	#region Score
	
	private float GetHighScoreSignHeight()
	{
		return PlayerStats.Instance.highScoreHeight;
	}

	private void UpdateStats()
	{
		if(fixedScore>PlayerStats.Instance.highScore)
		{
			oldRecord = (int)PlayerStats.Instance.highScore;
			recordBroke = true;
			PlayerStats.Instance.highScore = fixedScore;
			PlayerStats.Instance.highScoreHeight = height;
			PlayerStats.saveFile();
			PlayServices.Instance.addScoreToLeaderboard("",(int)fixedScore);
			//play animation
			highScoreSign.SetActive(false);
		}
	}

	private void SetScore()
	{
		if(score!=0 && score>fixedScore)
			fixedScore=score;
		text_.GetComponent<ScrollingText>().SetNum((int)fixedScore);
		//PlayerStats.Instance.cs[PlayerStats.Instance.challengeIndex].setProcess((int)fixedScore , "record");
	}

	private void OnScoreChanged(float height)
	{  
		if (height > startHeight)
			score=(float)Math.Round((height-startHeight)*10);
		this.height = height;
		//set the current score
		SetScore();
		// every time record is bitten save the file and push to leaderboard
		UpdateStats();
		//sign to show where is your highScore
		if(!recordBroke)
			highScoreSign.transform.position = new Vector3(highScoreSign.transform.position.x,GetHighScoreSignHeight(),0f);
	}
	
	#endregion

}
